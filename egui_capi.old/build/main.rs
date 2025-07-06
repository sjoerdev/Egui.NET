//! Generates C# bindings for the Vortex API.

#![feature(formatting_options)]

use csbindgen::*;
use std::env::*;
use std::fs::*;
use std::path::*;

/// Generates C FFI bindings for `egui`.
#[cfg(feature = "bindgen")]
mod bindgen;

/// Generates C# bindings for all C API files in the crate.
fn main() {
    let output_dir = PathBuf::from(var("CARGO_MANIFEST_DIR").expect("Failed to get manifest directory")).join("ag");
    
    #[cfg(feature = "bindgen")]
    bindgen::autogenerate(&output_dir);
    
    let lib_name;
    #[cfg(windows)]
    {
        lib_name = "egui_capi";
    }
    #[cfg(unix)]
    {
        lib_name = "libegui_capi";
    }

    let mut builder = Builder::default()
        .csharp_namespace("Egui")
        .csharp_class_name("Egui")
        .csharp_class_accessibility("public")
        .csharp_dll_name(lib_name)
        .csharp_generate_const_filter(|_| true);

    for file in get_all_files("src") {
        builder = builder.input_extern_file(file);
    }

    builder = builder.input_extern_file(output_dir.join("egui.rs"));

    let output_file = output_dir.join("Bindings.g.cs");
    builder.generate_csharp_file(&output_file).expect("Failed to generate C# bindings");

    let mut file_contents = read_to_string(&output_file).expect("Failed to read bindings file.");
    file_contents = file_contents.replace(" egui_", " ");
    file_contents = file_contents.replace(" Egui_", " ");
    write(output_file, file_contents).expect("Failed to generate renamed C# bindings");
}

/// Returns a list containing all files in the provided directory (and subdirectories).
fn get_all_files(path: impl AsRef<Path>) -> Vec<PathBuf> {
    let mut result = Vec::new();
    for maybe_entry in read_dir(path).expect("Failed to read directory") {
        let entry = maybe_entry.expect("Failed to read directory entry");
        let file_type = entry.file_type().expect("Failed to get file type");
        if file_type.is_dir() {
            result.extend(get_all_files(&entry.path()));
        }
        else if file_type.is_file() {
            result.push(entry.path());
        }
    }
    result
}
