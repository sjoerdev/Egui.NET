use convert_case::*;
use egui::*;
use egui::output::*;
use egui::panel::*;
use egui::scroll_area::*;
use egui::style::*;
use egui::text::*;
use egui::text_edit::*;
use egui::text_selection::*;
use egui::util::undoer::*;
use serde_generate::*;
use serde_generate::csharp::*;
use serde_reflection::*;
use std::path::*;

include!(concat!(env!("OUT_DIR"), "/tracer.rs"));

pub fn generate(path: &Path) {
    let mut registry = trace_serde_types();
    rename_struct_fields(&mut registry);
    
    let config = CodeGeneratorConfig::new("Egui".to_string())
        .with_serialization(true)
        .with_c_style_enums(true);
    let generator = CodeGenerator::new(&config);
    
    let path_to_clear = path.join("Egui");
    let _ = std::fs::remove_dir_all(path_to_clear);
    generator.write_source_files(path.to_path_buf(), &registry).expect("Failed to write source files");
}

fn trace_serde_types() -> Registry {
    let mut samples = Samples::new();
    let mut tracer = Tracer::new(TracerConfig::default()
        .default_u64_value(1)
        .record_samples_for_newtype_structs(true)
        .record_samples_for_tuple_structs(true)
        .record_samples_for_structs(true));

    trace_auto_serde_types(&mut tracer);

    tracer.trace_value(&mut samples, &Options::default()).expect("Failed to trace Options");
    tracer.trace_value(&mut samples, &UserData::default()).expect("Failed to trace UserData");
    
    tracer.trace_simple_type::<Align>().expect("Failed to trace Align");
    tracer.trace_simple_type::<FontFamily>().expect("Failed to trace FontFamily");
    tracer.trace_simple_type::<TextWrapMode>().expect("Failed to trace TextWrapMode");

    tracer.registry().expect("Failed to generate serde registry")
}

fn rename_struct_fields(registry: &mut Registry) {
    for item in registry.values_mut() {
        match item {
            ContainerFormat::Struct(nameds) => for field in nameds {
                field.name = field.name.to_case(Case::Pascal);
            },
            _ => {},
        }
    }
}