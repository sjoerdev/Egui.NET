namespace Egui;

/// <summary>
/// A text region that the user can edit the contents of.<br/>
/// See also <see cref="Ui.TextEditSingleline"/> and <see cref="Ui.TextEditMultiline"/>. 
/// </summary>
public ref partial struct TextEdit : IWidget
{
    private string? _textImmutable;
    private ref string _textMutable;
    private TextEditInner _inner;

    private TextEdit(string textImmutable)
    {
        _textImmutable = textImmutable;
    }

    private TextEdit(ref string textMutable)
    {
        _textMutable = ref textMutable;
    }

    /// <summary>
    /// No newlines (<c>\n</c>) allowed. Pressing enter key will result in the <c>TextEdit</c> losing focus (<c>Response.lostFocus</c>).
    /// </summary>
    public static TextEdit Singleline(string text)
    {
        var result = Multiline(text);
        result._inner.DesiredHeightRows = 1;
        result._inner.Multiline = false;
        result._inner.ClipText = true;
        return result;
    }
    
    /// <inheritdoc cref="Singleline"/>
    public static TextEdit Singleline(ref string text)
    {
        var result = Multiline(ref text);
        result._inner.DesiredHeightRows = 1;
        result._inner.Multiline = false;
        result._inner.ClipText = true;
        return result;
    }

    /// <summary>
    /// A <c>TextEdit</c> for code editing.<br/>
    /// 
    /// This will be multiline, monospace, and will insert tabs instead of moving focus.<br/>
    /// 
    /// See also <c>CodeEditor</c>.
    /// </summary>
    public static TextEdit Multiline(string text)
    {
        return new TextEdit(text).SetDefaults();
    }

    /// <inheritdoc cref="Multiline"/>
    public static TextEdit Multiline(ref string text)
    {
        return new TextEdit(ref text).SetDefaults();
    }

    /// <summary>
    /// Build a <c>TextEdit</c> focused on code editing.
    /// By default it comes with:
    /// - monospaced font
    /// - focus lock (tab will insert a tab character instead of moving focus)
    /// </summary>
    public readonly TextEdit CodeEditor()
    {
        return Font(new FontSelection.Style(new TextStyle.Monospace())).LockFocus(true);
    }

    /// <summary>
    /// Use if you want to set an explicit <c>Id</c> for this widget.
    /// </summary>
    public readonly TextEdit Id(Id id)
    {
        var result = this;
        result._inner.Id = id;
        return result;
    }

    /// <summary>
    /// A source for the unique <c>Id</c>, e.g. <c>.idSalt("secondTextEditField")</c> or <c>.idSalt(loopIndex)</c>.
    /// </summary>
    public readonly TextEdit IdSource(Id idSalt)
    {
        return IdSalt(idSalt);
    }

    /// <summary>
    /// Use if you want to set an explicit <c>Id</c> for this widget.
    /// </summary>
    public readonly TextEdit IdSalt(Id idSalt)
    {
        var result = this;
        result._inner.IdSalt = idSalt;
        return result;
    }

    /// <summary>
    /// Show a faint hint text when the text field is empty.<br/>
    /// 
    /// If the hint text needs to be persisted even when the text field has input,
    /// the following workaround can be used:
    /// </summary>
    public readonly TextEdit HintText(WidgetText hintText)
    {
        var result = this;
        result._inner.HintText = hintText;
        return result;
    }

    /// <summary>
    /// Set the background color of the <c>TextEdit</c>. The default is <c>TextEditBgColor</c>.
    /// </summary>
    public readonly TextEdit BackgroundColor(Color32 color)
    {
        var result = this;
        result._inner.BackgroundColor = color;
        return result;
    }

    /// <summary>
    /// Set a specific style for the hint text.
    /// </summary>
    public readonly TextEdit HintTextFont(FontSelection hintTextFont)
    {
        var result = this;
        result._inner.HintTextFont = hintTextFont;
        return result;
    }

    /// <summary>
    /// If true, hide the letters from view and prevent copying from the field.
    /// </summary>
    public readonly TextEdit Password(bool password)
    {
        var result = this;
        result._inner.Password = password;
        return result;
    }

    /// <summary>
    /// Pick a <c>FontId</c> or <c>TextStyle</c>.
    /// </summary>
    public readonly TextEdit Font(FontSelection fontSelection)
    {
        var result = this;
        result._inner.FontSelection = fontSelection;
        return result;
    }

    public readonly TextEdit TextColor(Color32 textColor)
    {
        var result = this;
        result._inner.TextColor = textColor;
        return result;
    }

    public readonly TextEdit TextColoOpt(Color32? textColor)
    {
        var result = this;
        result._inner.TextColor = textColor;
        return result;
    }

    /// <summary>
    /// Default is <c>True</c>. If set to <c>False</c> then you cannot interact with the text (neither edit or select it).<br/>
    /// 
    /// Consider using <c>AddEnabled</c> instead to also give the <c>TextEdit</c> a greyed out look.
    /// </summary>
    public readonly TextEdit Interactive(bool interative)
    {
        var result = this;
        result._inner.Interactive = interative;
        return result;
    }

    /// <summary>
    /// Default is <c>True</c>. If set to <c>False</c> there will be no frame showing that this is editable text!
    /// </summary>
    public readonly TextEdit Frame(bool frame)
    {
        var result = this;
        result._inner.Frame = frame;
        return result;
    }

    /// <summary>
    /// Set margin of text. Default is <c>Symmetric(4.0,2.0)</c>
    /// </summary>
    public readonly TextEdit Margin(Margin margin)
    {
        var result = this;
        result._inner.Margin = margin;
        return result;
    }

    /// <summary>
    /// Set to 0.0 to keep as small as possible.
    /// Set to <c>Infinity</c> to take up all available space (i.e. disable automatic word wrap).
    /// </summary>
    public readonly TextEdit DesiredWidth(float desiredWidth)
    {
        var result = this;
        result._inner.DesiredWidth = desiredWidth;
        return result;
    }

    /// <summary>
    /// Set the number of rows to show by default.
    /// The default for singleline text is <c>1</c>.
    /// The default for multiline text is <c>4</c>.
    /// </summary>
    public readonly TextEdit DesiredRows(nuint desiredHeightRows)
    {
        var result = this;
        result._inner.DesiredHeightRows = desiredHeightRows;
        return result;
    }

    public readonly TextEdit LockFocus(bool tabWillIndent)
    {
        var result = this;
        result._inner.EventFilter.Tab = tabWillIndent;
        return result;
    }

    /// <summary>
    /// When <c>True</c> (default), the cursor will initially be placed at the end of the text.<br/>
    /// 
    /// When <c>False</c>, the cursor will initially be placed at the beginning of the text.
    /// </summary>
    public readonly TextEdit CursorAtEnd(bool b)
    {
        var result = this;
        result._inner.CursorAtEnd = b;
        return result;
    }

    /// <summary>
    /// When <c>True</c> (default), overflowing text will be clipped.<br/>
    /// 
    /// When <c>False</c>, widget width will expand to make all text visible.<br/>
    /// 
    /// This only works for singleline <c>TextEdit</c>.
    /// </summary>
    public readonly TextEdit ClipText(bool b)
    {
        var result = this;
        if (!result._inner.Multiline)
        {
            result._inner.ClipText = b;   
        }
        return result;
    }

    /// <summary>
    /// Sets the limit for the amount of characters can be entered<br/>
    /// 
    /// This only works for singleline <c>TextEdit</c>
    /// </summary>
    public readonly TextEdit ChatLimit(nuint limit)
    {
        var result = this;
        result._inner.CharLimit = limit;
        return result;
    }

    /// <summary>
    /// Set the horizontal align of the inner text.
    /// </summary>
    public readonly TextEdit HorizontalAlign(Align align)
    {
        var result = this;
        result._inner.Align = new Align2(align, result._inner.Align.Y);
        return result;
    }

    /// <summary>
    /// Set the vertical align of the inner text.
    /// </summary>
    public readonly TextEdit VerticalAlign(Align align)
    {
        var result = this;
        result._inner.Align = new Align2(result._inner.Align.X, align);
        return result;
    }

    /// <summary>
    /// Set the minimum size of the <c>TextEdit</c>.
    /// </summary>
    public readonly TextEdit MinSize(Vec2 minSize)
    {
        var result = this;
        result._inner.MinSize = minSize;
        return result;
    }

    /// <summary>
    /// Set the return key combination.<br/>
    /// 
    /// This combination will cause a newline on multiline,
    /// whereas on singleline it will cause the widget to lose focus.<br/>
    /// 
    /// This combination is optional and can be disabled by passing <c>None</c> into this function.
    /// </summary>
    public readonly TextEdit ReturnKey(KeyboardShortcut? returnKey)
    {
        var result = this;
        result._inner.ReturnKey = returnKey;
        return result;
    }

    /// <summary>
    /// Sets all values to their default.
    /// </summary>
    private readonly TextEdit SetDefaults()
    {
        var result = this;
        result._inner.HintText = new WidgetText();
        result._inner.HintTextFont = null;
        result._inner.Id = null;
        result._inner.IdSalt = null;
        result._inner.FontSelection = new FontSelection();
        result._inner.TextColor = null;
        result._inner.Password = false;
        result._inner.Frame = true;
        result._inner.Margin = Egui.Margin.Symmetric(4, 2);
        result._inner.Multiline = true;
        result._inner.Interactive = true;
        result._inner.DesiredWidth = null;
        result._inner.DesiredHeightRows = 4;
        result._inner.EventFilter = new EventFilter
        {
            HorizontalArrows = true,
            VerticalArrows = true,
            Tab = false
        };
        result._inner.CursorAtEnd = true;
        result._inner.MinSize = new Vec2(0, 0);
        result._inner.Align = Align2.LeftTop;
        result._inner.ClipText = false;
        result._inner.CharLimit = nuint.MaxValue;
        result._inner.ReturnKey = new KeyboardShortcut(new Modifiers(), Key.Enter);
        result._inner.BackgroundColor = null;
        return result;
    }

    /// <inheritdoc/>
    Response IWidget.Ui(Ui ui)
    {
        ui.AssertInitialized();
        var mutable = _textImmutable is null;
        var textToSend = mutable ? _textMutable : _textImmutable;
        var (response, newText) = EguiMarshal.Call<nuint, TextEditInner, string, bool, (Response, string)>(EguiFn.egui_widgets_text_edit_builder_TextEdit_ui, ui.Ptr, _inner, textToSend!, _textImmutable is null);
        if (mutable)
        {
            _textMutable = newText;
        }
        return response;
    }

    private struct TextEditInner
    {
        public Egui.WidgetText HintText;
        public Egui.FontSelection? HintTextFont;
        public Egui.Id? Id;
        public Egui.Id? IdSalt;
        public Egui.FontSelection FontSelection;
        public Egui.Color32? TextColor;
        public bool Password;
        public bool Frame;
        public Egui.Margin Margin;
        public bool Multiline;
        public bool Interactive;
        public float? DesiredWidth;
        public ulong DesiredHeightRows;
        public Egui.EventFilter EventFilter;
        public bool CursorAtEnd;
        public Egui.Vec2 MinSize;
        public Egui.Align2 Align;
        public bool ClipText;
        public ulong CharLimit;
        public Egui.KeyboardShortcut? ReturnKey;
        public Egui.Color32? BackgroundColor;

        internal static void Serialize(Bincode.BincodeSerializer serializer, TextEditInner value) => value.Serialize(serializer);

        internal void Serialize(Bincode.BincodeSerializer serializer)
        {
            serializer.increase_container_depth();
            HintText.Serialize(serializer);
            serialize_option_FontSelection(HintTextFont, serializer);
            Egui.TraitHelpers.serialize_option_Id(Id, serializer);
            Egui.TraitHelpers.serialize_option_Id(IdSalt, serializer);
            FontSelection.Serialize(serializer);
            Egui.TraitHelpers.serialize_option_Color32(TextColor, serializer);
            serializer.serialize_bool(Password);
            serializer.serialize_bool(Frame);
            Margin.Serialize(serializer);
            serializer.serialize_bool(Multiline);
            serializer.serialize_bool(Interactive);
            Egui.TraitHelpers.serialize_option_f32(DesiredWidth, serializer);
            serializer.serialize_u64(DesiredHeightRows);
            EventFilter.Serialize(serializer);
            serializer.serialize_bool(CursorAtEnd);
            MinSize.Serialize(serializer);
            Align.Serialize(serializer);
            serializer.serialize_bool(ClipText);
            serializer.serialize_u64(CharLimit);
            serialize_option_KeyboardShortcut(ReturnKey, serializer);
            Egui.TraitHelpers.serialize_option_Color32(BackgroundColor, serializer);
            serializer.decrease_container_depth();
        }

        internal static TextEditInner Deserialize(Bincode.BincodeDeserializer deserializer) => throw new NotSupportedException();

        private static void serialize_option_FontSelection(Egui.FontSelection? value, Bincode.BincodeSerializer serializer)
        {
            if (value is not null)
            {
                serializer.serialize_option_tag(true);
                (value ?? default).Serialize(serializer);
            }
            else
            {
                serializer.serialize_option_tag(false);
            }
        }

        private static void serialize_option_KeyboardShortcut(Egui.KeyboardShortcut? value, Bincode.BincodeSerializer serializer)
        {
            if (value is not null)
            {
                serializer.serialize_option_tag(true);
                (value ?? default).Serialize(serializer);
            }
            else
            {
                serializer.serialize_option_tag(false);
            }
        }
    }
}