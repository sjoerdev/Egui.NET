// Copyright (c) Facebook, Inc. and its affiliates
// SPDX-License-Identifier: MIT OR Apache-2.0

use crate::{
    common,
    indent::{IndentConfig, IndentedWriter},
    CodeGeneratorConfig, Encoding,
};
use heck::CamelCase;
use include_dir::include_dir as include_directory;
use serde_reflection::{ContainerFormat, Format, FormatHolder, Named, Registry, VariantFormat};
use std::{
    collections::{BTreeMap, HashMap},
    fmt::Write as _,
    io::{Result, Write},
    path::PathBuf,
};

/// Main configuration object for code-generation in C#.
pub struct CodeGenerator<'a> {
    /// Language-independent configuration.
    config: &'a CodeGeneratorConfig,
    /// Mapping from external type names to fully-qualified class names (e.g. "MyClass" -> "MyNamespace.MyClass").
    /// Derived from `config.external_definitions`.
    external_qualified_names: HashMap<String, String>,
}

/// Shared state for the code generation of a C# source file.
struct CSharpEmitter<'a, T> {
    /// Writer.
    out: IndentedWriter<T>,
    /// Generator.
    generator: &'a CodeGenerator<'a>,
    /// Current namespace (e.g. vec!["MyNamespace", "MyClass"])
    current_namespace: Vec<String>,
    /// Current (non-qualified) generated class names that could clash with names in the registry
    /// (e.g. "Builder" or variant classes).
    /// * We count multiplicities to allow inplace backtracking.
    /// * Names in the registry (and a few base types such as "Decimal") are assumed to never clash.
    current_reserved_names: HashMap<String, usize>,
    /// When we find an enum with all Unit variants, we ser/de as a regular C# enum.
    /// We keep track of this so we can use the enum's extension class for ser/de since enums can't have methods.
    cstyle_enum_names: Vec<String>,
}

impl<'a> CodeGenerator<'a> {
    /// Create a C# code generator for the given config.
    pub fn new(config: &'a CodeGeneratorConfig) -> Self {
        let mut external_qualified_names = HashMap::new();
        for (namespace, names) in &config.external_definitions {
            for name in names {
                external_qualified_names
                    .insert(name.to_string(), format!("{}.{}", namespace, name));
            }
        }
        Self {
            config,
            external_qualified_names,
        }
    }

    /// Output class definitions for `registry` in separate source files.
    /// Source files will be created in a subdirectory of `install_dir` corresponding to the given
    /// package name (if any, otherwise `install_dir` itself).
    pub fn write_source_files(
        &self,
        install_dir: std::path::PathBuf,
        registry: &Registry,
    ) -> Result<std::path::PathBuf> {
        let current_namespace = self
            .config
            .module_name
            .split('.')
            .map(String::from)
            .collect::<Vec<_>>();

        let mut dir_path = install_dir;
        for part in &current_namespace {
            dir_path = dir_path.join(part);
        }
        std::fs::create_dir_all(&dir_path)?;

        // When we find an enum with all Unit variants, we ser/de as a regular C# enum.
        // We keep track of this so we can use the enum's extension class for ser/de since enums can't have methods.
        let mut cstyle_enum_names = Vec::new();
        if self.config.c_style_enums {
            for (name, format) in registry {
                if let ContainerFormat::Enum(variants) = format {
                    if variants.values().all(|f| f.value == VariantFormat::Unit) {
                        cstyle_enum_names.push(name.clone());
                    }
                }
            }
        }

        for (name, format) in registry {
            self.write_container_class(
                &dir_path,
                current_namespace.clone(),
                cstyle_enum_names.clone(),
                name,
                format,
            )?;
        }
        if self.config.serialization {
            self.write_helper_class(&dir_path, current_namespace, cstyle_enum_names, registry)?;
        }
        Ok(dir_path)
    }

    fn write_container_class(
        &self,
        dir_path: &std::path::Path,
        current_namespace: Vec<String>,
        cstyle_enum_names: Vec<String>,
        name: &str,
        format: &ContainerFormat,
    ) -> Result<()> {
        let mut file = std::fs::File::create(dir_path.join(name.to_string() + ".cs"))?;
        let mut emitter = CSharpEmitter {
            out: IndentedWriter::new(&mut file, IndentConfig::Space(4)),
            generator: self,
            current_namespace,
            current_reserved_names: HashMap::new(),
            cstyle_enum_names,
        };

        emitter.output_preamble()?;
        emitter.output_open_namespace(name)?;
        emitter.output_container(name, format)?;
        emitter.output_close_namespace()?;

        Ok(())
    }

    fn write_helper_class(
        &self,
        dir_path: &std::path::Path,
        current_namespace: Vec<String>,
        cstyle_enum_names: Vec<String>,
        registry: &Registry,
    ) -> Result<()> {
        let mut file = std::fs::File::create(dir_path.join("TraitHelpers.cs"))?;
        let mut emitter = CSharpEmitter {
            out: IndentedWriter::new(&mut file, IndentConfig::Space(4)),
            generator: self,
            current_namespace,
            current_reserved_names: HashMap::new(),
            cstyle_enum_names,
        };

        emitter.output_preamble()?;
        emitter.output_open_namespace("TraitHelpers")?;
        emitter.output_trait_helpers(registry)?;
        emitter.output_close_namespace()?;

        Ok(())
    }
}

impl<'a, T> CSharpEmitter<'a, T>
where
    T: Write,
{
    fn output_preamble(&mut self) -> Result<()> {
        writeln!(
            self.out,
            r"#pragma warning disable
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;"
        )?;
        Ok(())
    }

    fn output_open_namespace(&mut self, name: &str) -> Result<()> {
        writeln!(
            self.out,
            "\nnamespace {} {{",
            self.namespace_for_item(name)
        )?;
        self.out.indent();
        Ok(())
    }

    fn output_close_namespace(&mut self) -> Result<()> {
        self.out.unindent();
        writeln!(
            self.out,
            "\n}}",
        )?;
        Ok(())
    }

    /// Gets the namespace to use for a particular item.
    fn namespace_for_item(&self, name: &str) -> String {
        self.generator.config.namespaces.get(name).cloned().unwrap_or_else(|| self.generator.config.module_name.clone())
    }

    /// Compute a safe reference to the registry type `name` in the given context.
    /// If `name` is not marked as "reserved" (e.g. "Builder"), we compare the global
    /// name `self.external_qualified_names[name]` with the current namespace and try to use the
    /// short string `name` if possible.
    fn quote_qualified_name(&self, name: &str) -> String {
        let qname = self
            .generator
            .external_qualified_names
            .get(name)
            .cloned()
            .unwrap_or_else(|| format!("{}.{}", self.namespace_for_item(name), name));
        let qname = format!("{}.{}", self.namespace_for_item(name), name);
        let mut path = qname.split('.').collect::<Vec<_>>();
        if path.len() <= 1 {
            return qname;
        }
        let name = path.pop().unwrap();
        if true {
        //if self.current_reserved_names.contains_key(name) {
            return qname;
        }
        for (index, element) in path.iter().enumerate() {
            match self.current_namespace.get(index) {
                Some(e) if e == element => (),
                _ => {
                    return qname;
                }
            }
        }
        name.to_string()
    }

    fn output_comment(&mut self, name: &str) -> std::io::Result<()> {
        let mut path = self.current_namespace.clone();
        path.push(name.to_string());
        if let Some(doc) = self.generator.config.comments.get(&path) {
            let text = textwrap::indent(doc, "/// ").replace("\n\n", "\n///\n");
            write!(self.out, "{}", text)?;
        }
        Ok(())
    }

    fn output_custom_code(&mut self) -> std::io::Result<()> {
        if let Some(code) = self
            .generator
            .config
            .custom_code
            .get(&self.current_namespace)
        {
            writeln!(self.out, "\n{}", code)?;
        }
        Ok(())
    }

    fn is_nullable(&self, format: &Format) -> bool {
        use Format::*;
        match format {
            TypeName(_) => false,
            Str | Seq(_) | Map { .. } | TupleArray { .. } => true,
            Variable(_) => panic!("unexpected value"),
            _ => false,
        }
    }

    fn quote_type(&self, format: &Format) -> String {
        use Format::*;
        match format {
            TypeName(x) => self.quote_qualified_name(x),
            Unit => "Egui.Unit".into(),
            Bool => "bool".into(),
            I8 => "sbyte".into(),
            I16 => "short".into(),
            I32 => "int".into(),
            I64 => "long".into(),
            I128 => "BigInteger".into(),
            U8 => "byte".into(),
            U16 => "ushort".into(),
            U32 => "uint".into(),
            U64 => "ulong".into(),
            U128 => "BigInteger".into(),
            F32 => "float".into(),
            F64 => "double".into(),
            Char => "char".into(),
            Str => "string".into(),
            Bytes => "ImmutableList<byte>".into(),

            Option(format) => format!("{}?", self.quote_type(format)),
            Seq(format) => format!("ImmutableList<{}>", self.quote_type(format)),
            Map { key, value } => format!(
                "ImmutableDictionary<{}, {}>",
                self.quote_type(key),
                self.quote_type(value)
            ),
            Tuple(formats) => format!("({})", self.quote_types(formats)),
            TupleArray {
                content,
                size: _size,
            } => format!("ImmutableList<{}>", self.quote_type(content),),
            Variable(_) => panic!("unexpected value"),
        }
    }

    fn enter_class(&mut self, name: &str, reserved_subclass_names: &[&str]) {
        self.out.indent();
        self.current_namespace.push(name.to_string());
        for name in reserved_subclass_names {
            let entry = self
                .current_reserved_names
                .entry(name.to_string())
                .or_insert(0);
            *entry += 1;
        }
    }

    fn leave_class(&mut self, reserved_subclass_names: &[&str]) {
        self.out.unindent();
        self.current_namespace.pop();
        for name in reserved_subclass_names {
            let entry = self.current_reserved_names.get_mut(*name).unwrap();
            *entry -= 1;
            if *entry == 0 {
                self.current_reserved_names.remove(*name);
            }
        }
    }

    fn quote_types(&self, formats: &[Format]) -> String {
        formats
            .iter()
            .map(|f| self.quote_type(f))
            .collect::<Vec<_>>()
            .join(", ")
    }

    fn output_trait_helpers(&mut self, registry: &Registry) -> Result<()> {
        let mut subtypes = BTreeMap::new();
        for format in registry.values() {
            format
                .visit(&mut |f| {
                    if Self::needs_helper(f) {
                        subtypes.insert(common::mangle_type(f), f.clone());
                    }
                    Ok(())
                })
                .unwrap();
        }
        writeln!(self.out, "static partial class TraitHelpers {{")?;
        let reserved_names = &[];
        self.enter_class("TraitHelpers", reserved_names);
        for (mangled_name, subtype) in &subtypes {
            self.output_serialization_helper(mangled_name, subtype)?;
            self.output_deserialization_helper(mangled_name, subtype)?;
        }
        self.leave_class(reserved_names);
        writeln!(self.out, "}}\n")
    }

    fn needs_helper(format: &Format) -> bool {
        use Format::*;
        matches!(
            format,
            Option(_) | Seq(_) | Map { .. } | Tuple(_) | TupleArray { .. }
        )
    }

    fn quote_serialize_value(&self, value: &str, format: &Format) -> String {
        use Format::*;
        match format {
            TypeName(_) => format!("{}.Serialize(serializer);", value),
            Unit => format!("serializer.serialize_unit({});", value),
            Bool => format!("serializer.serialize_bool({});", value),
            I8 => format!("serializer.serialize_i8({});", value),
            I16 => format!("serializer.serialize_i16({});", value),
            I32 => format!("serializer.serialize_i32({});", value),
            I64 => format!("serializer.serialize_i64({});", value),
            I128 => format!("serializer.serialize_i128({});", value),
            U8 => format!("serializer.serialize_u8({});", value),
            U16 => format!("serializer.serialize_u16({});", value),
            U32 => format!("serializer.serialize_u32({});", value),
            U64 => format!("serializer.serialize_u64({});", value),
            U128 => format!("serializer.serialize_u128({});", value),
            F32 => format!("serializer.serialize_f32({});", value),
            F64 => format!("serializer.serialize_f64({});", value),
            Char => format!("serializer.serialize_char({});", value),
            Str => format!("serializer.serialize_str({});", value),
            Bytes => format!("serializer.serialize_bytes({});", value),
            _ => format!(
                "{}.serialize_{}({}, serializer);",
                self.quote_qualified_name("TraitHelpers"),
                common::mangle_type(format),
                value
            ),
        }
    }

    fn quote_deserialize(&self, format: &Format) -> String {
        use Format::*;
        match format {
            TypeName(name) => {
                if self.cstyle_enum_names.contains(name) {
                    format!(
                        "{}SerdeExtensions.Deserialize(deserializer)",
                        self.quote_qualified_name(name)
                    )
                } else {
                    format!(
                        "{}.Deserialize(deserializer)",
                        self.quote_qualified_name(name)
                    )
                }
            }
            Unit => "deserializer.deserialize_unit()".to_string(),
            Bool => "deserializer.deserialize_bool()".to_string(),
            I8 => "deserializer.deserialize_i8()".to_string(),
            I16 => "deserializer.deserialize_i16()".to_string(),
            I32 => "deserializer.deserialize_i32()".to_string(),
            I64 => "deserializer.deserialize_i64()".to_string(),
            I128 => "deserializer.deserialize_i128()".to_string(),
            U8 => "deserializer.deserialize_u8()".to_string(),
            U16 => "deserializer.deserialize_u16()".to_string(),
            U32 => "deserializer.deserialize_u32()".to_string(),
            U64 => "deserializer.deserialize_u64()".to_string(),
            U128 => "deserializer.deserialize_u128()".to_string(),
            F32 => "deserializer.deserialize_f32()".to_string(),
            F64 => "deserializer.deserialize_f64()".to_string(),
            Char => "deserializer.deserialize_char()".to_string(),
            Str => "deserializer.deserialize_str()".to_string(),
            Bytes => "deserializer.deserialize_bytes()".to_string(),
            _ => format!(
                "{}.deserialize_{}(deserializer)",
                self.quote_qualified_name("TraitHelpers"),
                common::mangle_type(format),
            ),
        }
    }

    fn output_serialization_helper(&mut self, name: &str, format0: &Format) -> Result<()> {
        use Format::*;

        write!(
            self.out,
            "public static void serialize_{}({} value, Bincode.BincodeSerializer serializer) {{",
            name,
            self.quote_type(format0)
        )?;
        self.out.indent();
        match format0 {
            Option(format) => {
                write!(
                    self.out,
                    r#"
if (value is not null) {{
    serializer.serialize_option_tag(true);
    {}
}} else {{
    serializer.serialize_option_tag(false);
}}
"#,
                    self.quote_serialize_value("(value ?? default)", format)
                )?;
            }

            Seq(format) => {
                write!(
                    self.out,
                    r#"
serializer.serialize_len(value.Count);
foreach (var item in value) {{
    {}
}}
"#,
                    self.quote_serialize_value("item", format)
                )?;
            }

            Map { key, value } => {
                write!(
                    self.out,
                    r#"
serializer.serialize_len(value.Count);
int[] offsets = new int[value.Count];
int count = 0;
foreach (KeyValuePair<{}, {}> entry in value) {{
    offsets[count++] = serializer.get_buffer_offset();
    {}
    {}
}}
serializer.sort_map_entries(offsets);
"#,
                    self.quote_type(key),
                    self.quote_type(value),
                    self.quote_serialize_value("entry.Key", key),
                    self.quote_serialize_value("entry.Value", value)
                )?;
            }

            Tuple(formats) => {
                writeln!(self.out)?;
                for (index, format) in formats.iter().enumerate() {
                    let expr = format!("value.Item{}", index + 1);
                    writeln!(self.out, "{}", self.quote_serialize_value(&expr, format))?;
                }
            }

            TupleArray { content, size } => {
                write!(
                    self.out,
                    r#"
if (value.Count != {0}) {{
    throw new Serde.SerializationException("Invalid length for fixed-size array: " + value.Count + " instead of " + {0});
}}
foreach (var item in value) {{
    {1}
}}
"#,
                    size,
                    self.quote_serialize_value("item", content),
                )?;
            }

            _ => panic!("unexpected case"),
        }
        self.out.unindent();
        writeln!(self.out, "}}\n")
    }

    fn output_deserialization_helper(&mut self, name: &str, format0: &Format) -> Result<()> {
        use Format::*;

        write!(
            self.out,
            "public static {} deserialize_{}(Bincode.BincodeDeserializer deserializer) {{",
            self.quote_type(format0),
            name,
        )?;
        self.out.indent();
        match format0 {
            Option(format) => {
                write!(
                    self.out,
                    r#"
bool tag = deserializer.deserialize_option_tag();
if (!tag) {{
    return null;
}} else {{
    return {};
}}
"#,
                    self.quote_deserialize(format),
                )?;
            }

            Seq(format) => {
                write!(
                    self.out,
                    r#"
long length = deserializer.deserialize_len();
{0}[] obj = new {0}[length];
for (int i = 0; i < length; i++) {{
    obj[i] = {1};
}}
return obj.ToImmutableList();
"#,
                    self.quote_type(format),
                    self.quote_deserialize(format)
                )?;
            }

            Map { key, value } => {
                write!(
                    self.out,
                    r#"
long length = deserializer.deserialize_len();
var obj = new Dictionary<{0}, {1}>();
int previous_key_start = 0;
int previous_key_end = 0;
for (long i = 0; i < length; i++) {{
    int key_start = deserializer.get_buffer_offset();
    var key = {2};
    int key_end = deserializer.get_buffer_offset();
    if (i > 0) {{
        deserializer.check_that_key_slices_are_increasing(
            new Serde.Range(previous_key_start, previous_key_end),
            new Serde.Range(key_start, key_end));
    }}
    previous_key_start = key_start;
    previous_key_end = key_end;
    var value = {3};
    obj[key] = value;
}}
return obj.ToImmutableDictionary();
"#,
                    self.quote_type(key),
                    self.quote_type(value),
                    self.quote_deserialize(key),
                    self.quote_deserialize(value),
                )?;
            }

            Tuple(formats) => {
                write!(
                    self.out,
                    r#"
return ({}
);
"#,
                    formats
                        .iter()
                        .map(|f| format!("\n    {}", self.quote_deserialize(f)))
                        .collect::<Vec<_>>()
                        .join(",")
                )?;
            }

            TupleArray { content, size } => {
                write!(
                    self.out,
                    r#"
{0}[] obj = new {0}[{1}];
for (int i = 0; i < {1}; i++) {{
    obj[i] = {2};
}}
return obj.ToImmutableList();
"#,
                    self.quote_type(content),
                    size,
                    self.quote_deserialize(content)
                )?;
            }

            _ => panic!("unexpected case"),
        }
        self.out.unindent();
        writeln!(self.out, "}}\n")
    }

    fn output_variant(
        &mut self,
        base: &str,
        index: u32,
        name: &str,
        variant: &VariantFormat,
    ) -> Result<()> {
        use VariantFormat::*;
        let fields = match variant {
            Unit => Vec::new(),
            NewType(format) => vec![Named {
                name: "Value".to_string(),
                value: format.as_ref().clone(),
            }],
            Tuple(formats) => formats
                .iter()
                .enumerate()
                .map(|(i, f)| Named {
                    name: format!("Item{}", i + 1),
                    value: f.clone(),
                })
                .collect(),
            Struct(fields) => fields.clone(),
            Variable(_) => panic!("incorrect value"),
        };

        let output_constructor = match variant {
            NewType(_) | Tuple(_) => true,
            _ => false
        };

        self.output_struct_or_variant_container(Some(base), Some(index), name, &fields, output_constructor)
    }

    fn output_variants(
        &mut self,
        base: &str,
        variants: &BTreeMap<u32, Named<VariantFormat>>,
    ) -> Result<()> {
        for (index, variant) in variants {
            self.output_variant(base, *index, &variant.name, &variant.value)?;
        }
        Ok(())
    }

    fn output_struct_or_variant_container(
        &mut self,
        variant_base: Option<&str>,
        variant_index: Option<u32>,
        name: &str,
        fields: &[Named<Format>],
        output_constructor: bool
    ) -> Result<()> {
        // Beginning of class
        writeln!(self.out)?;
        let fn_mods = if let Some(base) = variant_base {
            self.output_comment(name)?;
            writeln!(
                self.out,
                "public partial struct {0} : IEquatable<{0}> {{",
                name
            )?;
            ""
        } else {
            self.output_comment(name)?;
            writeln!(
                self.out,
                "public partial struct {0} : IEquatable<{0}> {{",
                name
            )?;
            ""
        };
        let reserved_names = &[];
        self.enter_class(name, reserved_names);

        // Fields
        for field in fields {
            self.output_comment(&field.name)?;
            writeln!(
                self.out,
                "{} {} {};",
                ["public", "private"][field.name.starts_with("_") as usize],
                self.quote_type(&field.value),
                field.name
            )?;
        }
        if !fields.is_empty() {
            writeln!(self.out)?;
        }

        // Constructor.
        if output_constructor {
            writeln!(
                self.out,
                "public {}({}) {{",
                name,
                fields
                    .iter()
                    .map(|f| format!("{} _{}", self.quote_type(&f.value), &f.name))
                    .collect::<Vec<_>>()
                    .join(", ")
            )?;
            self.out.indent();
            for field in fields {
                if self.is_nullable(&field.value) {
                    writeln!(
                        self.out,
                        "if (_{0} == null) throw new ArgumentNullException(nameof(_{0}));",
                        &field.name
                    )?;
                }
                writeln!(self.out, "{0} = _{0};", &field.name)?;
            }
            self.out.unindent();
            writeln!(self.out, "}}")?;
        }

        // Serialize
        if self.generator.config.serialization {
            writeln!(self.out, "\ninternal static void Serialize(Bincode.BincodeSerializer serializer, {name} value) => value.Serialize(serializer);");

            writeln!(
                self.out,
                "\ninternal {}void Serialize(Bincode.BincodeSerializer serializer) {{",
                fn_mods
            )?;
            self.out.indent();
            writeln!(self.out, "serializer.increase_container_depth();")?;
            if let Some(index) = variant_index {
                writeln!(self.out, "serializer.serialize_variant_index({});", index)?;
            }
            for field in fields {
                writeln!(
                    self.out,
                    "{}",
                    self.quote_serialize_value(&field.name, &field.value)
                )?;
            }
            writeln!(self.out, "serializer.decrease_container_depth();")?;
            self.out.unindent();
            writeln!(self.out, "}}")?;

            if variant_index.is_none() {
                for encoding in &self.generator.config.encodings {
                    self.output_class_serialize_for_encoding(*encoding)?;
                }
            }
        }

        // Deserialize (struct) or Load (variant)
        if self.generator.config.serialization {
            if variant_index.is_none() {
                writeln!(
                    self.out,
                    "\ninternal static {}{} Deserialize(Bincode.BincodeDeserializer deserializer) {{",
                    fn_mods, name,
                )?;
            } else {
                writeln!(
                    self.out,
                    "\ninternal static {} Load(Bincode.BincodeDeserializer deserializer) {{",
                    name,
                )?;
            }
            self.out.indent();
            writeln!(self.out, "deserializer.increase_container_depth();")?;
            writeln!(
                self.out,
                "{0} obj = default;\n\t{1}",
                name,
                fields
                    .iter()
                    .map(|f| format!("obj.{} = {};\n\t", f.name, self.quote_deserialize(&f.value)))
                    .collect::<Vec<_>>()
                    .join("")
            )?;
            writeln!(self.out, "deserializer.decrease_container_depth();")?;
            writeln!(self.out, "return obj;")?;
            self.out.unindent();
            writeln!(self.out, "}}")?;

            if variant_index.is_none() {
                for encoding in &self.generator.config.encodings {
                    self.output_class_deserialize_for_encoding(name, *encoding)?;
                }
            }
        }
        // Equality
        writeln!(
            self.out,
            "public override bool Equals(object? obj) => obj is {} other && Equals(other);\n",
            name
        )?;
        writeln!(
            self.out,
            "public static bool operator ==({0} left, {0} right) => Equals(left, right);\n",
            name
        )?;
        writeln!(
            self.out,
            "public static bool operator !=({0} left, {0} right) => !Equals(left, right);\n",
            name
        )?;

        writeln!(self.out, "public bool Equals({} other) {{", name)?;
        self.out.indent();
        //writeln!(self.out, "if (other == null) return false;")?;
        //writeln!(self.out, "if (ReferenceEquals(this, other)) return true;")?;
        for field in fields {
            writeln!(
                self.out,
                "if ({0} != other.{0}) return false;",
                &field.name,
            )?;
        }
        writeln!(self.out, "return true;")?;
        self.out.unindent();
        writeln!(self.out, "}}")?;

        // Hashing
        writeln!(self.out, "\npublic override int GetHashCode() {{")?;
        self.out.indent();
        writeln!(self.out, "unchecked {{")?;
        self.out.indent();
        writeln!(self.out, "int value = 7;")?;
        for field in fields {
            writeln!(
                self.out,
                "value = 31 * value + {0}.GetHashCode();",
                &field.name
            )?;
        }
        writeln!(self.out, "return value;")?;
        self.out.unindent();
        writeln!(self.out, "}}")?;
        self.out.unindent();
        writeln!(self.out, "}}\n")?;

        /*
        // Clone
        if variant_base.is_none() {
            // Derived classes can use the method inherited from the base class, it works with derived fields.
            writeln!(
                self.out,
                "/// <summary>Creates a shallow clone of the object.</summary>"
            )?;
            writeln!(
                self.out,
                "public {0} Clone() => ({0})MemberwiseClone();\n",
                name
            )?;
            writeln!(self.out, "object ICloneable.Clone() => Clone();\n")?;
        } */

        // Custom code
        self.output_custom_code()?;

        // End of class
        self.leave_class(reserved_names);
        writeln!(self.out, "}}")
    }

    fn output_enum_container(
        &mut self,
        name: &str,
        variants: &BTreeMap<u32, Named<VariantFormat>>,
    ) -> Result<()> {
        writeln!(self.out)?;
        self.output_comment(name)?;
        writeln!(
            self.out,
            "public partial struct {0} {{",
            name
        )?;
        let reserved_names = variants
            .values()
            .map(|v| v.name.as_str())
            .collect::<Vec<_>>();
        self.enter_class(name, &reserved_names);

        // Inner
        writeln!(self.out, "/// <summary>Gets the inner variant object. This can be used in switch cases to destructure the enum.</summary>")?;
        writeln!(self.out, "public object Inner {{ get {{")?;
        self.out.indent();
        writeln!(self.out, "switch (_variantId.GetValueOrDefault(-1)) {{")?;
        for (id, _) in variants {
            writeln!(self.out, "case {id}: return _variant{id};")?;
        }
        writeln!(
            self.out,
            r#"default: throw new InvalidOperationException("Unknown variant type");"#
        )?;
        writeln!(self.out, "}}")?;
        self.out.unindent();
        writeln!(self.out, "}} }}")?;

        writeln!(self.out, "private int? _variantId;")?;

        for (id, variant) in variants {
            writeln!(self.out, "\nprivate {} _variant{id};", variant.name)?;
        }

        for (id, variant) in variants {
            writeln!(self.out, "\npublic static implicit operator {name}({} value) {{", variant.name)?;
            writeln!(self.out, "    {name} result = default;")?;
            writeln!(self.out, "    result._variantId = {id};")?;
            writeln!(self.out, "    result._variant{id} = value;")?;
            writeln!(self.out, "    return result;")?;
            writeln!(self.out, "}}")?;
        }

        // Serialize/Deserialize
        if self.generator.config.serialization {
            writeln!(self.out, "\ninternal static void Serialize(Bincode.BincodeSerializer serializer, {name} value) => value.Serialize(serializer);");

            write!(
                self.out,
                "\ninternal void Serialize(Bincode.BincodeSerializer serializer) {{"
            )?;
            self.out.indent();
            writeln!(
                self.out,
                r#"
switch (_variantId.GetValueOrDefault(-1)) {{"#,
            )?;
            self.out.indent();
            for (index, _) in variants {
                writeln!(
                    self.out,
                    "case {index}: _variant{index}.Serialize(serializer); break;",
                )?;
            }
            writeln!(
                self.out,
                r#"default: throw new Serde.DeserializationException("Uninitialized {} value");"#,
                name,
            )?;
            self.out.unindent();
            writeln!(self.out, "}}")?;
            self.out.unindent();
            writeln!(self.out, "}}")?;
            
            write!(
                self.out,
                "\ninternal static {} Deserialize(Bincode.BincodeDeserializer deserializer) {{",
                name
            )?;
            self.out.indent();
            writeln!(
                self.out,
                r#"
int index = deserializer.deserialize_variant_index();
switch (index) {{"#,
            )?;
            self.out.indent();
            for (index, variant) in variants {
                writeln!(
                    self.out,
                    "case {}: return {}.Load(deserializer);",
                    index, variant.name,
                )?;
            }
            writeln!(
                self.out,
                r#"default: throw new Serde.DeserializationException("Unknown variant index for {}: " + index);"#,
                name,
            )?;
            self.out.unindent();
            writeln!(self.out, "}}")?;
            self.out.unindent();
            writeln!(self.out, "}}")?;

            for encoding in &self.generator.config.encodings {
                self.output_class_serialize_for_encoding(*encoding)?;
                self.output_class_deserialize_for_encoding(name, *encoding)?;
            }
        }

        // HashCode
        writeln!(self.out, "public override int GetHashCode() {{")?;
        self.out.indent();
        writeln!(self.out, "switch (_variantId.GetValueOrDefault(-1)) {{")?;
        for (id, _) in variants {
            writeln!(self.out, "case {id}: return _variant{id}.GetHashCode();")?;
        }
        writeln!(
            self.out,
            r#"default: throw new InvalidOperationException("Unknown variant type");"#
        )?;
        writeln!(self.out, "}}")?;
        self.out.unindent();
        writeln!(self.out, "}}")?;

        
        // Equals
        writeln!(
            self.out,
            "public override bool Equals(object? obj) => obj is {} other && Equals(other);\n",
            name
        )?;

        writeln!(self.out, "public bool Equals({} other) {{", name)?;
        self.out.indent();
        writeln!(self.out, "if (_variantId != other._variantId) return false;")?;
        writeln!(self.out, "switch (_variantId.GetValueOrDefault(-1)) {{")?;
        for id in variants.keys() {
            writeln!(
                self.out,
                "case {id}: return _variant{id}.Equals(other._variant{id});",
            )?;
        }
        writeln!(
            self.out,
            r#"default: return true;"#
        )?;
        writeln!(self.out, "}}")?;
        self.out.unindent();
        writeln!(self.out, "}}\n")?;

        writeln!(
            self.out,
            "public static bool operator ==({0} left, {0} right) => Equals(left, right);\n",
            name
        )?;
        writeln!(
            self.out,
            "public static bool operator !=({0} left, {0} right) => !Equals(left, right);\n",
            name
        )?;

        /*// Clone
        writeln!(
            self.out,
            "/// <summary>Creates a shallow clone of the object.</summary>"
        )?;
        writeln!(
            self.out,
            "public {0} Clone() => ({0})MemberwiseClone();\n",
            name
        )?;
        //writeln!(self.out, "object ICloneable.Clone() => Clone();\n")?;
*/
        self.output_variants(name, variants)?; 
        self.leave_class(&reserved_names);
        writeln!(self.out, "}}\n")
    }

    fn output_cstyle_enum(
        &mut self,
        name: &str,
        variants: &BTreeMap<u32, Named<VariantFormat>>,
    ) -> Result<()> {
        writeln!(self.out)?;
        self.output_comment(name)?;
        writeln!(self.out, "public enum {} {{", name)?;
        self.enter_class(name, &[]);
        self.out.indent();
        for (index, variant) in variants {
            self.output_comment(&variant.name)?;
            writeln!(self.out, "{} = {},", variant.name, index)?;
        }
        self.out.unindent();
        self.leave_class(&[]);
        writeln!(self.out, "}}")?;

        if self.generator.config.serialization {
            let ext_name = format!("{}SerdeExtensions", name);
            writeln!(self.out, "internal static class {} {{", ext_name)?;
            self.enter_class(&ext_name, &[]);

            writeln!(
                self.out,
                r#"
internal static void Serialize(this {0} value, Bincode.BincodeSerializer serializer) {{
    serializer.increase_container_depth();
    serializer.serialize_variant_index((int)value);
    serializer.decrease_container_depth();
}}

internal static {0} Deserialize(Bincode.BincodeDeserializer deserializer) {{
    deserializer.increase_container_depth();
    int index = deserializer.deserialize_variant_index();
    if (!Enum.IsDefined(typeof({0}), index))
        throw new Serde.DeserializationException("Unknown variant index for {}: " + index);
    {0} value = ({0})index;
    deserializer.decrease_container_depth();
    return value;
}}"#,
                name
            )?;

            for encoding in &self.generator.config.encodings {
                writeln!(
                    self.out,
                    r#"
internal static byte[] {0}Serialize(this {1} value)  {{
    Bincode.BincodeSerializer serializer = new {0}.{0}Serializer();
    Serialize(value, serializer);
    return serializer.get_bytes();
}}"#,
                    encoding.name().to_camel_case(),
                    name
                )?;
                self.output_class_deserialize_for_encoding(name, *encoding)?;
            }

            self.leave_class(&[]);
            writeln!(self.out, "}}")?;
        }

        Ok(())
    }

    fn output_class_serialize_for_encoding(&mut self, encoding: Encoding) -> Result<()> {
        writeln!(
            self.out,
            r#"
internal int {0}Serialize(byte[] outputBuffer) => {0}Serialize(new ArraySegment<byte>(outputBuffer));

internal int {0}Serialize(ArraySegment<byte> outputBuffer) {{
    Bincode.BincodeSerializer serializer = new {0}.{0}Serializer(outputBuffer);
    Serialize(serializer);
    return serializer.get_buffer_offset();
}}

internal byte[] {0}Serialize()  {{
    Bincode.BincodeSerializer serializer = new {0}.{0}Serializer();
    Serialize(serializer);
    return serializer.get_bytes();
}}"#,
            encoding.name().to_camel_case()
        )
    }

    fn output_class_deserialize_for_encoding(
        &mut self,
        name: &str,
        encoding: Encoding,
    ) -> Result<()> {
        writeln!(
            self.out,
            r#"
internal static {0} {1}Deserialize(byte[] input) => {1}Deserialize(new ArraySegment<byte>(input));

internal static {0} {1}Deserialize(ArraySegment<byte> input) {{
    if (input == null) {{
         throw new Serde.DeserializationException("Cannot deserialize null array");
    }}
    Bincode.BincodeDeserializer deserializer = new {1}.{1}Deserializer(input);
    {0} value = Deserialize(deserializer);
    if (deserializer.get_buffer_offset() < input.Length) {{
         throw new Serde.DeserializationException("Some input bytes were not read");
    }}
    return value;
}}"#,
            name,
            encoding.name().to_camel_case()
        )
    }

    fn output_container(&mut self, name: &str, format: &ContainerFormat) -> Result<()> {
        use ContainerFormat::*;
        let fields = match format {
            UnitStruct => Vec::new(),
            NewTypeStruct(format) => vec![Named {
                name: "_value".to_string(),
                value: format.as_ref().clone(),
            }],
            TupleStruct(formats) => formats
                .iter()
                .enumerate()
                .map(|(i, f)| Named {
                    name: format!("Item{}", i + 1),
                    value: f.clone(),
                })
                .collect::<Vec<_>>(),
            Struct(fields) => fields.clone(),
            Enum(variants) => {
                if variants
                    .iter()
                    .all(|(_i, v)| v.value == VariantFormat::Unit)
                    && self.cstyle_enum_names.contains(&name.into())
                {
                    self.output_cstyle_enum(name, variants)?;
                } else {
                    self.output_enum_container(name, variants)?;
                }
                return Ok(());
            }
        };
        
        let output_constructor = match format {
            ContainerFormat::NewTypeStruct(_) | ContainerFormat::TupleStruct(_) => true,
            _ => false
        };

        self.output_struct_or_variant_container(None, None, name, &fields, false)
    }
}

/// Installer for generated source files in C#.
pub struct Installer {
    install_dir: PathBuf,
}

impl Installer {
    pub fn new(install_dir: PathBuf) -> Self {
        Installer { install_dir }
    }

    fn install_runtime(
        &self,
        source_dir: include_dir::Dir,
        path: &str,
    ) -> std::result::Result<(), Box<dyn std::error::Error>> {
        let dir_path = self.install_dir.join(path);
        std::fs::create_dir_all(&dir_path)?;
        for entry in source_dir.files() {
            let mut file = std::fs::File::create(dir_path.join(entry.path()))?;
            file.write_all(entry.contents())?;
        }
        Ok(())
    }
}

impl crate::SourceInstaller for Installer {
    type Error = Box<dyn std::error::Error>;

    fn install_module(
        &self,
        config: &CodeGeneratorConfig,
        registry: &Registry,
    ) -> std::result::Result<(), Self::Error> {
        let name = config.module_name.clone();
        let generator = CodeGenerator::new(config);
        let dir_path = generator.write_source_files(self.install_dir.clone(), registry)?;

        if !config.package_manifest {
            return Ok(());
        }

        let back_path: String = "..\\"
            .to_string()
            .repeat(dir_path.strip_prefix(&self.install_dir)?.iter().count());
        let mut deps = vec!["Serde".to_string()];
        for encoding in &config.encodings {
            deps.push(encoding.name().to_camel_case());
        }
        let mut dependencies = String::new();
        for dep in deps {
            writeln!(
                &mut dependencies,
                "        <ProjectReference Include=\"{1}{0}\\{0}.csproj\" />",
                dep, back_path
            )?;
        }
        let mut proj = std::fs::File::create(dir_path.join(name + ".csproj"))?;
        write!(
            proj,
            r#"
<Project Sdk="Microsoft.NET.Sdk">
    <PropertyGroup>
        <TargetFramework>netstandard2.0</TargetFramework>
        <LangVersion>7.2</LangVersion>
    </PropertyGroup>
    <ItemGroup>
        <PackageReference Include="System.Memory" Version="4.5.4" />
        <PackageReference Include="System.ValueTuple" Version="4.5.0" />
    </ItemGroup>
    <ItemGroup>
{}    
    </ItemGroup>
</Project>
"#,
            dependencies
        )?;

        Ok(())
    }

    fn install_serde_runtime(&self) -> std::result::Result<(), Self::Error> {
        self.install_runtime(include_directory!("runtime/csharp/Serde"), "Serde")
    }

    fn install_bincode_runtime(&self) -> std::result::Result<(), Self::Error> {
        self.install_runtime(include_directory!("runtime/csharp/Bincode"), "Bincode")
    }

    fn install_bcs_runtime(&self) -> std::result::Result<(), Self::Error> {
        self.install_runtime(include_directory!("runtime/csharp/Bcs"), "Bcs")
    }
}
