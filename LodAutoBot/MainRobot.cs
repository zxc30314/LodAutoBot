using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using op_x86Net;
public class MainRobot
{
    // OpInterface op;
    public MainRobot()
    {
       // op = new OpInterface();
    }
    IntPtr hwnd;

    public enum VMKey : uint
    {
        Move = 0x0200,
        LeftDown = 0x201,
        LeftUp = 0x202
    }

    public enum MKKey : int
    {
        LButton = 0x01,
        ///<summary>
        ///Right mouse button
        ///</summary>
        RButton = 0x02,
        ///<summary>
        ///Control-break processing
        ///</summary>
        Cancel = 0x03,
        ///<summary>
        ///Middle mouse button (three-button mouse)
        ///</summary>
        MButton = 0x04,

    }
    #region dll引用
    [DllImport("user32.dll")]
    private static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rectangle rect);

    [DllImport("gdi32.dll")]
    private static extern IntPtr CreateCompatibleDC(
     IntPtr hdc // handle to DC
     );
    [DllImport("gdi32.dll")]
    private static extern IntPtr CreateCompatibleBitmap(
     IntPtr hdc,         // handle to DC
     int nWidth,      // width of bitmap, in pixels
     int nHeight      // height of bitmap, in pixels
     );
    [DllImport("gdi32.dll")]
    private static extern IntPtr SelectObject(
     IntPtr hdc,           // handle to DC
     IntPtr hgdiobj    // handle to object
     );
    [DllImport("gdi32.dll")]
    private static extern int DeleteDC(
     IntPtr hdc           // handle to DC
     );
    [DllImport("user32.dll")]
    private static extern bool PrintWindow(
     IntPtr hwnd,                // Window to copy,Handle to the window that will be copied.
     IntPtr hdcBlt,              // HDC to print into,Handle to the device context.
     UInt32 nFlags               // Optional flags,Specifies the drawing options. It can be one of the following values.
     );
    [DllImport("user32.dll")]
    private static extern IntPtr GetWindowDC(
     IntPtr hwnd
     );


    [DllImport("User32.Dll", EntryPoint = "PostMessageA")]
    private static extern bool PostMessage(IntPtr hWnd, uint msg, int wParam, int lParam);



    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetForegroundWindow(IntPtr hWnd);
    #endregion
    bool PostMessage(IntPtr hWnd, VMKey vMKey, MKKey mKKey, int IParm)
    {
        return PostMessage(hWnd, (uint)vMKey, (int)mKKey, IParm);
    }
    int _x, _y;
    IntPtr _handle;

    #region MouseInput
    /*
    int MakeLparam(int x, int y)
    {

        return x | (y << 16);
    }
    //public void SetHandle(int handle)
    //{
    //    _handle = (IntPtr)handle;
    //}


    public void LeftClick()
    {

        LeftClick(new Point(_x, _y));

    }
    public void LeftClick(Point p)
    {
        PostMessage((IntPtr)_handle, VMKey.LeftUp, (uint)0, MakeLparam(p.X, p.Y));
        PostMessage((IntPtr)_handle, VMKey.LeftDown, MKKey.LButton, MakeLparam(p.X, p.Y));
    //    PostMessage((IntPtr)_handle, VMKey.LeftDown, MKKey.LButton, MakeLparam(p.X, p.Y));
        PostMessage((IntPtr)_handle, VMKey.LeftUp,(uint)0, MakeLparam(p.X, p.Y));
    }
    public void LeftDown()
    {
        PostMessage((IntPtr)_handle, VMKey.LeftDown, MKKey.LButton, MakeLparam(_x, _y));

    }
    public void LeftUp()
    {

        PostMessage((IntPtr)_handle, VMKey.LeftUp, MKKey.LButton, MakeLparam(_x, _y));

    }
    public void MoveTo(int x, int y)
    {

        PostMessage((IntPtr)_handle, VMKey.Move, MKKey.LButton, MakeLparam(x, y));
        _x = x;
        _y = y;
    }
    public void MoveR(int rx, int ry)
    {
        MoveTo(_x + rx, _y + ry);
    }
    */
    #endregion

    [DllImport("op_x86.dll")]
    private static extern int iBindWindow(int hwnd, string display, string mouse, string keypad, int mode);
    [DllImport("op_x86.dll")]
    private static extern int IUnBindWindow();

    private static extern int IGetClientSize(int hwnd, out object width, out object height);
    public bool BindWindow(IntPtr hwnd, string display, string mouse, string keypad, int mode)
    {
        _handle = hwnd;
        return iBindWindow((int)hwnd, display, mouse, keypad, mode) != 0;
    }

    [DllImport("op_x86.dll")]
    private static extern int IFindPic(int x1, int y1, int x2, int y2, string files, string delta_color, double sim, int dir, out object x, out object y);
    [DllImport("op_x86.dll")]
    private static extern int IGetCursorPos(out object x, out object y);
    [DllImport("op_x86.dll")]
    private static extern int IMoveTo(int x, int y);
    [DllImport("op_x86.dll")]
    private static extern int ILeftDown();
    [DllImport("op_x86.dll")]
    private static extern int ILeftUp();
 
    private static extern int IMoveR(int x, int y);
    [DllImport("op_x86.dll")]
    private static extern int ILeftClick();
    public bool UnBindWindow()
    {
        _handle = IntPtr.Zero;

        return IUnBindWindow() != 0;
    }
    public (bool, int, int) FindPic(Rectangle rectangle, string files, string delta_color, double sim, int dir)
    {
        object width, height;
        if (rectangle.IsEmpty)
        {
            IGetClientSize((int)_handle, out width, out height);
            rectangle = new Rectangle(0, 0, (int)width, (int)height);
        }
        return (FindPic(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, files, delta_color, sim, dir));
    }


    public (bool, int, int) FindPic(int x1, int y1, int x2, int y2, string files, string delta_color, double sim, int dir)
    {

        object fx, fy;

        return (IFindPic(x1, y1, x2, y2, files, delta_color, sim, dir, out fx, out fy) != 0, (int)fx, (int)fy);
    }

    public bool MoveR(int x, int y) => IMoveR(x, y) != 1;

    public bool MoveTo(int x, int y) => IMoveTo(x, y) != 1;

    //  bool MoveToEx(int x, int y, int w, int h);

    public bool LeftClick()
    {
        object x, y;
        IGetCursorPos(out x, out y);
        IMoveTo((int)x, (int)y);
        ILeftDown();
        IMoveTo((int)x, (int)y);
        ILeftUp();
        return true;
    }


    public bool LeftDown() => ILeftDown() != 1;

    public bool LeftUp() => ILeftUp() != 1;




    void ErrorNoBind()
    {
        MessageBox.Show("沒有綁定", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }


}