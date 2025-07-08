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
use std::mem::*;

include!(concat!(env!("CARGO_MANIFEST_DIR"), "/../target/bindings/egui_fn.rs"));

const EGUI_FNS: EguiFnMap = egui_fn_map()
    .with_0(EguiFn::egui_containers_area_AreaState_default, egui::AreaState::default)
    .with_1(EguiFn::egui_containers_area_AreaState_default, go_one);

fn go_one(x: &str) {

}

struct EguiFnMap {
    /// A mapping from [`EguiFn`] bindings to the underlying function to invoke.
    inner: [Option<EguiFnInvoker>; EguiFn::ALL.len()]
}

impl EguiFnMap {
    /// Adds a function and returns the new map.
    pub const fn with_0<R>(mut self, binding: EguiFn, f: fn() -> R) -> Self
    where R: 'static + Serialize
    {
        self.inner[binding as usize] = Some(EguiFnInvoker::new(f));
        self
    }
    
    /// Adds a function and returns the new map.
    pub const fn with_1<A0, R>(mut self, binding: EguiFn, f: fn(A0) -> R) -> Self
    where A0: 'static + for<'a> Deserialize<'a>, R: 'static + Serialize
    {
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

impl<R: 'static + Serialize> EguiFnInvokable for fn() -> R
where R: 'static + Serialize
{
    fn invoke(&self, args: &[u8], ret: &mut Vec<u8>) {
        bincode::serialize_into(ret, &self()).expect("Failed to encode result")
    }
}



impl<A0, R> EguiFnInvokable for fn(A0) -> R
where A0: 'static + for<'a> Deserialize<'a>, R: 'static + Serialize
{
    fn invoke(&self, args: &[u8], ret: &mut Vec<u8>) {
        let (arg0, ) = bincode::deserialize(args).expect("Failed to decode args");
        bincode::serialize_into(ret, &self(arg0)).expect("Failed to encode result")
    }
}