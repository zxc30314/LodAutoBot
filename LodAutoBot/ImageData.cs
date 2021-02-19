using System.Drawing;

namespace LodAutoBot
{
    public class ImageData
    {

        public string[] Paths;
        public Rectangle[] Rectangle;
        public Color TextColor;
        public ImageData(string[] s, Rectangle[] r)
        {

            Paths = s;
            Rectangle = r;

        }

        public ImageData()
        {

            Paths = new string[0];
            Rectangle = default;
            TextColor = Color.Black;
        }
    }

}
