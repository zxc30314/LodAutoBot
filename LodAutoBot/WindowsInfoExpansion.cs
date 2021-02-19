using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace LodAutoBot
{
    public class WindowsInfoExpansion
    {
        public static string GetWindowClassName(IntPtr handle)
        {
            StringBuilder buffer = new StringBuilder(128);
            GetClassName(handle, buffer, buffer.Capacity);
            return buffer.ToString();
        }

        public static string GetWindowText(IntPtr handle)
        {
            int WM_GETTEXT = 0xD;
            int WM_GETTEXTLENGTH = 0x000E;
            StringBuilder buffer = new StringBuilder(SendMessage(handle, WM_GETTEXTLENGTH, 0, 0) + 1);
            SendMessage(handle, WM_GETTEXT, buffer.Capacity, buffer);
            return buffer.ToString();
        }
        public static Rectangle GetWindowRectangle(IntPtr handle)
        {
            Rect rect = new Rect();
            GetWindowRect(handle, out rect);
            return new Rectangle(rect.Left, rect.Top, (rect.Right - rect.Left) + 1, (rect.Bottom - rect.Top) + 1);
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(Point point);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassName(IntPtr handle, StringBuilder ClassName, int MaxCount);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr handle, int msg, int Param1, int Param2);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr handle, int msg, int Param, System.Text.StringBuilder text);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr handle, out Rect Rect);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        [DllImport("user32.dll")]
        private static extern int SetWindowText(IntPtr hWnd, string text);

    }

}
