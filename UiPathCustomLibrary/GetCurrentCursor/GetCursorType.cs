using System;
using System.Activities;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GetCurrentCursor
{
    public class GetCursorType : CodeActivity

    {
        [Category("Output"), Description("Cursor information will be returned as String representation")]
        public OutArgument<string> CursorType { get; set; }
        protected override void Execute(CodeActivityContext context)
        {
            var h = Cursors.WaitCursor.Handle;

            CURSORINFO pci;
            pci.cbSize = Marshal.SizeOf(typeof(CURSORINFO));
            GetCursorInfo(out pci);
            CursorType.Set(context, pci.hCursor.ToString());
        }
        struct POINT
        {
            public Int32 x;
            public Int32 y;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct CURSORINFO
        {
            public Int32 cbSize;        // Specifies the size, in bytes, of the structure. 
                                        // The caller must set this to Marshal.SizeOf(typeof(CURSORINFO)).
            public Int32 flags;         // Specifies the cursor state. This parameter can be one of the following values:
                                        //    0             The cursor is hidden.
                                        //    CURSOR_SHOWING    The cursor is showing.
            public IntPtr hCursor;          // Handle to the cursor. 
            public POINT ptScreenPos;       // A POINT structure that receives the screen coordinates of the cursor. 
        }

        [DllImport("user32.dll")]
        static extern bool GetCursorInfo(out CURSORINFO pci);
    }
}
