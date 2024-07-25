
namespace LodAutoBot;

partial class Form1
{

    // static string[] windowsStateImage = new string[] { "地圖", "地區", "探索完畢","發現怪物", "選擇同伴", "結算介面", "捕捉怪物機會", "製造所" ,"發現地下城"};

    /// <summary>
    /// 必需的设计器变量。
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// 清理所有正在使用的资源。
    /// </summary>
    /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows 窗体设计器生成的代码

    /// <summary>
    /// 设计器支持所需的方法 - 不要
    /// 使用代码编辑器修改此方法的内容。
    /// </summary>
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.buttonLoadSetting = new System.Windows.Forms.Button();
        this.buttonSaveSetting = new System.Windows.Forms.Button();
        this.label1 = new System.Windows.Forms.Label();
        this.label_Intimacy = new System.Windows.Forms.Label();
        this.tabPage2 = new System.Windows.Forms.TabPage();
        this.groupBox_UnderGround = new System.Windows.Forms.GroupBox();
        this.groupBox_Monster = new System.Windows.Forms.GroupBox();
        this.trackBar_Intimacy = new System.Windows.Forms.TrackBar();
        this.tabPage1 = new System.Windows.Forms.TabPage();
        this.checkBox_Event = new System.Windows.Forms.CheckBox();
        this.label_地下城 = new System.Windows.Forms.Label();
        this.trackBar_地下城 = new System.Windows.Forms.TrackBar();
        this.pictureBox1 = new System.Windows.Forms.PictureBox();
        this.遠征 = new System.Windows.Forms.CheckBox();
        this.捕捉 = new System.Windows.Forms.CheckBox();
        this.buttonStopFind = new System.Windows.Forms.Button();
        this.buttonStartFind = new System.Windows.Forms.Button();
        this.rebindButton = new System.Windows.Forms.Button();
        this.TitleNameBox = new System.Windows.Forms.TextBox();
        this.tabControl1 = new System.Windows.Forms.TabControl();
        this.tabPage3 = new System.Windows.Forms.TabPage();
        this.button1 = new System.Windows.Forms.Button();
        this.timer1 = new System.Windows.Forms.Timer(this.components);
        this.探索報告 = new System.Windows.Forms.CheckBox();
        this.tabPage2.SuspendLayout();
        this.groupBox_Monster.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.trackBar_Intimacy)).BeginInit();
        this.tabPage1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.trackBar_地下城)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
        this.tabControl1.SuspendLayout();
        this.tabPage3.SuspendLayout();
        this.SuspendLayout();
        // 
        // buttonLoadSetting
        // 
        this.buttonLoadSetting.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
        this.buttonLoadSetting.Location = new System.Drawing.Point(432, 380);
        this.buttonLoadSetting.Name = "buttonLoadSetting";
        this.buttonLoadSetting.Size = new System.Drawing.Size(75, 23);
        this.buttonLoadSetting.TabIndex = 2;
        this.buttonLoadSetting.Text = "LoadSetting";
        this.buttonLoadSetting.UseVisualStyleBackColor = true;
        this.buttonLoadSetting.Click += new System.EventHandler(this.ButtonLoadSetting_Click);
        // 
        // buttonSaveSetting
        // 
        this.buttonSaveSetting.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
        this.buttonSaveSetting.Location = new System.Drawing.Point(298, 380);
        this.buttonSaveSetting.Name = "buttonSaveSetting";
        this.buttonSaveSetting.Size = new System.Drawing.Size(75, 23);
        this.buttonSaveSetting.TabIndex = 3;
        this.buttonSaveSetting.Text = "SaveSetting";
        this.buttonSaveSetting.UseVisualStyleBackColor = true;
        this.buttonSaveSetting.Click += new System.EventHandler(this.ButtonSaveSetting_Click);
        // 
        // label1
        // 
        this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
                                                                   | System.Windows.Forms.AnchorStyles.Right)));
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(-3, 472);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(33, 12);
        this.label1.TabIndex = 1;
        this.label1.Text = "label1";
        this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // label_Intimacy
        // 
        this.label_Intimacy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
                                                                           | System.Windows.Forms.AnchorStyles.Right)));
        this.label_Intimacy.AutoSize = true;
        this.label_Intimacy.Location = new System.Drawing.Point(30, 240);
        this.label_Intimacy.Name = "label_Intimacy";
        this.label_Intimacy.Size = new System.Drawing.Size(53, 12);
        this.label_Intimacy.TabIndex = 2;
        this.label_Intimacy.Text = "非常警戒";
        this.label_Intimacy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // tabPage2
        // 
        this.tabPage2.AutoScroll = true;
        this.tabPage2.Controls.Add(this.groupBox_UnderGround);
        this.tabPage2.Controls.Add(this.groupBox_Monster);
        this.tabPage2.Location = new System.Drawing.Point(4, 22);
        this.tabPage2.Name = "tabPage2";
        this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
        this.tabPage2.Size = new System.Drawing.Size(516, 409);
        this.tabPage2.TabIndex = 1;
        this.tabPage2.Text = "遠征捕捉設定";
        this.tabPage2.UseVisualStyleBackColor = true;
        // 
        // groupBox_UnderGround
        // 
        this.groupBox_UnderGround.Location = new System.Drawing.Point(274, 43);
        this.groupBox_UnderGround.Name = "groupBox_UnderGround";
        this.groupBox_UnderGround.Size = new System.Drawing.Size(172, 306);
        this.groupBox_UnderGround.TabIndex = 1;
        this.groupBox_UnderGround.TabStop = false;
        this.groupBox_UnderGround.Text = "遠征地下城";
        // 
        // groupBox_Monster
        // 
        this.groupBox_Monster.Controls.Add(this.trackBar_Intimacy);
        this.groupBox_Monster.Controls.Add(this.label_Intimacy);
        this.groupBox_Monster.Location = new System.Drawing.Point(24, 43);
        this.groupBox_Monster.Name = "groupBox_Monster";
        this.groupBox_Monster.Size = new System.Drawing.Size(172, 306);
        this.groupBox_Monster.TabIndex = 0;
        this.groupBox_Monster.TabStop = false;
        this.groupBox_Monster.Text = "捕捉怪物";
        // 
        // trackBar_Intimacy
        // 
        this.trackBar_Intimacy.LargeChange = 1;
        this.trackBar_Intimacy.Location = new System.Drawing.Point(32, 255);
        this.trackBar_Intimacy.Maximum = 4;
        this.trackBar_Intimacy.Name = "trackBar_Intimacy";
        this.trackBar_Intimacy.Size = new System.Drawing.Size(104, 45);
        this.trackBar_Intimacy.TabIndex = 11;
        // 
        // tabPage1
        // 
        this.tabPage1.Controls.Add(this.探索報告);
        this.tabPage1.Controls.Add(this.checkBox_Event);
        this.tabPage1.Controls.Add(this.label_地下城);
        this.tabPage1.Controls.Add(this.trackBar_地下城);
        this.tabPage1.Controls.Add(this.buttonLoadSetting);
        this.tabPage1.Controls.Add(this.pictureBox1);
        this.tabPage1.Controls.Add(this.buttonSaveSetting);
        this.tabPage1.Controls.Add(this.遠征);
        this.tabPage1.Controls.Add(this.捕捉);
        this.tabPage1.Controls.Add(this.buttonStopFind);
        this.tabPage1.Controls.Add(this.buttonStartFind);
        this.tabPage1.Controls.Add(this.rebindButton);
        this.tabPage1.Controls.Add(this.TitleNameBox);
        this.tabPage1.Location = new System.Drawing.Point(4, 22);
        this.tabPage1.Name = "tabPage1";
        this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
        this.tabPage1.Size = new System.Drawing.Size(516, 409);
        this.tabPage1.TabIndex = 0;
        this.tabPage1.Text = "句柄";
        this.tabPage1.UseVisualStyleBackColor = true;
        // 
        // checkBox_Event
        // 
        this.checkBox_Event.AutoSize = true;
        this.checkBox_Event.Location = new System.Drawing.Point(9, 185);
        this.checkBox_Event.Name = "checkBox_Event";
        this.checkBox_Event.Size = new System.Drawing.Size(51, 16);
        this.checkBox_Event.TabIndex = 13;
        this.checkBox_Event.Text = "Event";
        this.checkBox_Event.UseVisualStyleBackColor = true;
        // 
        // label_地下城
        // 
        this.label_地下城.AutoSize = true;
        this.label_地下城.Location = new System.Drawing.Point(7, 216);
        this.label_地下城.Name = "label_地下城";
        this.label_地下城.Size = new System.Drawing.Size(33, 12);
        this.label_地下城.TabIndex = 12;
        this.label_地下城.Text = "label2";
        // 
        // trackBar_地下城
        // 
        this.trackBar_地下城.LargeChange = 1;
        this.trackBar_地下城.Location = new System.Drawing.Point(9, 231);
        this.trackBar_地下城.Maximum = 2;
        this.trackBar_地下城.Name = "trackBar_地下城";
        this.trackBar_地下城.Size = new System.Drawing.Size(104, 45);
        this.trackBar_地下城.TabIndex = 1;
        // 
        // pictureBox1
        // 
        this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
        this.pictureBox1.Location = new System.Drawing.Point(9, 6);
        this.pictureBox1.Name = "pictureBox1";
        this.pictureBox1.Size = new System.Drawing.Size(50, 50);
        this.pictureBox1.TabIndex = 10;
        this.pictureBox1.TabStop = false;
        this.pictureBox1.Click += new System.EventHandler(this.PictureBox1_Click);
        this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
        this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
        // 
        // 遠征
        // 
        this.遠征.AutoSize = true;
        this.遠征.Location = new System.Drawing.Point(9, 163);
        this.遠征.Name = "遠征";
        this.遠征.Size = new System.Drawing.Size(48, 16);
        this.遠征.TabIndex = 9;
        this.遠征.Text = "遠征";
        this.遠征.UseVisualStyleBackColor = true;
        // 
        // 捕捉
        // 
        this.捕捉.AutoSize = true;
        this.捕捉.Location = new System.Drawing.Point(9, 141);
        this.捕捉.Name = "捕捉";
        this.捕捉.Size = new System.Drawing.Size(48, 16);
        this.捕捉.TabIndex = 8;
        this.捕捉.Text = "捕捉";
        this.捕捉.UseVisualStyleBackColor = true;
        // 
        // buttonStopFind
        // 
        this.buttonStopFind.Location = new System.Drawing.Point(432, 323);
        this.buttonStopFind.Name = "buttonStopFind";
        this.buttonStopFind.Size = new System.Drawing.Size(75, 23);
        this.buttonStopFind.TabIndex = 7;
        this.buttonStopFind.Text = "StopFind";
        this.buttonStopFind.UseVisualStyleBackColor = true;
        this.buttonStopFind.Click += new System.EventHandler(this.ButtonStopFind_Click);
        // 
        // buttonStartFind
        // 
        this.buttonStartFind.Location = new System.Drawing.Point(432, 269);
        this.buttonStartFind.Name = "buttonStartFind";
        this.buttonStartFind.Size = new System.Drawing.Size(75, 23);
        this.buttonStartFind.TabIndex = 6;
        this.buttonStartFind.Text = "StartFind";
        this.buttonStartFind.UseVisualStyleBackColor = true;
        this.buttonStartFind.Click += new System.EventHandler(this.ButtonStartFind_Click);
        // 
        // rebindButton
        // 
        this.rebindButton.Location = new System.Drawing.Point(80, 34);
        this.rebindButton.Name = "rebindButton";
        this.rebindButton.Size = new System.Drawing.Size(75, 23);
        this.rebindButton.TabIndex = 5;
        this.rebindButton.Text = "解除綁定";
        this.rebindButton.UseVisualStyleBackColor = true;
        this.rebindButton.Click += new System.EventHandler(this.rebindButton_Click);
        // 
        // TitleNameBox
        // 
        this.TitleNameBox.Location = new System.Drawing.Point(80, 6);
        this.TitleNameBox.Name = "TitleNameBox";
        this.TitleNameBox.Size = new System.Drawing.Size(100, 22);
        this.TitleNameBox.TabIndex = 3;
        // 
        // tabControl1
        // 
        this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                                                                         | System.Windows.Forms.AnchorStyles.Left) 
                                                                        | System.Windows.Forms.AnchorStyles.Right)));
        this.tabControl1.Controls.Add(this.tabPage1);
        this.tabControl1.Controls.Add(this.tabPage2);
        this.tabControl1.Controls.Add(this.tabPage3);
        this.tabControl1.Location = new System.Drawing.Point(-1, -3);
        this.tabControl1.Name = "tabControl1";
        this.tabControl1.SelectedIndex = 0;
        this.tabControl1.Size = new System.Drawing.Size(524, 435);
        this.tabControl1.TabIndex = 0;
        // 
        // tabPage3
        // 
        this.tabPage3.Controls.Add(this.button1);
        this.tabPage3.Location = new System.Drawing.Point(4, 22);
        this.tabPage3.Name = "tabPage3";
        this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
        this.tabPage3.Size = new System.Drawing.Size(516, 409);
        this.tabPage3.TabIndex = 2;
        this.tabPage3.Text = "tabPage3";
        this.tabPage3.UseVisualStyleBackColor = true;
        // 
        // button1
        // 
        this.button1.Location = new System.Drawing.Point(192, 148);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(75, 23);
        this.button1.TabIndex = 0;
        this.button1.Text = "button1";
        this.button1.UseVisualStyleBackColor = true;
        // 
        // timer1
        // 
        this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
        // 
        // 探索報告
        // 
        this.探索報告.AutoSize = true;
        this.探索報告.Location = new System.Drawing.Point(9, 119);
        this.探索報告.Name = "探索報告";
        this.探索報告.Size = new System.Drawing.Size(72, 16);
        this.探索報告.TabIndex = 14;
        this.探索報告.Text = "探索報告";
        this.探索報告.UseVisualStyleBackColor = true;
        // 
        // Form1
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(522, 493);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.tabControl1);
        this.MaximizeBox = false;
        this.Name = "Form1";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "地下城之王腳本";
        this.tabPage2.ResumeLayout(false);
        this.groupBox_Monster.ResumeLayout(false);
        this.groupBox_Monster.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.trackBar_Intimacy)).EndInit();
        this.tabPage1.ResumeLayout(false);
        this.tabPage1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.trackBar_地下城)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
        this.tabControl1.ResumeLayout(false);
        this.tabPage3.ResumeLayout(false);
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button buttonLoadSetting;
    private System.Windows.Forms.Button buttonSaveSetting;
    private System.Windows.Forms.Label label_Intimacy;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.GroupBox groupBox_UnderGround;
    private System.Windows.Forms.GroupBox groupBox_Monster;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.CheckBox 遠征;
    private System.Windows.Forms.CheckBox 捕捉;
    private System.Windows.Forms.Button buttonStopFind;
    private System.Windows.Forms.Button buttonStartFind;
    private System.Windows.Forms.Button rebindButton;
    private System.Windows.Forms.TextBox TitleNameBox;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TrackBar trackBar_Intimacy;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.CheckBox checkBox_Event;
    private System.Windows.Forms.Label label_地下城;
    private System.Windows.Forms.TrackBar trackBar_地下城;
    private System.Windows.Forms.Timer timer1;
    private System.Windows.Forms.TabPage tabPage3;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.CheckBox 探索報告;
}