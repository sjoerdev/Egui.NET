#![feature(formatting_options)]

use convert_case::*;
use egui::*;
use egui::epaint::*;
use egui::Id;
use egui::os::*;
use egui::output::*;
use egui::panel::*;
use egui::scroll_area::*;
use egui::style::*;
use egui::text::*;
use egui::text_edit::*;
use egui::util::undoer::*;
use rustdoc_types::*;
use rustdoc_types::Id as RdId;
use rustdoc_types::Path as RdPath;
use serde_generate::*;
use serde_generate::csharp::*;
use serde_reflection::*;
use std::borrow::*;
use std::collections::HashMap;
use std::path::*;

/// Functions to exclude when automatically generating bindings.
const BINDING_EXCLUDE_FNS: &[&str] = &[
    "egui_containers_frame_Frame_corner_radius",
    "egui_containers_frame_Frame_stroke",
    "egui_containers_frame_Frame_shadow",
    "egui_data_input_RawInput_viewport",
    "egui_style_Visuals_window_stroke",
    "egui_containers_frame_Frame_inner_margin",
    "egui_style_Visuals_window_fill",
    "egui_style_ScrollAnimation_duration",
    "egui_style_Style_noninteractive",
    "egui_data_output_WidgetInfo_selected",
    "egui_style_ScrollStyle_floating",
    "egui_containers_frame_Frame_outer_margin",

    "egui_input_state_InputState_begin_pass",
    "egui_input_state_InputState_viewport",
    "egui_layout_Layout_align_size_within_rect",
    
    "egui_style_Style_text_styles",
    "egui_style_Visuals_noninteractive",
    "egui_data_output_OpenUrl_new_tab",
    "egui_text_selection_cursor_range_CursorRange_on_event",

    "egui_ui_stack_UiStack_has_visible_frame",
    "egui_ui_stack_UiStack_is_area_ui",
    "egui_ui_stack_UiStack_is_root_ui",
    "egui_ui_stack_UiStack_is_panel_ui",
    "egui_ui_stack_UiStack_contained_in",
    "egui_ui_stack_UiStack_frame",

    "egui_viewport_ViewportIdPair_from_self_and_parent",
];

/// Types to exclude from generation.
const BINDING_EXCLUDE_TYPES: &[&str] = &[
    "History",
    "Sense"
];

/// Types for which fields/serialization logic should not be generated.
const BINDING_EXCLUDE_TYPE_DEFINITIONS: &[&str] = &[
    "UiStack"
];

/// A list of fully-qualified function IDs to ignore during generation.
const IGNORE_FNS: &[&str] = &[
    // Random extra stuff which we don't want that got lumped into documentation
    "alloc_borrow_Cow_as_str",
    "alloc_borrow_Cow_clear",
    "alloc_borrow_Cow_delete_char_range",
    "alloc_borrow_Cow_insert_text",
    "alloc_borrow_Cow_is_mutable",
    "alloc_borrow_Cow_replace_with",
    "alloc_borrow_Cow_take",
    "alloc_string_String_as_str",
    "alloc_string_String_clear",
    "alloc_string_String_delete_char_range",
    "alloc_string_String_insert_text",
    "alloc_string_String_is_mutable",
    "alloc_string_String_replace_with",
    "alloc_string_String_take",
    "alloc_borrow_Cow_type_id",
    "alloc_string_String_type_id",
    "ab_glyph_outlined_Outline",
    "ab_glyph_outlined_OutlineCurve",
    "ahash_random_state_RandomState",
    "alloc_collections_btree_map_Keys",
    "alloc_collections_btree_map_ValuesMut",
    "alloc_collections_btree_map_drop_DropGuard",
    "alloc_vec_drain_drop_DropGuard",
    "core_cell_BorrowRefMut",
    "core_char_decode_DecodeUtf16",
    "core_convert_num_FloatToInt",
    "core_core_arch_simd_u32x4",
    "core_core_arch_simd_u64x4",
    "core_core_arch_x86___m128i",
    "core_core_simd_simd_num_int_SimdInt",
    "core_error_Source",
    "core_fmt_UpperExp",
    "core_iter_adapters_map_while_MapWhile",
    "core_num_nonzero_NonZeroU128_from_f64",
    "core_num_nonzero_NonZeroU128_to_f64",
    "core_num_nonzero_NonZeroU16_from_f64",
    "core_num_nonzero_NonZeroU16_to_f64",
    "core_num_nonzero_NonZeroU32_from_f64",
    "core_num_nonzero_NonZeroU32_to_f64",
    "core_num_nonzero_NonZeroU64_from_f64",
    "core_num_nonzero_NonZeroU64_to_f64",
    "core_num_nonzero_NonZeroU8_from_f64",
    "core_num_nonzero_NonZeroU8_to_f64",
    "core_num_nonzero_NonZeroUsize_from_f64",
    "core_num_nonzero_NonZeroUsize_to_f64",
    "core_pat_RangePattern",
    "core_ptr_unique_Unique",
    "core_slice_iter_RSplitMut",
    "core_str_iter_EscapeDefault",
    "core_str_iter_MatchIndicesInternal",
    "core_sync_atomic_AtomicU16",
    "core_task_wake_LocalWaker",
    "core_core_arch_x86_splat_JustOne",
    "core_core_arch_simd_splat_JustOne",
    "ab_glyph_font_arc_FontArc",
    "alloc_alloc_Global",
    "alloc_alloc_alloc",
    "alloc_collections_btree_set_Range",
    "alloc_collections_vec_deque_iter_mut_IterMut",
    "compiler_builtins_int_big_u256",
    "compiler_builtins_math_libm_support_env_Round",
    "core_array_iter_IntoIter",
    "core_clone_uninit_InitializingSlice",
    "core_core_arch_simd_i32x32",
    "core_core_simd_simd_ptr_mut_ptr_SimdMutPtr",
    "core_error_private_Internal",
    "core_future_ready_Ready",
    "core_iter_adapters_map_Map",
    "core_iter_adapters_map_windows_Buffer",
    "core_iter_adapters_skip_Skip",
    "core_marker_ConstParamTy_",
    "core_marker_PhantomData",
    "core_num_fmt_Formatted",
    "core_ops_arith_Neg",
    "core_ops_async_function_AsyncFnOnce",
    "core_ops_try_trait_Try",
    "core_ptr_with_exposed_provenance",
    "core_slice_iter_ChunksExactMut",
    "core_slice_iter_RSplit",
    "core_sync_atomic_AtomicU32",
    "core_sync_exclusive_Exclusive",
    "hashbrown_raw_RawTable",
    "lock_api_rwlock_RawRwLockRecursive",
    "once_cell",
    "rustc_demangle_TryDemangleError",
    "serde___private_de_StrDeserializer",
    "serde___private_ser_FlatMapSerializeTupleVariantAsMapValue",
    "serde_de_impls_deserialize_BoundVisitor",
    "serde_de_impls_deserialize_SocketAddrKind",
    "std_backtrace_rs_dbghelp_Init",
    "std_backtrace_rs_windows_sys_CONTEXT_0_0",
    "std_backtrace_rs_windows_sys_KNONVOLATILE_CONTEXT_POINTERS_1",
    "std_backtrace_rs_windows_sys_STACKFRAME64",
    "std_backtrace_rs_windows_sys_STACKFRAME_EX",
    "std_backtrace_rs_windows_sys_SYMBOL_INFOW",
    "std_collections_hash_map_OccupiedEntry",
    "std_collections_hash_map_VacantEntry",
    "std_collections_hash_set_Intersection",
    "std_f16",
    "std_io_stdio_StdinRaw",
    "std_panic_PanicHookInfo",
    "std_process_ChildStdin",
    "std_sys_net_connection_socket_TcpListener",
    "std_sys_pal_windows_c_windows_sys_CONSOLE_READCONSOLE_CONTROL",
    "std_sys_pal_windows_c_windows_sys_SYSTEM_INFO_0_0",
    "std_thread_local_AccessError",
    "ttf_parser_NormalizedCoordinate",
    "ttf_parser_ggg_feature_variations_FeatureVariations",
    "ttf_parser_parser_LazyArrayIter16",
    "ttf_parser_parser_Stream",
    "ttf_parser_tables_cff_encoding_Supplement",
    "ttf_parser_tables_feat_FeatureNames",
    "ttf_parser_tables_kerx_Subtable1",
    "ttf_parser_tables_morx_Chain",
    "ttf_parser_tables_morx_Coverage",
    "ttf_parser_tables_svg_SvgDocumentsList",
    "ttf_parser_tables_trak_TrackTableRecord",
    "zerocopy_AsBytes",
    "ab_glyph_font_Font",
    "alloc_collections_btree_append_MergeIter",
    "alloc_collections_btree_map_RangeMut",
    "alloc_collections_btree_set_Iter",
    "alloc_collections_btree_set_val_SetValZST",
    "alloc_ffi_c_str_NulError",
    "alloc_vec",
    "alloc_vec_drain_Drain",
    "bitflags_parser_ParseErrorKind",
    "compiler_builtins_math_libm_fma_Norm",
    "core_core_arch_simd_i8x32",
    "core_core_arch_simd_m16x16",
    "core_core_arch_simd_u32x2",
    "core_fmt_Arguments",
    "core_marker_UnsizedConstParamTy",
    "core_net_ip_addr_Ipv6MulticastScope",
    "core_ops_bit_Shl",
    "core_ops_range_Bound",
    "core_ops_range_RangeBounds",
    "core_ptr_without_provenance",
    "core_slice_iter_SplitInclusiveMut",
    "core_str_BytesIsNotEmpty",
    "core_str_pattern_MultiCharEqSearcher",
    "core_str_pattern_SearchStep",

    // Sense: these functions are disabled since Sense is implemented as a C# flags enum
    // with builtin bitwise operations
    "egui_sense_Sense_all",
    "egui_sense_Sense_bitand",
    "egui_sense_Sense_bitand_assign",
    "egui_sense_Sense_bitor",
    "egui_sense_Sense_bitor_assign",
    "egui_sense_Sense_bitxor",
    "egui_sense_Sense_bitxor_assign",
    "egui_sense_Sense_click",
    "egui_sense_Sense_click_and_drag",
    "egui_sense_Sense_clone",
    "egui_sense_Sense_complement",
    "egui_sense_Sense_contains",
    "egui_sense_Sense_difference",
    "egui_sense_Sense_drag",
    "egui_sense_Sense_empty",
    "egui_sense_Sense_extend",
    "egui_sense_Sense_focusable_noninteractive",
    "egui_sense_Sense_from_bits_truncate",
    "egui_sense_Sense_from_iter",
    "egui_sense_Sense_from_name",
    "egui_sense_Sense_hover",
    "egui_sense_Sense_insert",
    "egui_sense_Sense_intersection",
    "egui_sense_Sense_intersects",
    "egui_sense_Sense_into_iter",
    "egui_sense_Sense_is_all",
    "egui_sense_Sense_is_empty",
    "egui_sense_Sense_iter",
    "egui_sense_Sense_iter_names",
    "egui_sense_Sense_not",
    "egui_sense_Sense_remove",
    "egui_sense_Sense_set",
    "egui_sense_Sense_sub",
    "egui_sense_Sense_sub_assign",
    "egui_sense_Sense_symmetric_difference",
    "egui_sense_Sense_toggle",
    "egui_sense_Sense_union",

    "egui___run_test_ctx",
    "egui___run_test_ui",

    "serde_de_impls_deserialize_NonZeroVisitor",
    "serde_de_impls_deserialize_in_place_TupleInPlaceVisitor"
];

/// Function names to be ignored during generation.
const IGNORE_FN_NAMES: &[&str] = &[
    "add",
    "add_assign",
    "bits",
    "cmp",
    "deserialize",
    "div",
    "div_assign",
    "eq",
    "hash",
    "fmt",
    "from",
    "from_bits",
    "from_bits_retain",
    "index",
    "index_mut",
    "mul",
    "mul_assign",
    "partial_cmp",
    "serialize",
    "sub",
    "sub_assign",
    "value"
];

include!(concat!(env!("OUT_DIR"), "/tracer.rs"));

/// Holds context for use during bindings generation.
pub struct BindingsGenerator {
    /// Metadata about the crate being generated.
    krate: Crate,
    /// The output path to which data will be written.
    output_path: PathBuf,
    /// A registry of reflected information about `egui` types.
    registry: Registry
}

impl BindingsGenerator {
    /// Creates autogenerated bindings for all `serde` types.
    pub fn generate(path: &std::path::Path) {
        let mut krate = serde_json::from_str::<Crate>(include_str!("egui.json")).expect("Failed to parse egui");
        Self::merge_crates(&mut krate, &serde_json::from_str::<Crate>(include_str!("emath.json")).expect("Failed to parse emath"));
        Self::merge_crates(&mut krate, &serde_json::from_str::<Crate>(include_str!("epaint.json")).expect("Failed to parse epaint"));

        BindingsGenerator {
            krate,
            output_path: path.to_path_buf(),
            registry: Self::trace_serde_types()
        }.run()
    }

    /// Executes the bindings generator.
    fn run(mut self) {
        let path_to_clear = self.output_path.join("Egui");
        let _ = std::fs::remove_dir_all(path_to_clear);
        let _ = std::fs::create_dir_all(&self.output_path);

        let config = CodeGeneratorConfig::new("Egui".to_string())
            .with_serialization(true)
            .with_c_style_enums(true)
            .with_comments(self.gather_doc_comments());
        let generator = CodeGenerator::new(&config);

        self.emit_cs_fn_bindings();
        self.rename_struct_fields();
        self.remove_excluded_serialized_tys();
        
        generator.write_source_files(self.output_path, &self.registry).expect("Failed to write source files");
    }

    /// Removes types from the registry that should be excluded from the output.
    fn remove_excluded_serialized_tys(&mut self) {
        for ty in BINDING_EXCLUDE_TYPE_DEFINITIONS {
            let _ = self.registry.remove(*ty);
        }
    }

    /// Gets the C# name for a type, or returns [`None`] if the type
    /// could not be resolved.
    fn cs_type_name(&self, self_ty: Option<&str>, ty: &Type) -> Option<String> {
        Some(match ty {
            Type::ResolvedPath(path) => {
                let name = path.path.split("::").last().expect("Type was empty");
                if self.registry.contains_key(name) {
                    name.to_string()
                }
                else {
                    return None;
                }
            },
            Type::Generic(x) if x == "Self" => self_ty?.to_string(),
            Type::Primitive(x) => Self::cs_primitive_name(&x)?.to_string(),
            Type::Tuple(items) => format!("ValueTuple<{}>",
                items.iter().map(|x| self.cs_type_name(self_ty, x)).collect::<Option<Vec<_>>>()?.join(", ")),
            Type::Slice(type_)
            | Type::Array { type_, .. } => format!("ImmutableList<{}>", self.cs_type_name(self_ty, &type_)?),
            Type::BorrowedRef { is_mutable: false, type_, .. } => self.cs_type_name(self_ty, &type_)?,
            Type::DynTrait(_)
            | Type::Generic(_)
            | Type::ImplTrait(_)
            | Type::Pat { .. }
            | Type::Infer
            | Type::RawPointer { .. }
            | Type::QualifiedPath { .. }
            | Type::BorrowedRef { is_mutable: true, .. }
            | Type::FunctionPointer(_) => return None,
        })
    }

    /// Determines whether the type with the given name has a particular field.
    fn ty_has_field(&self, ty_name: &str, field: &str) -> bool {
        if let Some(ContainerFormat::Struct(fields)) = self.registry.get(ty_name) {
            if fields.iter().find(|x| x.name == field).is_some() {
                return true;
            }
        }

        false
    }

    /// Emits a C# file containing bindings for `egui` functions.
    fn emit_cs_fn_bindings(&self) {
        let mut result = String::new();

        result += "#pragma warning disable\n";
        result += "using System.Collections.Immutable;\n";
        result += "namespace Egui;\n";

        let mut bound_ids = Vec::new();
        for id in self.gather_fns() {
            if BINDING_EXCLUDE_FNS.contains(&&*self.fn_enum_variant_name(id)) {
                continue;
            }

            let result = self.emit_cs_fn_binding(&mut std::fmt::Formatter::new(&mut result, Default::default()), id);
            if result.is_ok() {
                bound_ids.push(id);
            }
        }

        self.emit_fn_enum(&bound_ids);
        std::fs::write(self.output_path.join("EguiFn.g.cs"), result).expect("Failed to write C# function bindings");
    }

    /// Writes a single C# method definition (with appropriate type qualifiers) to `f`.
    fn emit_cs_fn_binding(&self, f: &mut std::fmt::Formatter, id: RdId) -> std::fmt::Result {
        if let Some(impl_ty) = self.declaring_type(id).and_then(|x| self.krate.index.get(&x)) {
            if matches!(impl_ty.inner, ItemEnum::Struct(_)) {
                if let Some(ty_name) = impl_ty.name.as_deref() {
                    if self.registry.contains_key(ty_name) {
                        let mut fn_def = String::new();
                        self.emit_cs_fn(&mut std::fmt::Formatter::new(&mut fn_def, Default::default()), Some(ty_name), id)?;
                        writeln!(f, "public partial struct {ty_name} {{\n{fn_def}\n}}")?;
                        return Ok(());
                    }
                }
            }
        }

        Err(std::fmt::Error)
    }

    /// Writes a single C# method definition to `f`.
    fn emit_cs_fn(&self, f: &mut std::fmt::Formatter, ty_name: Option<&str>, id: RdId) -> std::fmt::Result {
        let item = &self.krate.index[&id];
        let ItemEnum::Function(func) = &item.inner else { panic!("Expected id to refer to a function") };

        let has_this = func.sig.inputs.first().map(|(name, ty)| name == "self" && !matches!(ty, Type::BorrowedRef { is_mutable: true, .. })).unwrap_or_default();
        let returns_this = func.sig.output.as_ref().map(|x| x == &Type::Generic("Self".to_string())).unwrap_or_default();
        let original_name = item.name.as_deref().expect("Failed to get function name");
        let cs_name = if ty_name.map(|x| self.ty_has_field(x, original_name)).unwrap_or_default() {
            format!("with_{original_name}").to_case(Case::Pascal)
        }
        else {
            original_name.to_case(Case::Pascal)
        };

        if let Some(comment) = self.get_doc_comment(id) {
            writeln!(f, "/// {}", comment.replace("\n", "\n/// "))?;
        }

        if !has_this && returns_this && (cs_name == "New" || cs_name == "Default") {
            write!(f, "public {}", ty_name.expect("Expected type to be provided"))?;
            self.emit_cs_fn_def(f, ty_name, id, FnType::Constructor, &func.sig)?;
        }
        else {
            let return_name = func.sig.output.as_ref().and_then(|x| self.cs_type_name(ty_name, x))
                .unwrap_or("void".to_string());
            
            write!(f, "public {} {return_name} {}", ["static", "readonly"][has_this as usize], cs_name)?;

            self.emit_cs_fn_def(f, ty_name, id, [FnType::Static, FnType::Instance][has_this as usize], &func.sig)?;
        }

        Ok(())
    }

    /// Emits the body of a C# function (excluding the name and return type).
    fn emit_cs_fn_def(&self, f: &mut std::fmt::Formatter, ty_name: Option<&str>, id: RdId, ty: FnType, sig: &FunctionSignature) -> std::fmt::Result {
        write!(f, "(")?;
        
        let mut first = true;

        let mut generic_args = Vec::new();
        
        for (name, ty) in sig.inputs.iter().skip((ty == FnType::Instance) as usize) {
            if !first {
                write!(f, ", ")?;
            }

            first = false;

            let param_ty = self.cs_type_name(ty_name, ty).ok_or(std::fmt::Error)?;
            generic_args.push(param_ty.clone());
            let cs_name = name.to_case(Case::Camel);
            write!(f, "{param_ty} {cs_name}")?;
        }
        
        writeln!(f, ") {{")?;

        if let Some(return_ty) = &sig.output {
            generic_args.push(self.cs_type_name(ty_name, return_ty).ok_or(std::fmt::Error)?);

            if ty == FnType::Constructor {
                write!(f, "   this = ")?;
            }
            else {
                write!(f, "   return ")?;
            }
        }
        else {
            write!(f, "    ")?;
        }

        if generic_args.is_empty() {
            writeln!(f, "EguiMarshal.Call(")?;
        }
        else {
            writeln!(f, "EguiMarshal.Call<{}>(", generic_args.join(", "))?;
        }

        let enum_name = format!("EguiFn.{}", self.fn_enum_variant_name(id));
        write!(f, "{}", [enum_name].into_iter().chain(sig.inputs.iter().skip((ty == FnType::Instance) as usize).map(|(name, _)| name.to_case(Case::Camel)))
            .collect::<Vec<_>>().join(", "))?;
        writeln!(f, ");")?;

        writeln!(f, "}}")?;
        Ok(())
    }

    /// Emits a registry of all functions that have been bound.
    /// Returns a 
    fn emit_fn_enum_bindings(&self, f: &mut std::fmt::Formatter, bound_ids: &[RdId]) -> std::fmt::Result {
        writeln!(f, "const AUTOGENERATED_EGUI_FNS: EguiFnMap = egui_fn_map()")?;

        for id in bound_ids {
            let ItemEnum::Function(func) = &self.krate.index[id].inner else { panic!("Expected function items only") };
            let cast_params = func.sig.inputs.iter().map(|(_, ty)| if matches!(ty, Type::BorrowedRef { .. }) { "&_" } else { "_" })
                .collect::<Vec<_>>().join(", ");
            let return_ty = if matches!(func.sig.output, Some(Type::BorrowedRef { .. })) { "_" } else { "_" };
            let enum_name = self.fn_enum_variant_name(*id);
            let path = self.fn_enum_path(*id);
            writeln!(f, "    .with(EguiFn::{enum_name}, {path} as fn({cast_params}) -> {return_ty})")?;
        }
        writeln!(f, ";")?;
        Ok(())
    }

    /// Emits an `enum` containing the names of all public `egui` functions.
    fn emit_fn_enum(&self, bound_ids: &[RdId]) {
        let variants = self.fn_enum_variant_names();
        let mut result = String::new();
        
        result += "#[allow(warnings)]\n";
        result += "#[derive(Clone, Copy, Debug)]\n";
        result += "#[repr(C)]\n";
        result += "pub enum EguiFn {\n    ";
        result += &variants.join(",\n    ").trim();
        result += "}\n";

        result += "impl EguiFn {\n";
        result += "    /// All enum variants.\n";
        result += "    pub const ALL: &[Self] = &[\n    ";
        result += &variants.iter().map(|x| "Self::".to_string() + x).collect::<Vec<_>>().join(",\n    ").trim();
        result += "    ];\n";
        result += "}\n";

        self.emit_fn_enum_bindings(&mut std::fmt::Formatter::new(&mut result, Default::default()), bound_ids)
            .expect("Failed to emit function enum bindings");

        std::fs::write(self.output_path.join("egui_fn.rs"), result).expect("Failed to write egui function enum");
    }

    /// Gets the variant names for an `enum` containing all public `egui` functions.
    fn fn_enum_variant_names(&self) -> Vec<String> {
        let mut result = self.gather_fns().into_iter().map(|id| self.fn_enum_variant_name(id)).collect::<Vec<_>>();
        result.sort();
        result
    }

    /// Gets the `EguiFn` variant name of `id`.
    fn fn_enum_variant_name(&self, id: RdId) -> String {
        if let Some(impl_ty) = self.declaring_type(id) {
            format!("{}_{}", self.krate.paths[&impl_ty].path.join("_"), self.krate.index[&id].name.clone().unwrap_or_default())
        }
        else if self.krate.paths.contains_key(&id) {
            format!("{}", self.krate.paths[&id].path.join("_"))
        }
        else {
            panic!("Item was not a bindable egui function")
        }
    }

    /// Gets the Rust path of a function with `id`.
    fn fn_enum_path(&self, id: RdId) -> String {
        if let Some(impl_ty) = self.declaring_type(id) {
            format!("{}::{}", self.krate.paths[&impl_ty].path.last().expect("Couldn't find type name"), self.krate.index[&id].name.clone().unwrap_or_default())
        }
        else if self.krate.paths.contains_key(&id) {
            self.krate.index[&id].name.clone().unwrap_or_default()
        }
        else {
            panic!("Item was not a bindable egui function")
        }
    }

    /// Gets all doc-comments to emit for types and fields.
    fn gather_doc_comments(&self) -> DocComments {
        let mut result = DocComments::default();
        for name in self.registry.keys() {
            if let Some(id) = self.get_type_id(name) {
                if let Some(docs) = self.get_doc_comment(id) {
                    result.insert(vec!["Egui".to_string(), name.clone()], docs);
                }

                let fields = match &self.krate.index[&id].inner {
                    ItemEnum::Struct(Struct { kind: StructKind::Plain { fields, .. }, .. }) => &**fields,
                    ItemEnum::Enum(Enum { variants, .. }) => &**variants,
                    _ => &[]
                };

                for field in fields {
                    let field_name = self.krate.index[field].name.clone().unwrap_or_default();
                    if let Some(docs) = self.get_doc_comment(*field) {
                        result.insert(vec!["Egui".to_string(), name.clone(), field_name.to_case(Case::Pascal)], docs);
                    }
                }
            }
        }
        result
    }

    /// Gets a list of all functions that should be bound for `egui`.
    fn gather_fns(&self) -> Vec<RdId> {
        self.krate.index.iter()
            .filter_map(|(id, item)| (
                item.crate_id == 0
                && matches!(item.inner, ItemEnum::Function(_))
                && (self.declaring_type(*id).is_some() || self.krate.paths.contains_key(id))
                && {
                    let variant_name = self.fn_enum_variant_name(*id);
                    variant_name.starts_with("e") &&!IGNORE_FNS.contains(&&*variant_name)
                }
                && item.name.as_deref().map(|x| !IGNORE_FN_NAMES.contains(&x)).unwrap_or(true)
            ).then_some(id.clone()))
            .collect()
    }

    /// Gets the ID of the type that declares the given function.
    fn declaring_type(&self, fn_id: RdId) -> Option<RdId> {
        self.krate.index.values()
            .filter_map(|item| if let ItemEnum::Impl(Impl { for_: Type::ResolvedPath(p), items, .. }) = &item.inner {
                items.contains(&fn_id).then_some(p.id)
            }
            else {
                None
            })
            .next()
    }
    
    /// Renames all fields in the registry from Rust to C# casing.
    fn rename_struct_fields(&mut self) {
        for item in self.registry.values_mut() {
            match item {
                ContainerFormat::Struct(nameds) => for field in nameds {
                    field.name = field.name.to_case(Case::Pascal);
                },
                _ => {},
            }
        }
    }
    
    /// Gets the C# doc-comment to use for an item given its ID.
    fn get_doc_comment(&self, id: RdId) -> Option<String> {
        let docs = self.krate.index[&id].docs.clone().unwrap_or_default();
        let converted_docs = Self::inline_code_to_cs(&Self::strip_links(&Self::strip_code_comments(docs.trim_end())));
        if converted_docs.is_empty() {
            None
        }
        else {
            Some(format!("<summary>\n{converted_docs}\n</summary>"))
        }
    }
    
    /// Gets the `rustdoc` ID for a type given its name.
    fn get_type_id(&self, name: &str) -> Option<RdId> {
        self.krate.index.iter()
            .filter_map(|(id, item)| (matches!(item.inner, ItemEnum::Enum(_) | ItemEnum::Struct(_)) && item.name.as_deref() == Some(name)).then_some(id.clone()))
            .next()
    }

    /// Performs reflection on `egui` types to determine fields.
    fn trace_serde_types() -> Registry {
        let mut samples = Samples::new();
        let mut tracer = Tracer::new(TracerConfig::default()
            .default_u64_value(1)
            .record_samples_for_newtype_structs(true)
            .record_samples_for_tuple_structs(true)
            .record_samples_for_structs(true));


        tracer.trace_value(&mut samples, &Options::default()).expect("Failed to trace Options");
        
        tracer.trace_type::<AlphaFromCoverage>(&samples).expect("Failed to trace AlphaFromCoverage");
        tracer.trace_type::<TextureId>(&samples).expect("Failed to trace AlphaFromCoverage");
        tracer.trace_simple_type::<Align>().expect("Failed to trace Align");
        tracer.trace_simple_type::<FontFamily>().expect("Failed to trace FontFamily");
        tracer.trace_simple_type::<TextWrapMode>().expect("Failed to trace TextWrapMode");
        tracer.trace_simple_type::<PinchType>().expect("Failed to trace PinchType");
        tracer.trace_simple_type::<PointerEvent>().expect("Failed to trace PointerEvent");
        tracer.trace_simple_type::<ProgressBarText>().expect("Failed to trace ProgressBarText");
        
        trace_auto_serde_types(&mut tracer);

        let mut result = tracer.registry().expect("Failed to generate serde registry");

        for ty in BINDING_EXCLUDE_TYPES {
            let _ = result.remove(*ty);
        }

        result
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
                let end_offset = base_offset + remaining + "`".len();

                result = Cow::Owned(result[..index].to_owned()
                    + "<c>" + &Self::strip_prefix_with_ending(&result[base_offset..base_offset + remaining], "::").to_case(Case::Pascal) + "</c>"
                    + &result[end_offset..]);
            }
            else {
                break;
            }
        }
        
        result.trim().to_string()
    }

    /// Converts the given Rust primitive to a C# primitive, or returns
    /// [`None`] if the primitive was not recognized.
    fn cs_primitive_name(x: &str) -> Option<&'static str> {
        Some(match x {
            "bool" => "bool",
            "u8" => "byte",
            "u16" => "ushort",
            "u32" => "uint",
            "u64" => "ulong",
            "i8" => "sbyte",
            "i16" => "short",
            "i32" => "int",
            "i64" => "long",
            "f32" => "float",
            "f64" => "double",
            "isize" => "nint",
            "usize" => "nuint",
            _ => return None
        })
    }

    /// Combines two crate documentation objects into one.
    fn merge_crates(target: &mut Crate, source: &Crate) {
        assert!(!target.includes_private);
        assert!(!source.includes_private);

        let mut remapper = CrateIdRemapper::new(target);

        for (old_id, item) in source.index.iter().map(|(a, b)| (*a, b.clone())) {
            let new_id = remapper.map(old_id);
            target.index.insert(new_id, Item {
                id: new_id,
                links: item.links.into_iter().map(|(k, v)| (k, remapper.map(v))).collect(),
                inner: match item.inner {
                    ItemEnum::Enum(x) => ItemEnum::Enum(Enum {
                        variants: x.variants.into_iter().map(|x| remapper.map(x)).collect(),
                        ..x
                    }),
                    ItemEnum::Struct(x) => ItemEnum::Struct(Struct {
                        kind: match x.kind {
                            StructKind::Unit => StructKind::Unit,
                            StructKind::Tuple(ids) => StructKind::Tuple(ids.into_iter().map(|x| x.map(|y| remapper.map(y))).collect()),
                            StructKind::Plain { fields, has_stripped_fields } => StructKind::Plain { fields: fields.into_iter().map(|x| remapper.map(x)).collect(), has_stripped_fields },
                        },
                        ..x
                    }),
                    ItemEnum::Function(function) => ItemEnum::Function(Function {
                        sig: FunctionSignature {
                            inputs: function.sig.inputs.into_iter().map(|(k, v)| (k, remapper.map_type(v))).collect(),
                            output: function.sig.output.map(|x| remapper.map_type(x)),
                            is_c_variadic: function.sig.is_c_variadic
                        },
                        ..function
                    }),
                    ItemEnum::Impl(x) => ItemEnum::Impl(Impl {
                        for_: remapper.map_type(x.for_),
                        trait_: x.trait_.map(|x| RdPath { id: remapper.map(x.id), ..x }),
                        items: x.items.iter().map(|x| remapper.map(*x)).collect(),
                        ..x
                    }),
                    x => x
                },
                ..item
            });
        }

        for (old_id, path) in source.paths.iter().map(|(a, b)| (*a, b.clone())) {
            let new_id = remapper.map(old_id);
            target.paths.insert(new_id, path);
        }
    }
}

/// How to emit a particular function.
#[derive(Copy, Clone, Debug, PartialEq, Eq)]
enum FnType {
    /// The function should be treated as a constructor.
    Constructor,
    /// The function should be treated as an instance method.
    Instance,
    /// The function should be treated as a static method.
    Static
}

/// Facilitates assigning new IDs to items.
struct CrateIdRemapper {
    /// The next available ID.
    next_id: u32,
    /// A map from old IDs to new IDs.
    remappings: HashMap<RdId, RdId>
}

impl CrateIdRemapper {
    /// Creates a new remapper.
    pub fn new(target: &Crate) -> Self {
        let next_id = target.index.keys().map(|x| x.0).max().unwrap_or_default() + 1;
        Self {
            next_id,
            remappings: HashMap::default()
        }
    }

    /// Maps an old ID to a new ID.
    pub fn map(&mut self, id: RdId) -> RdId {
        *self.remappings.entry(id).or_insert_with(|| {
            let result = Id(self.next_id);
            self.next_id += 1;
            result
        })
    }

    /// Maps all relevant IDs within `ty` to new ones.
    pub fn map_type(&mut self, ty: Type) -> Type {
        match ty {
            Type::ResolvedPath(path) => Type::ResolvedPath(RdPath {
                id: self.map(path.id),
                ..path
            }),
            x => x,
        }
    }
}