#![allow(warnings)]

#![feature(thread_local)]

/*use egui::*;
use egui::Id;
use egui::output::*;
use egui::panel::*;
use egui::scroll_area::*;
use egui::style::*;
use egui::text::*;
use egui::text_edit::*;
use egui::text_selection::*;
use egui::util::undoer::*;*/
use serde::*;
use serde::de::*;
use std::borrow::*;
use std::mem::*;

include!(concat!(env!("CARGO_MANIFEST_DIR"), "/../target/bindings/egui_fn.rs"));

/// A registry containing all `egui` functions callable from C#.
const EGUI_FNS: EguiFnMap = egui_fn_map()
    .with(EguiFn::egui_containers_frame_Frame_none, egui::containers::frame::Frame::none as fn() -> _)
    .with(EguiFn::egui_containers_area_AreaState_default, egui::AreaState::default as fn() -> _);

/// Describes the result of an `egui` call.
#[derive(Copy, Clone)]
#[repr(C)]
pub struct EguiInvokeResult {
    /// Whether the function was found.
    pub fn_found: bool,
    /// If [`Self::fn_found`], then the serialized data that the function returned.
    pub return_value: EguiSliceU8
}

impl EguiInvokeResult {
    /// The result that is returned if the function was found and executed.
    /// 
    /// # Safety
    /// 
    /// The returned object should not outlive `return_value`.
    pub const fn success(return_value: &[u8]) -> Self {
        Self {
            fn_found: true,
            return_value: EguiSliceU8::from_slice(return_value)
        }
    }

    /// The result that is returned if the function was not bound.
    pub const fn not_found() -> Self {
        Self {
            fn_found: false,
            return_value: EguiSliceU8::from_slice(&[])
        }
    }
}

/// Describes a section of a `u8` array.
#[derive(Copy, Clone)]
#[repr(C)]
pub struct EguiSliceU8 {
    /// A pointer to the data buffer.
    pub ptr: *const u8,
    /// The length of the data in bytes.
    pub len: usize
}

impl EguiSliceU8 {
    /// Converts `x` to an FFI-compatible slice.
    /// 
    /// # Safety
    /// 
    /// The lifetime of the returned slice should not outlive `x`. 
    pub const fn from_slice(x: &[u8]) -> Self {
        Self {
            ptr: x.as_ptr(),
            len: x.len()
        }
    }

    /// Converts this object to a Rust slice.
    /// 
    /// # Safety
    /// 
    /// For this function call to be sound, `self` must refer
    /// to a valid array of `u8` with at least [`Self::len`] elements.
    pub unsafe fn to_slice(&self) -> &[u8] {
        if self.len == 0 {
            &[]
        }
        else {
            std::slice::from_raw_parts(self.ptr, self.len)
        }
    }
}

#[no_mangle]
pub unsafe extern "C" fn egui_invoke(f: EguiFn, args: EguiSliceU8) -> EguiInvokeResult {
    /// The serialization buffer to which results will be written.
    #[thread_local]
    static mut RETURN_BUFFER: Vec<u8> = Vec::new();
    
    let return_buffer = &mut *std::ptr::addr_of_mut!(RETURN_BUFFER);
    if let Some(invoker) = EGUI_FNS.inner[f as usize] {
        invoker.invoke(args.to_slice(), return_buffer);
        EguiInvokeResult::success(&return_buffer)
    }
    else {
        EguiInvokeResult::not_found()
    }
}

struct EguiFnMap {
    /// A mapping from [`EguiFn`] bindings to the underlying function to invoke.
    inner: [Option<EguiFnInvoker>; EguiFn::ALL.len()]
}

impl EguiFnMap {
    /// Adds a function and returns the new map.
    pub const fn with(mut self, binding: EguiFn, f: impl EguiFnInvokable) -> Self {
        self.inner[binding as usize] = Some(EguiFnInvoker::new(f));
        self
    }
}

/// Creates a new, empty function map.
const fn egui_fn_map() -> EguiFnMap {
    EguiFnMap {
        inner: [None; EguiFn::ALL.len()]
    }
}

/// A type-erased `egui` function that may be called with serialized arguments/result.
#[derive(Copy, Clone, Debug)]
struct EguiFnInvoker {
    /// The `fn` object to pass to `func`.
    data: *const (),
    /// The [`EguiFnInvokable::invoke`] method to call.
    func: unsafe fn(*const (), &[u8], &mut Vec<u8>)
}

impl EguiFnInvoker {
    /// Stores the provided [`EguiFnInvokable`] on the stack for later use.
    /// `F` should be a `fn` pointer.
    pub const fn new<F: EguiFnInvokable>(f: F) -> Self {
        unsafe {
            if size_of::<F>() != size_of::<*const ()>() {
                panic!("Invokable function must be thin pointer");
            }

            Self {
                data: std::ptr::read(&f as *const _ as *const _),
                func: transmute(F::invoke as fn(_, _, _))
            }
        }
    }

    /// Invokes the underlying function.
    pub fn invoke(&self, args: &[u8], ret: &mut Vec<u8>) {
        unsafe {
            (self.func)(&self.data as *const _ as *const _, args, ret);
        }
    }
}

trait EguiFnInvokable: 'static + Copy + Send + Sync {
    fn invoke(&self, args: &[u8], ret: &mut Vec<u8>);
}

impl<T: 'static + Copy + Send + Sync + CallBorrow> EguiFnInvokable for T
where T::Input: 'static + DeserializeOwned, T::Output: Serialize
{
    fn invoke(&self, args: &[u8], ret: &mut Vec<u8>) {
        let deserialized_args = bincode::deserialize(args).expect("Failed to decode args");
        bincode::serialize_into(ret, &self.call(deserialized_args)).expect("Failed to encode result")
    }
}

trait CallBorrow {
    type Input;
    type Output;

    fn call(&self, args: Self::Input) -> Self::Output;
}

impl<R> CallBorrow for fn() -> R {
    type Input = ();
    type Output = R;

    fn call(&self, (): Self::Input) -> Self::Output {
        self()
    }
}

impl<R> CallBorrow for unsafe fn() -> R {
    type Input = ();
    type Output = R;

    fn call(&self, (): Self::Input) -> Self::Output {
        unsafe { self() }
    }
}

impl<A0, R> CallBorrow for fn(A0) -> R {
    type Input = (A0, );
    type Output = R;

    fn call(&self, (arg_0, ): Self::Input) -> Self::Output {
        self(arg_0)
    }
}

impl<A0: ?Sized + ToOwned, R> CallBorrow for fn(&A0) -> R {
    type Input = (A0::Owned, );
    type Output = R;

    fn call(&self, (arg_0, ): Self::Input) -> Self::Output {
        self(arg_0.borrow())
    }
}

impl<A0, A1, R> CallBorrow for fn(A0, A1) -> R {
    type Input = (A0, A1);
    type Output = R;

    fn call(&self, (arg_0, arg_1): Self::Input) -> Self::Output {
        self(arg_0, arg_1)
    }
}

impl<A0: ?Sized + ToOwned, A1, R> CallBorrow for fn(&A0, A1) -> R {
    type Input = (A0::Owned, A1);
    type Output = R;

    fn call(&self, (arg_0, arg_1, ): Self::Input) -> Self::Output {
        self(arg_0.borrow(), arg_1)
    }
}

impl<A0, A1: ?Sized + ToOwned, R> CallBorrow for fn(A0, &A1) -> R {
    type Input = (A0, A1::Owned);
    type Output = R;

    fn call(&self, (arg_0, arg_1): Self::Input) -> Self::Output {
        self(arg_0, arg_1.borrow())
    }
}

impl<A0: ?Sized + ToOwned, A1: ?Sized + ToOwned, R> CallBorrow for fn(&A0, &A1) -> R {
    type Input = (A0::Owned, A1::Owned);
    type Output = R;

    fn call(&self, (arg_0, arg_1, ): Self::Input) -> Self::Output {
        self(arg_0.borrow(), arg_1.borrow())
    }
}