using System.Drawing;
using System.Windows.Forms;

public partial class Form1
{
    public class CheckBoxControll : CheckBox
    {
        (Level level, Color color) data;


        public CheckBoxControll((Level level, Color color) data)
        {
            this.data = data;
            int left = 30, top = 20;
            Text = data.level.ToString();
            Top = top;
            Left = left;
            ForeColor = data.color;
            
        }

        public void AddTo(GroupBox groupBox, int offsetTop = 0)
        {
            Top += offsetTop;
            groupBox.Controls.Add(this);
        }
    }
}


