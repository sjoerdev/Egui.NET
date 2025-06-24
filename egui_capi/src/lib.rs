#![allow(unsafe_op_in_unsafe_fn)]

use egui::*;
use egui::epaint::text::cursor::*;
use std::mem::*;

include!("../ag/egui.rs");

/// Holds an [`egui`] object on the heap for external access.
pub struct EguiObject<T: Send + Sync>(T);

impl<T: Send + Sync> EguiObject<T> {
    /// Pushes the provided value onto the heap and returns a pointer to the allocated object.
    pub fn to_heap(value: T) -> *mut Self {
        Box::into_raw(Box::new(Self(value)))
    }

    /// Consumes the pointer and removes the associated object from the heap.
    ///
    /// # Safety
    ///
    /// For this function call to be sound, `self` must be a valid object pointer,
    /// and no other references to it must exist.
    pub unsafe fn from_heap(value: *mut Self) -> T {
        Box::from_raw(value).0
    }
}

impl<T: Send + Sync> std::ops::Deref for EguiObject<T> {
    type Target = T;

    fn deref(&self) -> &Self::Target {
        &self.0
    }
}

impl<T: Send + Sync> std::ops::DerefMut for EguiObject<T> {
    fn deref_mut(&mut self) -> &mut Self::Target {
        &mut self.0
    }
}

impl<'a, T: Send + Sync> From<&'a T> for &'a EguiObject<T> {
    fn from(value: &'a T) -> Self {
        unsafe { transmute(value) }
    }
}

impl<'a, T: Send + Sync> From<&'a mut T> for &'a mut EguiObject<T> {
    fn from(value: &'a mut T) -> Self {
        unsafe { transmute(value) }
    }
}

/// egui tracks widgets frame-to-frame using [`Id`]s.
///
/// For instance, if you start dragging a slider one frame, egui stores
/// the sliders [`Id`] as the current active id so that next frame when
/// you move the mouse the same slider changes, even if the mouse has
/// moved outside the slider.
///
/// For some widgets [`Id`]s are also used to persist some state about the
/// widgets, such as Window position or whether not a collapsing header region is open.
///
/// This implies that the [`Id`]s must be unique.
///
/// For simple things like sliders and buttons that don't have any memory and
/// doesn't move we can use the location of the widget as a source of identity.
/// For instance, a slider only needs a unique and persistent ID while you are
/// dragging the slider. As long as it is still while moving, that is fine.
///
/// For things that need to persist state even after moving (windows, collapsing headers)
/// the location of the widgets is obviously not good enough. For instance,
/// a collapsing region needs to remember whether or not it is open even
/// if the layout next frame is different and the collapsing is not lower down
/// on the screen.
///
/// Then there are widgets that need no identifiers at all, like labels,
/// because they have no state nor are interacted with.
///
/// This is niche-optimized to that `Option<Id>` is the same size as `Id`.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiId(u64);

impl From<Id> for EguiId {
    fn from(value: Id) -> Self {
        Self(value.value())
    }
}

impl From<EguiId> for Id {
    fn from(value: EguiId) -> Self {
        if value.0 == 0 {
            Self::NULL
        }
        else {
            // Safety: the inner representation of Id is a NonZeroU64
            unsafe { transmute(value) }
        }
    }
}

/// left/center/right or top/center/bottom alignment for e.g. anchors and layouts.
#[derive(Clone, Copy, Default)]
#[repr(C)]
pub enum EguiAlign {
    /// Left or top.
    #[default]
    Min,
    /// Horizontal or vertical center.
    Center,
    /// Right or bottom.
    Max,
}

impl From<Align> for EguiAlign {
    fn from(value: Align) -> Self {
        match value {
            Align::Min => Self::Min,
            Align::Center => Self::Center,
            Align::Max => Self::Max,
        }
    }
}

impl From<EguiAlign> for Align {
    fn from(value: EguiAlign) -> Self {
        match value {
            EguiAlign::Min => Self::Min,
            EguiAlign::Center => Self::Center,
            EguiAlign::Max => Self::Max,
        }
    }
}

/// This format is used for space-efficient color representation (32 bits).
///
/// Instead of manipulating this directly it is often better
/// to first convert it to either [`Rgba`] or [`crate::Hsva`].
///
/// Internally this uses 0-255 gamma space `sRGBA` color with premultiplied alpha.
/// Alpha channel is in linear space.
///
/// The special value of alpha=0 means the color is to be treated as an additive color.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiColor32 {
    /// The red channel.
    pub r: u8,
    /// The green channel.
    pub g: u8,
    /// The blue channel.
    pub b: u8,
    /// The alpha channel.
    pub a: u8
}

impl From<Color32> for EguiColor32 {
    fn from(value: Color32) -> Self {
        Self {
            r: value.r(),
            g: value.g(),
            b: value.b(),
            a: value.a()
        }
    }
}

impl From<EguiColor32> for Color32 {
    fn from(value: EguiColor32) -> Self {
        Self::from_rgba_premultiplied(value.r, value.g, value.b, value.a)
    }
}

/// A vector has a direction and length.
/// A [`Vec2`] is often used to represent a size.
///
/// emath represents positions using [`crate::Pos2`].
///
/// Normally the units are points (logical pixels).
#[derive(Clone, Copy, Default)]
#[repr(C)]
pub struct EguiVec2 {
    /// Rightwards. Width.
    pub x: f32,
    /// Downwards. Height.
    pub y: f32
}

impl From<Vec2> for EguiVec2 {
    fn from(value: Vec2) -> Self {
        Self {
            x: value.x,
            y: value.y
        }
    }
}

impl From<EguiVec2> for Vec2 {
    fn from(value: EguiVec2) -> Self {
        Self {
            x: value.x,
            y: value.y
        }
    }
}

/// A position on screen.
///
/// Normally given in points (logical pixels).
///
/// Mathematically this is known as a "point", but the term position was chosen so not to
/// conflict with the unit (one point = X physical pixels).
#[derive(Clone, Copy, Default)]
#[repr(C)]
pub struct EguiPos2 {
    /// How far to the right.
    pub x: f32,
    /// How far down.
    pub y: f32,
    // implicit w = 1
}

impl From<Pos2> for EguiPos2 {
    fn from(value: Pos2) -> Self {
        Self {
            x: value.x,
            y: value.y
        }
    }
}

impl From<EguiPos2> for Pos2 {
    fn from(value: EguiPos2) -> Self {
        Self {
            x: value.x,
            y: value.y
        }
    }
}

/// What sort of interaction is a widget sensitive to?
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiSense(u8);

impl From<Sense> for EguiSense {
    fn from(value: Sense) -> Self {
        Self(value.bits())
    }
}

impl From<EguiSense> for Sense {
    fn from(value: EguiSense) -> Self {
        Self::from_bits_truncate(value.0)
    }
}

/// A rectangular region of space.
///
/// Usually a [`Rect`] has a positive (or zero) size,
/// and then [`Self::min`] `<=` [`Self::max`].
/// In these cases [`Self::min`] is the left-top corner
/// and [`Self::max`] is the right-bottom corner.
///
/// A rectangle is allowed to have a negative size, which happens when the order
/// of `min` and `max` are swapped. These are usually a sign of an error.
///
/// Normally the unit is points (logical pixels) in screen space coordinates.
///
/// `Rect` does NOT implement `Default`, because there is no obvious default value.
/// [`Rect::ZERO`] may seem reasonable, but when used as a bounding box, [`Rect::NOTHING`]
/// is a better default - so be explicit instead!
#[derive(Clone, Copy, Default)]
#[repr(C)]
pub struct EguiRect {
    /// One of the corners of the rectangle, usually the left top one.
    pub min: EguiPos2,
    /// The other corner, opposing [`Self::min`]. Usually the right bottom one.
    pub max: EguiPos2,
}

impl From<Rect> for EguiRect {
    fn from(value: Rect) -> Self {
        Self {
            min: value.min.into(),
            max: value.max.into()
        }
    }
}

impl From<EguiRect> for Rect {
    fn from(value: EguiRect) -> Self {
        Self {
            min: value.min.into(),
            max: value.max.into()
        }
    }
}

/// Describes the width and color of a line.
///
/// The default stroke is the same as [`Stroke::NONE`].
#[derive(Clone, Copy, Default)]
#[repr(C)]
pub struct EguiStroke {
    pub width: f32,
    pub color: Color32,
}

impl From<Stroke> for EguiStroke {
    fn from(value: Stroke) -> Self {
        Self {
            width: value.width,
            color: value.color.into()
        }
    }
}

impl From<EguiStroke> for Stroke {
    fn from(value: EguiStroke) -> Self {
        Self {
            width: value.width,
            color: value.color.into()
        }
    }
}

/// How rounded the corners of things should be.
///
/// This specific the _corner radius_ of the underlying geometric shape (e.g. rectangle).
/// If there is a stroke, then the stroke will have an inner and outer corner radius
/// which will depends on its width and [`crate::StrokeKind`].
///
/// The rounding uses `u8` to save space,
/// so the amount of rounding is limited to integers in the range `[0, 255]`.
///
/// For calculations, you may want to use [`crate::CornerRadiusF32`] instead, which uses `f32`.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiCornerRadius {
    /// Radius of the rounding of the North-West (left top) corner.
    pub nw: u8,

    /// Radius of the rounding of the North-East (right top) corner.
    pub ne: u8,

    /// Radius of the rounding of the South-West (left bottom) corner.
    pub sw: u8,

    /// Radius of the rounding of the South-East (right bottom) corner.
    pub se: u8,
}

impl From<CornerRadius> for EguiCornerRadius {
    fn from(value: CornerRadius) -> Self {
        Self {
            nw: value.nw,
            ne: value.ne,
            sw: value.sw,
            se: value.se
        }
    }
}

impl From<EguiCornerRadius> for CornerRadius {
    fn from(value: EguiCornerRadius) -> Self {
        Self {
            nw: value.nw,
            ne: value.ne,
            sw: value.sw,
            se: value.se
        }
    }
}

/// Character cursor.
///
/// The default cursor is zero.
#[derive(Clone, Copy, Default)]
#[repr(C)]
pub struct EguiCCursor {
    /// Character offset (NOT byte offset!).
    pub index: usize,

    /// If this cursors sits right at the border of a wrapped row break (NOT paragraph break)
    /// do we prefer the next row?
    /// This is *almost* always what you want, *except* for when
    /// explicitly clicking the end of a row or pressing the end key.
    pub prefer_next_row: bool,
}

impl From<CCursor> for EguiCCursor {
    fn from(value: CCursor) -> Self {
        Self {
            index: value.index,
            prefer_next_row: value.prefer_next_row
        }
    }
}

impl From<EguiCCursor> for CCursor {
    fn from(value: EguiCCursor) -> Self {
        Self {
            index: value.index,
            prefer_next_row: value.prefer_next_row
        }
    }
}

/// Row Cursor
#[derive(Clone, Copy, Default)]
#[repr(C)]
pub struct EguiRCursor {
    /// 0 is first row, and so on.
    /// Note that a single paragraph can span multiple rows.
    /// (a paragraph is text separated by `\n`).
    pub row: usize,

    /// Character based (NOT bytes).
    /// It is fine if this points to something beyond the end of the current row.
    /// When moving up/down it may again be within the next row.
    pub column: usize,
}

impl From<RCursor> for EguiRCursor {
    fn from(value: RCursor) -> Self {
        Self {
            row: value.row,
            column: value.column
        }
    }
}

impl From<EguiRCursor> for RCursor {
    fn from(value: EguiRCursor) -> Self {
        Self {
            row: value.row,
            column: value.column
        }
    }
}

/// Paragraph Cursor
#[derive(Clone, Copy, Default)]
#[repr(C)]
pub struct EguiPCursor {
    /// 0 is first paragraph, and so on.
    /// Note that a single paragraph can span multiple rows.
    /// (a paragraph is text separated by `\n`).
    pub paragraph: usize,

    /// Character based (NOT bytes).
    /// It is fine if this points to something beyond the end of the current paragraph.
    /// When moving up/down it may again be within the next paragraph.
    pub offset: usize,

    /// If this cursors sits right at the border of a wrapped row break (NOT paragraph break)
    /// do we prefer the next row?
    /// This is *almost* always what you want, *except* for when
    /// explicitly clicking the end of a row or pressing the end key.
    pub prefer_next_row: bool,
}

impl From<PCursor> for EguiPCursor {
    fn from(value: PCursor) -> Self {
        Self {
            paragraph: value.paragraph,
            offset: value.offset,
            prefer_next_row: value.prefer_next_row
        }
    }
}

impl From<EguiPCursor> for PCursor {
    fn from(value: EguiPCursor) -> Self {
        Self {
            paragraph: value.paragraph,
            offset: value.offset,
            prefer_next_row: value.prefer_next_row
        }
    }
}

/// All different types of cursors together.
///
/// They all point to the same place, but in their own different ways.
/// pcursor/rcursor can also point to after the end of the paragraph/row.
/// Does not implement `PartialEq` because you must think which cursor should be equivalent.
///
/// The default cursor is the zero-cursor, to the first character.
#[derive(Clone, Copy, Default)]
pub struct EguiCursor {
    pub ccursor: EguiCCursor,
    pub rcursor: EguiRCursor,
    pub pcursor: EguiPCursor,
}

impl From<Cursor> for EguiCursor {
    fn from(value: Cursor) -> Self {
        Self {
            ccursor: value.ccursor.into(),
            rcursor: value.rcursor.into(),
            pcursor: value.pcursor.into()
        }
    }
}