using LodAutoBot;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

public partial class Form1 : Form
{



    public Form1()
    {
        InitializeComponent();
        Start();
        timer1.Interval = 300000;
        TimerSet(null, null);
        UnderGroundSet(null, null);
        checkBox_Event.CheckedChanged += new EventHandler(TimerSet);
        trackBar_地下城.ValueChanged += UnderGroundSet;
    }

    private int UnderGroundState;

    private void UnderGroundSet(object sender, EventArgs e)
    {
        string[] text = new string[] { "封鎖", "協商", "佔領" };
        label_地下城.Text = text[trackBar_地下城.Value];
        UnderGroundState = trackBar_地下城.Value;
    }

    private void TimerSet(object sender, EventArgs e)
    {
        timer1.Enabled = checkBox_Event.Checked;
        isOpenEventPage = checkBox_Event.Checked;
    }

    private readonly DmControll dm = new DmControll();
    private Thread mainThread;
    private static readonly string ImagePath = Path.Combine(Application.StartupPath, "Image");
    private static readonly string clickImagePath = Path.Combine(ImagePath, "Click");
    private static readonly string windowsStateImagePath = Path.Combine(ImagePath, "Windows");
    private static readonly string monsterImagePath = Path.Combine(ImagePath, "Monster");
    private static readonly string underGroundImagePath = Path.Combine(ImagePath, "UnderGround");
    private static readonly string intimacyImagePath = Path.Combine(ImagePath, "Intimacy");
    private Dictionary<string, ImageData> windowsData = new Dictionary<string, ImageData>();
    private Dictionary<string, ImageData> clickData = new Dictionary<string, ImageData>();
    private Dictionary<string, ImageData> monsterData = new Dictionary<string, ImageData>();
    private Dictionary<string, ImageData> underGroundData = new Dictionary<string, ImageData>();
    private Dictionary<string, ImageData> intimacyData = new Dictionary<string, ImageData>();
    private Dictionary<Level, CheckBoxControll> expeditionUnderGround = new Dictionary<Level, CheckBoxControll>();
    private Dictionary<Level, CheckBoxControll> catchMonsters = new Dictionary<Level, CheckBoxControll>();
    private delegate void UpdateState(string text);

    private static int intimacyInt;
    private bool isOpenEventPage;

    public Dictionary<Level, CheckBoxControll> CreatCheckBoxControll(GroupBox groupBox)
    {
        Dictionary<Level, CheckBoxControll> checkBoxControll = new Dictionary<Level, CheckBoxControll>();

        (Level level, Color color)[] data = new (Level level, Color color)[]
        {
           (Level.普通,Tools.HexToColor("#7c7c7c")),
           (Level.高級,Tools.HexToColor("#8cc067")),
           (Level.稀有,Tools.HexToColor("#4274c6")),
           (Level.古代,Tools.HexToColor("#9463c7")),
           (Level.傳說,Tools.HexToColor("#d8733c")),
           (Level.不滅,Tools.HexToColor("#ba3230")),
           (Level.神話,Tools.HexToColor("#ffe71b")),
           (Level.幻想,Tools.HexToColor("#ff00dd"))
        };
        int height = 0;
        int offsetY = 2;
        foreach ((Level level, Color color) item in data)
        {
            CheckBoxControll temp = new CheckBoxControll(item);
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
        expeditionUnderGround = CreatCheckBoxControll(groupBox_UnderGround);
        catchMonsters = CreatCheckBoxControll(groupBox_Monster);

        trackBar_Intimacy.ValueChanged += new System.EventHandler(SetIntimacy);
        SetButton(false, false);
    }

    private void SetIntimacy(object sender, EventArgs e)
    {
        intimacyInt = trackBar_Intimacy.Value;
        label_Intimacy.Text = ((Intimacy)trackBar_Intimacy.Value).ToString();
    }



    public State state;

    private void SetButton(bool isBinding, bool isRun)
    {
        rebindButton.Enabled = isBinding;
        TitleNameBox.Enabled = false;
        buttonStartFind.Enabled = isBinding && !isRun;
        buttonStopFind.Enabled = isBinding && isRun;
        pictureBox1.Enabled = !isBinding;
    }




    public enum State { 未知, 地圖, 地區, 探索完畢, 發現怪物, 選擇同伴, 戰鬥中, 結算介面, 捕捉怪物機會, 捕捉到怪物, 製造所, 發現地下城, 桌面, 封鎖地下城, 地下城封鎖完畢, 找地下城, 別人領地, 地下城目錄, 地下城發現獎勵, 收益報告, EventPage, 通知, 訪客 };

    public enum ClickImage { 地圖, 探索完畢_白, 探索完畢_藍, 再一次, 問候, 嘗試捕捉, 戰鬥開始, Skip, 結算介面, 關閉, 確認, 立即封鎖, 強制封鎖, 協商封鎖, 探索報告_小, 遠征, 開始遠征, 探索其他領地, 地下城資訊, 前往地下城, 地下城封鎖完畢, 村收, TimeEvent }

    public enum Level { 普通, 高級, 稀有, 古代, 傳說, 不滅, 神話, 幻想 }
    public enum UnderGroundLevel { 普通, 高級, 稀有, 古代, 傳說20000, 傳說30000, 傳說40000, 不滅, 神話 }
    public enum Intimacy { 非常警戒, 警戒, 熟悉, 親近, 信賴 }

    private void ChangeState(State mstate)
    {
        state = mstate;
        WriteState(mstate.ToString());
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

    private (bool isOK, ImageData data) LoadImageData(string path, string childPath)
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
        bool isOk;
        ImageData image;

        for (int i = 0; i < Enum.GetNames(childPath).Length; i++)
        {

            (isOk, image) = LoadImageData(path, Enum.GetName(childPath, i));

            if (isOk)
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

    private void MouseDrop(int x, int y, int count)
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

    private void MainThread()
    {
        bool isFind = false;
        bool expeditionNotFind = false;
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
                    if (!dm.FindAllPicture(windowsData, State.地圖))
                    {
                        ChangeState(State.未知);
                        break;
                    }
                    Thread.Sleep(500);
                    if (isOpenEventPage)
                    {
                        isOpenEventPage = false;
                        dm.MoveTo(36, 382);
                        dm.LeftClick();
                    }
                    Thread.Sleep(500);
                    if (dm.FindAllPictureAndClick(clickData, ClickImage.村收))
                    {
                        break;
                    }
                    else
                    {
                        Thread.Sleep(500);
                        if (!dm.FindAllPicture(clickData, ClickImage.探索報告_小) && 遠征.Checked)
                        {
                            dm.FindAllPictureAndClick(clickData, ClickImage.遠征);
                        }

                        else if (dm.FindAllPicture(clickData, ClickImage.探索報告_小))
                        {

                            dm.FindAllPictureAndClick(clickData, ClickImage.地圖);

                        }
                    }

                    break;
                case State.地區:

                    //拖動

                    Thread.Sleep(100);
                    if (!dm.FindAllPicture(windowsData, State.地區))
                    {
                        ChangeState(State.未知);
                        break;
                    }
                    Thread.Sleep(100);
                    dm.FindAllPictureAndClick(clickData, ClickImage.探索完畢_藍);
                    Thread.Sleep(100);

                    if (!dm.FindAllPictureAndClick(clickData, ClickImage.探索完畢_白) && !dm.FindAllPicture(clickData, ClickImage.探索完畢_藍))
                    {
                        int[] x = new int[] { 10, -1, 0, 1, 0 };
                        int[] y = new int[] { 10, 0, -1, -0, 1 };
                        int[] count = new int[] { 1, 4, 2, 4, 2 };
                        for (int i = 0; i < x.Length; i++)
                        {
                            if (!isFind)
                            {
                                DoMouseDropLoop(x[i], y[i], count[i]);
                            }
                            else
                            {
                                isFind = dm.FindAllPictureAndClick(clickData, ClickImage.探索完畢_藍);
                            }
                            Thread.Sleep(100);

                        }
                        if (!isFind)
                        {
                            dm.FindAllPictureAndClick(clickData, ClickImage.關閉);
                        }

                    }

                    Thread.Sleep(300);
                    if (!dm.FindAllPicture(windowsData, State.地區))
                    {
                        ChangeState(State.未知);
                        break;
                    }

                    void DoMouseDropLoop(int tx, int ty, int tcount)
                    {
                        for (int i = 0; i < tcount; i++)
                        {

                            if (dm.FindAllPictureAndClick(clickData, ClickImage.探索完畢_白) || !dm.FindAllPicture(windowsData, State.地區))
                            {
                                isFind = true;
                                break;
                            }
                            Thread.Sleep(300);
                            MouseDrop(tx, ty, 30);
                            Thread.Sleep(300);
                        }
                    }

                    break;
                case State.探索完畢:
                    if (!dm.FindAllPicture(windowsData, State.探索完畢))
                    {
                        ChangeState(State.未知);
                        break;
                    }

                    Thread.Sleep(100);
                    dm.FindAllPictureAndClick(clickData, ClickImage.再一次);

                    break;
                case State.發現怪物:
                    if (!dm.FindAllPicture(windowsData, State.發現怪物))
                    {
                        ChangeState(State.未知);
                        break;
                    }

                    Thread.Sleep(100);

                    if (!捕捉.Checked)
                    {
                        dm.FindAllPictureAndClick(clickData, ClickImage.問候);//問候

                        break;
                    }
                    isFind = false;

                    for (int i = 0; i < Enum.GetNames(typeof(Level)).Length; i++)
                    {
                        if (catchMonsters[(Level)i].Checked && dm.FindAllPicture(monsterData, (Level)i))
                        {

                            for (int j = intimacyInt; j < Enum.GetNames(typeof(Intimacy)).Length; j++)
                            {

                                if (dm.FindAllPicture(intimacyData, (Intimacy)j))
                                {
                                    isFind = true;

                                    dm.FindAllPictureAndClick(clickData, ClickImage.嘗試捕捉);//捕捉
                                    break;
                                }
                            }
                        }
                    }

                    if (!isFind)
                    {

                        dm.FindAllPictureAndClick(clickData, ClickImage.問候);//問候

                    }


                    //Thread.Sleep(100);

                    break;
                case State.選擇同伴:
                    if (!dm.FindAllPicture(windowsData, State.選擇同伴))
                    {
                        ChangeState(State.未知);
                        break;
                    }

                    Thread.Sleep(100);
                    dm.FindAllPictureAndClick(clickData, ClickImage.戰鬥開始);
                    break;
                case State.戰鬥中:
                    if (!dm.FindAllPicture(windowsData, State.戰鬥中))
                    {
                        ChangeState(State.未知);
                        break;
                    }

                    Thread.Sleep(100);
                    dm.FindAllPictureAndClick(clickData, ClickImage.Skip);
                    break;
                case State.結算介面:
                    if (!dm.FindAllPicture(windowsData, State.結算介面))
                    {
                        ChangeState(State.未知);
                        break;
                    }
                    Thread.Sleep(100);
                    dm.FindAllPictureAndClick(clickData, ClickImage.結算介面);
                    break;
                case State.捕捉怪物機會:
                    if (!dm.FindAllPicture(windowsData, State.捕捉怪物機會))
                    {
                        ChangeState(State.未知);
                        break;
                    }

                    Thread.Sleep(100);
                    dm.FindAllPictureAndClick(clickData, ClickImage.關閉);
                    break;
                case State.捕捉到怪物:
                    if (!dm.FindAllPicture(windowsData, State.捕捉到怪物))
                    {
                        ChangeState(State.未知);
                        break;
                    }

                    Thread.Sleep(100);
                    dm.FindAllPictureAndClick(clickData, ClickImage.確認);
                    break;
                case State.製造所:
                    if (!dm.FindAllPicture(windowsData, State.製造所))
                    {
                        ChangeState(State.未知);
                        break;
                    }

                    Thread.Sleep(100);
                    dm.FindAllPictureAndClick(clickData, ClickImage.關閉);
                    break;
                case State.發現地下城:
                    if (!dm.FindAllPicture(windowsData, State.發現地下城))
                    {
                        ChangeState(State.未知);
                        break;
                    }

                    Thread.Sleep(100);
                    switch (UnderGroundState)
                    {
                        case 0:// 封鎖
                            if (!dm.FindAllPictureAndClick(clickData, ClickImage.立即封鎖))
                            {
                                dm.FindAllPictureAndClick(clickData, ClickImage.強制封鎖);
                            }
                            break;
                        case 1://協商
                            if (!dm.FindAllPictureAndClick(clickData, ClickImage.立即封鎖))
                            {
                                dm.FindAllPictureAndClick(clickData, ClickImage.關閉);
                            }
                            break;
                        case 2://佔領
                            if (!dm.FindAllPictureAndClick(clickData, ClickImage.戰鬥開始))
                            {
                                dm.FindAllPictureAndClick(clickData, ClickImage.關閉);
                            }
                            break;
                        default:
                            break;
                    }

                    break;
                case State.桌面:
                    if (!dm.FindAllPictureAndClick(windowsData, State.桌面))
                    {
                        ChangeState(State.未知);
                        break;
                    }
                    Thread.Sleep(100);
                    break;
                case State.找地下城:
                    if (!dm.FindAllPicture(windowsData, State.找地下城))
                    {
                        ChangeState(State.未知);
                        break;
                    }

                    expeditionNotFind = false;
                    Thread.Sleep(100);
                    dm.FindAllPictureAndClick(clickData, ClickImage.開始遠征);
                    break;
                case State.別人領地:
                    if (!dm.FindAllPicture(windowsData, State.別人領地))
                    {
                        ChangeState(State.未知);
                        break;
                    }

                    Thread.Sleep(100);
                    if (expeditionNotFind)
                    {
                        dm.FindAllPictureAndClick(clickData, ClickImage.探索其他領地);
                        expeditionNotFind = false;
                    }
                    else
                    {
                        dm.FindAllPictureAndClick(clickData, ClickImage.地下城資訊);
                    }

                    break;
                case State.地下城目錄:

                    if (!dm.FindAllPicture(windowsData, State.地下城目錄))
                    {
                        ChangeState(State.未知);
                        break;
                    }
                    //127,259,195,806  Level

                    //407,256,635,876
                    for (int i = Enum.GetNames(typeof(Level)).Length - 1; i >= 0; i--)
                    {
                        if (expeditionUnderGround[(Level)i].Checked && dm.FindAllPictureAndClick(underGroundData, (Level)i, new Rectangle(127, 259, 635, 876)))
                        {
                            expeditionNotFind = false;
                            Thread.Sleep(100);
                            dm.FindAllPictureAndClick(clickData, ClickImage.前往地下城);
                            break;
                        }
                        else
                        {
                            expeditionNotFind = true;
                        }
                    }
                    if (expeditionNotFind)
                    {
                        dm.FindAllPictureAndClick(clickData, ClickImage.關閉);
                    }

                    break;
                case State.地下城發現獎勵:
                    if (!dm.FindAllPictureAndClick(windowsData, State.地下城發現獎勵))
                    {
                        ChangeState(State.未知);
                        break;
                    }
                    expeditionNotFind = isFind = false;

                    break;
                case State.封鎖地下城:
                    if (!dm.FindAllPicture(windowsData, State.封鎖地下城))
                    {
                        ChangeState(State.未知);
                        break;
                    }
                    Thread.Sleep(100);


                    switch (UnderGroundState)
                    {
                        case 0:// 封鎖
                            dm.FindAllPictureAndClick(clickData, ClickImage.強制封鎖);
                            break;
                        case 1://協商
                            dm.FindAllPictureAndClick(clickData, ClickImage.協商封鎖);
                            break;

                        default:
                            dm.FindAllPictureAndClick(clickData, ClickImage.關閉);
                            break;
                    }
                    break;
                case State.地下城封鎖完畢:
                    if (!dm.FindAllPictureAndClick(windowsData, State.地下城封鎖完畢))
                    {
                        ChangeState(State.未知);
                        break;
                    }
                    Thread.Sleep(100);

                    break;
                case State.收益報告:
                    if (!dm.FindAllPicture(windowsData, State.收益報告))
                    {
                        ChangeState(State.未知);
                        break;
                    }
                    Thread.Sleep(100);
                    dm.FindAllPictureAndClick(clickData, ClickImage.確認);

                    break;
                case State.EventPage:
                    if (!dm.FindAllPicture(windowsData, State.EventPage))
                    {
                        ChangeState(State.未知);
                        break;
                    }
                    Thread.Sleep(100);
                    dm.FindAllPictureAndClick(clickData, ClickImage.TimeEvent);
                    Thread.Sleep(1000);
                    dm.FindAllPictureAndClick(clickData, ClickImage.確認);
                    Thread.Sleep(5000);
                    dm.FindAllPictureAndClick(clickData, ClickImage.關閉);
                    break;
                case State.通知:

                    if (!dm.FindAllPicture(windowsData, State.通知))
                    {
                        ChangeState(State.未知);
                        break;
                    }
                    Thread.Sleep(100);
                    dm.FindAllPictureAndClick(clickData, ClickImage.確認);
                    break;
                case State.訪客:

                    if (!dm.FindAllPicture(windowsData, State.訪客))
                    {
                        ChangeState(State.未知);
                        break;
                    }
                    Thread.Sleep(100);
                    dm.FindAllPictureAndClick(clickData, ClickImage.關閉);
                    break;
                default:
                    break;
            }
        }
    }

    private void StartFind()
    {
        mainThread = new Thread(MainThread);

        if (mainThread.IsAlive)
        {
            return;

        }
        mainThread.Start();
        SetButton(true, IsRunning());
    }

    private void StopFind()
    {
        if (mainThread == null)
        {
            return;
        }
        if (!mainThread.IsAlive)
        {
            return;

        }

        mainThread.Abort();
        SetButton(true, IsRunning());
    }

    private void RestWindows(Dictionary<string, ImageData> datas)
    {

        for (int i = 1; i < datas.Count; i++)
        {
            for (int j = 0; j < datas.ElementAt(i).Value.Paths.Length; j++)
            {
                if (dm.FindPicture(datas.ElementAt(i).Value.Paths[j], 0.7, datas.ElementAt(i).Value.Rectangle[j]).isFind)
                {
                    ChangeState((State)i);
                }
            }

        }

    }
    private bool IsRunning()
    {
        return mainThread != null && mainThread.IsAlive;
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

        void OnBindEnd((bool isOk, string processTitie) value)
        {
            SetButton(value.isOk, IsRunning());
            label1.Text = $"綁定:{value.isOk} Windows:{value.processTitie}";
        }
    }
    private void rebindButton_Click(object sender, EventArgs e)
    {
        StopFind();
        dm.UnBindWindow(onUnBind);

        void onUnBind(bool value)
        {
            SetButton(!value, IsRunning());
            label1.Text = $"解除綁定:{value}";
        }
    }


    private void Timer1_Tick(object sender, EventArgs e)
    {
        isOpenEventPage = true;
    }

    private void TrackBar_地下城_Scroll(object sender, EventArgs e)
    {

    }

    private void button1_Click(object sender, EventArgs e)
    {
        expeditionUnderGround[Level.普通].CheckedChanged += AAA;

    }

    private void AAA(object sender, EventArgs e)
    {

    }
    #endregion
}



