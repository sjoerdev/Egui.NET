use rustdoc_types::*;
use std::fmt::Write;
use std::path::*;

const EXCLUDE_TYPES: &[&str] = &[
    "Event",  // todo
    "InputState",  // todo
    "Options",
    "PointerState",  // todo
    "RawInput",  // todo
    "Undoer",
    "UserData",
    "ViewportCommand"  // todo
];

/// Determines whether `x` implements the trait with `path`.
fn impls_contains(krate: &Crate, impls: &[Id], path: &str) -> bool {
    for id in impls {
        let ItemEnum::Impl(impl_block) = &krate.index[id].inner else { unreachable!() };
        if impl_block.trait_.as_ref().map(|x| x.path == path).unwrap_or_default() {
            return true;
        }
    }

    false
}

fn gather_serde_tys(krate: &Crate, exclude_tys: &[&str]) -> Vec<Id> {
    let mut result = Vec::new();

    for (id, item) in &krate.index {
        let impls = match &item.inner {
            ItemEnum::Enum(x) => &x.impls,
            ItemEnum::Struct(x) => &x.impls,
            _ => continue
        };

        if item.name.as_deref().map(|x| exclude_tys.contains(&x)).unwrap_or_default() {
            continue;
        }

        if krate.paths[id].crate_id == 0
            && impls_contains(&krate, impls, "Serialize")
            && impls_contains(&krate, impls, "Deserialize") {
            result.push(id.clone());
        }
    }

    result.sort_by_key(|x| krate.index[x].name.as_deref());

    result
}

fn emit_serde_tracer(krate: &Crate, exclude_tys: &[&str]) -> String {
    let ids = gather_serde_tys(krate, exclude_tys);

    let mut result = String::new();
    
    result.push_str("/// Registers all serializable `egui` types with the reflection system.\n");
    result.push_str("fn trace_auto_serde_types(tracer: &mut ::serde_reflection::Tracer) {\n");

    for id in ids {
        let name = krate.index[&id].name.clone().unwrap_or_default();
        write!(&mut result, "    tracer.trace_simple_type::<{name}>().expect(\"Failed to trace {name}\");\n").expect("Failed to write to string");
    }

    result.push_str("}\n");
    result
}

fn main() {
    let krate = serde_json::from_str::<Crate>(include_str!("../src/egui.json")).expect("Failed to parse egui");
    let out_dir = std::env::var("OUT_DIR").expect("Failed to get output directory");
    let out_file = PathBuf::from(out_dir).join("tracer.rs");
    let tracer_definition = emit_serde_tracer(&krate, EXCLUDE_TYPES);
    std::fs::write(out_file, tracer_definition).expect("Failed to write tracer bindings");
}