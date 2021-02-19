using System;
using System.Drawing;

namespace LodAutoBot
{
    public class WindowInfo
    {
        public int ProcessId;
        public IntPtr Handle;
        public string ClassName;
        public string Text;
        public Rectangle Rectangle;

        public WindowInfo(IntPtr Handle)
        {
            SetInfo(Handle);
        }

        public WindowInfo(Point cursorPosition)
        {
            SetInfo(WindowsInfoExpansion.WindowFromPoint(cursorPosition));
        }

        private void SetInfo(IntPtr Handle)
        {
            this.Handle = Handle;
            ClassName = WindowsInfoExpansion.GetWindowClassName(Handle);
            Text = WindowsInfoExpansion.GetWindowText(Handle);
            Rectangle = WindowsInfoExpansion.GetWindowRectangle(Handle);
            WindowsInfoExpansion.GetWindowThreadProcessId(Handle, out ProcessId);
        }
    }

}
