//! Allows for efficiently communicating with a C#-side `egui` Context from [Egui.NET](https://github.com/DouglasDwyer/Egui.NET).

use std::ops::Deref;

use egui::*;
use egui::epaint::*;
use serde::*;

/// Allows for passing `egui` data back and forth with C#.
#[repr(C)]
pub struct EguiFfi {
    /// The serialized bytes of the most recent [`FullOutput`].
    full_output: FfiVec<u8>,
    /// The meshes from the most recent call to [`Context::tessellate`].
    meshes: FfiVec<(Rect, FfiTextureId, FfiVec<u32>, FfiVec<Vertex>)>,
    /// The most recent raw input.
    raw_input: FfiVec<u8>,
}

impl EguiFfi {
    /// Gets the most recent [`FullOutput`]. [`FullOutput::shapes`] and
    /// [`FullOutput::viewport_output`] are not included.
    pub fn full_output(&self) -> FullOutput {
        bincode::deserialize::<FullOutput2>(&self.full_output)
            .expect("Failed to deserialize FullOutput").into()
    }

    /// Sets the most recent [`FullOutput`].
    pub fn set_full_output(&mut self, full_output: FullOutput) {
        self.full_output = bincode::serialize(&FullOutput2::from(full_output))
            .expect("Failed to serialize FullOutput").into();
    }

    /// Gets the most recent [`RawInput`].
    pub fn raw_input(&self) -> RawInput {
        bincode::deserialize(&self.raw_input)
            .expect("Failed to deserialize RawInput")
    }

    /// Sets the most recent [`RawInput`].
    pub fn set_raw_input(&mut self, raw_input: RawInput) {
        self.raw_input = bincode::serialize(&raw_input)
            .expect("Failed to serialize RawInput").into();
    }

    /// Gets the most recent output of [`Context::tessellate`].
    pub fn meshes(&self) -> Vec<(Rect, Mesh)> {
        self.meshes.iter().map(|(clip_rect, texture_id, indices, vertices)|
            (*clip_rect, Mesh {
                indices: indices.to_vec(),
                texture_id: (*texture_id).into(),
                vertices: vertices.to_vec()
            })).collect()
    }

    /// Sets the most recent output of [`Context::tessellate`].
    pub fn set_meshes(&mut self, meshes: impl IntoIterator<Item = (Rect, Mesh)>) {
        self.meshes = meshes.into_iter()
            .map(|(clip_rect, mesh)| (clip_rect, mesh.texture_id.into(), mesh.indices.into(), mesh.vertices.into()))
            .collect::<Vec<_>>().into()
    }
}

impl Default for EguiFfi {
    fn default() -> Self {
        let mut result = Self {
            full_output: Vec::new().into(),
            meshes: Vec::new().into(),
            raw_input: Vec::new().into()
        };

        result.set_full_output(FullOutput::default());
        result.set_raw_input(RawInput::default());
        
        result
    }
}

/// Holds the serializable members of [`FullOutput`]
/// (since [`FullOutput`] is not serializable).
#[derive(Clone, Serialize, Deserialize)]
struct FullOutput2 {
    /// The [`FullOutput::platform_output`] field.
    pub platform_output: PlatformOutput,
    /// The [`FullOutput::textures_delta`] field.
    pub textures_delta: TexturesDelta,
    /// The [`FullOutput::pixels_per_point`] field.
    pub pixels_per_point: f32
}

impl From<FullOutput2> for FullOutput {
    fn from(value: FullOutput2) -> Self {
        Self {
            pixels_per_point: value.pixels_per_point,
            platform_output: value.platform_output,
            textures_delta: value.textures_delta,
            ..Default::default()
        }
    }
}

impl From<FullOutput> for FullOutput2 {
    fn from(value: FullOutput) -> Self {
        Self {
            pixels_per_point: value.pixels_per_point,
            platform_output: value.platform_output,
            textures_delta: value.textures_delta
        }
    }
}

/// An FFI-compatible version of [`TextureId`].
#[derive(Copy, Clone, Debug)]
#[repr(C)]
struct FfiTextureId {
    /// The type of ID.
    pub kind: TextureIdKind,
    /// The integer identifier.
    pub id: u64
}

impl From<TextureId> for FfiTextureId {
    fn from(value: TextureId) -> Self {
        match value {
            TextureId::Managed(id) => Self {
                kind: TextureIdKind::Managed,
                id
            },
            TextureId::User(id) => Self {
                kind: TextureIdKind::User,
                id
            }
        }
    }
}

impl From<FfiTextureId> for TextureId {
    fn from(value: FfiTextureId) -> Self {
        match value.kind {
            TextureIdKind::Managed => TextureId::Managed(value.id),
            TextureIdKind::User => TextureId::User(value.id),
        }
    }
}

/// Identifies a variant of [`TextureId`].
#[derive(Copy, Clone, Debug)]
#[repr(C)]
enum TextureIdKind {
    /// [`TextureId::Managed`]
    Managed,
    /// [`TextureId::User`]
    User
}

/// A [`Vec`] that can be passed across FFI boundaries.
#[derive(Debug)]
#[repr(C)]
struct FfiVec<T> {
    /// Pointer to the allocation.
    ptr: *mut T,
    /// The length of the vector.
    len: usize,
    /// The capacity of the vector.
    capacity: usize,
    /// The function to call in order to free the vector.
    on_free: unsafe extern "C" fn(&mut Self)
}

impl<T> FfiVec<T> {
    /// Frees the vector using its original allocator.
    unsafe extern "C" fn on_free(&mut self) {
        Vec::from_raw_parts(self.ptr, self.len, self.capacity);
    }
}

impl<T> From<Vec<T>> for FfiVec<T> {
    fn from(mut value: Vec<T>) -> Self {
        let ptr = value.as_mut_ptr();
        let len = value.len();
        let capacity = value.capacity();
        std::mem::forget(value);
        Self {
            ptr,
            len,
            capacity,
            on_free: Self::on_free
        }
    }
}

impl<T> Deref for FfiVec<T> {
    type Target = [T];

    fn deref(&self) -> &Self::Target {
        unsafe {
            std::slice::from_raw_parts(self.ptr, self.len)
        }
    }
}

impl<T> Drop for FfiVec<T> {
    fn drop(&mut self) {
        unsafe {
            (self.on_free)(self)
        }
    }
}