using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Security.Permissions;
using System.Threading.Tasks;
using testdm;

namespace LodAutoBot
{
    public class DmControll
    {
        private WindowInfo CurWindow;
        private readonly CDmSoft dm = new CDmSoft();
        private Process process;
        private IntPtr hwnd;
        public const double sim = 0.7;

        public void SetCurrentWindow(WindowInfo windowinfo)
        {
            CurWindow = windowinfo;
        }

        public int Capture()
        {
            return dm.Capture(0, 0, CurWindow.Rectangle.Size.Width, CurWindow.Rectangle.Size.Height + 30, "test.bmp");
        }

        public void BindWindow(Action<(bool success, string processTitie)> onBinded)
        {
            if (CurWindow == null)
            {
                onBinded?.Invoke((false, "Error not setWindows"));
                return;
            }

            process = Process.GetProcessById(CurWindow.ProcessId);
            hwnd = process.MainWindowHandle;

            int result = dm.BindWindow((int)hwnd, "dx2", "windows3", "windows", 1);
            onBinded?.Invoke((result == 1, process.MainWindowTitle));
        }

        public void UnBindWindow(Action<bool> onUnBinded)
        {
            int result = dm.UnBindWindow();
            CurWindow = null;
            process = null;
            onUnBinded?.Invoke(result == 1);
        }


        public (bool b, ImageData image) FindImageData(Dictionary<string, ImageData> datas, Enum type)
        {
            if (datas.ContainsKey(type.ToString()))
            {
                return (true, datas[type.ToString()]);
            }

            return (false, null);
        }

        public bool FindAllPicture(Dictionary<string, ImageData> imageDatas, Enum data, double sim = sim)
        {
            bool isFindImage = false;
            if (imageDatas.ContainsKey(data.ToString()))
            {
                ImageData imageData = imageDatas[data.ToString()];
                for (int j = 0; j < imageData.Paths.Length; j++)
                {
                    if (FindPicture(imageData.Paths[j], sim, imageData.Rectangle[j]).isFind)
                    {
                        isFindImage = true;
                    }
                }
            }

            return isFindImage;
        }

        public bool FindAllPictureAndClick(Dictionary<string, ImageData> imageDatas, Enum data, float sim = 0.7f)
        {
            bool isFindImage = false;

            if (imageDatas.ContainsKey(data.ToString()))
            {
                ImageData imageData = imageDatas[data.ToString()];

                for (int j = 0; j < imageDatas[data.ToString()].Paths.Length; j++)
                {
                    if (FindPictureAndClick(imageData.Paths[j], sim, imageData.Rectangle[j]))
                    {
                        isFindImage = true;
                        break;
                    }
                }
            }

            return isFindImage;
        }

        public bool FindAllPictureAndClick(Dictionary<string, ImageData> imageDatas, Enum data, Rectangle rectangle, float sim = 0.7f)
        {
            bool isFindImage = false;

            if (imageDatas.ContainsKey(data.ToString()))
            {
                ImageData imageData = imageDatas[data.ToString()];

                for (int j = 0; j < imageDatas[data.ToString()].Paths.Length; j++)
                {
                    if (FindPictureAndClick(imageData.Paths[j], sim, rectangle))
                    {
                        isFindImage = true;
                        break;
                    }
                }
            }

            return isFindImage;
        }

        public (bool isFind, Point point) FindPicture(string picturePath, double sim, Rectangle rectangle = default)
        {
            if (rectangle == default)
            {
                dm.GetClientSize((int)hwnd, out Size size);
                rectangle = new Rectangle(new Point(0, 0), size);
            }

            rectangle.X -= 30;
            rectangle.Y -= 30;
            rectangle.Size += new Size(60, 60);
            bool result = dm.FindPic(rectangle, picturePath, "000000", sim, 0, out Point tempPoint);

            return (result, tempPoint);
        }

        public bool FindPictureAndClick(string picturePath, double sim, Rectangle rectangle = default)
        {
            (bool isFind, Point point) = FindPicture(picturePath, sim, rectangle);

            if (isFind)
            {
                dm.MoveTo(point);
                dm.LeftClick();
            }

            return isFind;
        }

        public void LeftDown()
        {
            dm.LeftDown();
        }

        public void MoveR(int v1, int v2)
        {
            dm.MoveR(v1, v2);
        }

        public void LeftUp()
        {
            dm.LeftUp();
        }

        public void MoveTo(int x, int y)
        {
            dm.MoveTo(x, y);
        }

        public void LeftClick()
        {
            dm.LeftClick();
        }
    }
}