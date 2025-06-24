use convert_case::*;
use std::borrow::*;
use std::fmt::*;

/// Generates C# code for a type.
pub struct DisplayCs<'a, T: DisplayBindings>(pub &'a T);

impl<'a, T: DisplayBindings> Display for DisplayCs<'a, T> {
    fn fmt(&self, f: &mut Formatter<'_>) -> Result {
        self.0.write_cs(f)
    }
}

/// Generates Rust code for a type.
pub struct DisplayRs<'a, T: DisplayBindings>(pub &'a T);

impl<'a, T: DisplayBindings> Display for DisplayRs<'a, T> {
    fn fmt(&self, f: &mut Formatter<'_>) -> Result {
        self.0.write_rs(f)
    }
}

/// A binding type that can generate either Rust or C# code.
pub trait DisplayBindings {
    /// Generates the C#-side code for this binding.
    fn write_cs(&self, f: &mut Formatter) -> Result;

    /// Generates the Rust-side code for this binding.
    fn write_rs(&self, f: &mut Formatter) -> Result;
}

/// A builtin, non-primitive type with special properties.
#[derive(Clone, Debug, PartialEq, Eq, PartialOrd, Ord)]
pub enum BuiltinType {
    /// A [`std::time::SystemTime`].
    DateTime,
    /// The type is nullable or optional.
    Option(Box<TypeReference>),
    /// A UTF8-encoded sequence of characters.
    String,
    /// A [`std::time::Duration`].
    TimeSpan,
    /// The type is a list of items.
    Vec(Box<TypeReference>)
}

impl BuiltinType {
    /// Whether this reference contains a type that could not be resolved.
    pub fn contains_unknown(&self) -> bool {
        match self {
            Self::Option(x) => x.contains_unknown(),
            Self::Vec(_) => true,
            _ => false
        }
    }

    /// Generates C# code that converts `value` to the Rust FFI equivalent.
    pub fn cs_to_rs_conversion(&self, value: &str, f: &mut Formatter) -> Result {
        match self {
            Self::DateTime => write!(f, "TIME_CONV_TODO({value})"),
            Self::Option(_) => {
                write!(f, "new {} {{\n", DisplayRs(self))?;
                write!(f, "    has_value = {value}.HasValue,\n")?;
                write!(f, "    value = {value}.GetValueOrDefault(),\n")?;
                write!(f, "}}")
            },
            Self::String => write!(f, "STRING_CONV_TODO"),
            Self::TimeSpan => write!(f, "Egui.TIME_SECOND * {value}.TotalSeconds"),
            Self::Vec(_) => write!(f, "VEC_CONV_TODO({value})"),
        }
    }

    /// Generates C# code that converts `value` from the Rust FFI equivalent.
    pub fn rs_to_cs_conversion(&self, value: &str, f: &mut Formatter) -> Result {
        match self {
            Self::DateTime => write!(f, "TIME_CONV_TODO({value})"),
            Self::Option(type_reference) => {
                write!(f, "{value}.has_value ? ")?;
                type_reference.cs_to_rs_conversion(&format!("{value}.value"), f)?;
                write!(f, " : null")
            },
            Self::String => write!(f, "STRING_CONV_TODO"),
            Self::TimeSpan => write!(f, "TimeSpan.FromSeconds({value} / Egui.TIME_SECOND)"),
            Self::Vec(_) => write!(f, "VEC_CONV_TODO({value})"),
        }
    }
}

impl DisplayBindings for BuiltinType {
    fn write_cs(&self, f: &mut Formatter) -> Result {
        match self {
            Self::DateTime => write!(f, "DateTime"),
            Self::Option(ty) => write!(f, "{}?", DisplayCs(&**ty)),
            Self::String => write!(f, "string"),
            Self::TimeSpan => write!(f, "TimeSpan"),
            Self::Vec(ty) => write!(f, "List<{}>", DisplayCs(&**ty)),
        }
    }

    fn write_rs(&self, f: &mut Formatter) -> Result {
        match self {
            Self::DateTime => write!(f, "EguiPosixTime"),
            Self::Option(ty) => {
                let rs_name = format!("{}", DisplayRs(&**ty));
                write!(f, "EguiOption{}", rs_name.strip_prefix("Egui").unwrap_or(&rs_name).to_case(Case::Pascal))
            },
            Self::String => write!(f, "EguiString"),
            Self::TimeSpan => write!(f, "EguiDuration"),
            Self::Vec(ty) => write!(f, "VEC_TY_TODO({})", DisplayRs(&**ty)),
        }
    }
}

/// A primitive type that can be shared between C# and Rust.
#[derive(Clone, Debug, PartialEq, Eq, PartialOrd, Ord)]
pub enum PrimitiveType {
    /// The [`bool`] type.
    Bool,
    /// The [`u8`] type.
    U8,
    /// The [`u16`] type.
    U16,
    /// The [`u32`] type.
    U32,
    /// The [`u64`] type.
    U64,
    /// The [`i8`] type.
    I8,
    /// The [`i16`] type.
    I16,
    /// The [`i32`] type.
    I32,
    /// The [`i64`] type.
    I64,
    /// The [`f32`] type.
    F32,
    /// The [`f64`] type.
    F64,
    /// The [`isize`] type.
    ISize,
    /// The [`usize`] type.
    USize
}

impl DisplayBindings for PrimitiveType {
    fn write_cs(&self, f: &mut Formatter) -> Result {
        write!(f, "{}", match self {
            PrimitiveType::Bool => "bool",
            PrimitiveType::U8 => "byte",
            PrimitiveType::U16 => "ushort",
            PrimitiveType::U32 => "uint",
            PrimitiveType::U64 => "ulong",
            PrimitiveType::I8 => "sbyte",
            PrimitiveType::I16 => "short",
            PrimitiveType::I32 => "int",
            PrimitiveType::I64 => "long",
            PrimitiveType::F32 => "float",
            PrimitiveType::F64 => "double",
            PrimitiveType::ISize => "nint",
            PrimitiveType::USize => "nuint",
        })
    }

    fn write_rs(&self, f: &mut Formatter) -> Result {
        write!(f, "{}", match self {
            PrimitiveType::Bool => "bool",
            PrimitiveType::U8 => "u8",
            PrimitiveType::U16 => "u16",
            PrimitiveType::U32 => "u32",
            PrimitiveType::U64 => "u64",
            PrimitiveType::I8 => "i8",
            PrimitiveType::I16 => "i16",
            PrimitiveType::I32 => "i32",
            PrimitiveType::I64 => "i64",
            PrimitiveType::F32 => "f32",
            PrimitiveType::F64 => "f64",
            PrimitiveType::ISize => "isize",
            PrimitiveType::USize => "usize",
        })
    }
}

/// Defines the data necessary to use or marshal another type.
#[derive(Clone, Debug, PartialEq, Eq, PartialOrd, Ord)]
pub enum TypeReference {
    /// A builtin non-primitive type.
    Builtin(BuiltinType),
    /// The item is a class backed by a handle.
    Class(TypeName),
    /// The item is a simple numeric enum.
    Enum(TypeName),
    /// The type is externally-provided.
    Primitive(PrimitiveType),
    /// The item is a POD struct.
    Struct(TypeName),
    /// The item could not be resolved.
    Unknown(String)
}

impl TypeReference {
    /// Whether this reference contains a type that could not be resolved.
    pub fn contains_unknown(&self) -> bool {
        match self {
            Self:: Builtin(x) => x.contains_unknown(),
            Self::Unknown(_) => true,
            _ => false
        }
    }

    /// Generates C# code that converts `value` to the Rust FFI equivalent.
    pub fn cs_to_rs_conversion(&self, value: &str, f: &mut Formatter) -> Result {
        match self {
            Self::Builtin(x) => x.cs_to_rs_conversion(value, f),
            Self::Class(_) => write!(f, "CLASS_CONV_TODO({value})"),
            Self::Enum(type_name) => write!(f, "{value}.Cast<{}, {}>()", type_name.cs, type_name.rs),
            Self::Primitive(_) => write!(f, "{value}"),
            Self::Struct(type_name) => write!(f, "({}){value}", type_name.rs),
            Self::Unknown(_) => write!(f, "{value}")
        }
    }

    /// Generates C# code that converts `value` from the Rust FFI equivalent.
    pub fn rs_to_cs_conversion(&self, value: &str, f: &mut Formatter) -> Result {
        match self {
            Self::Builtin(x) => x.rs_to_cs_conversion(value, f),
            Self::Class(_) => write!(f, "CLASS_CONV_TODO({value})"),
            Self::Enum(type_name) => write!(f, "{value}.Cast<{}, {}>()", type_name.rs, type_name.cs),
            Self::Primitive(_) => write!(f, "{value}"),
            Self::Struct(type_name) => write!(f, "({}){value}", type_name.cs),
            Self::Unknown(_) => write!(f, "{value}")
        }
    }

    /// Writes the definition of an `EguiOption...` struct that can hold this type.
    pub fn rs_option_wrapper(&self, f: &mut Formatter) -> Result {
        write!(f, "/// A type that may contain a value or nothing.\n")?;
        write!(f, "#[derive(Copy, Clone, Default)]\n")?;
        write!(f, "#[repr(C)]\n")?;
        write!(f, "pub struct {} {{\n", DisplayRs(&Self::Builtin(BuiltinType::Option(Box::new(self.clone())))))?;
        write!(f, "    /// True if this is `Some`.\n")?;
        write!(f, "    pub has_value: bool,\n")?;
        write!(f, "    /// The underlying value if this is `Some`.\n")?;
        write!(f, "    pub value: {},\n", DisplayRs(self))?;
        write!(f, "}}\n")?;
        Ok(())
    }

    /// Gets the direct name of this type, if any.
    pub fn type_name(&self) -> Option<&TypeName> {
        match self {
            Self::Class(x) | Self::Enum(x) | Self::Struct(x) => Some(x),
            _ => None
        }
    }
}

impl DisplayBindings for TypeReference {
    fn write_cs(&self, f: &mut Formatter) -> Result {
        match self {
            Self::Builtin(x) => x.write_cs(f),
            Self::Enum(name) => write!(f, "{}", name.cs),
            Self::Primitive(primitive_type) => primitive_type.write_cs(f),
            Self::Class(name) | Self::Struct(name) => write!(f, "{}", name.cs),
            Self::Unknown(x) => write!(f, "{x}")
        }
    }

    fn write_rs(&self, f: &mut Formatter) -> Result {
        match self {
            Self::Builtin(x) => x.write_rs(f),
            Self::Enum(name) => write!(f, "{}", name.rs),
            Self::Primitive(primitive_type) => primitive_type.write_rs(f),
            Self::Class(name) | Self::Struct(name) => write!(f, "{}", name.rs),
            Self::Unknown(x) => write!(f, "{x}")
        }
    }
}

/// A top-level type definition.
#[derive(Clone, Debug, PartialEq, Eq, PartialOrd, Ord)]
pub enum Type {
    /// A simple enum without any payload data.
    Enum {
        /// The name of the type.
        name: TypeName,
        /// The possible enum values.
        variants: Vec<EnumVariant>,
        /// The doc-comment to include.
        docs: String,
    },
    /// A heap-allocated object backed by a handle.
    Class {
        /// The name of the type.
        name: TypeName,
        /// The possible struct fields.
        fields: Vec<StructField>,
        /// The possible methods.
        methods: Vec<Method>,
        /// Whether the struct implements [`Default`] on the Rust side.
        has_default: bool,
        /// The doc-comment to include.
        docs: String
    },
    /// A plain-old-data type that can be copied from C# to Rust or vice-versa.
    Struct {
        /// The name of the type.
        name: TypeName,
        /// The possible struct fields.
        fields: Vec<StructField>,
        /// The possible methods.
        methods: Vec<Method>,
        /// Whether the struct implements [`Default`] on the Rust side.
        has_default: bool,
        /// The doc-comment to include.
        docs: String
    }
}

impl Type {
    /// Gets the doc-comment associated with this item.
    pub fn docs(&self) -> &str {
        match self {
            Type::Enum { docs, .. } => docs,
            Type::Class { docs, .. } => docs,
            Type::Struct { docs, .. } => docs
        }
    }

    /// Gets a list of all qualified C# member names, including fields and methods that may
    /// not be generated to prevent errors..
    pub fn cs_members(&self) -> Vec<String> {
        match self {
            Type::Enum { .. } => Vec::new(),
            Type::Class { name, fields, methods, .. }
            | Type::Struct { name, fields, methods, .. } => {
                let mut result = Vec::new();

                for field in fields {
                    result.push(format!("{}.{}", name.cs, field.cs_name()));
                }

                for method in methods {
                    result.push(format!("{}.{}", name.cs, method.cs_name()));
                }

                result
            }
        }
    }

    /// The original name of the type.
    pub fn name(&self) -> &TypeName {
        match self {
            Type::Enum { name, .. } => name,
            Type::Class { name, .. } => name,
            Type::Struct { name, .. } => name
        }
    }

    /// A list of all type names referenced by this type (in its fields and methods).
    pub fn referenced_types(&self) -> Vec<TypeReference> {

        match self {
            Type::Enum { .. } => Vec::new(),
            Type::Class { fields, methods, .. }
            | Type::Struct { fields, methods, .. } => {
                let mut result = Vec::new();

                for field in fields {
                    result.push(field.ty.clone());
                }

                for method in methods {
                    if let Some(return_ty) = &method.return_ty {
                        result.push(return_ty.clone());
                    }
                    
                    for param in &method.parameters {
                        result.push(param.ty.clone());
                    }
                }

                result
            }
        }
    }

    /// Creates the casts between the idiomatic-C# and Rust-FFI bindings.
    fn write_cs_struct_casts(&self, f: &mut Formatter) -> Result {
        write_cs_docs(f, "Converts between .")?;
        write!(f, "public static readonly {} Default = ({})Egui.{}_default();\n", self.name().cs, self.name().cs, self.name().rs_fn)?;
        Ok(())
    }

    /// Creates the default field for a struct type in C#.
    fn write_cs_struct_default(&self, f: &mut Formatter) -> Result {
        write_cs_docs(f, "Returns the \"default value\" for a type.")?;
        write!(f, "public static readonly {} Default = ({})Egui.{}_default();\n", self.name().cs, self.name().cs, self.name().rs_fn)?;
        Ok(())
    }

    /// Writes the conversion operator from the Rust FFI type to the C# type.
    /// Panics if `self` is not [`Self::Struct`].
    fn write_cs_struct_conversion(&self, f: &mut Formatter) -> Result {
        let Self::Struct { fields, .. } = self else { panic!("Type was not a struct") };
        let has_unknown_tys = fields.iter().any(|x| x.ty.contains_unknown());

        if has_unknown_tys {
            write!(f, "/* UNIMPLEMENTED (conversion)\n")?;
        }

        write_cs_docs(f, "Converts from the Rust to the C# version of this type.")?;
        write!(f, "public static {} operator({} value) => new {} {{\n", self.name().cs, self.name().rs, self.name().cs)?;

        let mut field_str = String::new();
        for field in fields {
            write!(&mut field_str, "{} = ", field.cs_name())?;
            field.ty.rs_to_cs_conversion(&format!("value.{}", field.rs_name()), &mut Formatter::new(&mut field_str, f.options()))?;
            write!(&mut field_str, ",\n")?;
        }

        write!(f, "{}", &indent(&field_str))?;
        write!(f, "}}\n");

        if has_unknown_tys {
            write!(f, "*/\n")?;
        }

        Ok(())
    }

    /// Writes the conversion operator from the C# type to the Rust FFI type.
    /// Panics if `self` is not [`Self::Struct`].
    fn write_rs_struct_conversion(&self, f: &mut Formatter) -> Result {
        let Self::Struct { fields, .. } = self else { panic!("Type was not a struct") };
        let has_unknown_tys = fields.iter().any(|x| x.ty.contains_unknown());

        if has_unknown_tys {
            write!(f, "/* UNIMPLEMENTED (conversion)\n")?;
        }

        write_cs_docs(f, "Converts from the C# to the Rust version of this type.")?;
        write!(f, "public static {} operator({} value) => new {} {{\n", self.name().rs, self.name().cs, self.name().rs)?;

        let mut field_str = String::new();
        for field in fields {
            write!(&mut field_str, "{} = ", field.rs_name())?;
            field.ty.cs_to_rs_conversion(&format!("value.{}", field.cs_name()), &mut Formatter::new(&mut field_str, f.options()))?;
            write!(&mut field_str, ",\n")?;
        }

        write!(f, "{}", &indent(&field_str))?;
        write!(f, "}}\n");

        if has_unknown_tys {
            write!(f, "*/\n")?;
        }

        Ok(())
    }
 
    /// Creates the C#-side destructor for this type, assuming that it is a handle.
    fn write_cs_destructor(&self, f: &mut Formatter) -> Result {
        write!(f, "/// <inheritdoc/>\n")?;
        write!(f, "protected override void Free(EguiObject* pointer) {{\n")?;
        write!(f, "    Egui.gui_{}_drop(pointer);\n", self.name().rs_fn)?;
        write!(f, "}}\n")?;
        Ok(())
    }

    /// Creates the default field initializer for a struct type in Rust.
    fn write_rs_struct_default(&self, f: &mut Formatter) -> Result {
        write_rs_docs(f, "Returns the \"default value\" for a type.")?;
        write!(f, "#[no_mangle]\n")?;
        write!(f, "pub extern \"C\" fn egui_{}_default() -> {} {{\n", self.name().rs_fn, self.name().rs)?;
        write!(f, "    let value = {}::default();\n", self.name().original)?;
        write!(f, "    {} {{\n", self.name().rs)?;

        let Self::Struct { fields, .. } = self else { panic!("Item was not struct") };
        for field in fields {
            write!(f, "        {}: value.{}.into(),\n", field.rs_name(), field.name)?;
        }

        write!(f, "    }}\n")?;
        write!(f, "}}\n")?;
        Ok(())
    }

    /// Creates the Rust-side destructor for this type, assuming that it is a handle.
    fn write_rs_destructor(&self, f: &mut Formatter) -> Result {
        write!(f, "/// Frees the provided object.\n")?;
        write!(f, "///\n")?;
        write!(f, "/// # Safety\n")?;
        write!(f, "///\n")?;
        write!(f, "/// For this call to be sound, the pointer must refer to a live object of the corret type.\n");
        write!(f, "#[no_mangle]\n")?;
        write!(f, "pub unsafe extern \"C\" fn egui_gui_{}_drop(value: *mut EguiObject<{}>) {{\n",
            self.name().rs_fn, self.name().original)?;
        write!(f, "    EguiHandle::from_heap(value);\n")?;
        write!(f, "}}\n")?;
        Ok(())
    }
}

impl DisplayBindings for Type {
    fn write_cs(&self, f: &mut Formatter) -> Result {
        write_cs_docs(f, self.docs())?;
        match self {
            Type::Enum { variants, .. } => {
                write!(f, "public enum {} {{\n", self.name().cs)?;

                let mut members = String::new();
                for variant in variants {
                    write!(&mut members, "{}\n", DisplayCs(variant))?;
                }
                write!(f, "{}", &indent(&members))?;

                write!(f, "}}\n")?;
            },
            Type::Class { fields, has_default, methods, .. } => {
                write!(f, "public unsafe partial sealed class {} : EguiHandle {{\n", self.name().cs)?;

                let mut members = String::new();
                let mut member_names = Vec::new();

                for field in fields {
                    member_names.push(field.cs_name());
                }

                self.write_cs_destructor(&mut Formatter::new(&mut members, f.options()))?;

                for method in methods {
                    member_names.push(method.cs_name());
                    write!(&mut members, "{}\n", DisplayCs(method))?;
                }

                write!(f, "{}", &indent(&members))?;

                write!(f, "}}\n")?;
            },
            Type::Struct { fields, has_default, methods, .. } => {
                write!(f, "public unsafe partial struct {} {{\n", self.name().cs)?;
                
                if *has_default {
                    let mut default = String::new();
                    self.write_cs_struct_default(&mut Formatter::new(&mut default, f.options()))?;
                    write!(f, "{}", &indent(&default))?;
                    write!(f, "\n");
                }

                let mut members = String::new();
                let mut member_names = Vec::new();
                for field in fields {
                    member_names.push(field.cs_name());
                    write!(&mut members, "{}\n", DisplayCs(field))?;
                }

                for method in methods {
                    member_names.push(method.cs_name());
                    write!(&mut members, "{}\n", DisplayCs(method))?;
                }

                self.write_cs_struct_conversion(&mut Formatter::new(&mut members, f.options()))?;
                write!(&mut members, "\n")?;
                self.write_rs_struct_conversion(&mut Formatter::new(&mut members, f.options()))?;

                write!(f, "{}", &indent(&members))?;

                write!(f, "}}\n")?;
            }
        }
        Ok(())
    }

    fn write_rs(&self, f: &mut Formatter) -> Result {
        match self {
            Type::Enum { variants, .. } => {
                write_rs_docs(f, self.docs())?;
                write!(f, "#[derive(Copy, Clone, Default)]\n")?;
                write!(f, "#[repr(C)]\n")?;
                write!(f, "pub enum {} {{\n", self.name().rs)?;
                
                let mut members = String::new();
                for (i, variant) in variants.into_iter().enumerate() {
                    if i == 0 {
                        write!(&mut members, "#[default]\n")?;
                    }

                    write!(&mut members, "{}\n", DisplayRs(variant))?;
                }
                write!(f, "{}", &indent(&members))?;

                write!(f, "}}\n")?;
            },
            Type::Class { .. } => {
                //self.write_rs_destructor(f);
            },
            Type::Struct { fields, has_default, .. } => {
                write_rs_docs(f, self.docs())?;
                write!(f, "#[derive(Copy, Clone, Default)]\n")?;
                write!(f, "#[repr(C)]\n")?;
                write!(f, "pub struct {} {{\n", self.name().rs)?;
                
                let mut members = String::new();
                for field in fields {
                    write!(&mut members, "{}\n", DisplayRs(field))?;
                }
                write!(f, "{}", &indent(&members))?;

                write!(f, "}}\n\n")?;

                /*if *has_default {
                    self.write_rs_struct_default(f)?;
                    write!(f, "\n")?;
                }*/
            }
        }
        Ok(())
    }
}

/// An enum variant.
#[derive(Clone, Debug, PartialEq, Eq, PartialOrd, Ord)]
pub struct EnumVariant {
    /// The name of the variant.
    pub name: String,
    /// The index of the variant, if any.
    pub index: Option<u64>,
    /// The doc-comment to include.
    pub docs: String
}

impl DisplayBindings for EnumVariant {
    fn write_cs(&self, f: &mut Formatter<'_>) -> Result {
        write_cs_docs(f, &self.docs)?;
        if let Some(index) = self.index {
            write!(f, "{} = {index},", self.name)?;
        }
        else {
            write!(f, "{},", self.name)?;
        }

        Ok(())
    }

    fn write_rs(&self, f: &mut Formatter) -> Result {
        write_rs_docs(f, &self.docs)?;
        if let Some(index) = self.index {
            write!(f, "{} = {index},", self.name)?;
        }
        else {
            write!(f, "{},", self.name)?;
        }

        Ok(())
    }
}

/// Describes a method on a class or struct.
#[derive(Clone, Debug, PartialEq, Eq, PartialOrd, Ord)]
pub struct Method {
    /// The name of the method.
    pub name: String,
    /// `true` if this is an instance method.
    pub has_this: bool,
    /// The return type of the method (or [`None`] for `void`).
    pub return_ty: Option<TypeReference>,
    /// The parameters that will be passed to the method.
    pub parameters: Vec<MethodParameter>,
    /// The owning type name.
    pub ty: TypeName,
    /// The doc-comment to include.
    pub docs: String
}

impl Method {
    /// Gets the modified type name for the public C# API.
    pub fn cs_name(&self) -> String {
        self.name.to_case(Case::Pascal)
    }

    /// Whether this method represents a constructor.
    pub fn is_constructor(&self) -> bool {
        !self.has_this && self.return_ty.is_some() && self.name == "new"
    }

    /// Gets the modified type name for C FFI.
    pub fn rs_name(&self) -> String {
        format!("{}_{}", self.ty.rs_fn, self.name)
    }
}

impl DisplayBindings for Method {
    fn write_cs(&self, f: &mut Formatter) -> Result {
        let has_unknown = self.return_ty.as_ref().map(TypeReference::contains_unknown).unwrap_or(false)
            || self.parameters.iter().any(|x| x.ty.contains_unknown());
        
        if self.is_constructor() {
            write!(f, "public {}(", self.ty.cs)?;
        }
        else {
            let static_modifier = if self.has_this { "" } else { "static " };
            let return_ty = self.return_ty.as_ref().map(|x| format!("{}", DisplayCs(x))).unwrap_or("void".to_string());
            write!(f, "public {static_modifier}{return_ty} {}(", self.cs_name())?;
        }

        for (index, parameter) in self.parameters.iter().enumerate() {
            if 0 < index {
                write!(f, ", ")?;
            }

            write!(f, "{} {}", DisplayCs(&parameter.ty), parameter.cs_name())?;
        }

        write!(f, ") {{\n")?;

        let mut body = String::new();
        let mut body_f = Formatter::new(&mut body, f.options());
        write!(body_f, "Egui.{}(", self.rs_name())?;

        for (index, parameter) in self.parameters.iter().enumerate() {
            if 0 < index {
                write!(body_f, ", ")?;
            }

            write!(body_f, "TODO_ARG({})", parameter.cs_name())?;
        }

        write!(body_f, ");")?;

        write!(f, "{}\n", &indent(&body))?;
        write!(f, "}}\n")?;

        Ok(())
    }

    fn write_rs(&self, f: &mut Formatter) -> Result {
        todo!()
    }
}

/// Describes a parameter in a method.
#[derive(Clone, Debug, PartialEq, Eq, PartialOrd, Ord)]
pub struct MethodParameter {
    /// The name of the parameter.
    pub name: String,
    /// The type of the parameter.
    pub ty: TypeReference
}

impl MethodParameter {
    /// Gets the modified type name for the public C# API.
    pub fn cs_name(&self) -> String {
        self.name.to_case(Case::Camel)
    }

    /// Gets the modified type name for C FFI.
    pub fn rs_name(&self) -> String {
        self.name.to_string()
    }
}

/// Describes the field of a struct.
#[derive(Clone, Debug, PartialEq, Eq, PartialOrd, Ord)]
pub struct StructField {
    /// The name of the field.
    pub name: String,
    /// The type of the field.
    pub ty: TypeReference,
    /// The doc-comment to include.
    pub docs: String
}

impl StructField {
    /// Gets the modified type name for the public C# API.
    pub fn cs_name(&self) -> String {
        self.name.to_case(Case::Pascal)
    }

    /// Gets the modified type name for C FFI.
    pub fn rs_name(&self) -> String {
        self.name.to_string()
    }
}

impl DisplayBindings for StructField {
    fn write_cs(&self, f: &mut Formatter) -> Result {
        if self.ty.contains_unknown() {
            write!(f, "/* UNIMPLEMENTED (field) \n")?;
        }

        write_cs_docs(f, &self.docs)?;
        write!(f, "public {} {};\n", DisplayCs(&self.ty), self.cs_name())?;

        if self.ty.contains_unknown() {
            write!(f, "*/\n")?;
        }

        Ok(())
    }

    fn write_rs(&self, f: &mut Formatter) -> Result {
        if self.ty.contains_unknown() {
            write!(f, "/* UNIMPLEMENTED (field) \n")?;
        }

        write_rs_docs(f, &self.docs)?;
        write!(f, "pub {}: {},", self.rs_name(), DisplayRs(&self.ty))?;

        if self.ty.contains_unknown() {
            write!(f, "*/\n")?;
        }

        Ok(())
    }
}

/// Determines how a type is written in C# or Rust.
#[derive(Clone, Debug, PartialEq, Eq, PartialOrd, Ord)]
pub struct TypeName {
    /// The modified type name for the public C# API.
    pub cs: String,
    /// The modified type name for C FFI.
    pub rs: String,
    /// The modified type name that will be inserted before C FFI functions.
    pub rs_fn: String,
    /// The original full name of the type.
    pub original: String,
    /// The original simple name of the type (the last element in the path).
    pub simple: String
}

impl TypeName {
    /// Creates a type name from the original Rust path of the type.
    pub fn from_path(parts: &[String]) -> Self {
        let name = parts.last().expect("Path list was empty.");
        let cs = name.clone();
        let rs = "Egui".to_string() + name;
        let rs_fn = name.to_case(Case::Snake);
        let original = "::".to_string() + &parts.join("::");
        let simple = name.clone();

        Self {
            cs,
            rs,
            rs_fn,
            original,
            simple
        }
    }
}

/// Adds one level of indentation (four spaces) to every line
/// of the string.
fn indent(value: &str) -> String {
    if value.is_empty() {
        String::new()
    }
    else {
        "    ".to_string() + &value.trim_end().replace("\n", "\n    ") + "\n"
    }
}

/// Removes all code examples from the given documentation string.
fn strip_code_comments(docs: &str) -> String {
    let mut result = Cow::Borrowed(docs);
    while let Some(index) = result.find("```") {
        let base_offset = index + "```".len();
        if let Some(remaining) = result[base_offset..].find("```") {
            result = Cow::Owned(result[..index].trim_end().to_owned() + result[base_offset + remaining + "```".len()..].trim_start());
        }
        else {
            break;
        }
    }
    
    result.trim().to_string()
}

/// Removes all links from the given documentation string.
fn strip_links(docs: &str) -> String {
    let mut result = Cow::Borrowed(docs);
    while let Some(index) = result.find("[") {
        let base_offset = index + "[".len();
        if let Some(remaining) = result[base_offset..].find("]") {
            let inner_text = result[base_offset..base_offset + remaining].to_string();
            let rest_begin = base_offset + remaining + "]".len();
            let final_offset = if result[rest_begin..].starts_with("(") {
                rest_begin + result[rest_begin..].find(")").expect("Malformatted documentation link") + ")".len()
            }
            else {
                rest_begin
            };

            result = Cow::Owned(result[..index].to_owned() + &inner_text + &result[final_offset..]);
        }
        else {
            break;
        }
    }
    
    result.trim().to_string()
}

/// Removes everything before the last instance of `ending` in `value`.
fn strip_prefix_with_ending(value: &str, ending: &str) -> String {
    let mut start_pos = 0;
    while let Some(prefix) = value[start_pos..].find(ending) {
        start_pos = start_pos + prefix + ending.len();
    }
    value[start_pos..].to_string()
}

/// Converts all inline code snippets to their C# equivalent.
fn inline_code_to_cs(docs: &str) -> String {
    let mut result = Cow::Borrowed(docs);
    while let Some(index) = result.find("`") {
        let base_offset = index + "`".len();
        if let Some(remaining) = result[base_offset..].find("`") {
            let mut end_offset = base_offset + remaining + "`".len();
            //let start_offset = result[base_offset..end_offset].

            result = Cow::Owned(result[..index].to_owned()
                + "<c>" + &strip_prefix_with_ending(&result[base_offset..base_offset + remaining], "::").to_case(Case::Pascal) + "</c>"
                + &result[end_offset..]);
        }
        else {
            break;
        }
    }
    
    result.trim().to_string()
}

/// Writes a C# summary doc-comment.
fn write_cs_docs(f: &mut Formatter, docs: &str) -> Result {
    if !docs.is_empty() {
        write!(f, "/// <summary>\n")?;
        write!(f, "/// {}\n", inline_code_to_cs(&strip_links(&strip_code_comments(docs.trim_end()))).replace("\n", "\n/// "))?;
        write!(f, "/// </summary>\n")?;
    }
    Ok(())
}

/// Writes a Rust doc-comment.
fn write_rs_docs(f: &mut Formatter, docs: &str) -> Result {
    if !docs.is_empty() {
        write!(f, "/// {}\n", docs.trim_end().replace("\n", "\n/// "))?;
    }

    Ok(())
}