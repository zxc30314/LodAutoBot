using System;
using System.Drawing;
using System.Windows.Forms;
using static LodAutoBot.Form1;

namespace LodAutoBot
{
    class Tools
    {
        public static void AddCheckBoxes(GroupBox groupBox, CheckBox[] checkBoxes, Color[] color_TextLevels)
        {
            int left = 30, top = 20;

            for (int i = 0; i < checkBoxes.Length; i++)
            {
                checkBoxes[i] = new CheckBox();
                checkBoxes[i].Top = top;
                checkBoxes[i].Left = left;
                checkBoxes[i].Text = ((Level)i).ToString();
                checkBoxes[i].ForeColor = color_TextLevels[i];
                groupBox.Controls.Add(checkBoxes[i]);

                top += checkBoxes[i].Height + 2;
            }
        }

        public static Color HexToColor(string hex)
        {
            string color = hex;
            if (color.StartsWith("#"))
                color = color.Remove(0, 1);
            byte r, g, b;
            if (color.Length == 3)
            {
                r = Convert.ToByte(color[0] + "" + color[0], 16);
                g = Convert.ToByte(color[1] + "" + color[1], 16);
                b = Convert.ToByte(color[2] + "" + color[2], 16);
            }
            else if (color.Length == 6)
            {
                r = Convert.ToByte(color[0] + "" + color[1], 16);
                g = Convert.ToByte(color[2] + "" + color[3], 16);
                b = Convert.ToByte(color[4] + "" + color[5], 16);
            }
            else
            {
                throw new ArgumentException("Hex color " + color + " is invalid.");
            }
            return Color.FromArgb(255, r, g, b);
        }

    }
}
