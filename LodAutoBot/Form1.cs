using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LodAutoBot;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        Start();
        timer1.Interval = 300000;
        UnderGroundSet(null, null);
        trackBar_地下城.ValueChanged += UnderGroundSet;
        遠征.CheckedChanged += (x, e) => Data.hasJourney = 遠征.Checked;
        探索報告.CheckedChanged += (x, e) => Data.useReport = 探索報告.Checked;
    }

    private int UnderGroundState;

    private void UnderGroundSet(object sender, EventArgs e)
    {
        string[] text = new string[] { "封鎖", "協商", "佔領" };
        label_地下城.Text = text[trackBar_地下城.Value];
        UnderGroundState = trackBar_地下城.Value;
    }


    private readonly DmControll dm = Data.dm;
    private static readonly string ImagePath = Path.Combine(Application.StartupPath, "Image");
    private static readonly string clickImagePath = Path.Combine(ImagePath, "Click");
    private static readonly string windowsStateImagePath = Path.Combine(ImagePath, "Windows");
    private static readonly string monsterImagePath = Path.Combine(ImagePath, "Monster");
    private static readonly string underGroundImagePath = Path.Combine(ImagePath, "UnderGround");
    private static readonly string intimacyImagePath = Path.Combine(ImagePath, "Intimacy");
    private static readonly string otherImagePath = Path.Combine(ImagePath, "Other");

    private delegate void UpdateState(string text);

    private static int intimacyInt;
    private bool isOpenEventPage;

    private CancellationTokenSource cancellationTokenSource = new();

    private Dictionary<Level, global::Form1.CheckBoxControll> CreatCheckBoxControl(GroupBox groupBox)
    {
        Dictionary<Level, global::Form1.CheckBoxControll> checkBoxControll = new Dictionary<Level, global::Form1.CheckBoxControll>();

        (Level level, Color color)[] data = new (Level level, Color color)[]
        {
            (Level.普通, Tools.HexToColor("#7c7c7c")),
            (Level.高級, Tools.HexToColor("#8cc067")),
            (Level.稀有, Tools.HexToColor("#4274c6")),
            (Level.古代, Tools.HexToColor("#9463c7")),
            (Level.傳說, Tools.HexToColor("#d8733c")),
            (Level.不滅, Tools.HexToColor("#ba3230")),
            (Level.神話, Tools.HexToColor("#ffe71b")),
            (Level.幻想, Tools.HexToColor("#ff00dd"))
        };
        int height = 0;
        int offsetY = 2;
        foreach ((Level level, Color color) item in data)
        {
            global::Form1.CheckBoxControll temp = new global::Form1.CheckBoxControll(item);
            temp.AddTo(groupBox, height);
            checkBoxControll.Add(item.level, temp);
            height += temp.Size.Height + offsetY;
        }

        return checkBoxControll;
    }

    private void WriteState(string text)
    {
        if (label1.InvokeRequired)
        {
            UpdateState d = new UpdateState(WriteState);
            Invoke(d, new object[] { text });
        }
        else
        {
            label1.Text = text;
            label1.Update();
        }
    }

    private void Start()
    {
        ButtonLoadSetting_Click(null, null);
        Data.expeditionUnderGround = CreatCheckBoxControl(groupBox_UnderGround);
        Data.catchMonsters = CreatCheckBoxControl(groupBox_Monster);

        trackBar_Intimacy.ValueChanged += new EventHandler(SetIntimacy);
        SetButton(false, false);
    }

    private void SetIntimacy(object sender, EventArgs e)
    {
        intimacyInt = trackBar_Intimacy.Value;
        label_Intimacy.Text = ((Intimacy)trackBar_Intimacy.Value).ToString();
    }

    private void SetButton(bool isBinding, bool isRun)
    {
        rebindButton.Enabled = isBinding;
        TitleNameBox.Enabled = false;
        buttonStartFind.Enabled = isBinding && !isRun;
        buttonStopFind.Enabled = isBinding && isRun;
        pictureBox1.Enabled = !isBinding;
    }


    public enum Intimacy
    {
        非常警戒,
        警戒,
        熟悉,
        親近,
        信賴
    }

    public   enum Other
    {
        發現的怪物0
    }

    private void ButtonSaveSetting_Click(object sender, EventArgs e)
    {
    }

    private void ButtonLoadSetting_Click(object sender, EventArgs e)
    {
        ChichExists(windowsStateImagePath, typeof(State));
        ChichExists(clickImagePath, typeof(ClickImage));
        ChichExists(monsterImagePath, typeof(Level));
        ChichExists(underGroundImagePath, typeof(Level));
        ChichExists(intimacyImagePath, typeof(Intimacy));
        ChichExists(otherImagePath, typeof(Other));

        SetImageData(windowsStateImagePath, typeof(State), ref Data.windowsData);
        SetImageData(clickImagePath, typeof(ClickImage), ref Data.clickData);
        SetImageData(monsterImagePath, typeof(Level), ref Data.monsterData);
        SetImageData(underGroundImagePath, typeof(Level), ref Data.underGroundData);
        SetImageData(intimacyImagePath, typeof(Intimacy), ref Data.intimacyData);
        SetImageData(otherImagePath, typeof(Other), ref Data.otherData);
    }

    private void ChichExists(string imagePath, Type typef)
    {
        string tempPath = string.Empty;
        if (!Directory.Exists(imagePath))
        {
            Directory.CreateDirectory(imagePath);
        }

        for (int i = 0; i < Enum.GetNames(typef).Length; i++)
        {
            tempPath = Path.Combine(imagePath, Enum.GetNames(typef)[i]);

            if (!Directory.Exists(tempPath))
            {
                Directory.CreateDirectory(tempPath);
            }
        }
    }

    private (bool success, ImageData data) LoadImageData(string path, string childPath)
    {
        DirectoryInfo directory = new DirectoryInfo(Path.Combine(path, childPath));

        (bool, ImageData) result = (false, null);
        if (directory.Exists)
        {
            bool haveFile = Directory.GetFiles(directory.FullName, "*.Bmp").Length != 0;
            if (haveFile)
            {
                FileInfo[] file;
                file = directory.GetFiles("*.Bmp");
                IEnumerable<string> tempImagePath = file.Select((x) => x.FullName);
                IEnumerable<Rectangle> tempRange = file.Select(ToSelect);
                result = (true, new ImageData(tempImagePath.ToArray(), tempRange.ToArray()));
            }
        }

        Rectangle ToSelect(FileInfo fileName)
        {
            string temp = Path.GetFileNameWithoutExtension(fileName.FullName);
            int[] values = temp.Split(',').Select(TryParseInt32).ToArray();
            Rectangle tempResult = default;
            if (values.Length == 4)
            {
                tempResult = new Rectangle(values[0], values[1], values[2], values[3]);
            }

            return tempResult;
        }

        int TryParseInt32(string text)
        {
            return int.TryParse(text, out int value) ? value : 0;
        }

        return result;
    }

    private void SetImageData(string path, Type childPath, ref Dictionary<string, ImageData> dic)
    {
        dic.Clear();
        bool success;
        ImageData image;

        for (int i = 0; i < Enum.GetNames(childPath).Length; i++)
        {
            (success, image) = LoadImageData(path, Enum.GetName(childPath, i));

            if (success)
            {
                dic.Add(Enum.GetName(childPath, i), image);
            }
            else
            {
                dic.Add(Enum.GetName(childPath, i), new ImageData());
            }
        }
    }


    protected override void OnClosed(EventArgs e)
    {
        timer1.Enabled = false;
        StopFind();
        base.OnClosed(e);
    }

    private void ButtonStartFind_Click(object sender, EventArgs e)
    {
        StartFind();
    }

    private void ButtonStopFind_Click(object sender, EventArgs e)
    {
        StopFind();
    }

    private async Task StartFind()
    {
        cancellationTokenSource = new CancellationTokenSource();
        SetButton(true, true);
        await Task.Run(() => new StateControl().Setup(cancellationTokenSource.Token), cancellationTokenSource.Token);
    }

    private void StopFind()
    {
        cancellationTokenSource.Cancel();
        SetButton(true, false);
    }

    #region View

    private void PictureBox1_Click(object sender, EventArgs e)
    {
    }

    private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            dm.SetCurrentWindow(new WindowInfo(Cursor.Position));
        }
    }

    private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            dm.BindWindow(OnBindEnd);
        }

        void OnBindEnd((bool success, string processTitie) value)
        {
            SetButton(value.success, false);
            label1.Text = $"綁定:{value.success} Windows:{value.processTitie}";
        }
    }

    private void rebindButton_Click(object sender, EventArgs e)
    {
        StopFind();
        dm.UnBindWindow(onUnBind);

        void onUnBind(bool value)
        {
            SetButton(!value, true);
            label1.Text = $"解除綁定:{value}";
        }
    }


    private void Timer1_Tick(object sender, EventArgs e)
    {
        isOpenEventPage = true;
    }

    #endregion
}