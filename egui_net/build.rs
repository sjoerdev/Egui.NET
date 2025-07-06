use std::path::*;

/// Generates all files for `egui`.
fn main() {
    let manifest_dir = PathBuf::from(std::env::var("CARGO_MANIFEST_DIR").expect("Failed to get root directory"));
    let out_dir = manifest_dir.join("g");
    egui_net_bindgen::generate(&out_dir);
}