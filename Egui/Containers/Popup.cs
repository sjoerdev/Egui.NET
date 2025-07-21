using System.Collections.Immutable;

namespace Egui.Containers;

/// <summary>
/// A popup container.
/// </summary>
public ref struct Popup
{
    /// <summary>
    /// Return the anchor rect of the popup.
    /// 
    /// Returns <c>null</c> if the anchor is <see cref="PopupAnchor.Pointer"/>. 
    /// </summary>
    public Rect? AnchorRect => _anchor.Rect(_id, Ctx);

    /// <summary>
    /// Get the expected size of the popup.
    /// </summary>
    public Vec2? ExpectedSize => AreaState.Load(Ctx, _id)?.Size;

    /// <summary>
    /// Calculate the best alignment for the popup, based on the last size and screen rect.
    /// </summary>
    public RectAlign BestAlign
    {
        get
        {
            var expectedPopupSize = ExpectedSize.GetValueOrDefault(new Vec2(_width.GetValueOrDefault(), 0));
            var maybeAnchorRect = _anchor.Rect(_id, Ctx);
            if (!maybeAnchorRect.HasValue)
            {
                return _rectAlign;
            }

            var anchorRect = maybeAnchorRect.Value;
            return RectAlign.FindBestAlign(new[] { _rectAlign }
                .Concat(_alternativeAligns ?? _rectAlign.Symmetries.Concat(RectAlign.MenuAligns)),
                Ctx.ScreenRect,
                anchorRect,
                _gap,
                expectedPopupSize).GetValueOrDefault();
        }
    }

    /// <summary>
    /// Get the expected rect the popup will be shown in.
    /// 
    /// Returns <c>null</c> if the popup wasn't shown before or anchor is <see cref="PopupAnchor.Pointer"/> and
    /// there is no pointer. 
    /// </summary>
    public Rect? PopupRect
    {
        get
        {
            var size = ExpectedSize;
            var anchor = AnchorRect;
            if (size.HasValue && anchor.HasValue)
            {
                return BestAlign.AlignRect(anchor.Value, size.Value, _gap);
            }
            else
            {
                return null;
            }
        }
    }

    /// <summary>
    /// Is the popup open?
    /// </summary>
    public bool IsOpen
    {
        get
        {
            switch (_openKind)
            {
                case OpenKind.Open:
                    return true;
                case OpenKind.Closed:
                    return false;
                case OpenKind.Bool:
                    return _open;
                default:
                    return IsIdOpen(Ctx, _id);
            }
        }
    }

    private Id _id;
    public readonly Context Ctx;
    private PopupAnchor _anchor;
    private RectAlign _rectAlign;
    private ImmutableList<RectAlign>? _alternativeAligns;
    private LayerId _layerId;
    private OpenKind _openKind;
    private SetOpenCommand? _memoryCommand;
    private ref bool _open;
    private PopupCloseBehavior _closeBehavior;
    private UiStackInfo? _info;
    private PopupKind _kind;
    private float _gap;
    private bool _widgetClickedElsewhere;
    private float? _width;
    private Sense _sense;
    private Layout _layout;
    private Frame? _frame;
    private Style? _style;
    private StyleMode _styleMode;


    /// <summary>
    /// Create a new popup
    /// </summary>
    public Popup(Id id, Context ctx, PopupAnchor anchor, LayerId layerId)
    {
        _id = id;
        Ctx = ctx;
        _anchor = anchor;
        _openKind = OpenKind.Open;
        _info = null;
        _kind = PopupKind.Popup;
        _layerId = layerId;
        _rectAlign = RectAlign.BottomStart;
        _alternativeAligns = null;
        _width = null;
        _sense = Egui.Sense.Click;
        _layout = new Layout();
        _frame = null;
        _style = null;
        _styleMode = StyleMode.None;
    }

    /// <summary>
    /// Show a popup relative to some widget.
    /// The popup will  always be open.
    /// 
    /// See <see cref="Menu"/> and <see cref="ContextMenu"/> for common use cases.  
    /// </summary>
    public static Popup FromResponse(Response response)
    {
        var result = new Popup(DefaultResponseId(response), null!, response, response.LayerId);
        result._widgetClickedElsewhere = response.ClickedElsewhere;
        return result;
    }

    /// <summary>
    /// Show a popup relative to some widget,
    /// toggling the open state based on the widget's click state.
    /// 
    /// See <see cref="Menu"/> and <see cref="ContextMenu"/> for common use cases.
    /// </summary>=
    public static Popup FromToggleButtonResponse(Response buttonResponse)
    {
        SetOpenCommand? command;
        if (buttonResponse.Clicked)
        {
            command = new SetOpenCommand.Toggle();
        }
        else
        {
            command = null;
        }

        return FromResponse(buttonResponse)
            .OpenMemory(command);
    }

    /// <summary>
    /// Show a popup when the widget was clicked.
    /// Sets the layout to <c>Layout.TopDownJustified(Align.Min)</c>.
    /// </summary>
    public static Popup Menu(Response buttonResponse)
    {
        var result = FromToggleButtonResponse(buttonResponse)
            .Kind(PopupKind.Menu)
            .Layout(Egui.Layout.TopDownJustified(Egui.Align.Min))
            .Gap(0);
        result._styleMode = StyleMode.MenuStyle;
        return result;
    }

    /// <summary>
    /// Store the open state via <see cref="Memory"/>.
    /// You can set the state via the first <see cref="SetOpenCommand"/> param.  
    /// </summary>
    public static Popup ContextMenu(Response response)
    {
        SetOpenCommand? command;
        if (response.SecondaryClicked)
        {
            command = new SetOpenCommand.Bool { Value = true };
        }
        else if (response.Clicked)
        {
            command = new SetOpenCommand.Bool { Value = false };
        }
        else
        {
            command = null;
        }

        return Menu(response)
            .OpenMemory(command)
            .AtPointerFixed();
    }

    private Popup(Popup previous, ref bool open)
    {
        this = previous;
        _openKind = OpenKind.Bool;
        _open = ref open;
    }

    /// <summary>
    /// The default ID when constructing a popup from the <see cref="Response"/>. 
    /// </summary>
    public static Id DefaultResponseId(Response response)
    {
        return response.Id.With("popup");
    }

    /// <summary>
    /// Close all currently open popups.
    /// </summary>
    public static void CloseAll(Context ctx)
    {
        EguiMarshal.Call(EguiFn.egui_containers_popup_Popup_close_all, ctx.Handle.ptr);
    }

    /// <summary>
    /// Close the given popup, if it is open.
    /// 
    /// See also <see cref="CloseAll"/> if you want to close any/all currently open popups.
    /// </summary>
    public static void CloseId(Context ctx, Id id)
    {
        EguiMarshal.Call(EguiFn.egui_containers_popup_Popup_close_id, ctx.Handle.ptr, id);
    }

    /// <summary>
    /// Open the given popup and close all others.
    /// 
    /// If you are NOT using <see cref="Show"/>, you must also call
    /// <see cref="Memory.KeepPopupOpen"/> as long as you're showing the popup.  
    /// </summary>
    public static void OpenId(Context ctx, Id id)
    {
        EguiMarshal.Call(EguiFn.egui_containers_popup_Popup_open_id, ctx.Handle.ptr, id);
    }

    /// <summary>
    /// Toggle the given popup between closed and open.
    /// Note: At most, only one popup can be open at a time.
    /// </summary>
    public static void ToggleId(Context ctx, Id id)
    {
        EguiMarshal.Call(EguiFn.egui_containers_popup_Popup_toggle_id, ctx.Handle.ptr, id);
    }

    /// <summary>
    /// Get the position for this popup, if it is open.
    /// </summary>
    public static Pos2? PositionOfId(Context ctx, Id id)
    {
        return EguiMarshal.Call<Id, Pos2?>(EguiFn.egui_containers_popup_Popup_position_of_id, ctx.Handle.ptr, id);
    }

    /// <summary>
    /// Is the given popup open?
    /// </summary>
    public static bool IsIdOpen(Context ctx, Id id)
    {
        return EguiMarshal.Call<Id, bool>(EguiFn.egui_containers_popup_Popup_is_id_open, ctx.Handle.ptr, id);
    }

    /// <summary>
    /// Is the given popup open?
    /// </summary>
    public static bool IsAnyOpen(Context ctx)
    {
        return EguiMarshal.Call<bool>(EguiFn.egui_containers_popup_Popup_is_any_open, ctx.Handle.ptr);
    }

    /// <summary>
    /// Get the ID of the popup.
    /// </summary>
    public readonly Id GetId()
    {
        return _id;
    }

    /// <summary>
    /// Return the <see cref="PopupAnchor"/> of the popup. 
    /// </summary>
    public readonly PopupAnchor GetAnchor()
    {
        return _anchor;
    }

    /// <summary>
    /// Set the kind of the popup. Used for <see cref="Area.Kind"/> and <see cref="Area.Order"/>.
    /// </summary>
    public readonly Popup Kind(PopupKind kind)
    {
        var result = this;
        result._kind = kind;
        return result;
    }

    /// <summary>
    /// Set the <see cref="UiStackInfo"/> of the popup's <see cref="Ui"/>.  
    /// </summary>
    public readonly Popup Info(UiStackInfo info)
    {
        var result = this;
        result._info = info;
        return result;
    }

    /// <summary>
    /// Set the <see cref="RectAlign"/> of the popup relative to the <see cref="PopupAnchor"/> .
    /// This is the default position, and will be used if it fits.
    /// See <see cref="AlignAlternatives"/> for more on this.
    /// </summary>
    public readonly Popup Align(RectAlign positionAlign)
    {
        var result = this;
        result._rectAlign = positionAlign;
        return result;
    }

    /// <summary>
    /// Set alternative positions to try if the default one doesn't fit. Set to an empty slice to
    /// always use the position you set with <see cref="Align"/> .
    /// By default, this will try <see cref="RectAlign.Symmetries"/> and then <see cref="RectAlign.MenuAligns"/> .
    /// </summary>
    public readonly Popup AlignAlternatives(ImmutableList<RectAlign> alternatives)
    {
        var result = this;
        result._alternativeAligns = alternatives;
        return result;
    }

    /// <summary>
    /// Set whether the popup is open or closed.
    /// </summary>
    public readonly Popup Open(ref bool open)
    {
        return new Popup(this, ref open);
    }

    public readonly Popup OpenMemory(SetOpenCommand? command)
    {
        var result = this;
        result._openKind = OpenKind.Memory;
        result._memoryCommand = command;
        return result;
    }

    /// <summary>
    /// Set the close behavior of a popup.
    /// 
    /// This will do nothing if <see cref="Open"/> was called. 
    /// </summary>
    public readonly Popup CloseBehavior(PopupCloseBehavior closeBehavior)
    {
        var result = this;
        result._closeBehavior = closeBehavior;
        return result;
    }

    /// <summary>
    /// Show the popup relative to the pointer.
    /// </summary>
    public readonly Popup AtPointer()
    {
        var result = this;
        result._anchor = new PopupAnchor.Pointer();
        return result;
    }

    /// <summary>
    /// Remember the pointer position at the time of opening the popup, and show the popup
    /// relative to that.
    /// </summary>
    public readonly Popup AtPointerFixed()
    {
        var result = this;
        result._anchor = new PopupAnchor.PointerFixed();
        return result;
    }

    /// <summary>
    /// Show the popup relative to a specific position.
    /// </summary>
    public readonly Popup AtPosition(Pos2 position)
    {
        var result = this;
        result._anchor = new PopupAnchor.Position() { Value = position };
        return result;
    }

    /// <summary>
    /// Show the popup relative to the given <see cref="PopupAnchor"/> .
    /// </summary>
    public readonly Popup Anchor(PopupAnchor anchor)
    {
        var result = this;
        result._anchor = anchor;
        return result;
    }

    /// <summary>
    /// Set the gap between the anchor and the popup.
    /// </summary>
    public readonly Popup Gap(float gap)
    {
        var result = this;
        result._gap = gap;
        return result;
    }

    /// <summary>
    /// Set the frame of the popup.
    /// </summary>
    public readonly Popup Frame(Frame frame)
    {
        var result = this;
        result._frame = frame;
        return result;
    }

    /// <summary>
    /// Set the sense of the popup.
    /// </summary>
    public readonly Popup Sense(Sense sense)
    {
        var result = this;
        result._sense = sense;
        return result;
    }

    /// <summary>
    /// Set the layout of the popup.
    /// </summary>
    public readonly Popup Layout(Layout layout)
    {
        var result = this;
        result._layout = layout;
        return result;
    }

    /// <summary>
    /// The width that will be passed to <see cref="Area.DefaultWidth"/> .
    /// </summary>
    public readonly Popup Width(float width)
    {
        var result = this;
        result._width = width;
        return result;
    }

    /// <summary>
    /// Set the id of the Area.
    /// </summary>
    public readonly Popup Id(Id id)
    {
        var result = this;
        result._id = id;
        return result;
    }

    /// <summary>
    /// Set the style for the popup contents.
    /// 
    /// Default: is <c>MenuStyle</c> for <see cref="Menu"/> and <see cref="ContextMenu"/>
    /// or <c>null</c> otherwise.  
    /// </summary>
    public readonly Popup Style(Style style)
    {
        var result = this;
        result._style = style;
        result._styleMode = StyleMode.OverrideStyle;
        return this;
    }

    private enum StyleMode
    {
        None,
        MenuStyle,
        OverrideStyle
    }

    private enum OpenKind
    {
        Open,
        Closed,
        Bool,
        Memory
    }
}