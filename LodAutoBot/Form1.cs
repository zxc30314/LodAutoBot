using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using testdm;

public partial class Form1 : Form
{




    public Form1()
    {
        InitializeComponent();
        Start();

    }


    class ImageData
    {

        public string[] Paths;
        public Range[] range;
        public Color TextColor;
        public ImageData(string[] s, Range[] r)
        {

            Paths = s;
            range = r;

        }

        public ImageData()
        {

            Paths = new string[0];
            range = new Range[0];
            TextColor = Color.Black;
        }
    }


    Process process;
    string titleName;

    Thread mainThread;
    bool isRun;
    static string ImagePath = Path.Combine(Application.StartupPath, "Image");

    static string clickImagePath = Path.Combine(ImagePath, "Click");
    static string windowsStateImagePath = Path.Combine(ImagePath, "Windows");
    static string monsterImagePath = Path.Combine(ImagePath, "Monster");
    static string underGroundImagePath = Path.Combine(ImagePath, "UnderGround");
    static string intimacyImagePath = Path.Combine(ImagePath, "Intimacy");

    Dictionary<string, ImageData> windowsData = new Dictionary<string, ImageData>();
    Dictionary<string, ImageData> clickData = new Dictionary<string, ImageData>();
    Dictionary<string, ImageData> monsterData = new Dictionary<string, ImageData>();
    Dictionary<string, ImageData> underGroundData = new Dictionary<string, ImageData>();
    Dictionary<string, ImageData> intimacyData = new Dictionary<string, ImageData>();

    CheckBox[] checkBoxes_Monster = new CheckBox[Enum.GetNames(typeof(Level)).Length];
    CheckBox[] checkBoxes_UnderGround = new CheckBox[Enum.GetNames(typeof(Level)).Length];
    Color[] color_TextLevel = new Color[] { HexToColor("#7c7c7c"), HexToColor("#8cc067"), HexToColor("#4274c6"), HexToColor("#9463c7"), HexToColor("#d8733c"), HexToColor("#ba3230"), HexToColor("#ffe71b"), HexToColor("#ff00dd") };
    Color[] color_UnderGroundMonsterLevel = new Color[] { HexToColor("#323232"), HexToColor("#2F5648"), HexToColor("#624730"), HexToColor("#563243"), HexToColor("#34558B"), HexToColor("#202263") };

    private delegate void UpdateState(string text);

    static int intimacyInt;

    private void WriteState(string text)
    {
        if (label1.InvokeRequired)
        {
            var d = new UpdateState(WriteState);
            Invoke(d, new object[] { text });
        }
        else
        {
            label1.Text = text;
            label1.Update();
        }
    }

    static Color HexToColor(string hex)
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
    void Start()
    {

        ButtonLoadSetting_Click(null, null);

        AddCheckBoxes(groupBox_Monster, checkBoxes_Monster);

        AddCheckBoxes(groupBox_UnderGround, checkBoxes_UnderGround);
        trackBar_Intimacy.ValueChanged += new System.EventHandler(this.SetIntimacy);
    }

    void SetIntimacy(object sender, EventArgs e)
    {
        intimacyInt = trackBar_Intimacy.Value;
        label_Intimacy.Text = ((Intimacy)trackBar_Intimacy.Value).ToString();
    }

    void AddCheckBoxes(GroupBox groupBox, CheckBox[] checkBoxes)
    {


        int left = 30, top = 20;

        for (int i = 0; i < checkBoxes.Length; i++)
        {
            checkBoxes[i] = new CheckBox();
            checkBoxes[i].Top = top;
            checkBoxes[i].Left = left;
            checkBoxes[i].Text = ((Level)i).ToString();
            checkBoxes[i].ForeColor = color_TextLevel[i];
            groupBox.Controls.Add(checkBoxes[i]);

            top += checkBoxes[i].Height + 2;
        }


    }

    CDmSoft dm;
    int hwnd;
    public State state;
    public bool isBind;








    [DllImport("user32.dll")]
    static extern int SetWindowText(IntPtr hWnd, string text);

    void bindButton_Click(object sender, EventArgs e)
    {
        dm = new CDmSoft();
        int id = -1;

        Process[] localByName = Process.GetProcessesByName("Nox");


        if (TitleNameBox.Text == "")
        {


            for (int i = 0; i < localByName.Length; i++)
            {
                if (localByName[i].ProcessName.Split('_')[0] != "綁定中")
                {
                    id = i;
                    break;

                }

            }
        }
        else
        {
            for (int i = 0; i < localByName.Length; i++)
            {
                if (localByName[i].ProcessName.Split('_')[0] != "綁定中" && localByName[i].MainWindowTitle == TitleNameBox.Text)
                {
                    id = i;
                    break;
                }

            }


        }

        if (id == -1)
        {
            isBind = false;
            label1.Text = "查找句柄失敗";
            label1.Update();
            return;
        }

        hwnd = (int)localByName[id].MainWindowHandle;
        isBind = hwnd != 0 && id != -1;
        SetButton();
        int setBind;
        setBind = dm.BindWindow(hwnd, "gdi", "windows3", "windows", 1);

        if (setBind == 1)
        {
            process = localByName[id];
            titleName = localByName[id].MainWindowTitle;

            label1.Text = "窗口綁定成功";
            TitleNameBox.Text = titleName;
            label1.Update();

        }





    }
    void SetButton()
    {
        bindButton.Enabled = !isBind;
        rebindButton.Enabled = isBind;
        TitleNameBox.Enabled = !isBind;
        buttonStartFind.Enabled = isBind && !isRun;
        buttonStopFind.Enabled = isBind && isRun;

    }

    private void rebindButton_Click(object sender, EventArgs e)
    {
        if (process != null)
        {
            StopFind();
            SetWindowText(process.MainWindowHandle, titleName);
            isBind = false;
            SetButton();
            dm = null;
            label1.Text = "解除綁定";
        }
    }


    public enum State { 未知, 地圖, 地區, 探索完畢, 發現怪物, 選擇同伴, 戰鬥中, 結算介面, 捕捉怪物機會, 捕捉到怪物, 製造所, 發現地下城, 桌面,封鎖地下城,地下城封鎖完畢, 找地下城, 別人領地, 地下城目錄, 地下城發現獎勵 };

    public enum ClickImage { 地圖, 探索完畢_白, 探索完畢_藍, 再一次, 問候, 嘗試捕捉, 戰鬥開始, Skip, 結算介面, 關閉, 確認,立即封鎖,強制封鎖, 遠征, 開始遠征, 地下城資訊, 前往地下城 ,地下城封鎖完畢}

    public enum Level { 普通, 高級, 稀有, 古代, 傳說, 不滅, 神話, 幻想 }

    public enum Intimacy { 非常警戒, 警戒, 熟悉, 親近, 信賴 }
    void ChangeState(State mstate)
    {
        this.state = mstate;
        WriteState(mstate.ToString());
    }


    class SettingData
    {

        public SettingData()
        {

        }
        public string[] picturePath = new string[11];


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

        SetImageData(windowsStateImagePath, typeof(State), ref windowsData);
        SetImageData(clickImagePath, typeof(ClickImage), ref clickData);
        SetImageData(monsterImagePath, typeof(Level), ref monsterData);
        SetImageData(underGroundImagePath, typeof(Level), ref underGroundData);
        SetImageData(intimacyImagePath, typeof(Intimacy), ref intimacyData);
    }
    void ChichExists(string imagePath, Type typef)
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

    (bool, ImageData) ReImageData(string path, string childPath)
    {



        DirectoryInfo di = new DirectoryInfo(Path.Combine(path, childPath));
        bool haveFile = false;
        try
        {
            haveFile = Directory.GetFiles(di.FullName, "*.Bmp").Length != 0;

        }
        catch (Exception)
        {


        }
        if (haveFile)
        {

            FileInfo[] file;
            file = di.GetFiles("*.Bmp");
            var tempImagePath = file.Select((x) => x.FullName);

            var tempRange = file.Select((x) => new Range(x.Name.Split('.')[0].Split(',').Select((s, i) => int.TryParse(s, out i) ? i : -1).ToArray()));
            Range[] iii = new Range[1];
            return (true, new ImageData(tempImagePath.ToArray(), tempRange.ToArray()));

        }


        return (false, null);



    }

    void SetImageData(string path, Type childPath, ref Dictionary<string, ImageData> dic)
    {
        dic.Clear();
        bool b;
        ImageData image;

        for (int i = 0; i < Enum.GetNames(childPath).Length; i++)
        {

            (b, image) = ReImageData(path, Enum.GetName(childPath, i));

            if (b)
            {
                dic.Add(Enum.GetName(childPath, i), image);
            }
            else
            {
                dic.Add(Enum.GetName(childPath, i), new ImageData());
            }

        }

    }

    private void Form1_Load(object sender, EventArgs e)
    {

    }


    protected override void OnClosed(EventArgs e)
    {
        if (process != null)
        {
            SetWindowText(process.MainWindowHandle, titleName);

        }
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

    void MouseDrop(int x, int y, int count)
    {
        dm.MoveTo(800, 450);
        dm.LeftDown();
        for (int i = 0; i < count; i++)
        {
            dm.MoveR(i * x, i * y);
            Thread.Sleep(20);
        }
        dm.LeftUp();






    }
    void MainThread()
    {

        ChangeState(State.未知);
        while (true)
        {

            Thread.Sleep(500);




            switch (state)
            {
                case State.未知:


                    RestWindows(windowsData);
                    Thread.Sleep(100);

                    break;
                case State.地圖:
                    if (!FindAllPicture(windowsData, State.地圖))
                    {
                        ChangeState(State.未知);
                        break;
                    }
                    Thread.Sleep(100);
                    if (遠征.Checked)
                    {
                        FindAllPictureAndClick(clickData, ClickImage.遠征);
                    }

                    else
                    {

                        FindAllPictureAndClick(clickData, ClickImage.地圖);

                    }




                    break;
                case State.地區:




                    //拖動

                    while (true)
                    {
                        Thread.Sleep(1000);
                        if (!FindAllPicture(windowsData, State.地區))
                        {
                            ChangeState(State.未知);
                            break;
                        }

                        if (FindAllPictureAndClick(clickData, ClickImage.探索完畢_白))
                        {
                            Thread.Sleep(1000);
                            if (FindAllPictureAndClick(clickData, ClickImage.探索完畢_藍))
                            {
                                break;
                            }
                        }
                        else
                        {
                            int[] x = new int[] { 5, -1, 0, 1, 0 };
                            int[] y = new int[] { 5, 0, -1, 0, 1 };

                            for (int i = 0; i < 5; i++)
                            {
                                if (!FindAllPictureAndClick(clickData, ClickImage.探索完畢_白))
                                {

                                    MouseDrop(x[i], y[i], 50);
                                    Thread.Sleep(1000);

                                }
                                else
                                {

                                    break;
                                }
                            }



                        }
                    }



                    break;
                case State.探索完畢:
                    if (!FindAllPicture(windowsData, State.探索完畢))
                    {
                        ChangeState(State.未知);
                        break;
                    }

                    Thread.Sleep(100);
                    FindAllPictureAndClick(clickData, ClickImage.再一次);

                    break;
                case State.發現怪物:
                    if (!FindAllPicture(windowsData, State.發現怪物))
                    {
                        ChangeState(State.未知);
                        break;
                    }

                    Thread.Sleep(100);

                    if (!捕捉.Checked)
                    {
                        FindAllPictureAndClick(clickData, ClickImage.問候);//問候

                        break;
                    }
                    bool isFind = false;
                    for (int i = 0; i < Enum.GetNames(typeof(Level)).Length; i++)
                    {
                        if (checkBoxes_Monster[i].Checked && FindAllPicture(monsterData, (Level)i))
                        {

                            for (int j = intimacyInt; j < Enum.GetNames(typeof(Intimacy)).Length; j++)
                            {

                                if (FindAllPicture(intimacyData, (Intimacy)j))
                                {
                                    isFind = true;

                                    FindAllPictureAndClick(clickData, ClickImage.嘗試捕捉);//捕捉
                                    break;
                                }
                            }


                        }

                    }

                    if (!isFind)
                    {

                        FindAllPictureAndClick(clickData, ClickImage.問候);//問候

                    }


                    //Thread.Sleep(100);

                    break;
                case State.選擇同伴:
                    if (!FindAllPicture(windowsData, State.選擇同伴))
                    {
                        ChangeState(State.未知);
                        break;
                    }

                    Thread.Sleep(100);
                    FindAllPictureAndClick(clickData, ClickImage.戰鬥開始);
                    break;
                case State.戰鬥中:
                    if (!FindAllPicture(windowsData, State.戰鬥中))
                    {
                        ChangeState(State.未知);
                        break;
                    }

                    Thread.Sleep(100);
                    FindAllPictureAndClick(clickData, ClickImage.Skip);
                    break;
                case State.結算介面:
                    if (!FindAllPicture(windowsData, State.結算介面))
                    {
                        ChangeState(State.未知);
                        break;
                    }
                    Thread.Sleep(100);
                    FindAllPictureAndClick(clickData, ClickImage.結算介面);
                    break;
                case State.捕捉怪物機會:
                    if (!FindAllPicture(windowsData, State.捕捉怪物機會))
                    {
                        ChangeState(State.未知);
                        break;
                    }

                    Thread.Sleep(100);
                    FindAllPictureAndClick(clickData, ClickImage.關閉);
                    break;
                case State.捕捉到怪物:
                    if (!FindAllPicture(windowsData, State.捕捉到怪物))
                    {
                        ChangeState(State.未知);
                        break;
                    }

                    Thread.Sleep(100);
                    FindAllPictureAndClick(clickData, ClickImage.確認);
                    break;
                case State.製造所:
                    if (!FindAllPicture(windowsData, State.製造所))
                    {
                        ChangeState(State.未知);
                        break;
                    }

                    Thread.Sleep(100);
                    FindAllPictureAndClick(clickData, ClickImage.關閉);
                    break;
                case State.發現地下城:
                    if (!FindAllPicture(windowsData, State.發現地下城))
                    {
                        ChangeState(State.未知);
                        break;
                    }

                    Thread.Sleep(100);
                    FindAllPictureAndClick(clickData, ClickImage.立即封鎖);
                    break;
                case State.桌面:
                    if (!FindAllPictureAndClick(windowsData, State.桌面))
                    {
                        ChangeState(State.未知);
                        break;
                    }
                    Thread.Sleep(100);
                    break;
                case State.找地下城:
                    if (!FindAllPicture(windowsData, State.找地下城))
                    {
                        ChangeState(State.未知);
                        break;
                    }
                    Thread.Sleep(100);
                    FindAllPictureAndClick(clickData, ClickImage.開始遠征);
                    break;
                case State.別人領地:
                    if (!FindAllPicture(windowsData, State.別人領地))
                    {
                        ChangeState(State.未知);
                        break;
                    }

                    Thread.Sleep(100);
                    FindAllPictureAndClick(clickData, ClickImage.地下城資訊);
                    break;
                case State.地下城目錄:
                    if (!FindAllPicture(windowsData, State.地下城目錄))
                    {
                        ChangeState(State.未知);
                        break;
                    }


                    break;
                case State.地下城發現獎勵:
                    if (!FindAllPicture(clickData, State.地下城發現獎勵))
                    {
                        ChangeState(State.未知);
                        break;
                    }
                    break;
                case State.封鎖地下城:
                    if (!FindAllPicture(windowsData, State.封鎖地下城))
                    {
                        ChangeState(State.未知);
                        break;
                    }
                    Thread.Sleep(100);
                    FindAllPictureAndClick(clickData, ClickImage.強制封鎖);


                    break;
                case State.地下城封鎖完畢:
                    if (!FindAllPictureAndClick(windowsData, State.地下城封鎖完畢))
                    {
                        ChangeState(State.未知);
                        break;
                    }
                    Thread.Sleep(100);
     
                    break;
                default:
                    break;
            }



        }


    }
    (bool b, ImageData image) FindImageData(Dictionary<string, ImageData> datas, Enum type)
    {

        if (datas.ContainsKey(type.ToString()))
        {

            return (true, datas[type.ToString()]);
        }


        return (false, null);

    }

    void RestWindows(Dictionary<string, ImageData> datas)
    {

        for (int i = 1; i < datas.Count; i++)
        {
            for (int j = 0; j < datas.ElementAt(i).Value.Paths.Length; j++)
            {
                if (FindPicture(datas.ElementAt(i).Value.Paths[j], 0.7, datas.ElementAt(i).Value.range[j]).Item1 != -1)
                {
                    ChangeState((State)i);
                }
            }

        }

    }


    bool FindAllPicture(Dictionary<string, ImageData> imageDatas, Enum data)
    {
        bool isFindImage = false;


        // (isFindData, imageData) = FindImageData(imageDatas, data);
        if (imageDatas.ContainsKey(data.ToString()))
        {
            ImageData imageData = imageDatas[data.ToString()];
            for (int j = 0; j < imageData.Paths.Length; j++)
            {

                if (FindPicture(imageData.Paths[j], 0.7, imageData.range[j]).Item1 != -1)
                {
                    isFindImage = true;

                }
            }
        }
        return isFindImage;
    }
    bool FindAllPictureAndClick(Dictionary<string, ImageData> imageDatas, Enum data)
    {
        bool isFindImage = false;

        if (imageDatas.ContainsKey(data.ToString()))
        {
            ImageData imageData = imageDatas[data.ToString()];

            for (int j = 0; j < imageDatas[data.ToString()].Paths.Length; j++)
            {

                if (FindPictureAndClick(imageData.Paths[j], 0.7, imageData.range[j]) != -1)
                {
                    isFindImage = true;
                    break;

                }
            }
        }
        return isFindImage;
    }


    //開始找探險完畢
    void StartFind()
    {
        mainThread = new Thread(MainThread);

        if (mainThread.IsAlive)
        {
            return;

        }
        isRun = true;
        SetButton();
        mainThread.Start();
    }
    void StopFind()
    {
        if (mainThread == null)
        {
            return;
        }
        if (!mainThread.IsAlive)
        {
            return;

        }
        isRun = false;
        dm.UnBindWindow();
        SetButton();
        mainThread.Abort();
    }

    class Range
    {
        public int x1, y1, x2, y2;
        public Range(int x1, int y1, int x2, int y2)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
        }
        public Range(int[] i)
        {
            if (i.Length == 4)
            {
                this.x1 = i[0];
                this.y1 = i[1];
                this.x2 = i[2];
                this.y2 = i[3];
            }
            else
            {
                x1 = 0;
                y1 = 0;
                x2 = 2000;
                y2 = 2000;
            }

        }

    }



    (int, int, int) FindPicture(string picturePath, double sim, Range rectangle = null)
    {

        object x1 = 0;
        object y1 = 0;
        object x2 = 0;
        object y2 = 0;
        object fx, fy;
        if (rectangle == null)
        {
            x1 = 0;
            y1 = 0;
            dm.GetClientSize(hwnd, out x2, out y2);
        }
        else
        {
            x1 = rectangle.x1;
            y1 = rectangle.y1;
            x2 = rectangle.x2;
            y2 = rectangle.y2;
        }

        int res = dm.FindPic((int)x1 - 5, (int)y1 - 5, (int)x2 + 5, (int)y2 + 5, picturePath, "000000", sim, 0, out fx, out fy);

        return (res, (int)fx, (int)fy);
    }

    int FindPictureAndClick(string picturePath, double sim, Range rectangle = null)
    {
        (int res, int intX, int intY) = FindPicture(picturePath, sim, rectangle);

        if (res >= 0)
        {
            dm.MoveTo(intX, intY);
            dm.LeftClick();
        }
        return res;
    }

    void StartBattle()
    {

    }



    void BattleSkip()
    {

    }

  

    private void Form1_Load_1(object sender, EventArgs e)
    {

    }
}


