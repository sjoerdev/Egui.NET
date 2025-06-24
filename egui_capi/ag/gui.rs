/// Indicate whether a popup will be shown above or below the box.
#[derive(Copy, Clone)]
#[repr(C)]
pub enum VxAboveOrBelow {
    Above,
    Below,
}

/// What options to show for alpha
#[derive(Copy, Clone)]
#[repr(C)]
pub enum VxAlpha {
    /// Set alpha to 1.0, and show no option for it.
    Opaque,
    /// Only show normal blend options for alpha.
    OnlyBlend,
    /// Show both blend and additive options.
    BlendOrAdditive,
}

#[derive(Copy, Clone)]
#[repr(C)]
pub enum VxCursorGrab {
    None,
    Confined,
    Locked,
}

/// A mouse cursor icon.
/// 
/// egui emits a [`CursorIcon`] in [`PlatformOutput`] each frame as a request to the integration.
/// 
/// Loosely based on <https://developer.mozilla.org/en-US/docs/Web/CSS/cursor>.
#[derive(Copy, Clone)]
#[repr(C)]
pub enum VxCursorIcon {
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
#[derive(Copy, Clone)]
#[repr(C)]
pub enum VxDirection {
    LeftToRight,
    RightToLeft,
    TopDown,
    BottomUp,
}

#[derive(Copy, Clone)]
#[repr(C)]
pub enum VxIMEPurpose {
    Normal,
    Password,
    Terminal,
}

/// The unit associated with the numeric value of a mouse wheel event
#[derive(Copy, Clone)]
#[repr(C)]
pub enum VxMouseWheelUnit {
    /// Number of ui points (logical pixels)
    Point,
    /// Number of lines
    Line,
    /// Number of pages
    Page,
}

/// How to display numeric color values.
#[derive(Copy, Clone)]
#[repr(C)]
pub enum VxNumericColorSpace {
    /// RGB is 0-255 in gamma space.
    /// 
    /// Alpha is 0-255 in linear space.
    GammaByte,
    /// 0-1 in linear space.
    Linear,
}

/// Different layer categories
#[derive(Copy, Clone)]
#[repr(C)]
pub enum VxOrder {
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
#[derive(Copy, Clone)]
#[repr(C)]
pub enum VxPointerButton {
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
#[derive(Copy, Clone)]
#[repr(C)]
pub enum VxPopupCloseBehavior {
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

#[derive(Copy, Clone)]
#[repr(C)]
pub enum VxResizeDirection {
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
#[derive(Copy, Clone)]
#[repr(C)]
pub enum VxScrollBarVisibility {
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
#[derive(Copy, Clone)]
#[repr(C)]
pub enum VxSide {
    Left,
    Right,
}

/// Specifies how values in a [`Slider`] are clamped.
#[derive(Copy, Clone)]
#[repr(C)]
pub enum VxSliderClamping {
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
#[derive(Copy, Clone)]
#[repr(C)]
pub enum VxSliderOrientation {
    Horizontal,
    Vertical,
}

#[derive(Copy, Clone)]
#[repr(C)]
pub enum VxSystemTheme {
    SystemDefault,
    Light,
    Dark,
}

/// Dark or Light theme.
#[derive(Copy, Clone)]
#[repr(C)]
pub enum VxTheme {
    /// Dark mode: light text on a dark background.
    Dark,
    /// Light mode: dark text on a light background.
    Light,
}

/// The user's theme preference.
#[derive(Copy, Clone)]
#[repr(C)]
pub enum VxThemePreference {
    /// Dark mode: light text on a dark background.
    Dark,
    /// Light mode: dark text on a light background.
    Light,
    /// Follow the system's theme preference.
    System,
}

/// [`Top`](TopBottomSide::Top) or [`Bottom`](TopBottomSide::Bottom)
#[derive(Copy, Clone)]
#[repr(C)]
pub enum VxTopBottomSide {
    Top,
    Bottom,
}

/// In what phase a touch event is in.
#[derive(Copy, Clone)]
#[repr(C)]
pub enum VxTouchPhase {
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
#[derive(Copy, Clone)]
#[repr(C)]
pub enum VxUiKind {
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
#[derive(Copy, Clone)]
#[repr(C)]
pub enum VxUserAttentionType {
    /// Request an elevated amount of animations and flair for the window and the task bar or dock icon.
    Critical,
    /// Request a standard amount of attention-grabbing actions.
    Informational,
    /// Reset the attention request and interrupt related animations and flashes.
    Reset,
}

/// The different types of viewports supported by egui.
#[derive(Copy, Clone)]
#[repr(C)]
pub enum VxViewportClass {
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
#[derive(Copy, Clone)]
#[repr(C)]
pub enum VxViewportEvent {
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
#[derive(Copy, Clone)]
#[repr(C)]
pub enum VxWidgetType {
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

#[derive(Copy, Clone)]
#[repr(C)]
pub enum VxWindowLevel {
    Normal,
    AlwaysOnBottom,
    AlwaysOnTop,
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_area_drop(value: *mut VxObject<::egui::containers::area::Area>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_areas_drop(value: *mut VxObject<::egui::memory::Areas>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_bar_state_drop(value: *mut VxObject<::egui::menu::BarState>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_button_drop(value: *mut VxObject<::egui::widgets::button::Button>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_cache_storage_drop(value: *mut VxObject<::egui::cache::cache_storage::CacheStorage>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_central_panel_drop(value: *mut VxObject<::egui::containers::panel::CentralPanel>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_checkbox_drop(value: *mut VxObject<::egui::widgets::checkbox::Checkbox>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_collapsing_header_drop(value: *mut VxObject<::egui::containers::collapsing_header::CollapsingHeader>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_collapsing_response_drop(value: *mut VxObject<::egui::containers::collapsing_header::CollapsingResponse>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_collapsing_state_drop(value: *mut VxObject<::egui::containers::collapsing_header::CollapsingState>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_combo_box_drop(value: *mut VxObject<::egui::containers::combo_box::ComboBox>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_debug_rect_drop(value: *mut VxObject<::egui::pass_state::DebugRect>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_default_bytes_loader_drop(value: *mut VxObject<::egui::load::bytes_loader::DefaultBytesLoader>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_default_texture_loader_drop(value: *mut VxObject<::egui::load::texture_loader::DefaultTextureLoader>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_drag_and_drop_drop(value: *mut VxObject<::egui::drag_and_drop::DragAndDrop>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_drag_value_drop(value: *mut VxObject<::egui::widgets::drag_value::DragValue>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_frame_cache_drop(value: *mut VxObject<::egui::cache::frame_cache::FrameCache>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_frame_publisher_drop(value: *mut VxObject<::egui::cache::frame_publisher::FramePublisher>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_grid_drop(value: *mut VxObject<::egui::grid::Grid>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_header_response_drop(value: *mut VxObject<::egui::containers::collapsing_header::HeaderResponse>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_hyperlink_drop(value: *mut VxObject<::egui::widgets::hyperlink::Hyperlink>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_id_type_map_drop(value: *mut VxObject<::egui::util::id_type_map::IdTypeMap>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_image_drop(value: *mut VxObject<::egui::widgets::image::Image>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_image_button_drop(value: *mut VxObject<::egui::widgets::image_button::ImageButton>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_immediate_viewport_drop(value: *mut VxObject<::egui::viewport::ImmediateViewport>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_inner_response_drop(value: *mut VxObject<::egui::response::InnerResponse>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_input_options_drop(value: *mut VxObject<::egui::input_state::InputOptions>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_input_state_drop(value: *mut VxObject<::egui::input_state::InputState>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_interaction_snapshot_drop(value: *mut VxObject<::egui::interaction::InteractionSnapshot>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_label_drop(value: *mut VxObject<::egui::widgets::label::Label>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_label_selection_state_drop(value: *mut VxObject<::egui::text_selection::label_text_selection::LabelSelectionState>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_link_drop(value: *mut VxObject<::egui::widgets::hyperlink::Link>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_memory_drop(value: *mut VxObject<::egui::memory::Memory>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_menu_root_manager_drop(value: *mut VxObject<::egui::menu::MenuRootManager>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_menu_state_drop(value: *mut VxObject<::egui::menu::MenuState>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_modal_drop(value: *mut VxObject<::egui::containers::modal::Modal>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_modal_response_drop(value: *mut VxObject<::egui::containers::modal::ModalResponse>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_options_drop(value: *mut VxObject<::egui::memory::Options>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_painter_drop(value: *mut VxObject<::egui::painter::Painter>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_pass_state_drop(value: *mut VxObject<::egui::pass_state::PassState>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_per_layer_state_drop(value: *mut VxObject<::egui::pass_state::PerLayerState>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_per_widget_tooltip_state_drop(value: *mut VxObject<::egui::pass_state::PerWidgetTooltipState>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_pointer_state_drop(value: *mut VxObject<::egui::input_state::PointerState>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_prepared_drop(value: *mut VxObject<::egui::containers::frame::Prepared>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_progress_bar_drop(value: *mut VxObject<::egui::widgets::progress_bar::ProgressBar>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_radio_button_drop(value: *mut VxObject<::egui::widgets::radio_button::RadioButton>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_resize_drop(value: *mut VxObject<::egui::containers::resize::Resize>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_response_drop(value: *mut VxObject<::egui::response::Response>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_rich_text_drop(value: *mut VxObject<::egui::widget_text::RichText>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_scene_drop(value: *mut VxObject<::egui::containers::scene::Scene>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_scroll_area_drop(value: *mut VxObject<::egui::containers::scroll_area::ScrollArea>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_scroll_area_output_drop(value: *mut VxObject<::egui::containers::scroll_area::ScrollAreaOutput>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_scroll_target_drop(value: *mut VxObject<::egui::pass_state::ScrollTarget>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_selectable_label_drop(value: *mut VxObject<::egui::widgets::selected_label::SelectableLabel>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_separator_drop(value: *mut VxObject<::egui::widgets::separator::Separator>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_side_panel_drop(value: *mut VxObject<::egui::containers::panel::SidePanel>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_sides_drop(value: *mut VxObject<::egui::containers::sides::Sides>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_slider_drop(value: *mut VxObject<::egui::widgets::slider::Slider>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_spinner_drop(value: *mut VxObject<::egui::widgets::spinner::Spinner>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_state_drop(value: *mut VxObject<::egui::containers::scroll_area::State>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_sub_menu_drop(value: *mut VxObject<::egui::menu::SubMenu>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_sub_menu_button_drop(value: *mut VxObject<::egui::menu::SubMenuButton>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_text_cursor_state_drop(value: *mut VxObject<::egui::text_selection::text_cursor_state::TextCursorState>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_text_edit_drop(value: *mut VxObject<::egui::widgets::text_edit::builder::TextEdit>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_text_edit_output_drop(value: *mut VxObject<::egui::widgets::text_edit::output::TextEditOutput>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_text_edit_state_drop(value: *mut VxObject<::egui::widgets::text_edit::state::TextEditState>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_tooltip_pass_state_drop(value: *mut VxObject<::egui::pass_state::TooltipPassState>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_top_bottom_panel_drop(value: *mut VxObject<::egui::containers::panel::TopBottomPanel>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_ui_drop(value: *mut VxObject<::egui::ui::Ui>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_ui_stack_drop(value: *mut VxObject<::egui::ui_stack::UiStack>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_ui_stack_iterator_drop(value: *mut VxObject<::egui::ui_stack::UiStackIterator>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_undoer_drop(value: *mut VxObject<::egui::util::undoer::Undoer>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_viewport_state_drop(value: *mut VxObject<::egui::context::ViewportState>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_widget_hits_drop(value: *mut VxObject<::egui::hit_test::WidgetHits>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_widget_rects_drop(value: *mut VxObject<::egui::widget_rect::WidgetRects>) {
    VxHandle::from_heap(value);
}

/// Frees the provided object.
///
/// # Safety
///
/// For this call to be sound, the pointer must refer to a live object of the corret type.
#[no_mangle]
pub unsafe extern "C" fn vx_gui_window_drop(value: *mut VxObject<::egui::containers::window::Window>) {
    VxHandle::from_heap(value);
}

/// State of an [`Area`] that is persisted between frames.
/// 
/// Areas back [`crate::Window`]s and other floating containers,
/// like tooltips and the popups of [`crate::ComboBox`].
#[derive(Copy, Clone)]
#[repr(C)]
pub struct VxAreaState {
    /// Last known position of the pivot.
    pub pivot_pos: VxOptionPos2,
    /* UNIMPLEMENTED (field) 
    /// The anchor point of the area, i.e. where on the area the [`Self::pivot_pos`] refers to.
    pub pivot: crate::Align2,*/
    
    /// Last known size.
    /// 
    /// Area size is intentionally NOT persisted between sessions,
    /// so that a bad tooltip or menu size won't be remembered forever.
    /// A resizable [`crate::Window`] remembers the size the user picked using
    /// the state in the [`crate::Resize`] container.
    pub size: VxOptionVec2,
    /// If false, clicks goes straight through to what is behind us. Useful for tooltips etc.
    pub interactable: bool,
    /// At what time was this area first shown?
    /// 
    /// Used to fade in the area.
    pub last_became_visible_at: VxOptionF64,
}

/// Returns the "default value" for a type.
#[no_mangle]
pub extern "C" fn vx_area_state_default() -> VxAreaState {
    let value = ::egui::containers::area::AreaState::default();
    VxAreaState {
        pivot_pos: value.pivot_pos.into(),
        pivot: value.pivot.into(),
        size: value.size.into(),
        interactable: value.interactable.into(),
        last_became_visible_at: value.last_became_visible_at.into(),
    }
}


/// A selected text range (could be a range of length zero).
/// 
/// The selection is based on character count (NOT byte count!).
#[derive(Copy, Clone)]
#[repr(C)]
pub struct VxCCursorRange {
    /// When selecting with a mouse, this is where the mouse was released.
    /// When moving with e.g. shift+arrows, this is what moves.
    /// Note that the two ends can come in any order, and also be equal (no selection).
    pub primary: VxCCursor,
    /// When selecting with a mouse, this is where the mouse was first pressed.
    /// This part of the cursor does not move when shift is down.
    pub secondary: VxCCursor,
}

/// Returns the "default value" for a type.
#[no_mangle]
pub extern "C" fn vx_c_cursor_range_default() -> VxCCursorRange {
    let value = ::egui::text_selection::cursor_range::CCursorRange::default();
    VxCCursorRange {
        primary: value.primary.into(),
        secondary: value.secondary.into(),
    }
}


/// A selected text range (could be a range of length zero).
#[derive(Copy, Clone)]
#[repr(C)]
pub struct VxCursorRange {
    /// When selecting with a mouse, this is where the mouse was released.
    /// When moving with e.g. shift+arrows, this is what moves.
    /// Note that the two ends can come in any order, and also be equal (no selection).
    pub primary: VxCursor,
    /// When selecting with a mouse, this is where the mouse was first pressed.
    /// This part of the cursor does not move when shift is down.
    pub secondary: VxCursor,
}

/// Returns the "default value" for a type.
#[no_mangle]
pub extern "C" fn vx_cursor_range_default() -> VxCursorRange {
    let value = ::egui::text_selection::cursor_range::CursorRange::default();
    VxCursorRange {
        primary: value.primary.into(),
        secondary: value.secondary.into(),
    }
}


/// Options for help debug egui by adding extra visualization
#[derive(Copy, Clone)]
#[repr(C)]
pub struct VxDebugOptions {
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

/// Returns the "default value" for a type.
#[no_mangle]
pub extern "C" fn vx_debug_options_default() -> VxDebugOptions {
    let value = ::egui::style::DebugOptions::default();
    VxDebugOptions {
        debug_on_hover: value.debug_on_hover.into(),
        debug_on_hover_with_all_modifiers: value.debug_on_hover_with_all_modifiers.into(),
        hover_shows_next: value.hover_shows_next.into(),
        show_expand_width: value.show_expand_width.into(),
        show_expand_height: value.show_expand_height.into(),
        show_resize: value.show_resize.into(),
        show_interactive_widgets: value.show_interactive_widgets.into(),
        show_widget_hits: value.show_widget_hits.into(),
        show_unaligned: value.show_unaligned.into(),
    }
}


/// A file dropped into egui.
#[derive(Copy, Clone)]
#[repr(C)]
pub struct VxDroppedFile {
    /* UNIMPLEMENTED (field) 
    /// Set by the `egui-winit` backend.
    pub path: VxOptionStd::path::pathBuf,*/
    
    /// Name of the file. Set by the `eframe` web backend.
    pub name: VxString,
    /// With the `eframe` web backend, this is set to the mime-type of the file (if available).
    pub mime: VxString,
    /// Set by the `eframe` web backend.
    pub last_modified: VxOptionPosixTime,
    /* UNIMPLEMENTED (field) 
    /// Set by the `eframe` web backend.
    pub bytes: VxOptionStd::sync::arc,*/
}

/// Returns the "default value" for a type.
#[no_mangle]
pub extern "C" fn vx_dropped_file_default() -> VxDroppedFile {
    let value = ::egui::data::input::DroppedFile::default();
    VxDroppedFile {
        path: value.path.into(),
        name: value.name.into(),
        mime: value.mime.into(),
        last_modified: value.last_modified.into(),
        bytes: value.bytes.into(),
    }
}


/// Controls which events that a focused widget will have exclusive access to.
/// 
/// Currently this only controls a few special keyboard events,
/// but in the future this `struct` should be extended into a full callback thing.
/// 
/// Any events not covered by the filter are given to the widget, but are not exclusive.
#[derive(Copy, Clone)]
#[repr(C)]
pub struct VxEventFilter {
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

/// Returns the "default value" for a type.
#[no_mangle]
pub extern "C" fn vx_event_filter_default() -> VxEventFilter {
    let value = ::egui::data::input::EventFilter::default();
    VxEventFilter {
        tab: value.tab.into(),
        horizontal_arrows: value.horizontal_arrows.into(),
        vertical_arrows: value.vertical_arrows.into(),
        escape: value.escape.into(),
    }
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
#[derive(Copy, Clone)]
#[repr(C)]
pub struct VxFrame {
    /// Margin within the painted frame.
    /// 
    /// Known as `padding` in CSS.
    pub inner_margin: VxMargin,
    /// The background fill color of the frame, within the [`Self::stroke`].
    /// 
    /// Known as `background` in CSS.
    pub fill: VxColor4,
    /// The width and color of the outline around the frame.
    /// 
    /// The width of the stroke is part of the total margin/padding of the frame.
    pub stroke: VxStroke,
    /// The rounding of the _outer_ corner of the [`Self::stroke`]
    /// (or, if there is no stroke, the outer corner of [`Self::fill`]).
    /// 
    /// In other words, this is the corner radius of the _widget rect_.
    pub corner_radius: VxCornerRadius,
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
    pub outer_margin: VxMargin,
    /// Optional drop-shadow behind the frame.
    pub shadow: VxShadow,
}

/// Returns the "default value" for a type.
#[no_mangle]
pub extern "C" fn vx_frame_default() -> VxFrame {
    let value = ::egui::containers::frame::Frame::default();
    VxFrame {
        inner_margin: value.inner_margin.into(),
        fill: value.fill.into(),
        stroke: value.stroke.into(),
        corner_radius: value.corner_radius.into(),
        outer_margin: value.outer_margin.into(),
        shadow: value.shadow.into(),
    }
}


/// A file about to be dropped into egui.
#[derive(Copy, Clone)]
#[repr(C)]
pub struct VxHoveredFile {
    /* UNIMPLEMENTED (field) 
    /// Set by the `egui-winit` backend.
    pub path: VxOptionStd::path::pathBuf,*/
    
    /// With the `eframe` web backend, this is set to the mime-type of the file (if available).
    pub mime: VxString,
}

/// Returns the "default value" for a type.
#[no_mangle]
pub extern "C" fn vx_hovered_file_default() -> VxHoveredFile {
    let value = ::egui::data::input::HoveredFile::default();
    VxHoveredFile {
        path: value.path.into(),
        mime: value.mime.into(),
    }
}


/// Information about text being edited.
/// 
/// Useful for IME.
#[derive(Copy, Clone)]
#[repr(C)]
pub struct VxIMEOutput {
    /// Where the [`crate::TextEdit`] is located on screen.
    pub rect: VxIBox2,
    /// Where the primary cursor is.
    /// 
    /// This is a very thin rectangle.
    pub cursor_rect: VxIBox2,
}


#[derive(Copy, Clone)]
#[repr(C)]
pub struct VxImageOptions {
    /// Select UV range. Default is (0,0) in top-left, (1,1) bottom right.
    pub uv: VxIBox2,
    /// A solid color to put behind the image. Useful for transparent images.
    pub bg_fill: VxColor4,
    /// Multiply image color with this. Default is WHITE (no tint).
    pub tint: VxColor4,
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
    pub rotation: VxOptionTuple([resolvedPath(path{Path:"emath::rot2",Id:Id(6930),Args:Some(angleBracketed{Args:[],Constraints:[]})}),ResolvedPath(path{Path:"crate::vec2",Id:Id(150),Args:Some(angleBracketed{Args:[],Constraints:[]})})]),*/
    
    /// Round the corners of the image.
    /// 
    /// The default is no rounding ([`CornerRadius::ZERO`]).
    /// 
    /// Due to limitations in the current implementation,
    /// this will turn off any rotation of the image.
    pub corner_radius: VxCornerRadius,
}

/// Returns the "default value" for a type.
#[no_mangle]
pub extern "C" fn vx_image_options_default() -> VxImageOptions {
    let value = ::egui::widgets::image::ImageOptions::default();
    VxImageOptions {
        uv: value.uv.into(),
        bg_fill: value.bg_fill.into(),
        tint: value.tint.into(),
        rotation: value.rotation.into(),
        corner_radius: value.corner_radius.into(),
    }
}


/// This type determines the constraints on how
/// the size of an image should be calculated.
#[derive(Copy, Clone)]
#[repr(C)]
pub struct VxImageSize {
    /// Whether or not the final size should maintain the original aspect ratio.
    /// 
    /// This setting is applied last.
    /// 
    /// This defaults to `true`.
    pub maintain_aspect_ratio: bool,
    /// Determines the maximum size of the image.
    /// 
    /// Defaults to `Vec2::INFINITY` (no limit).
    pub max_size: VxVec2,
    /* UNIMPLEMENTED (field) 
    /// Determines how the image should shrink/expand/stretch/etc. to fit within its allocated space.
    /// 
    /// This setting is applied first.
    /// 
    /// Defaults to `ImageFit::Fraction([1, 1])`
    pub fit: ImageFit,*/
}

/// Returns the "default value" for a type.
#[no_mangle]
pub extern "C" fn vx_image_size_default() -> VxImageSize {
    let value = ::egui::widgets::image::ImageSize::default();
    VxImageSize {
        maintain_aspect_ratio: value.maintain_aspect_ratio.into(),
        max_size: value.max_size.into(),
        fit: value.fit.into(),
    }
}


/// How and when interaction happens.
#[derive(Copy, Clone)]
#[repr(C)]
pub struct VxInteraction {
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

/// Returns the "default value" for a type.
#[no_mangle]
pub extern "C" fn vx_interaction_default() -> VxInteraction {
    let value = ::egui::style::Interaction::default();
    VxInteraction {
        interact_radius: value.interact_radius.into(),
        resize_grab_radius_side: value.resize_grab_radius_side.into(),
        resize_grab_radius_corner: value.resize_grab_radius_corner.into(),
        show_tooltips_only_when_still: value.show_tooltips_only_when_still.into(),
        tooltip_delay: value.tooltip_delay.into(),
        tooltip_grace_time: value.tooltip_grace_time.into(),
        selectable_labels: value.selectable_labels.into(),
        multi_widget_text_select: value.multi_widget_text_select.into(),
    }
}


/// A keyboard shortcut, e.g. `Ctrl+Alt+W`.
/// 
/// Can be used with [`crate::InputState::consume_shortcut`]
/// and [`crate::Context::format_shortcut`].
#[derive(Copy, Clone)]
#[repr(C)]
pub struct VxKeyboardShortcut {
    pub modifiers: VxModifiers,
    pub logical_key: VxKey,
}


/// An identifier for a paint layer.
/// Also acts as an identifier for [`crate::Area`]:s.
#[derive(Copy, Clone)]
#[repr(C)]
pub struct VxLayerId {
    pub order: VxOrder,
    pub id: VxId,
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
#[derive(Copy, Clone)]
#[repr(C)]
pub struct VxLayout {
    /// Main axis direction
    pub main_dir: VxDirection,
    /// If true, wrap around when reading the end of the main direction.
    /// For instance, for `main_dir == Direction::LeftToRight` this will
    /// wrap to a new row when we reach the right side of the `max_rect`.
    pub main_wrap: bool,
    /// How to align things on the main axis.
    pub main_align: VxAlign,
    /// Justify the main axis?
    pub main_justify: bool,
    /// How to align things on the cross axis.
    /// For vertical layouts: put things to left, center or right?
    /// For horizontal layouts: put things to top, center or bottom?
    pub cross_align: VxAlign,
    /// Justify the cross axis?
    /// For vertical layouts justify mean all widgets get maximum width.
    /// For horizontal layouts justify mean all widgets get maximum height.
    pub cross_justify: bool,
}

/// Returns the "default value" for a type.
#[no_mangle]
pub extern "C" fn vx_layout_default() -> VxLayout {
    let value = ::egui::layout::Layout::default();
    VxLayout {
        main_dir: value.main_dir.into(),
        main_wrap: value.main_wrap.into(),
        main_align: value.main_align.into(),
        main_justify: value.main_justify.into(),
        cross_align: value.cross_align.into(),
        cross_justify: value.cross_justify.into(),
    }
}


/// Menu root associated with an Id from a Response
#[derive(Copy, Clone)]
#[repr(C)]
pub struct VxMenuRoot {
    /* UNIMPLEMENTED (field) 
    pub menu_state: std::sync::Arc,*/
    
    pub id: VxId,
}


/// Names of different modifier keys.
/// 
/// Used to name modifiers.
#[derive(Copy, Clone)]
#[repr(C)]
pub struct VxModifierNames {
    pub is_short: bool,
    pub alt: VxString,
    pub ctrl: VxString,
    pub shift: VxString,
    pub mac_cmd: VxString,
    pub mac_alt: VxString,
    /// What goes between the names
    pub concat: VxString,
}


/// State of the modifier keys. These must be fed to egui.
/// 
/// The best way to compare [`Modifiers`] is by using [`Modifiers::matches`].
/// 
/// NOTE: For cross-platform uses, ALT+SHIFT is a bad combination of modifiers
/// as on mac that is how you type special characters,
/// so those key presses are usually not reported to egui.
#[derive(Copy, Clone)]
#[repr(C)]
pub struct VxModifiers {
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

/// Returns the "default value" for a type.
#[no_mangle]
pub extern "C" fn vx_modifiers_default() -> VxModifiers {
    let value = ::egui::data::input::Modifiers::default();
    VxModifiers {
        alt: value.alt.into(),
        ctrl: value.ctrl.into(),
        shift: value.shift.into(),
        mac_cmd: value.mac_cmd.into(),
        command: value.command.into(),
    }
}


/// All you probably need to know about a multi-touch gesture.
#[derive(Copy, Clone)]
#[repr(C)]
pub struct VxMultiTouchInfo {
    /// Point in time when the gesture started.
    pub start_time: f64,
    /// Position of the pointer at the time the gesture started.
    pub start_pos: VxPos2,
    /// Center position of the current gesture (average of all touch points).
    pub center_pos: VxPos2,
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
    pub zoom_delta_2d: VxVec2,
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
    pub translation_delta: VxVec2,
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
#[derive(Copy, Clone)]
#[repr(C)]
pub struct VxOpenUrl {
    pub url: VxString,
    /// If `true`, open the url in a new tab.
    /// If `false` open it in the same tab.
    /// Only matters when in a web browser.
    pub new_tab: bool,
}


#[derive(Copy, Clone)]
#[repr(C)]
pub struct VxPCursorRange {
    /// When selecting with a mouse, this is where the mouse was released.
    /// When moving with e.g. shift+arrows, this is what moves.
    /// Note that the two ends can come in any order, and also be equal (no selection).
    pub primary: VxPCursor,
    /// When selecting with a mouse, this is where the mouse was first pressed.
    /// This part of the cursor does not move when shift is down.
    pub secondary: VxPCursor,
}

/// Returns the "default value" for a type.
#[no_mangle]
pub extern "C" fn vx_p_cursor_range_default() -> VxPCursorRange {
    let value = ::egui::text_selection::cursor_range::PCursorRange::default();
    VxPCursorRange {
        primary: value.primary.into(),
        secondary: value.secondary.into(),
    }
}


/// The non-rendering part of what egui emits each frame.
/// 
/// You can access (and modify) this with [`crate::Context::output`].
/// 
/// The backend should use this.
#[derive(Copy, Clone)]
#[repr(C)]
pub struct VxPlatformOutput {
    /* UNIMPLEMENTED (field) 
    /// Commands that the egui integration should execute at the end of a frame.
    pub commands: VEC_TY_TODO(OutputCommand),*/
    
    /// Set the cursor to this icon.
    pub cursor_icon: VxCursorIcon,
    /// If set, open this url.
    pub open_url: VxOptionOpenUrl,
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
    pub copied_text: VxString,
    /* UNIMPLEMENTED (field) 
    /// Events that may be useful to e.g. a screen reader.
    pub events: VEC_TY_TODO(OutputEvent),*/
    
    /// Is there a mutable [`TextEdit`](crate::TextEdit) under the cursor?
    /// Use by `eframe` web to show/hide mobile keyboard and IME agent.
    pub mutable_text_under_cursor: bool,
    /// This is set if, and only if, the user is currently editing text.
    /// 
    /// Useful for IME.
    pub ime: VxOptionImeOutput,
    /// How many ui passes is this the sum of?
    /// 
    /// See [`crate::Context::request_discard`] for details.
    /// 
    /// This is incremented at the END of each frame,
    /// so this will be `0` for the first pass.
    pub num_completed_passes: usize,
    /// Was [`crate::Context::request_discard`] called during the latest pass?
    /// 
    /// If so, what was the reason(s) for it?
    /// 
    /// If empty, there was never any calls.
    pub request_discard_reasons: VEC_TY_TODO(VxRepaintCause),
}

/// Returns the "default value" for a type.
#[no_mangle]
pub extern "C" fn vx_platform_output_default() -> VxPlatformOutput {
    let value = ::egui::data::output::PlatformOutput::default();
    VxPlatformOutput {
        commands: value.commands.into(),
        cursor_icon: value.cursor_icon.into(),
        open_url: value.open_url.into(),
        copied_text: value.copied_text.into(),
        events: value.events.into(),
        mutable_text_under_cursor: value.mutable_text_under_cursor.into(),
        ime: value.ime.into(),
        num_completed_passes: value.num_completed_passes.into(),
        request_discard_reasons: value.request_discard_reasons.into(),
    }
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
#[derive(Copy, Clone)]
#[repr(C)]
pub struct VxRawInput {
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
    pub screen_rect: VxOptionIBox2,
    /// Maximum size of one side of the font texture.
    /// 
    /// Ask your graphics drivers about this. This corresponds to `GL_MAX_TEXTURE_SIZE`.
    /// 
    /// The default is a very small (but very portable) 2048.
    pub max_texture_side: VxOptionUsize,
    /// Monotonically increasing time, in seconds. Relative to whatever. Used for animations.
    /// If `None` is provided, egui will assume a time delta of `predicted_dt` (default 1/60 seconds).
    pub time: VxOptionF64,
    /// Should be set to the expected time between frames when painting at vsync speeds.
    /// The default for this is 1/60.
    /// Can safely be left at its default value.
    pub predicted_dt: f32,
    /// Which modifier keys are down at the start of the frame?
    pub modifiers: VxModifiers,
    /* UNIMPLEMENTED (field) 
    /// In-order events received this frame.
    /// 
    /// There is currently no way to know if egui handles a particular event,
    /// but you can check if egui is using the keyboard with [`crate::Context::wants_keyboard_input`]
    /// and/or the pointer (mouse/touch) with [`crate::Context::is_using_pointer`].
    pub events: VEC_TY_TODO(Event),*/
    
    /// Dragged files hovering over egui.
    pub hovered_files: VEC_TY_TODO(VxHoveredFile),
    /// Dragged files dropped into egui.
    /// 
    /// Note: when using `eframe` on Windows, this will always be empty if drag-and-drop support has
    /// been disabled in [`crate::viewport::ViewportBuilder`].
    pub dropped_files: VEC_TY_TODO(VxDroppedFile),
    /// The native window has the keyboard focus (i.e. is receiving key presses).
    /// 
    /// False when the user alt-tab away from the application, for instance.
    pub focused: bool,
    /// Does the OS use dark or light mode?
    /// 
    /// `None` means "don't know".
    pub system_theme: VxOptionTheme,
}

/// Returns the "default value" for a type.
#[no_mangle]
pub extern "C" fn vx_raw_input_default() -> VxRawInput {
    let value = ::egui::data::input::RawInput::default();
    VxRawInput {
        viewport_id: value.viewport_id.into(),
        viewports: value.viewports.into(),
        screen_rect: value.screen_rect.into(),
        max_texture_side: value.max_texture_side.into(),
        time: value.time.into(),
        predicted_dt: value.predicted_dt.into(),
        modifiers: value.modifiers.into(),
        events: value.events.into(),
        hovered_files: value.hovered_files.into(),
        dropped_files: value.dropped_files.into(),
        focused: value.focused.into(),
        system_theme: value.system_theme.into(),
    }
}


/// What called [`Context::request_repaint`] or [`Context::request_discard`]?
#[derive(Copy, Clone)]
#[repr(C)]
pub struct VxRepaintCause {
    /// What file had the call that requested the repaint?
    pub file: VxString,
    /// What line number of the call that requested the repaint?
    pub line: u32,
    /* UNIMPLEMENTED (field) 
    /// Explicit reason; human readable.
    pub reason: std::borrow::Cow,*/
}


/// Information given to the backend about when it is time to repaint the ui.
/// 
/// This is given in the callback set by [`Context::set_request_repaint_callback`].
#[derive(Copy, Clone)]
#[repr(C)]
pub struct VxRequestRepaintInfo {
    /* UNIMPLEMENTED (field) 
    /// This is used to specify what viewport that should repaint.
    pub viewport_id: crate::ViewportId,*/
    
    /// Repaint after this duration. If zero, repaint as soon as possible.
    pub delay: VxDuration,
    /// The number of fully completed passes, of the entire lifetime of the [`Context`].
    /// 
    /// This can be compared to [`Context::cumulative_pass_nr`] to see if we we still
    /// need another repaint (ui pass / frame), or if one has already happened.
    pub current_cumulative_pass_nr: u64,
}


#[derive(Copy, Clone)]
#[repr(C)]
pub struct VxRowVertexIndices {
    pub row: usize,
    /* UNIMPLEMENTED (field) 
    pub vertex_indices: Array { type_: Primitive("u32"), len: "6" },*/
}


/// Scroll animation configuration, used when programmatically scrolling somewhere (e.g. with `[crate::Ui::scroll_to_cursor]`)
/// The animation duration is calculated based on the distance to be scrolled via `[ScrollAnimation::points_per_second]`
/// and can be clamped to a min / max duration via `[ScrollAnimation::duration]`.
#[derive(Copy, Clone)]
#[repr(C)]
pub struct VxScrollAnimation {
    /// With what speed should we scroll? (Default: 1000.0)
    pub points_per_second: f32,
    /// The min / max scroll duration.
    pub duration: VxRangef,
}

/// Returns the "default value" for a type.
#[no_mangle]
pub extern "C" fn vx_scroll_animation_default() -> VxScrollAnimation {
    let value = ::egui::style::ScrollAnimation::default();
    VxScrollAnimation {
        points_per_second: value.points_per_second.into(),
        duration: value.duration.into(),
    }
}


/// Controls the spacing and visuals of a [`crate::ScrollArea`].
/// 
/// There are three presets to chose from:
/// * [`Self::solid`]
/// * [`Self::thin`]
/// * [`Self::floating`]
#[derive(Copy, Clone)]
#[repr(C)]
pub struct VxScrollStyle {
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

/// Returns the "default value" for a type.
#[no_mangle]
pub extern "C" fn vx_scroll_style_default() -> VxScrollStyle {
    let value = ::egui::style::ScrollStyle::default();
    VxScrollStyle {
        floating: value.floating.into(),
        bar_width: value.bar_width.into(),
        handle_min_length: value.handle_min_length.into(),
        bar_inner_margin: value.bar_inner_margin.into(),
        bar_outer_margin: value.bar_outer_margin.into(),
        floating_width: value.floating_width.into(),
        floating_allocated_width: value.floating_allocated_width.into(),
        foreground_color: value.foreground_color.into(),
        dormant_background_opacity: value.dormant_background_opacity.into(),
        active_background_opacity: value.active_background_opacity.into(),
        interact_background_opacity: value.interact_background_opacity.into(),
        dormant_handle_opacity: value.dormant_handle_opacity.into(),
        active_handle_opacity: value.active_handle_opacity.into(),
        interact_handle_opacity: value.interact_handle_opacity.into(),
    }
}


/// Selected text, selected elements etc
#[derive(Copy, Clone)]
#[repr(C)]
pub struct VxSelection {
    pub bg_fill: VxColor4,
    pub stroke: VxStroke,
}

/// Returns the "default value" for a type.
#[no_mangle]
pub extern "C" fn vx_selection_default() -> VxSelection {
    let value = ::egui::style::Selection::default();
    VxSelection {
        bg_fill: value.bg_fill.into(),
        stroke: value.stroke.into(),
    }
}


#[derive(Copy, Clone)]
#[repr(C)]
pub struct VxSettings {
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

/// Returns the "default value" for a type.
#[no_mangle]
pub extern "C" fn vx_settings_default() -> VxSettings {
    let value = ::egui::util::undoer::Settings::default();
    VxSettings {
        max_undos: value.max_undos.into(),
        stable_time: value.stable_time.into(),
        auto_save_interval: value.auto_save_interval.into(),
    }
}


/// A texture with a known size.
#[derive(Copy, Clone)]
#[repr(C)]
pub struct VxSizedTexture {
    pub id: VxTextureId,
    pub size: VxVec2,
}


/// Controls the sizes and distances between widgets.
#[derive(Copy, Clone)]
#[repr(C)]
pub struct VxSpacing {
    /// Horizontal and vertical spacing between widgets.
    /// 
    /// To add extra space between widgets, use [`Ui::add_space`].
    /// 
    /// `item_spacing` is inserted _after_ adding a widget, so to increase the spacing between
    /// widgets `A` and `B` you need to change `item_spacing` before adding `A`.
    pub item_spacing: VxVec2,
    /// Horizontal and vertical margins within a window frame.
    pub window_margin: VxMargin,
    /// Button size is text size plus this on each side
    pub button_padding: VxVec2,
    /// Horizontal and vertical margins within a menu frame.
    pub menu_margin: VxMargin,
    /// Indent collapsing regions etc by this much.
    pub indent: f32,
    /// Minimum size of a [`DragValue`], color picker button, and other small widgets.
    /// `interact_size.y` is the default height of button, slider, etc.
    /// Anything clickable should be (at least) this size.
    pub interact_size: VxVec2,
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
    pub default_area_size: VxVec2,
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
    pub scroll: VxScrollStyle,
}

/// Returns the "default value" for a type.
#[no_mangle]
pub extern "C" fn vx_spacing_default() -> VxSpacing {
    let value = ::egui::style::Spacing::default();
    VxSpacing {
        item_spacing: value.item_spacing.into(),
        window_margin: value.window_margin.into(),
        button_padding: value.button_padding.into(),
        menu_margin: value.menu_margin.into(),
        indent: value.indent.into(),
        interact_size: value.interact_size.into(),
        slider_width: value.slider_width.into(),
        slider_rail_height: value.slider_rail_height.into(),
        combo_width: value.combo_width.into(),
        text_edit_width: value.text_edit_width.into(),
        icon_width: value.icon_width.into(),
        icon_width_inner: value.icon_width_inner.into(),
        icon_spacing: value.icon_spacing.into(),
        default_area_size: value.default_area_size.into(),
        tooltip_width: value.tooltip_width.into(),
        menu_width: value.menu_width.into(),
        menu_spacing: value.menu_spacing.into(),
        indent_ends_with_horizontal_line: value.indent_ends_with_horizontal_line.into(),
        combo_height: value.combo_height.into(),
        scroll: value.scroll.into(),
    }
}


/// Specifies the look and feel of egui.
/// 
/// You can change the visuals of a [`Ui`] with [`Ui::style_mut`]
/// and of everything with [`crate::Context::set_style_of`].
/// To choose between dark and light style, use [`crate::Context::set_theme`].
/// 
/// If you want to change fonts, use [`crate::Context::set_fonts`] instead.
#[derive(Copy, Clone)]
#[repr(C)]
pub struct VxStyle {
    /* UNIMPLEMENTED (field) 
    /// If set this will change the default [`TextStyle`] for all widgets.
    /// 
    /// On most widgets you can also set an explicit text style,
    /// which will take precedence over this.
    pub override_text_style: VxOptionTextStyle,*/
    
    /* UNIMPLEMENTED (field) 
    /// If set this will change the font family and size for all widgets.
    /// 
    /// On most widgets you can also set an explicit text style,
    /// which will take precedence over this.
    pub override_font_id: VxOptionCrate::fontId,*/
    
    /// How to vertically align text.
    /// 
    /// Set to `None` to use align that depends on the current layout.
    pub override_text_valign: VxOptionAlign,
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
    pub wrap: VxOptionBool,
    /* UNIMPLEMENTED (field) 
    /// If set, labels, buttons, etc. will use this to determine whether to wrap or truncate the
    /// text at the right edge of the [`Ui`] they are in, or to extend it. By default, this is
    /// `None`.
    /// 
    /// * `None`: follow layout (with may wrap)
    /// * `Some(mode)`: use the specified mode as default
    pub wrap_mode: VxOptionCrate::textWrapMode,*/
    
    /// Sizes and distances between widgets
    pub spacing: VxSpacing,
    /// How and when interaction happens.
    pub interaction: VxInteraction,
    /// Colors etc.
    pub visuals: VxVisuals,
    /// How many seconds a typical animation should last.
    pub animation_time: f32,
    /// Options to help debug why egui behaves strangely.
    /// 
    /// Only available in debug builds.
    pub debug: VxDebugOptions,
    /// Show tooltips explaining [`DragValue`]:s etc when hovered.
    /// 
    /// This only affects a few egui widgets.
    pub explanation_tooltips: bool,
    /// Show the URL of hyperlinks in a tooltip when hovered.
    pub url_in_tooltip: bool,
    /// If true and scrolling is enabled for only one direction, allow horizontal scrolling without pressing shift
    pub always_scroll_the_only_direction: bool,
    /// The animation that should be used when scrolling a [`crate::ScrollArea`] using e.g. [`Ui::scroll_to_rect`].
    pub scroll_animation: VxScrollAnimation,
}

/// Returns the "default value" for a type.
#[no_mangle]
pub extern "C" fn vx_style_default() -> VxStyle {
    let value = ::egui::style::Style::default();
    VxStyle {
        override_text_style: value.override_text_style.into(),
        override_font_id: value.override_font_id.into(),
        override_text_valign: value.override_text_valign.into(),
        text_styles: value.text_styles.into(),
        drag_value_text_style: value.drag_value_text_style.into(),
        number_formatter: value.number_formatter.into(),
        wrap: value.wrap.into(),
        wrap_mode: value.wrap_mode.into(),
        spacing: value.spacing.into(),
        interaction: value.interaction.into(),
        visuals: value.visuals.into(),
        animation_time: value.animation_time.into(),
        debug: value.debug.into(),
        explanation_tooltips: value.explanation_tooltips.into(),
        url_in_tooltip: value.url_in_tooltip.into(),
        always_scroll_the_only_direction: value.always_scroll_the_only_direction.into(),
        scroll_animation: value.scroll_animation.into(),
    }
}


/// Look and feel of the text cursor.
#[derive(Copy, Clone)]
#[repr(C)]
pub struct VxTextCursorStyle {
    /// The color and width of the text cursor
    pub stroke: VxStroke,
    /// Show where the text cursor would be if you clicked?
    pub preview: bool,
    /// Should the cursor blink?
    pub blink: bool,
    /// When blinking, this is how long the cursor is visible.
    pub on_duration: f32,
    /// When blinking, this is how long the cursor is invisible.
    pub off_duration: f32,
}

/// Returns the "default value" for a type.
#[no_mangle]
pub extern "C" fn vx_text_cursor_style_default() -> VxTextCursorStyle {
    let value = ::egui::style::TextCursorStyle::default();
    VxTextCursorStyle {
        stroke: value.stroke.into(),
        preview: value.preview.into(),
        blink: value.blink.into(),
        on_duration: value.on_duration.into(),
        off_duration: value.off_duration.into(),
    }
}


/// Build a [`Ui`] as the child of another [`Ui`].
/// 
/// By default, everything is inherited from the parent,
/// except for `max_rect` which by default is set to
/// the parent [`Ui::available_rect_before_wrap`].
#[derive(Copy, Clone)]
#[repr(C)]
pub struct VxUiBuilder {
    pub id_salt: VxOptionId,
    pub ui_stack_info: VxUiStackInfo,
    pub layer_id: VxOptionLayerId,
    pub max_rect: VxOptionIBox2,
    pub layout: VxOptionLayout,
    pub disabled: bool,
    pub invisible: bool,
    pub sizing_pass: bool,
    /* UNIMPLEMENTED (field) 
    pub style: VxOptionStd::sync::arc,*/
    
    pub sense: VxOptionSense,
}

/// Returns the "default value" for a type.
#[no_mangle]
pub extern "C" fn vx_ui_builder_default() -> VxUiBuilder {
    let value = ::egui::ui_builder::UiBuilder::default();
    VxUiBuilder {
        id_salt: value.id_salt.into(),
        ui_stack_info: value.ui_stack_info.into(),
        layer_id: value.layer_id.into(),
        max_rect: value.max_rect.into(),
        layout: value.layout.into(),
        disabled: value.disabled.into(),
        invisible: value.invisible.into(),
        sizing_pass: value.sizing_pass.into(),
        style: value.style.into(),
        sense: value.sense.into(),
    }
}


/// Information about a [`crate::Ui`] to be included in the corresponding [`UiStack`].
#[derive(Copy, Clone)]
#[repr(C)]
pub struct VxUiStackInfo {
    pub kind: VxOptionUiKind,
    pub frame: VxFrame,
    /* UNIMPLEMENTED (field) 
    pub tags: UiTags,*/
}

/// Returns the "default value" for a type.
#[no_mangle]
pub extern "C" fn vx_ui_stack_info_default() -> VxUiStackInfo {
    let value = ::egui::ui_stack::UiStackInfo::default();
    VxUiStackInfo {
        kind: value.kind.into(),
        frame: value.frame.into(),
        tags: value.tags.into(),
    }
}


/// A wrapper around `dyn Any`, used for passing custom user data
/// to [`crate::ViewportCommand::Screenshot`].
#[derive(Copy, Clone)]
#[repr(C)]
pub struct VxUserData {
    /* UNIMPLEMENTED (field) 
    /// A user value given to the screenshot command,
    /// that will be returned in [`crate::Event::Screenshot`].
    pub data: VxOptionStd::sync::arc,*/
}

/// Returns the "default value" for a type.
#[no_mangle]
pub extern "C" fn vx_user_data_default() -> VxUserData {
    let value = ::egui::data::user_data::UserData::default();
    VxUserData {
        data: value.data.into(),
    }
}


/// A pair of [`ViewportId`], used to identify a viewport and its parent.
#[derive(Copy, Clone)]
#[repr(C)]
pub struct VxViewportIdPair {
    /* UNIMPLEMENTED (field) 
    pub this: ViewportId,*/
    
    /* UNIMPLEMENTED (field) 
    pub parent: ViewportId,*/
}

/// Returns the "default value" for a type.
#[no_mangle]
pub extern "C" fn vx_viewport_id_pair_default() -> VxViewportIdPair {
    let value = ::egui::viewport::ViewportIdPair::default();
    VxViewportIdPair {
        this: value.this.into(),
        parent: value.parent.into(),
    }
}


/// Information about the current viewport, given as input each frame.
/// 
/// `None` means "unknown".
/// 
/// All units are in ui "points", which can be calculated from native physical pixels
/// using `pixels_per_point` = [`crate::Context::zoom_factor`] * `[Self::native_pixels_per_point`];
#[derive(Copy, Clone)]
#[repr(C)]
pub struct VxViewportInfo {
    /* UNIMPLEMENTED (field) 
    /// Parent viewport, if known.
    pub parent: VxOptionCrate::viewportId,*/
    
    /// Name of the viewport, if known.
    pub title: VxOptionString,
    pub events: VEC_TY_TODO(VxViewportEvent),
    /// The OS native pixels-per-point.
    /// 
    /// This should always be set, if known.
    /// 
    /// On web this takes browser scaling into account,
    /// and corresponds to [`window.devicePixelRatio`](https://developer.mozilla.org/en-US/docs/Web/API/Window/devicePixelRatio) in JavaScript.
    pub native_pixels_per_point: VxOptionF32,
    /// Current monitor size in egui points.
    pub monitor_size: VxOptionVec2,
    /// The inner rectangle of the native window, in monitor space and ui points scale.
    /// 
    /// This is the content rectangle of the viewport.
    pub inner_rect: VxOptionIBox2,
    /// The outer rectangle of the native window, in monitor space and ui points scale.
    /// 
    /// This is the content rectangle plus decoration chrome.
    pub outer_rect: VxOptionIBox2,
    /// Are we minimized?
    pub minimized: VxOptionBool,
    /// Are we maximized?
    pub maximized: VxOptionBool,
    /// Are we in fullscreen mode?
    pub fullscreen: VxOptionBool,
    /// Is the window focused and able to receive input?
    /// 
    /// This should be the same as [`RawInput::focused`].
    pub focused: VxOptionBool,
}

/// Returns the "default value" for a type.
#[no_mangle]
pub extern "C" fn vx_viewport_info_default() -> VxViewportInfo {
    let value = ::egui::data::input::ViewportInfo::default();
    VxViewportInfo {
        parent: value.parent.into(),
        title: value.title.into(),
        events: value.events.into(),
        native_pixels_per_point: value.native_pixels_per_point.into(),
        monitor_size: value.monitor_size.into(),
        inner_rect: value.inner_rect.into(),
        outer_rect: value.outer_rect.into(),
        minimized: value.minimized.into(),
        maximized: value.maximized.into(),
        fullscreen: value.fullscreen.into(),
        focused: value.focused.into(),
    }
}


/// Controls the visual style (colors etc) of egui.
/// 
/// You can change the visuals of a [`Ui`] with [`Ui::visuals_mut`]
/// and of everything with [`crate::Context::set_visuals_of`].
/// 
/// If you want to change fonts, use [`crate::Context::set_fonts`] instead.
#[derive(Copy, Clone)]
#[repr(C)]
pub struct VxVisuals {
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
    pub override_text_color: VxOptionColor4,
    /// Visual styles of widgets
    pub widgets: VxWidgets,
    pub selection: VxSelection,
    /// The color used for [`crate::Hyperlink`],
    pub hyperlink_color: VxColor4,
    /// Something just barely different from the background color.
    /// Used for [`crate::Grid::striped`].
    pub faint_bg_color: VxColor4,
    /// Very dark or light color (for corresponding theme).
    /// Used as the background of text edits, scroll bars and others things
    /// that needs to look different from other interactive stuff.
    pub extreme_bg_color: VxColor4,
    /// Background color behind code-styled monospaced labels.
    pub code_bg_color: VxColor4,
    /// A good color for warning text (e.g. orange).
    pub warn_fg_color: VxColor4,
    /// A good color for error text (e.g. red).
    pub error_fg_color: VxColor4,
    pub window_corner_radius: VxCornerRadius,
    pub window_shadow: VxShadow,
    pub window_fill: VxColor4,
    pub window_stroke: VxStroke,
    /// Highlight the topmost window.
    pub window_highlight_topmost: bool,
    pub menu_corner_radius: VxCornerRadius,
    /// Panel background color
    pub panel_fill: VxColor4,
    pub popup_shadow: VxShadow,
    pub resize_corner_size: f32,
    /// How the text cursor acts.
    pub text_cursor: VxTextCursorStyle,
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
    pub handle_shape: VxHandleShape,
    /// Should the cursor change when the user hovers over an interactive/clickable item?
    /// 
    /// This is consistent with a lot of browser-based applications (vscode, github
    /// all turn your cursor into [`CursorIcon::PointingHand`] when a button is
    /// hovered) but it is inconsistent with native UI toolkits.
    pub interact_cursor: VxOptionCursorIcon,
    /// Show a spinner when loading an image.
    pub image_loading_spinners: bool,
    /// How to display numeric color values.
    pub numeric_color_space: VxNumericColorSpace,
}

/// Returns the "default value" for a type.
#[no_mangle]
pub extern "C" fn vx_visuals_default() -> VxVisuals {
    let value = ::egui::style::Visuals::default();
    VxVisuals {
        dark_mode: value.dark_mode.into(),
        override_text_color: value.override_text_color.into(),
        widgets: value.widgets.into(),
        selection: value.selection.into(),
        hyperlink_color: value.hyperlink_color.into(),
        faint_bg_color: value.faint_bg_color.into(),
        extreme_bg_color: value.extreme_bg_color.into(),
        code_bg_color: value.code_bg_color.into(),
        warn_fg_color: value.warn_fg_color.into(),
        error_fg_color: value.error_fg_color.into(),
        window_corner_radius: value.window_corner_radius.into(),
        window_shadow: value.window_shadow.into(),
        window_fill: value.window_fill.into(),
        window_stroke: value.window_stroke.into(),
        window_highlight_topmost: value.window_highlight_topmost.into(),
        menu_corner_radius: value.menu_corner_radius.into(),
        panel_fill: value.panel_fill.into(),
        popup_shadow: value.popup_shadow.into(),
        resize_corner_size: value.resize_corner_size.into(),
        text_cursor: value.text_cursor.into(),
        clip_rect_margin: value.clip_rect_margin.into(),
        button_frame: value.button_frame.into(),
        collapsing_header_frame: value.collapsing_header_frame.into(),
        indent_has_left_vline: value.indent_has_left_vline.into(),
        striped: value.striped.into(),
        slider_trailing_fill: value.slider_trailing_fill.into(),
        handle_shape: value.handle_shape.into(),
        interact_cursor: value.interact_cursor.into(),
        image_loading_spinners: value.image_loading_spinners.into(),
        numeric_color_space: value.numeric_color_space.into(),
    }
}


/// Describes a widget such as a [`crate::Button`] or a [`crate::TextEdit`].
#[derive(Copy, Clone)]
#[repr(C)]
pub struct VxWidgetInfo {
    /// The type of widget this is.
    pub typ: VxWidgetType,
    /// Whether the widget is enabled.
    pub enabled: bool,
    /// The text on labels, buttons, checkboxes etc.
    pub label: VxOptionString,
    /// The contents of some editable text (for [`TextEdit`](crate::TextEdit) fields).
    pub current_text_value: VxOptionString,
    /// The previous text value.
    pub prev_text_value: VxOptionString,
    /// The current value of checkboxes and radio buttons.
    pub selected: VxOptionBool,
    /// The current value of sliders etc.
    pub value: VxOptionF64,
    /// Selected range of characters in [`Self::current_text_value`].
    pub text_selection: VxOptionRangeInclusive,
}


/// Used to store each widget's [Id], [Rect] and [Sense] each frame.
/// 
/// Used to check which widget gets input when a user clicks somewhere.
#[derive(Copy, Clone)]
#[repr(C)]
pub struct VxWidgetRect {
    /// The globally unique widget id.
    /// 
    /// For interactive widgets, this better be globally unique.
    /// If not there will be weird bugs,
    /// and also big red warning test on the screen in debug builds
    /// (see [`crate::Options::warn_on_id_clash`]).
    /// 
    /// You can ensure globally unique ids using [`crate::Ui::push_id`].
    pub id: VxId,
    /// What layer the widget is on.
    pub layer_id: VxLayerId,
    /// The full widget rectangle, in local layer coordinates.
    pub rect: VxIBox2,
    /// Where the widget is, in local layer coordinates.
    /// 
    /// This is after clipping with the parent ui clip rect.
    pub interact_rect: VxIBox2,
    /// How the widget responds to interaction.
    /// 
    /// Note: if [`Self::enabled`] is `false`, then
    /// the widget _effectively_ doesn't sense anything,
    /// but can still have the same `Sense`.
    /// This is because the sense informs the styling of the widget,
    /// but we don't want to change the style when a widget is disabled
    /// (that is handled by the `Painter` directly).
    pub sense: VxSense,
    /// Is the widget enabled?
    pub enabled: bool,
}


/// bg = background, fg = foreground.
#[derive(Copy, Clone)]
#[repr(C)]
pub struct VxWidgetVisuals {
    /// Background color of widgets that must have a background fill,
    /// such as the slider background, a checkbox background, or a radio button background.
    /// 
    /// Must never be [`Color32::TRANSPARENT`].
    pub bg_fill: VxColor4,
    /// Background color of widgets that can _optionally_ have a background fill, such as buttons.
    /// 
    /// May be [`Color32::TRANSPARENT`].
    pub weak_bg_fill: VxColor4,
    /// For surrounding rectangle of things that need it,
    /// like buttons, the box of the checkbox, etc.
    /// Should maybe be called `frame_stroke`.
    pub bg_stroke: VxStroke,
    /// Button frames etc.
    pub corner_radius: VxCornerRadius,
    /// Stroke and text color of the interactive part of a component (button text, slider grab, check-mark, …).
    pub fg_stroke: VxStroke,
    /// Make the frame this much larger.
    pub expansion: f32,
}


/// The visuals of widgets for different states of interaction.
#[derive(Copy, Clone)]
#[repr(C)]
pub struct VxWidgets {
    /// The style of a widget that you cannot interact with.
    /// * `noninteractive.bg_stroke` is the outline of windows.
    /// * `noninteractive.bg_fill` is the background color of windows.
    /// * `noninteractive.fg_stroke` is the normal text color.
    pub noninteractive: VxWidgetVisuals,
    /// The style of an interactive widget, such as a button, at rest.
    pub inactive: VxWidgetVisuals,
    /// The style of an interactive widget while you hover it, or when it is highlighted.
    /// 
    /// See [`Response::hovered`], [`Response::highlighted`] and [`Response::highlight`].
    pub hovered: VxWidgetVisuals,
    /// The style of an interactive widget as you are clicking or dragging it.
    pub active: VxWidgetVisuals,
    /// The style of a button that has an open menu beneath it (e.g. a combo-box)
    pub open: VxWidgetVisuals,
}

/// Returns the "default value" for a type.
#[no_mangle]
pub extern "C" fn vx_widgets_default() -> VxWidgets {
    let value = ::egui::style::Widgets::default();
    VxWidgets {
        noninteractive: value.noninteractive.into(),
        inactive: value.inactive.into(),
        hovered: value.hovered.into(),
        active: value.active.into(),
        open: value.open.into(),
    }
}


/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionPosixTime {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: VxPosixTime,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionString {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: VxString,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionCollapsingState {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: VxCollapsingState,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionState {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: VxState,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionTextEditState {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: VxTextEditState,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionCursorIcon {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: VxCursorIcon,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionTheme {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: VxTheme,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionUiKind {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: VxUiKind,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionBool {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: bool,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionF32 {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: f32,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionF64 {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: f64,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionUsize {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: usize,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionString {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: VxString,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionAlign {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: VxAlign,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionAreaState {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: VxAreaState,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionCCursorRange {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: VxCCursorRange,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionColor4 {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: VxColor4,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionCursor {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: VxCursor,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionCursorRange {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: VxCursorRange,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionFrame {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: VxFrame,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionIBox2 {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: VxIBox2,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionImeOutput {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: VxIMEOutput,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionId {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: VxId,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionLayerId {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: VxLayerId,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionLayout {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: VxLayout,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionMultiTouchInfo {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: VxMultiTouchInfo,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionOpenUrl {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: VxOpenUrl,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionPos2 {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: VxPos2,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionRangeInclusive {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: VxRangeInclusive,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionSense {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: VxSense,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionUiStackInfo {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: VxUiStackInfo,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionVec2 {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: VxVec2,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionWidgetInfo {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: VxWidgetInfo,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionWidgetRect {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: VxWidgetRect,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionArc {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: Arc,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionDebugRect {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: DebugRect,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionDuration {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: Duration,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionGeneric("r") {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: Generic("R"),
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionGeneric("state") {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: Generic("State"),
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionGeneric("t") {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: Generic("T"),
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionGeneric("value") {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: Generic("Value"),
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionImage {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: Image,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionInnerResponse {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: InnerResponse,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionTsTransform {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: TSTransform,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionTextEditState {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: TextEditState,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionTextStyle {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: TextStyle,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionTuple([resolvedPath(path{Path:"layerId",Id:Id(209),Args:Some(angleBracketed{Args:[],Constraints:[]})}),Primitive("usize")]) {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: Tuple([ResolvedPath(Path { path: "LayerId", id: Id(209), args: Some(AngleBracketed { args: [], constraints: [] }) }), Primitive("usize")]),
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionTuple([resolvedPath(path{Path:"emath::rot2",Id:Id(6930),Args:Some(angleBracketed{Args:[],Constraints:[]})}),ResolvedPath(path{Path:"crate::vec2",Id:Id(150),Args:Some(angleBracketed{Args:[],Constraints:[]})})]) {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: Tuple([ResolvedPath(Path { path: "emath::Rot2", id: Id(6930), args: Some(AngleBracketed { args: [], constraints: [] }) }), ResolvedPath(Path { path: "crate::Vec2", id: Id(150), args: Some(AngleBracketed { args: [], constraints: [] }) })]),
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionWidgetText {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: WidgetText,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionCrate::fontId {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: crate::FontId,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionCrate::response {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: crate::Response,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionCrate::textWrapMode {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: crate::TextWrapMode,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionCrate::viewportId {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: crate::ViewportId,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionStd::path::pathBuf {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: std::path::PathBuf,
}

/// A type that may contain a value or nothing.
#[derive(Copy, Clone, Default)]
#[repr(C)]
pub struct VxOptionStd::sync::arc {
    /// True if this is `Some`.
    pub has_value: bool,
    /// The underlying value if this is `Some`.
    pub value: std::sync::Arc,
}

