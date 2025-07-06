use egui::*;
use egui::output::*;
use egui::panel::*;
use egui::scroll_area::*;
use egui::style::*;
use egui::text::*;
use egui::text_edit::*;
use egui::text_selection::*;
use egui::util::undoer::*;
use serde_reflection::*;

include!(concat!(env!("OUT_DIR"), "/tracer.rs"));

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

fn main() {
    let registry = trace_serde_types();
    println!("Hello, world! got {:?}", registry.keys().cloned().collect::<Vec<_>>());
}
