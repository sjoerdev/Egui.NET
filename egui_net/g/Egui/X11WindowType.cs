#pragma warning disable
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public enum X11WindowType {
            /// <summary>
            /// This is a normal, top-level window.
            /// </summary>
            Normal = 0,
            /// <summary>
            /// A desktop feature. This can include a single window containing desktop icons with the same dimensions as the
            /// screen, allowing the desktop environment to have full control of the desktop, without the need for proxying
            /// root window clicks.
            /// </summary>
            Desktop = 1,
            /// <summary>
            /// A dock or panel feature. Typically a Window Manager would keep such windows on top of all other windows.
            /// </summary>
            Dock = 2,
            /// <summary>
            /// Toolbar windows. "Torn off" from the main application.
            /// </summary>
            Toolbar = 3,
            /// <summary>
            /// Pinnable menu windows. "Torn off" from the main application.
            /// </summary>
            Menu = 4,
            /// <summary>
            /// A small persistent utility window, such as a palette or toolbox.
            /// </summary>
            Utility = 5,
            /// <summary>
            /// The window is a splash screen displayed as an application is starting up.
            /// </summary>
            Splash = 6,
            /// <summary>
            /// This is a dialog window.
            /// </summary>
            Dialog = 7,
            /// <summary>
            /// A dropdown menu that usually appears when the user clicks on an item in a menu bar.
            /// This property is typically used on override-redirect windows.
            /// </summary>
            DropdownMenu = 8,
            /// <summary>
            /// A popup menu that usually appears when the user right clicks on an object.
            /// This property is typically used on override-redirect windows.
            /// </summary>
            PopupMenu = 9,
            /// <summary>
            /// A tooltip window. Usually used to show additional information when hovering over an object with the cursor.
            /// This property is typically used on override-redirect windows.
            /// </summary>
            Tooltip = 10,
            /// <summary>
            /// The window is a notification.
            /// This property is typically used on override-redirect windows.
            /// </summary>
            Notification = 11,
            /// <summary>
            /// This should be used on the windows that are popped up by combo boxes.
            /// This property is typically used on override-redirect windows.
            /// </summary>
            Combo = 12,
            /// <summary>
            /// This indicates the window is being dragged.
            /// This property is typically used on override-redirect windows.
            /// </summary>
            Dnd = 13,
    }
    internal static class X11WindowTypeExtensions {

        internal static void Serialize(this X11WindowType value, Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_variant_index((int)value);
            serializer.decrease_container_depth();
        }

        internal static X11WindowType Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            int index = deserializer.deserialize_variant_index();
            if (!Enum.IsDefined(typeof(X11WindowType), index))
                throw new Serde.DeserializationException("Unknown variant index for X11WindowType: " + index);
            X11WindowType value = (X11WindowType)index;
            deserializer.decrease_container_depth();
            return value;
        }
    }

} // end of namespace Egui
