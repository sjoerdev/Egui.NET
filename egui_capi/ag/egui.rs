/// Indicate whether a popup will be shown above or below the box.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub enum EguiAboveOrBelow {
    #[default]
    Above,
    Below,
}

/// What options to show for alpha
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub enum EguiAlpha {
    #[default]
    /// Set alpha to 1.0, and show no option for it.
    Opaque,
    /// Only show normal blend options for alpha.
    OnlyBlend,
    /// Show both blend and additive options.
    BlendOrAdditive,
}

#[derive(Copy, Clone, Default)]
#[repr(C)]
pub enum EguiCursorGrab {
    #[default]
    None,
    Confined,
    Locked,
}

/// A mouse cursor icon.
/// 
/// egui emits a [`CursorIcon`] in [`PlatformOutput`] each frame as a request to the integration.
/// 
/// Loosely based on <https://developer.mozilla.org/en-US/docs/Web/CSS/cursor>.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub enum EguiCursorIcon {
    #[default]
    /// Normal cursor icon, whatever that is.
    Default,
    /// Show no cursor
    None,
    /// A context menu is available
    ContextMenu,
    /// Question mark
    Help,
    /// Pointing hand, used for e.g. web links
    PointingHand,
    /// Shows that processing is being done, but that the program is still interactive.
    Progress,
    /// Not yet ready, try later.
    Wait,
    /// Hover a cell in a table
    Cell,
    /// For precision work
    Crosshair,
    /// Text caret, e.g. "Click here to edit text"
    Text,
    /// Vertical text caret, e.g. "Click here to edit vertical text"
    VerticalText,
    /// Indicated an alias, e.g. a shortcut
    Alias,
    /// Indicate that a copy will be made
    Copy,
    /// Omnidirectional move icon (e.g. arrows in all cardinal directions)
    Move,
    /// Can't drop here
    NoDrop,
    /// Forbidden
    NotAllowed,
    /// The thing you are hovering can be grabbed
    Grab,
    /// You are grabbing the thing you are hovering
    Grabbing,
    /// Something can be scrolled in any direction (panned).
    AllScroll,
    /// Horizontal resize `-` to make something wider or more narrow (left to/from right)
    ResizeHorizontal,
    /// Diagonal resize `/` (right-up to/from left-down)
    ResizeNeSw,
    /// Diagonal resize `\` (left-up to/from right-down)
    ResizeNwSe,
    /// Vertical resize `|` (up-down or down-up)
    ResizeVertical,
    /// Resize something rightwards (e.g. when dragging the right-most edge of something)
    ResizeEast,
    /// Resize something down and right (e.g. when dragging the bottom-right corner of something)
    ResizeSouthEast,
    /// Resize something downwards (e.g. when dragging the bottom edge of something)
    ResizeSouth,
    /// Resize something down and left (e.g. when dragging the bottom-left corner of something)
    ResizeSouthWest,
    /// Resize something leftwards (e.g. when dragging the left edge of something)
    ResizeWest,
    /// Resize something up and left (e.g. when dragging the top-left corner of something)
    ResizeNorthWest,
    /// Resize something up (e.g. when dragging the top edge of something)
    ResizeNorth,
    /// Resize something up and right (e.g. when dragging the top-right corner of something)
    ResizeNorthEast,
    /// Resize a column
    ResizeColumn,
    /// Resize a row
    ResizeRow,
    /// Enhance!
    ZoomIn,
    /// Let's get a better overview
    ZoomOut,
}

/// Layout direction, one of [`LeftToRight`](Direction::LeftToRight), [`RightToLeft`](Direction::RightToLeft), [`TopDown`](Direction::TopDown), [`BottomUp`](Direction::BottomUp).
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub enum EguiDirection {
    #[default]
    LeftToRight,
    RightToLeft,
    TopDown,
    BottomUp,
}

#[derive(Copy, Clone, Default)]
#[repr(C)]
pub enum EguiIMEPurpose {
    #[default]
    Normal,
    Password,
    Terminal,
}

/// The unit associated with the numeric value of a mouse wheel event
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub enum EguiMouseWheelUnit {
    #[default]
    /// Number of ui points (logical pixels)
    Point,
    /// Number of lines
    Line,
    /// Number of pages
    Page,
}

/// How to display numeric color values.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub enum EguiNumericColorSpace {
    #[default]
    /// RGB is 0-255 in gamma space.
    /// 
    /// Alpha is 0-255 in linear space.
    GammaByte,
    /// 0-1 in linear space.
    Linear,
}

/// Different layer categories
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub enum EguiOrder {
    #[default]
    /// Painted behind all floating windows
    Background,
    /// Normal moveable windows that you reorder by click
    Middle,
    /// Popups, menus etc that should always be painted on top of windows
    /// Foreground objects can also have tooltips
    Foreground,
    /// Things floating on top of everything else, like tooltips.
    /// You cannot interact with these.
    Tooltip,
    /// Debug layer, always painted last / on top
    Debug,
}

/// Mouse button (or similar for touch input)
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub enum EguiPointerButton {
    #[default]
    /// The primary mouse button is usually the left one.
    Primary = 0,
    /// The secondary mouse button is usually the right one,
    /// and most often used for context menus or other optional things.
    Secondary = 1,
    /// The tertiary mouse button is usually the middle mouse button (e.g. clicking the scroll wheel).
    Middle = 2,
    /// The first extra mouse button on some mice. In web typically corresponds to the Browser back button.
    Extra1 = 3,
    /// The second extra mouse button on some mice. In web typically corresponds to the Browser forward button.
    Extra2 = 4,
}

/// Determines popup's close behavior
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub enum EguiPopupCloseBehavior {
    #[default]
    /// Popup will be closed on click anywhere, inside or outside the popup.
    /// 
    /// It is used in [`crate::ComboBox`].
    CloseOnClick,
    /// Popup will be closed if the click happened somewhere else
    /// but in the popup's body
    CloseOnClickOutside,
    /// Clicks will be ignored. Popup might be closed manually by calling [`crate::Memory::close_popup`]
    /// or by pressing the escape button
    IgnoreClicks,
}

#[derive(Copy, Clone, Default)]
#[repr(C)]
pub enum EguiResizeDirection {
    #[default]
    North,
    South,
    East,
    West,
    NorthEast,
    SouthEast,
    NorthWest,
    SouthWest,
}

/// Indicate whether the horizontal and vertical scroll bars must be always visible, hidden or visible when needed.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub enum EguiScrollBarVisibility {
    #[default]
    /// Hide scroll bar even if they are needed.
    /// 
    /// You can still scroll, with the scroll-wheel
    /// and by dragging the contents, but there is no
    /// visual indication of how far you have scrolled.
    AlwaysHidden,
    /// Show scroll bars only when the content size exceeds the container,
    /// i.e. when there is any need to scroll.
    /// 
    /// This is the default.
    VisibleWhenNeeded,
    /// Always show the scroll bar, even if the contents fit in the container
    /// and there is no need to scroll.
    AlwaysVisible,
}

/// [`Left`](Side::Left) or [`Right`](Side::Right)
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub enum EguiSide {
    #[default]
    Left,
    Right,
}

/// Specifies how values in a [`Slider`] are clamped.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub enum EguiSliderClamping {
    #[default]
    /// Values are not clamped.
    /// 
    /// This means editing the value with the keyboard,
    /// or dragging the number next to the slider will always work.
    /// 
    /// The actual slider part is always clamped though.
    Never,
    /// Users cannot enter new values that are outside the range.
    /// 
    /// Existing values remain intact though.
    Edits,
    /// Always clamp values, even existing ones.
    Always,
}

/// Specifies the orientation of a [`Slider`].
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub enum EguiSliderOrientation {
    #[default]
    Horizontal,
    Vertical,
}

#[derive(Copy, Clone, Default)]
#[repr(C)]
pub enum EguiSystemTheme {
    #[default]
    SystemDefault,
    Light,
    Dark,
}

/// Dark or Light theme.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub enum EguiTheme {
    #[default]
    /// Dark mode: light text on a dark background.
    Dark,
    /// Light mode: dark text on a light background.
    Light,
}

/// The user's theme preference.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub enum EguiThemePreference {
    #[default]
    /// Dark mode: light text on a dark background.
    Dark,
    /// Light mode: dark text on a light background.
    Light,
    /// Follow the system's theme preference.
    System,
}

/// [`Top`](TopBottomSide::Top) or [`Bottom`](TopBottomSide::Bottom)
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub enum EguiTopBottomSide {
    #[default]
    Top,
    Bottom,
}

/// In what phase a touch event is in.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub enum EguiTouchPhase {
    #[default]
    /// User just placed a touch point on the touch surface
    Start,
    /// User moves a touch point along the surface. This event is also sent when
    /// any attributes (position, force, …) of the touch point change.
    Move,
    /// User lifted the finger or pen from the surface, or slid off the edge of
    /// the surface
    End,
    /// Touch operation has been disrupted by something (various reasons are possible,
    /// maybe a pop-up alert or any other kind of interruption which may not have
    /// been intended by the user)
    Cancel,
}

/// What kind is this [`crate::Ui`]?
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub enum EguiUiKind {
    #[default]
    /// A [`crate::Window`].
    Window,
    /// A [`crate::CentralPanel`].
    CentralPanel,
    /// A left [`crate::SidePanel`].
    LeftPanel,
    /// A right [`crate::SidePanel`].
    RightPanel,
    /// A top [`crate::TopBottomPanel`].
    TopPanel,
    /// A bottom [`crate::TopBottomPanel`].
    BottomPanel,
    /// A modal [`crate::Modal`].
    Modal,
    /// A [`crate::Frame`].
    Frame,
    /// A [`crate::ScrollArea`].
    ScrollArea,
    /// A [`crate::Resize`].
    Resize,
    /// The content of a regular menu.
    Menu,
    /// The content of a popup menu.
    Popup,
    /// A tooltip, as shown by e.g. [`crate::Response::on_hover_ui`].
    Tooltip,
    /// A picker, such as color picker.
    Picker,
    /// A table cell (from the `egui_extras` crate).
    TableCell,
    /// An [`crate::Area`] that is not of any other kind.
    GenericArea,
}

/// Types of attention to request from a user when a native window is not in focus.
/// 
/// See [winit's documentation][user_attention_type] for platform-specific meaning of the attention types.
/// 
/// [user_attention_type]: https://docs.rs/winit/latest/winit/window/enum.UserAttentionType.html
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub enum EguiUserAttentionType {
    #[default]
    /// Request an elevated amount of animations and flair for the window and the task bar or dock icon.
    Critical,
    /// Request a standard amount of attention-grabbing actions.
    Informational,
    /// Reset the attention request and interrupt related animations and flashes.
    Reset,
}

/// The different types of viewports supported by egui.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub enum EguiViewportClass {
    #[default]
    /// The root viewport; i.e. the original window.
    Root,
    /// A viewport run independently from the parent viewport.
    /// 
    /// This is the preferred type of viewport from a performance perspective.
    /// 
    /// Create these with [`crate::Context::show_viewport_deferred`].
    Deferred,
    /// A viewport run inside the parent viewport.
    /// 
    /// This is the easier type of viewport to use, but it is less performant
    /// at it requires both parent and child to repaint if any one of them needs repainting,
    /// which effectively produces double work for two viewports, and triple work for three viewports, etc.
    /// 
    /// Create these with [`crate::Context::show_viewport_immediate`].
    Immediate,
    /// The fallback, when the egui integration doesn't support viewports,
    /// or [`crate::Context::embed_viewports`] is set to `true`.
    Embedded,
}

/// An input event from the backend into egui, about a specific [viewport](crate::viewport).
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub enum EguiViewportEvent {
    #[default]
    /// The user clicked the close-button on the window, or similar.
    /// 
    /// If this is the root viewport, the application will exit
    /// after this frame unless you send a
    /// [`crate::ViewportCommand::CancelClose`] command.
    /// 
    /// If this is not the root viewport,
    /// it is up to the user to hide this viewport the next frame.
    /// 
    /// This even will wake up both the child and parent viewport.
    Close,
}

/// The different types of built-in widgets in egui
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub enum EguiWidgetType {
    #[default]
    Label,
    /// e.g. a hyperlink
    Link,
    TextEdit,
    Button,
    Checkbox,
    RadioButton,
    /// A group of radio buttons.
    RadioGroup,
    SelectableLabel,
    ComboBox,
    Slider,
    DragValue,
    ColorButton,
    ImageButton,
    Image,
    CollapsingHeader,
    ProgressIndicator,
    Window,
    /// If you cannot fit any of the above slots.
    /// 
    /// If this is something you think should be added, file an issue.
    Other,
}

#[derive(Copy, Clone, Default)]
#[repr(C)]
pub enum EguiWindowLevel {
    #[default]
    Normal,
    AlwaysOnBottom,
    AlwaysOnTop,
}













































































/// State of an [`Area`] that is persisted between frames.
/// 
/// Areas back [`crate::Window`]s and other floating containers,
/// like tooltips and the popups of [`crate::ComboBox`].
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiAreaState {
    /// Last known position of the pivot.
    pub pivot_pos: EguiOptionPos2,
    /* UNIMPLEMENTED (field) 
    /// The anchor point of the area, i.e. where on the area the [`Self::pivot_pos`] refers to.
    pub pivot: crate::Align2,*/
    
    /// Last known size.
    /// 
    /// Area size is intentionally NOT persisted between sessions,
    /// so that a bad tooltip or menu size won't be remembered forever.
    /// A resizable [`crate::Window`] remembers the size the user picked using
    /// the state in the [`crate::Resize`] container.
    pub size: EguiOptionVec2,
    /// If false, clicks goes straight through to what is behind us. Useful for tooltips etc.
    pub interactable: bool,
    /// At what time was this area first shown?
    /// 
    /// Used to fade in the area.
    pub last_became_visible_at: EguiOptionF64,
}


/// A selected text range (could be a range of length zero).
/// 
/// The selection is based on character count (NOT byte count!).
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiCCursorRange {
    /// When selecting with a mouse, this is where the mouse was released.
    /// When moving with e.g. shift+arrows, this is what moves.
    /// Note that the two ends can come in any order, and also be equal (no selection).
    pub primary: EguiCCursor,
    /// When selecting with a mouse, this is where the mouse was first pressed.
    /// This part of the cursor does not move when shift is down.
    pub secondary: EguiCCursor,
}


/// A selected text range (could be a range of length zero).
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiCursorRange {
    /// When selecting with a mouse, this is where the mouse was released.
    /// When moving with e.g. shift+arrows, this is what moves.
    /// Note that the two ends can come in any order, and also be equal (no selection).
    pub primary: EguiCursor,
    /// When selecting with a mouse, this is where the mouse was first pressed.
    /// This part of the cursor does not move when shift is down.
    pub secondary: EguiCursor,
}


/// Options for help debug egui by adding extra visualization
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiDebugOptions {
    /// Always show callstack to ui on hover.
    /// 
    /// Useful for figuring out where in the code some UI is being created.
    /// 
    /// Only works in debug builds.
    /// Requires the `callstack` feature.
    /// Does not work on web.
    pub debug_on_hover: bool,
    /// Show callstack for the current widget on hover if all modifier keys are pressed down.
    /// 
    /// Useful for figuring out where in the code some UI is being created.
    /// 
    /// Only works in debug builds.
    /// Requires the `callstack` feature.
    /// Does not work on web.
    /// 
    /// Default is `true` in debug builds, on native, if the `callstack` feature is enabled.
    pub debug_on_hover_with_all_modifiers: bool,
    /// If we show the hover ui, include where the next widget is placed.
    pub hover_shows_next: bool,
    /// Show which widgets make their parent wider
    pub show_expand_width: bool,
    /// Show which widgets make their parent higher
    pub show_expand_height: bool,
    pub show_resize: bool,
    /// Show an overlay on all interactive widgets.
    pub show_interactive_widgets: bool,
    /// Show interesting widgets under the mouse cursor.
    pub show_widget_hits: bool,
    /// If true, highlight widgets that are not aligned to [`emath::GUI_ROUNDING`].
    /// 
    /// See [`emath::GuiRounding`] for more.
    pub show_unaligned: bool,
}


/// A file dropped into egui.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiDroppedFile {
    /* UNIMPLEMENTED (field) 
    /// Set by the `egui-winit` backend.
    pub path: EguiOptionStd::path::pathBuf,*/
    
    /// Name of the file. Set by the `eframe` web backend.
    pub name: EguiString,
    /// With the `eframe` web backend, this is set to the mime-type of the file (if available).
    pub mime: EguiString,
    /// Set by the `eframe` web backend.
    pub last_modified: EguiOptionPosixTime,
    /* UNIMPLEMENTED (field) 
    /// Set by the `eframe` web backend.
    pub bytes: EguiOptionStd::sync::arc,*/
}


/// Controls which events that a focused widget will have exclusive access to.
/// 
/// Currently this only controls a few special keyboard events,
/// but in the future this `struct` should be extended into a full callback thing.
/// 
/// Any events not covered by the filter are given to the widget, but are not exclusive.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiEventFilter {
    /// If `true`, pressing tab will act on the widget,
    /// and NOT move focus away from the focused widget.
    /// 
    /// Default: `false`
    pub tab: bool,
    /// If `true`, pressing horizontal arrows will act on the
    /// widget, and NOT move focus away from the focused widget.
    /// 
    /// Default: `false`
    pub horizontal_arrows: bool,
    /// If `true`, pressing vertical arrows will act on the
    /// widget, and NOT move focus away from the focused widget.
    /// 
    /// Default: `false`
    pub vertical_arrows: bool,
    /// If `true`, pressing escape will act on the widget,
    /// and NOT surrender focus from the focused widget.
    /// 
    /// Default: `false`
    pub escape: bool,
}


/// A frame around some content, including margin, colors, etc.
/// 
/// ## Definitions
/// The total (outer) size of a frame is
/// `content_size + inner_margin + 2 * stroke.width + outer_margin`.
/// 
/// Everything within the stroke is filled with the fill color (if any).
/// 
/// ```text
/// +-----------------^-------------------------------------- -+
/// |                 | outer_margin                           |
/// |    +------------v----^------------------------------+    |
/// |    |                 | stroke width                 |    |
/// |    |    +------------v---^---------------------+    |    |
/// |    |    |                | inner_margin        |    |    |
/// |    |    |    +-----------v----------------+    |    |    |
/// |    |    |    |             ^              |    |    |    |
/// |    |    |    |             |              |    |    |    |
/// |    |    |    |<------ content_size ------>|    |    |    |
/// |    |    |    |             |              |    |    |    |
/// |    |    |    |             v              |    |    |    |
/// |    |    |    +------- content_rect -------+    |    |    |
/// |    |    |                                      |    |    |
/// |    |    +-------------fill_rect ---------------+    |    |
/// |    |                                                |    |
/// |    +----------------- widget_rect ------------------+    |
/// |                                                          |
/// +---------------------- outer_rect ------------------------+
/// ```
/// 
/// The four rectangles, from inside to outside, are:
/// * `content_rect`: the rectangle that is made available to the inner [`Ui`] or widget.
/// * `fill_rect`: the rectangle that is filled with the fill color (inside the stroke, if any).
/// * `widget_rect`: is the interactive part of the widget (what sense clicks etc).
/// * `outer_rect`: what is allocated in the outer [`Ui`], and is what is returned by [`Response::rect`].
/// 
/// ## Usage
/// 
/// ```
/// # egui::__run_test_ui(|ui| {
/// egui::Frame::none()
///     .fill(egui::Color32::RED)
///     .show(ui, |ui| {
///         ui.label("Label with red background");
///     });
/// # });
/// ```
/// 
/// ## Dynamic color
/// If you want to change the color of the frame based on the response of
/// the widget, you need to break it up into multiple steps:
/// 
/// ```
/// # egui::__run_test_ui(|ui| {
/// let mut frame = egui::Frame::default().inner_margin(4.0).begin(ui);
/// {
///     let response = frame.content_ui.label("Inside the frame");
///     if response.hovered() {
///         frame.frame.fill = egui::Color32::RED;
///     }
/// }
/// frame.end(ui); // Will "close" the frame.
/// # });
/// ```
/// 
/// You can also respond to the hovering of the frame itself:
/// 
/// ```
/// # egui::__run_test_ui(|ui| {
/// let mut frame = egui::Frame::default().inner_margin(4.0).begin(ui);
/// {
///     frame.content_ui.label("Inside the frame");
///     frame.content_ui.label("This too");
/// }
/// let response = frame.allocate_space(ui);
/// if response.hovered() {
///     frame.frame.fill = egui::Color32::RED;
/// }
/// frame.paint(ui);
/// # });
/// ```
/// 
/// Note that you cannot change the margins after calling `begin`.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiFrame {
    /// Margin within the painted frame.
    /// 
    /// Known as `padding` in CSS.
    pub inner_margin: EguiMargin,
    /// The background fill color of the frame, within the [`Self::stroke`].
    /// 
    /// Known as `background` in CSS.
    pub fill: EguiColor32,
    /// The width and color of the outline around the frame.
    /// 
    /// The width of the stroke is part of the total margin/padding of the frame.
    pub stroke: EguiStroke,
    /// The rounding of the _outer_ corner of the [`Self::stroke`]
    /// (or, if there is no stroke, the outer corner of [`Self::fill`]).
    /// 
    /// In other words, this is the corner radius of the _widget rect_.
    pub corner_radius: EguiCornerRadius,
    /// Margin outside the painted frame.
    /// 
    /// Similar to what is called `margin` in CSS.
    /// However, egui does NOT do "Margin Collapse" like in CSS,
    /// i.e. when placing two frames next to each other,
    /// the distance between their borders is the SUM
    /// of their other margins.
    /// In CSS the distance would be the MAX of their outer margins.
    /// Supporting margin collapse is difficult, and would
    /// requires complicating the already complicated egui layout code.
    /// 
    /// Consider using [`crate::Spacing::item_spacing`]
    /// for adding space between widgets.
    pub outer_margin: EguiMargin,
    /// Optional drop-shadow behind the frame.
    pub shadow: EguiShadow,
}


/// A file about to be dropped into egui.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiHoveredFile {
    /* UNIMPLEMENTED (field) 
    /// Set by the `egui-winit` backend.
    pub path: EguiOptionStd::path::pathBuf,*/
    
    /// With the `eframe` web backend, this is set to the mime-type of the file (if available).
    pub mime: EguiString,
}


/// Information about text being edited.
/// 
/// Useful for IME.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiIMEOutput {
    /// Where the [`crate::TextEdit`] is located on screen.
    pub rect: EguiRect,
    /// Where the primary cursor is.
    /// 
    /// This is a very thin rectangle.
    pub cursor_rect: EguiRect,
}


#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiImageOptions {
    /// Select UV range. Default is (0,0) in top-left, (1,1) bottom right.
    pub uv: EguiRect,
    /// A solid color to put behind the image. Useful for transparent images.
    pub bg_fill: EguiColor32,
    /// Multiply image color with this. Default is WHITE (no tint).
    pub tint: EguiColor32,
    /* UNIMPLEMENTED (field) 
    /// Rotate the image about an origin by some angle
    /// 
    /// Positive angle is clockwise.
    /// Origin is a vector in normalized UV space ((0,0) in top-left, (1,1) bottom right).
    /// 
    /// To rotate about the center you can pass `Vec2::splat(0.5)` as the origin.
    /// 
    /// Due to limitations in the current implementation,
    /// this will turn off rounding of the image.
    pub rotation: EguiOptionTuple([resolvedPath(path{Path:"emath::rot2",Id:Id(6930),Args:Some(angleBracketed{Args:[],Constraints:[]})}),ResolvedPath(path{Path:"crate::vec2",Id:Id(150),Args:Some(angleBracketed{Args:[],Constraints:[]})})]),*/
    
    /// Round the corners of the image.
    /// 
    /// The default is no rounding ([`CornerRadius::ZERO`]).
    /// 
    /// Due to limitations in the current implementation,
    /// this will turn off any rotation of the image.
    pub corner_radius: EguiCornerRadius,
}


/// This type determines the constraints on how
/// the size of an image should be calculated.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiImageSize {
    /// Whether or not the final size should maintain the original aspect ratio.
    /// 
    /// This setting is applied last.
    /// 
    /// This defaults to `true`.
    pub maintain_aspect_ratio: bool,
    /// Determines the maximum size of the image.
    /// 
    /// Defaults to `Vec2::INFINITY` (no limit).
    pub max_size: EguiVec2,
    /* UNIMPLEMENTED (field) 
    /// Determines how the image should shrink/expand/stretch/etc. to fit within its allocated space.
    /// 
    /// This setting is applied first.
    /// 
    /// Defaults to `ImageFit::Fraction([1, 1])`
    pub fit: ImageFit,*/
}


/// How and when interaction happens.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiInteraction {
    /// How close a widget must be to the mouse to have a chance to register as a click or drag.
    /// 
    /// If this is larger than zero, it gets easier to hit widgets,
    /// which is important for e.g. touch screens.
    pub interact_radius: f32,
    /// Radius of the interactive area of the side of a window during drag-to-resize.
    pub resize_grab_radius_side: f32,
    /// Radius of the interactive area of the corner of a window during drag-to-resize.
    pub resize_grab_radius_corner: f32,
    /// If `false`, tooltips will show up anytime you hover anything, even if mouse is still moving
    pub show_tooltips_only_when_still: bool,
    /// Delay in seconds before showing tooltips after the mouse stops moving
    pub tooltip_delay: f32,
    /// If you have waited for a tooltip and then hover some other widget within
    /// this many seconds, then show the new tooltip right away,
    /// skipping [`Self::tooltip_delay`].
    /// 
    /// This lets the user quickly move over some dead space to hover the next thing.
    pub tooltip_grace_time: f32,
    /// Can you select the text on a [`crate::Label`] by default?
    pub selectable_labels: bool,
    /// Can the user select text that span multiple labels?
    /// 
    /// The default is `true`, but text selection can be slightly glitchy,
    /// so you may want to disable it.
    pub multi_widget_text_select: bool,
}


/// A keyboard shortcut, e.g. `Ctrl+Alt+W`.
/// 
/// Can be used with [`crate::InputState::consume_shortcut`]
/// and [`crate::Context::format_shortcut`].
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiKeyboardShortcut {
    pub modifiers: EguiModifiers,
    pub logical_key: EguiKey,
}


/// An identifier for a paint layer.
/// Also acts as an identifier for [`crate::Area`]:s.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiLayerId {
    pub order: EguiOrder,
    pub id: EguiId,
}


/// The layout of a [`Ui`][`crate::Ui`], e.g. "vertical & centered".
/// 
/// ```
/// # egui::__run_test_ui(|ui| {
/// ui.with_layout(egui::Layout::right_to_left(egui::Align::TOP), |ui| {
///     ui.label("world!");
///     ui.label("Hello");
/// });
/// # });
/// ```
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiLayout {
    /// Main axis direction
    pub main_dir: EguiDirection,
    /// If true, wrap around when reading the end of the main direction.
    /// For instance, for `main_dir == Direction::LeftToRight` this will
    /// wrap to a new row when we reach the right side of the `max_rect`.
    pub main_wrap: bool,
    /// How to align things on the main axis.
    pub main_align: EguiAlign,
    /// Justify the main axis?
    pub main_justify: bool,
    /// How to align things on the cross axis.
    /// For vertical layouts: put things to left, center or right?
    /// For horizontal layouts: put things to top, center or bottom?
    pub cross_align: EguiAlign,
    /// Justify the cross axis?
    /// For vertical layouts justify mean all widgets get maximum width.
    /// For horizontal layouts justify mean all widgets get maximum height.
    pub cross_justify: bool,
}


/// Menu root associated with an Id from a Response
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiMenuRoot {
    /* UNIMPLEMENTED (field) 
    pub menu_state: std::sync::Arc,*/
    
    pub id: EguiId,
}


/// Names of different modifier keys.
/// 
/// Used to name modifiers.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiModifierNames {
    pub is_short: bool,
    /* UNIMPLEMENTED (field) 
    pub alt: str,*/
    
    /* UNIMPLEMENTED (field) 
    pub ctrl: str,*/
    
    /* UNIMPLEMENTED (field) 
    pub shift: str,*/
    
    /* UNIMPLEMENTED (field) 
    pub mac_cmd: str,*/
    
    /* UNIMPLEMENTED (field) 
    pub mac_alt: str,*/
    
    /* UNIMPLEMENTED (field) 
    /// What goes between the names
    pub concat: str,*/
}


/// State of the modifier keys. These must be fed to egui.
/// 
/// The best way to compare [`Modifiers`] is by using [`Modifiers::matches`].
/// 
/// NOTE: For cross-platform uses, ALT+SHIFT is a bad combination of modifiers
/// as on mac that is how you type special characters,
/// so those key presses are usually not reported to egui.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiModifiers {
    /// Either of the alt keys are down (option ⌥ on Mac).
    pub alt: bool,
    /// Either of the control keys are down.
    /// When checking for keyboard shortcuts, consider using [`Self::command`] instead.
    pub ctrl: bool,
    /// Either of the shift keys are down.
    pub shift: bool,
    /// The Mac ⌘ Command key. Should always be set to `false` on other platforms.
    pub mac_cmd: bool,
    /// On Windows and Linux, set this to the same value as `ctrl`.
    /// On Mac, this should be set whenever one of the ⌘ Command keys are down (same as `mac_cmd`).
    /// This is so that egui can, for instance, select all text by checking for `command + A`
    /// and it will work on both Mac and Windows.
    pub command: bool,
}


/// All you probably need to know about a multi-touch gesture.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiMultiTouchInfo {
    /// Point in time when the gesture started.
    pub start_time: f64,
    /// Position of the pointer at the time the gesture started.
    pub start_pos: EguiPos2,
    /// Center position of the current gesture (average of all touch points).
    pub center_pos: EguiPos2,
    /// Number of touches (fingers) on the surface. Value is ≥ 2 since for a single touch no
    /// [`MultiTouchInfo`] is created.
    pub num_touches: usize,
    /// Proportional zoom factor (pinch gesture).
    /// * `zoom = 1`: no change
    /// * `zoom < 1`: pinch together
    /// * `zoom > 1`: pinch spread
    pub zoom_delta: f32,
    /// 2D non-proportional zoom factor (pinch gesture).
    /// 
    /// For horizontal pinches, this will return `[z, 1]`,
    /// for vertical pinches this will return `[1, z]`,
    /// and otherwise this will return `[z, z]`,
    /// where `z` is the zoom factor:
    /// * `zoom = 1`: no change
    /// * `zoom < 1`: pinch together
    /// * `zoom > 1`: pinch spread
    pub zoom_delta_2d: EguiVec2,
    /// Rotation in radians. Moving fingers around each other will change this value. This is a
    /// relative value, comparing the orientation of fingers in the current frame with the previous
    /// frame. If all fingers are resting, this value is `0.0`.
    pub rotation_delta: f32,
    /// Relative movement (comparing previous frame and current frame) of the average position of
    /// all touch points. Without movement this value is `Vec2::ZERO`.
    /// 
    /// Note that this may not necessarily be measured in screen points (although it _will_ be for
    /// most mobile devices). In general (depending on the touch device), touch coordinates cannot
    /// be directly mapped to the screen. A touch always is considered to start at the position of
    /// the pointer, but touch movement is always measured in the units delivered by the device,
    /// and may depend on hardware and system settings.
    pub translation_delta: EguiVec2,
    /// Current force of the touch (average of the forces of the individual fingers). This is a
    /// value in the interval `[0.0 .. =1.0]`.
    /// 
    /// Note 1: A value of `0.0` either indicates a very light touch, or it means that the device
    /// is not capable of measuring the touch force at all.
    /// 
    /// Note 2: Just increasing the physical pressure without actually moving the finger may not
    /// necessarily lead to a change of this value.
    pub force: f32,
}


/// What URL to open, and how.
/// 
/// Use with [`crate::Context::open_url`].
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiOpenUrl {
    pub url: EguiString,
    /// If `true`, open the url in a new tab.
    /// If `false` open it in the same tab.
    /// Only matters when in a web browser.
    pub new_tab: bool,
}


#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiPCursorRange {
    /// When selecting with a mouse, this is where the mouse was released.
    /// When moving with e.g. shift+arrows, this is what moves.
    /// Note that the two ends can come in any order, and also be equal (no selection).
    pub primary: EguiPCursor,
    /// When selecting with a mouse, this is where the mouse was first pressed.
    /// This part of the cursor does not move when shift is down.
    pub secondary: EguiPCursor,
}


/// The non-rendering part of what egui emits each frame.
/// 
/// You can access (and modify) this with [`crate::Context::output`].
/// 
/// The backend should use this.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiPlatformOutput {
    /* UNIMPLEMENTED (field) 
    /// Commands that the egui integration should execute at the end of a frame.
    pub commands: VEC_TY_TODO(OutputCommand),*/
    
    /// Set the cursor to this icon.
    pub cursor_icon: EguiCursorIcon,
    /// If set, open this url.
    pub open_url: EguiOptionOpenUrl,
    /// If set, put this text in the system clipboard. Ignore if empty.
    /// 
    /// This is often a response to [`crate::Event::Copy`] or [`crate::Event::Cut`].
    /// 
    /// ```
    /// # egui::__run_test_ui(|ui| {
    /// if ui.button("📋").clicked() {
    ///     ui.output_mut(|o| o.copied_text = "some_text".to_string());
    /// }
    /// # });
    /// ```
    pub copied_text: EguiString,
    /* UNIMPLEMENTED (field) 
    /// Events that may be useful to e.g. a screen reader.
    pub events: VEC_TY_TODO(OutputEvent),*/
    
    /// Is there a mutable [`TextEdit`](crate::TextEdit) under the cursor?
    /// Use by `eframe` web to show/hide mobile keyboard and IME agent.
    pub mutable_text_under_cursor: bool,
    /// This is set if, and only if, the user is currently editing text.
    /// 
    /// Useful for IME.
    pub ime: EguiOptionImeOutput,
    /// How many ui passes is this the sum of?
    /// 
    /// See [`crate::Context::request_discard`] for details.
    /// 
    /// This is incremented at the END of each frame,
    /// so this will be `0` for the first pass.
    pub num_completed_passes: usize,
    /* UNIMPLEMENTED (field) 
    /// Was [`crate::Context::request_discard`] called during the latest pass?
    /// 
    /// If so, what was the reason(s) for it?
    /// 
    /// If empty, there was never any calls.
    pub request_discard_reasons: VEC_TY_TODO(EguiRepaintCause),*/
}


/// What the integrations provides to egui at the start of each frame.
/// 
/// Set the values that make sense, leave the rest at their `Default::default()`.
/// 
/// You can check if `egui` is using the inputs using
/// [`crate::Context::wants_pointer_input`] and [`crate::Context::wants_keyboard_input`].
/// 
/// All coordinates are in points (logical pixels) with origin (0, 0) in the top left .corner.
/// 
/// Ii "points" can be calculated from native physical pixels
/// using `pixels_per_point` = [`crate::Context::zoom_factor`] * `native_pixels_per_point`;
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiRawInput {
    /* UNIMPLEMENTED (field) 
    /// The id of the active viewport.
    pub viewport_id: crate::ViewportId,*/
    
    /* UNIMPLEMENTED (field) 
    /// Information about all egui viewports.
    pub viewports: crate::ViewportIdMap,*/
    
    /// Position and size of the area that egui should use, in points.
    /// Usually you would set this to
    /// 
    /// `Some(Rect::from_min_size(Default::default(), screen_size_in_points))`.
    /// 
    /// but you could also constrain egui to some smaller portion of your window if you like.
    /// 
    /// `None` will be treated as "same as last frame", with the default being a very big area.
    pub screen_rect: EguiOptionRect,
    /// Maximum size of one side of the font texture.
    /// 
    /// Ask your graphics drivers about this. This corresponds to `GL_MAX_TEXTURE_SIZE`.
    /// 
    /// The default is a very small (but very portable) 2048.
    pub max_texture_side: EguiOptionUsize,
    /// Monotonically increasing time, in seconds. Relative to whatever. Used for animations.
    /// If `None` is provided, egui will assume a time delta of `predicted_dt` (default 1/60 seconds).
    pub time: EguiOptionF64,
    /// Should be set to the expected time between frames when painting at vsync speeds.
    /// The default for this is 1/60.
    /// Can safely be left at its default value.
    pub predicted_dt: f32,
    /// Which modifier keys are down at the start of the frame?
    pub modifiers: EguiModifiers,
    /* UNIMPLEMENTED (field) 
    /// In-order events received this frame.
    /// 
    /// There is currently no way to know if egui handles a particular event,
    /// but you can check if egui is using the keyboard with [`crate::Context::wants_keyboard_input`]
    /// and/or the pointer (mouse/touch) with [`crate::Context::is_using_pointer`].
    pub events: VEC_TY_TODO(Event),*/
    
    /* UNIMPLEMENTED (field) 
    /// Dragged files hovering over egui.
    pub hovered_files: VEC_TY_TODO(EguiHoveredFile),*/
    
    /* UNIMPLEMENTED (field) 
    /// Dragged files dropped into egui.
    /// 
    /// Note: when using `eframe` on Windows, this will always be empty if drag-and-drop support has
    /// been disabled in [`crate::viewport::ViewportBuilder`].
    pub dropped_files: VEC_TY_TODO(EguiDroppedFile),*/
    
    /// The native window has the keyboard focus (i.e. is receiving key presses).
    /// 
    /// False when the user alt-tab away from the application, for instance.
    pub focused: bool,
    /// Does the OS use dark or light mode?
    /// 
    /// `None` means "don't know".
    pub system_theme: EguiOptionTheme,
}


/// What called [`Context::request_repaint`] or [`Context::request_discard`]?
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiRepaintCause {
    /* UNIMPLEMENTED (field) 
    /// What file had the call that requested the repaint?
    pub file: str,*/
    
    /// What line number of the call that requested the repaint?
    pub line: u32,
    /* UNIMPLEMENTED (field) 
    /// Explicit reason; human readable.
    pub reason: std::borrow::Cow,*/
}


/// Information given to the backend about when it is time to repaint the ui.
/// 
/// This is given in the callback set by [`Context::set_request_repaint_callback`].
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiRequestRepaintInfo {
    /* UNIMPLEMENTED (field) 
    /// This is used to specify what viewport that should repaint.
    pub viewport_id: crate::ViewportId,*/
    
    /// Repaint after this duration. If zero, repaint as soon as possible.
    pub delay: EguiDuration,
    /// The number of fully completed passes, of the entire lifetime of the [`Context`].
    /// 
    /// This can be compared to [`Context::cumulative_pass_nr`] to see if we we still
    /// need another repaint (ui pass / frame), or if one has already happened.
    pub current_cumulative_pass_nr: u64,
}


#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiRowVertexIndices {
    pub row: usize,
    /* UNIMPLEMENTED (field) 
    pub vertex_indices: Array { type_: Primitive("u32"), len: "6" },*/
}


/// Scroll animation configuration, used when programmatically scrolling somewhere (e.g. with `[crate::Ui::scroll_to_cursor]`)
/// The animation duration is calculated based on the distance to be scrolled via `[ScrollAnimation::points_per_second]`
/// and can be clamped to a min / max duration via `[ScrollAnimation::duration]`.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiScrollAnimation {
    /// With what speed should we scroll? (Default: 1000.0)
    pub points_per_second: f32,
    /// The min / max scroll duration.
    pub duration: EguiRangef,
}


/// Controls the spacing and visuals of a [`crate::ScrollArea`].
/// 
/// There are three presets to chose from:
/// * [`Self::solid`]
/// * [`Self::thin`]
/// * [`Self::floating`]
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiScrollStyle {
    /// If `true`, scroll bars float above the content, partially covering it.
    /// 
    /// If `false`, the scroll bars allocate space, shrinking the area
    /// available to the contents.
    /// 
    /// This also changes the colors of the scroll-handle to make
    /// it more promiment.
    pub floating: bool,
    /// The width of the scroll bars at it largest.
    pub bar_width: f32,
    /// Make sure the scroll handle is at least this big
    pub handle_min_length: f32,
    /// Margin between contents and scroll bar.
    pub bar_inner_margin: f32,
    /// Margin between scroll bar and the outer container (e.g. right of a vertical scroll bar).
    /// Only makes sense for non-floating scroll bars.
    pub bar_outer_margin: f32,
    /// The thin width of floating scroll bars that the user is NOT hovering.
    /// 
    /// When the user hovers the scroll bars they expand to [`Self::bar_width`].
    pub floating_width: f32,
    /// How much space is allocated for a floating scroll bar?
    /// 
    /// Normally this is zero, but you could set this to something small
    /// like 4.0 and set [`Self::dormant_handle_opacity`] and
    /// [`Self::dormant_background_opacity`] to e.g. 0.5
    /// so as to always show a thin scroll bar.
    pub floating_allocated_width: f32,
    /// If true, use colors with more contrast. Good for floating scroll bars.
    pub foreground_color: bool,
    /// The opaqueness of the background when the user is neither scrolling
    /// nor hovering the scroll area.
    /// 
    /// This is only for floating scroll bars.
    /// Solid scroll bars are always opaque.
    pub dormant_background_opacity: f32,
    /// The opaqueness of the background when the user is hovering
    /// the scroll area, but not the scroll bar.
    /// 
    /// This is only for floating scroll bars.
    /// Solid scroll bars are always opaque.
    pub active_background_opacity: f32,
    /// The opaqueness of the background when the user is hovering
    /// over the scroll bars.
    /// 
    /// This is only for floating scroll bars.
    /// Solid scroll bars are always opaque.
    pub interact_background_opacity: f32,
    /// The opaqueness of the handle when the user is neither scrolling
    /// nor hovering the scroll area.
    /// 
    /// This is only for floating scroll bars.
    /// Solid scroll bars are always opaque.
    pub dormant_handle_opacity: f32,
    /// The opaqueness of the handle when the user is hovering
    /// the scroll area, but not the scroll bar.
    /// 
    /// This is only for floating scroll bars.
    /// Solid scroll bars are always opaque.
    pub active_handle_opacity: f32,
    /// The opaqueness of the handle when the user is hovering
    /// over the scroll bars.
    /// 
    /// This is only for floating scroll bars.
    /// Solid scroll bars are always opaque.
    pub interact_handle_opacity: f32,
}


/// Selected text, selected elements etc
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiSelection {
    pub bg_fill: EguiColor32,
    pub stroke: EguiStroke,
}


#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiSettings {
    /// Maximum number of undos.
    /// If your state is resource intensive, you should keep this low.
    /// 
    /// Default: `100`
    pub max_undos: usize,
    /// When that state hasn't changed for this many seconds,
    /// create a new undo point (if one is needed).
    /// 
    /// Default value: `1.0` seconds.
    pub stable_time: f32,
    /// If the state is changing so often that we never get to `stable_time`,
    /// then still create a save point every `auto_save_interval` seconds,
    /// so we have something to undo to.
    /// 
    /// Default value: `30` seconds.
    pub auto_save_interval: f32,
}


/// A texture with a known size.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiSizedTexture {
    pub id: EguiTextureId,
    pub size: EguiVec2,
}


/// Controls the sizes and distances between widgets.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiSpacing {
    /// Horizontal and vertical spacing between widgets.
    /// 
    /// To add extra space between widgets, use [`Ui::add_space`].
    /// 
    /// `item_spacing` is inserted _after_ adding a widget, so to increase the spacing between
    /// widgets `A` and `B` you need to change `item_spacing` before adding `A`.
    pub item_spacing: EguiVec2,
    /// Horizontal and vertical margins within a window frame.
    pub window_margin: EguiMargin,
    /// Button size is text size plus this on each side
    pub button_padding: EguiVec2,
    /// Horizontal and vertical margins within a menu frame.
    pub menu_margin: EguiMargin,
    /// Indent collapsing regions etc by this much.
    pub indent: f32,
    /// Minimum size of a [`DragValue`], color picker button, and other small widgets.
    /// `interact_size.y` is the default height of button, slider, etc.
    /// Anything clickable should be (at least) this size.
    pub interact_size: EguiVec2,
    /// Default width of a [`Slider`].
    pub slider_width: f32,
    /// Default rail height of a [`Slider`].
    pub slider_rail_height: f32,
    /// Default (minimum) width of a [`ComboBox`].
    pub combo_width: f32,
    /// Default width of a [`crate::TextEdit`].
    pub text_edit_width: f32,
    /// Checkboxes, radio button and collapsing headers have an icon at the start.
    /// This is the width/height of the outer part of this icon (e.g. the BOX of the checkbox).
    pub icon_width: f32,
    /// Checkboxes, radio button and collapsing headers have an icon at the start.
    /// This is the width/height of the inner part of this icon (e.g. the check of the checkbox).
    pub icon_width_inner: f32,
    /// Checkboxes, radio button and collapsing headers have an icon at the start.
    /// This is the spacing between the icon and the text
    pub icon_spacing: f32,
    /// The size used for the [`Ui::max_rect`] the first frame.
    /// 
    /// Text will wrap at this width, and images that expand to fill the available space
    /// will expand to this size.
    /// 
    /// If the contents are smaller than this size, the area will shrink to fit the contents.
    /// If the contents overflow, the area will grow.
    pub default_area_size: EguiVec2,
    /// Width of a tooltip (`on_hover_ui`, `on_hover_text` etc).
    pub tooltip_width: f32,
    /// The default wrapping width of a menu.
    /// 
    /// Items longer than this will wrap to a new line.
    pub menu_width: f32,
    /// Horizontal distance between a menu and a submenu.
    pub menu_spacing: f32,
    /// End indented regions with a horizontal line
    pub indent_ends_with_horizontal_line: bool,
    /// Height of a combo-box before showing scroll bars.
    pub combo_height: f32,
    /// Controls the spacing of a [`crate::ScrollArea`].
    pub scroll: EguiScrollStyle,
}


/// Specifies the look and feel of egui.
/// 
/// You can change the visuals of a [`Ui`] with [`Ui::style_mut`]
/// and of everything with [`crate::Context::set_style_of`].
/// To choose between dark and light style, use [`crate::Context::set_theme`].
/// 
/// If you want to change fonts, use [`crate::Context::set_fonts`] instead.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiStyle {
    /* UNIMPLEMENTED (field) 
    /// If set this will change the default [`TextStyle`] for all widgets.
    /// 
    /// On most widgets you can also set an explicit text style,
    /// which will take precedence over this.
    pub override_text_style: EguiOptionTextStyle,*/
    
    /* UNIMPLEMENTED (field) 
    /// If set this will change the font family and size for all widgets.
    /// 
    /// On most widgets you can also set an explicit text style,
    /// which will take precedence over this.
    pub override_font_id: EguiOptionCrate::fontId,*/
    
    /// How to vertically align text.
    /// 
    /// Set to `None` to use align that depends on the current layout.
    pub override_text_valign: EguiOptionAlign,
    /* UNIMPLEMENTED (field) 
    /// The [`FontFamily`] and size you want to use for a specific [`TextStyle`].
    /// 
    /// The most convenient way to look something up in this is to use [`TextStyle::resolve`].
    /// 
    /// If you would like to overwrite app `text_styles`
    /// 
    /// ```
    /// # let mut ctx = egui::Context::default();
    /// use egui::FontFamily::Proportional;
    /// use egui::FontId;
    /// use egui::TextStyle::*;
    /// use std::collections::BTreeMap;
    /// 
    /// // Redefine text_styles
    /// let text_styles: BTreeMap<_, _> = [
    ///   (Heading, FontId::new(30.0, Proportional)),
    ///   (Name("Heading2".into()), FontId::new(25.0, Proportional)),
    ///   (Name("Context".into()), FontId::new(23.0, Proportional)),
    ///   (Body, FontId::new(18.0, Proportional)),
    ///   (Monospace, FontId::new(14.0, Proportional)),
    ///   (Button, FontId::new(14.0, Proportional)),
    ///   (Small, FontId::new(10.0, Proportional)),
    /// ].into();
    /// 
    /// // Mutate global styles with new text styles
    /// ctx.all_styles_mut(move |style| style.text_styles = text_styles.clone());
    /// ```
    pub text_styles: std::collections::BTreeMap,*/
    
    /* UNIMPLEMENTED (field) 
    /// The style to use for [`DragValue`] text.
    pub drag_value_text_style: TextStyle,*/
    
    /* UNIMPLEMENTED (field) 
    /// How to format numbers as strings, e.g. in a [`crate::DragValue`].
    /// 
    /// You can override this to e.g. add thousands separators.
    pub number_formatter: NumberFormatter,*/
    
    /// If set, labels, buttons, etc. will use this to determine whether to wrap the text at the
    /// right edge of the [`Ui`] they are in. By default, this is `None`.
    /// 
    /// **Note**: this API is deprecated, use `wrap_mode` instead.
    /// 
    /// * `None`: use `wrap_mode` instead
    /// * `Some(true)`: wrap mode defaults to [`crate::TextWrapMode::Wrap`]
    /// * `Some(false)`: wrap mode defaults to [`crate::TextWrapMode::Extend`]
    pub wrap: EguiOptionBool,
    /* UNIMPLEMENTED (field) 
    /// If set, labels, buttons, etc. will use this to determine whether to wrap or truncate the
    /// text at the right edge of the [`Ui`] they are in, or to extend it. By default, this is
    /// `None`.
    /// 
    /// * `None`: follow layout (with may wrap)
    /// * `Some(mode)`: use the specified mode as default
    pub wrap_mode: EguiOptionCrate::textWrapMode,*/
    
    /// Sizes and distances between widgets
    pub spacing: EguiSpacing,
    /// How and when interaction happens.
    pub interaction: EguiInteraction,
    /// Colors etc.
    pub visuals: EguiVisuals,
    /// How many seconds a typical animation should last.
    pub animation_time: f32,
    /// Options to help debug why egui behaves strangely.
    /// 
    /// Only available in debug builds.
    pub debug: EguiDebugOptions,
    /// Show tooltips explaining [`DragValue`]:s etc when hovered.
    /// 
    /// This only affects a few egui widgets.
    pub explanation_tooltips: bool,
    /// Show the URL of hyperlinks in a tooltip when hovered.
    pub url_in_tooltip: bool,
    /// If true and scrolling is enabled for only one direction, allow horizontal scrolling without pressing shift
    pub always_scroll_the_only_direction: bool,
    /// The animation that should be used when scrolling a [`crate::ScrollArea`] using e.g. [`Ui::scroll_to_rect`].
    pub scroll_animation: EguiScrollAnimation,
}


/// Look and feel of the text cursor.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiTextCursorStyle {
    /// The color and width of the text cursor
    pub stroke: EguiStroke,
    /// Show where the text cursor would be if you clicked?
    pub preview: bool,
    /// Should the cursor blink?
    pub blink: bool,
    /// When blinking, this is how long the cursor is visible.
    pub on_duration: f32,
    /// When blinking, this is how long the cursor is invisible.
    pub off_duration: f32,
}


/// Build a [`Ui`] as the child of another [`Ui`].
/// 
/// By default, everything is inherited from the parent,
/// except for `max_rect` which by default is set to
/// the parent [`Ui::available_rect_before_wrap`].
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiUiBuilder {
    pub id_salt: EguiOptionId,
    pub ui_stack_info: EguiUiStackInfo,
    pub layer_id: EguiOptionLayerId,
    pub max_rect: EguiOptionRect,
    pub layout: EguiOptionLayout,
    pub disabled: bool,
    pub invisible: bool,
    pub sizing_pass: bool,
    /* UNIMPLEMENTED (field) 
    pub style: EguiOptionStd::sync::arc,*/
    
    pub sense: EguiOptionSense,
}


/// Information about a [`crate::Ui`] to be included in the corresponding [`UiStack`].
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiUiStackInfo {
    pub kind: EguiOptionUiKind,
    pub frame: EguiFrame,
    /* UNIMPLEMENTED (field) 
    pub tags: UiTags,*/
}


/// A wrapper around `dyn Any`, used for passing custom user data
/// to [`crate::ViewportCommand::Screenshot`].
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiUserData {
    /* UNIMPLEMENTED (field) 
    /// A user value given to the screenshot command,
    /// that will be returned in [`crate::Event::Screenshot`].
    pub data: EguiOptionStd::sync::arc,*/
}


/// A pair of [`ViewportId`], used to identify a viewport and its parent.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiViewportIdPair {
    /* UNIMPLEMENTED (field) 
    pub this: ViewportId,*/
    
    /* UNIMPLEMENTED (field) 
    pub parent: ViewportId,*/
}


/// Information about the current viewport, given as input each frame.
/// 
/// `None` means "unknown".
/// 
/// All units are in ui "points", which can be calculated from native physical pixels
/// using `pixels_per_point` = [`crate::Context::zoom_factor`] * `[Self::native_pixels_per_point`];
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiViewportInfo {
    /* UNIMPLEMENTED (field) 
    /// Parent viewport, if known.
    pub parent: EguiOptionCrate::viewportId,*/
    
    /// Name of the viewport, if known.
    pub title: EguiOptionString,
    /* UNIMPLEMENTED (field) 
    pub events: VEC_TY_TODO(EguiViewportEvent),*/
    
    /// The OS native pixels-per-point.
    /// 
    /// This should always be set, if known.
    /// 
    /// On web this takes browser scaling into account,
    /// and corresponds to [`window.devicePixelRatio`](https://developer.mozilla.org/en-US/docs/Web/API/Window/devicePixelRatio) in JavaScript.
    pub native_pixels_per_point: EguiOptionF32,
    /// Current monitor size in egui points.
    pub monitor_size: EguiOptionVec2,
    /// The inner rectangle of the native window, in monitor space and ui points scale.
    /// 
    /// This is the content rectangle of the viewport.
    pub inner_rect: EguiOptionRect,
    /// The outer rectangle of the native window, in monitor space and ui points scale.
    /// 
    /// This is the content rectangle plus decoration chrome.
    pub outer_rect: EguiOptionRect,
    /// Are we minimized?
    pub minimized: EguiOptionBool,
    /// Are we maximized?
    pub maximized: EguiOptionBool,
    /// Are we in fullscreen mode?
    pub fullscreen: EguiOptionBool,
    /// Is the window focused and able to receive input?
    /// 
    /// This should be the same as [`RawInput::focused`].
    pub focused: EguiOptionBool,
}


/// Controls the visual style (colors etc) of egui.
/// 
/// You can change the visuals of a [`Ui`] with [`Ui::visuals_mut`]
/// and of everything with [`crate::Context::set_visuals_of`].
/// 
/// If you want to change fonts, use [`crate::Context::set_fonts`] instead.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiVisuals {
    /// If true, the visuals are overall dark with light text.
    /// If false, the visuals are overall light with dark text.
    /// 
    /// NOTE: setting this does very little by itself,
    /// this is more to provide a convenient summary of the rest of the settings.
    pub dark_mode: bool,
    /// Override default text color for all text.
    /// 
    /// This is great for setting the color of text for any widget.
    /// 
    /// If `text_color` is `None` (default), then the text color will be the same as the
    /// foreground stroke color (`WidgetVisuals::fg_stroke`)
    /// and will depend on whether or not the widget is being interacted with.
    /// 
    /// In the future we may instead modulate
    /// the `text_color` based on whether or not it is interacted with
    /// so that `visuals.text_color` is always used,
    /// but its alpha may be different based on whether or not
    /// it is disabled, non-interactive, hovered etc.
    pub override_text_color: EguiOptionColor32,
    /// Visual styles of widgets
    pub widgets: EguiWidgets,
    pub selection: EguiSelection,
    /// The color used for [`crate::Hyperlink`],
    pub hyperlink_color: EguiColor32,
    /// Something just barely different from the background color.
    /// Used for [`crate::Grid::striped`].
    pub faint_bg_color: EguiColor32,
    /// Very dark or light color (for corresponding theme).
    /// Used as the background of text edits, scroll bars and others things
    /// that needs to look different from other interactive stuff.
    pub extreme_bg_color: EguiColor32,
    /// Background color behind code-styled monospaced labels.
    pub code_bg_color: EguiColor32,
    /// A good color for warning text (e.g. orange).
    pub warn_fg_color: EguiColor32,
    /// A good color for error text (e.g. red).
    pub error_fg_color: EguiColor32,
    pub window_corner_radius: EguiCornerRadius,
    pub window_shadow: EguiShadow,
    pub window_fill: EguiColor32,
    pub window_stroke: EguiStroke,
    /// Highlight the topmost window.
    pub window_highlight_topmost: bool,
    pub menu_corner_radius: EguiCornerRadius,
    /// Panel background color
    pub panel_fill: EguiColor32,
    pub popup_shadow: EguiShadow,
    pub resize_corner_size: f32,
    /// How the text cursor acts.
    pub text_cursor: EguiTextCursorStyle,
    /// Allow child widgets to be just on the border and still have a stroke with some thickness
    pub clip_rect_margin: f32,
    /// Show a background behind buttons.
    pub button_frame: bool,
    /// Show a background behind collapsing headers.
    pub collapsing_header_frame: bool,
    /// Draw a vertical lien left of indented region, in e.g. [`crate::CollapsingHeader`].
    pub indent_has_left_vline: bool,
    /// Whether or not Grids and Tables should be striped by default
    /// (have alternating rows differently colored).
    pub striped: bool,
    /// Show trailing color behind the circle of a [`Slider`]. Default is OFF.
    /// 
    /// Enabling this will affect ALL sliders, and can be enabled/disabled per slider with [`Slider::trailing_fill`].
    pub slider_trailing_fill: bool,
    /// Shape of the handle for sliders and similar widgets.
    /// 
    /// Changing this will affect ALL sliders, and can be enabled/disabled per slider with [`Slider::handle_shape`].
    pub handle_shape: EguiHandleShape,
    /// Should the cursor change when the user hovers over an interactive/clickable item?
    /// 
    /// This is consistent with a lot of browser-based applications (vscode, github
    /// all turn your cursor into [`CursorIcon::PointingHand`] when a button is
    /// hovered) but it is inconsistent with native UI toolkits.
    pub interact_cursor: EguiOptionCursorIcon,
    /// Show a spinner when loading an image.
    pub image_loading_spinners: bool,
    /// How to display numeric color values.
    pub numeric_color_space: EguiNumericColorSpace,
}


/// Describes a widget such as a [`crate::Button`] or a [`crate::TextEdit`].
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiWidgetInfo {
    /// The type of widget this is.
    pub typ: EguiWidgetType,
    /// Whether the widget is enabled.
    pub enabled: bool,
    /// The text on labels, buttons, checkboxes etc.
    pub label: EguiOptionString,
    /// The contents of some editable text (for [`TextEdit`](crate::TextEdit) fields).
    pub current_text_value: EguiOptionString,
    /// The previous text value.
    pub prev_text_value: EguiOptionString,
    /// The current value of checkboxes and radio buttons.
    pub selected: EguiOptionBool,
    /// The current value of sliders etc.
    pub value: EguiOptionF64,
    /// Selected range of characters in [`Self::current_text_value`].
    pub text_selection: EguiOptionRangeInclusive,
}


/// Used to store each widget's [Id], [Rect] and [Sense] each frame.
/// 
/// Used to check which widget gets input when a user clicks somewhere.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiWidgetRect {
    /// The globally unique widget id.
    /// 
    /// For interactive widgets, this better be globally unique.
    /// If not there will be weird bugs,
    /// and also big red warning test on the screen in debug builds
    /// (see [`crate::Options::warn_on_id_clash`]).
    /// 
    /// You can ensure globally unique ids using [`crate::Ui::push_id`].
    pub id: EguiId,
    /// What layer the widget is on.
    pub layer_id: EguiLayerId,
    /// The full widget rectangle, in local layer coordinates.
    pub rect: EguiRect,
    /// Where the widget is, in local layer coordinates.
    /// 
    /// This is after clipping with the parent ui clip rect.
    pub interact_rect: EguiRect,
    /// How the widget responds to interaction.
    /// 
    /// Note: if [`Self::enabled`] is `false`, then
    /// the widget _effectively_ doesn't sense anything,
    /// but can still have the same `Sense`.
    /// This is because the sense informs the styling of the widget,
    /// but we don't want to change the style when a widget is disabled
    /// (that is handled by the `Painter` directly).
    pub sense: EguiSense,
    /// Is the widget enabled?
    pub enabled: bool,
}


/// bg = background, fg = foreground.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiWidgetVisuals {
    /// Background color of widgets that must have a background fill,
    /// such as the slider background, a checkbox background, or a radio button background.
    /// 
    /// Must never be [`Color32::TRANSPARENT`].
    pub bg_fill: EguiColor32,
    /// Background color of widgets that can _optionally_ have a background fill, such as buttons.
    /// 
    /// May be [`Color32::TRANSPARENT`].
    pub weak_bg_fill: EguiColor32,
    /// For surrounding rectangle of things that need it,
    /// like buttons, the box of the checkbox, etc.
    /// Should maybe be called `frame_stroke`.
    pub bg_stroke: EguiStroke,
    /// Button frames etc.
    pub corner_radius: EguiCornerRadius,
    /// Stroke and text color of the interactive part of a component (button text, slider grab, check-mark, …).
    pub fg_stroke: EguiStroke,
    /// Make the frame this much larger.
    pub expansion: f32,
}


/// The visuals of widgets for different states of interaction.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiWidgets {
    /// The style of a widget that you cannot interact with.
    /// * `noninteractive.bg_stroke` is the outline of windows.
    /// * `noninteractive.bg_fill` is the background color of windows.
    /// * `noninteractive.fg_stroke` is the normal text color.
    pub noninteractive: EguiWidgetVisuals,
    /// The style of an interactive widget, such as a button, at rest.
    pub inactive: EguiWidgetVisuals,
    /// The style of an interactive widget while you hover it, or when it is highlighted.
    /// 
    /// See [`Response::hovered`], [`Response::highlighted`] and [`Response::highlight`].
    pub hovered: EguiWidgetVisuals,
    /// The style of an interactive widget as you are clicking or dragging it.
    pub active: EguiWidgetVisuals,
    /// The style of a button that has an open menu beneath it (e.g. a combo-box)
    pub open: EguiWidgetVisuals,
}


/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiOptionPosixTime {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: EguiPosixTime,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiOptionString {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: EguiString,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiOptionCollapsingState {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: EguiCollapsingState,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiOptionState {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: EguiState,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiOptionTextEditState {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: EguiTextEditState,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiOptionCursorIcon {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: EguiCursorIcon,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiOptionTheme {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: EguiTheme,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiOptionUiKind {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: EguiUiKind,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiOptionBool {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: bool,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiOptionF32 {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: f32,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiOptionF64 {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: f64,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiOptionUsize {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: usize,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiOptionAlign {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: EguiAlign,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiOptionAreaState {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: EguiAreaState,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiOptionCCursorRange {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: EguiCCursorRange,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiOptionColor32 {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: EguiColor32,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiOptionCursor {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: EguiCursor,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiOptionCursorRange {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: EguiCursorRange,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiOptionFrame {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: EguiFrame,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiOptionImeOutput {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: EguiIMEOutput,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiOptionId {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: EguiId,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiOptionLayerId {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: EguiLayerId,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiOptionLayout {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: EguiLayout,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiOptionMultiTouchInfo {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: EguiMultiTouchInfo,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiOptionOpenUrl {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: EguiOpenUrl,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiOptionPos2 {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: EguiPos2,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiOptionRangeInclusive {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: EguiRangeInclusive,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiOptionRect {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: EguiRect,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiOptionSense {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: EguiSense,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiOptionUiStackInfo {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: EguiUiStackInfo,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiOptionVec2 {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: EguiVec2,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiOptionWidgetInfo {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: EguiWidgetInfo,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct EguiOptionWidgetRect {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: EguiWidgetRect,
}

