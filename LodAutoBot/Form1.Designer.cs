
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
            this.buttonLoadSetting = new System.Windows.Forms.Button();
            this.buttonSaveSetting = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox_Monster = new System.Windows.Forms.GroupBox();
            this.groupBox_UnderGround = new System.Windows.Forms.GroupBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.bindButton = new System.Windows.Forms.Button();
            this.TitleNameBox = new System.Windows.Forms.TextBox();
            this.rebindButton = new System.Windows.Forms.Button();
            this.buttonStartFind = new System.Windows.Forms.Button();
            this.buttonStopFind = new System.Windows.Forms.Button();
            this.捕捉 = new System.Windows.Forms.CheckBox();
            this.遠征 = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonLoadSetting
            // 
            this.buttonLoadSetting.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonLoadSetting.Location = new System.Drawing.Point(424, 434);
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
            this.buttonSaveSetting.Location = new System.Drawing.Point(300, 434);
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
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(275, 471);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "label2";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            // groupBox_Monster
            // 
            this.groupBox_Monster.Location = new System.Drawing.Point(24, 43);
            this.groupBox_Monster.Name = "groupBox_Monster";
            this.groupBox_Monster.Size = new System.Drawing.Size(172, 306);
            this.groupBox_Monster.TabIndex = 0;
            this.groupBox_Monster.TabStop = false;
            this.groupBox_Monster.Text = "捕捉怪物";
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
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.遠征);
            this.tabPage1.Controls.Add(this.捕捉);
            this.tabPage1.Controls.Add(this.buttonStopFind);
            this.tabPage1.Controls.Add(this.buttonStartFind);
            this.tabPage1.Controls.Add(this.rebindButton);
            this.tabPage1.Controls.Add(this.TitleNameBox);
            this.tabPage1.Controls.Add(this.bindButton);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(516, 409);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "句柄";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // bindButton
            // 
            this.bindButton.Location = new System.Drawing.Point(9, 24);
            this.bindButton.Name = "bindButton";
            this.bindButton.Size = new System.Drawing.Size(75, 23);
            this.bindButton.TabIndex = 0;
            this.bindButton.Text = "綁定窗口";
            this.bindButton.UseVisualStyleBackColor = true;
            this.bindButton.Click += new System.EventHandler(this.bindButton_Click);
            // 
            // TitleNameBox
            // 
            this.TitleNameBox.Location = new System.Drawing.Point(80, 61);
            this.TitleNameBox.Name = "TitleNameBox";
            this.TitleNameBox.Size = new System.Drawing.Size(100, 22);
            this.TitleNameBox.TabIndex = 3;
            // 
            // rebindButton
            // 
            this.rebindButton.Location = new System.Drawing.Point(105, 24);
            this.rebindButton.Name = "rebindButton";
            this.rebindButton.Size = new System.Drawing.Size(75, 23);
            this.rebindButton.TabIndex = 5;
            this.rebindButton.Text = "解除綁定";
            this.rebindButton.UseVisualStyleBackColor = true;
            this.rebindButton.Click += new System.EventHandler(this.rebindButton_Click);
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
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(-1, -3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(524, 435);
            this.tabControl1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 493);
            this.Controls.Add(this.buttonLoadSetting);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonSaveSetting);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "地下城之王腳本";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabPage2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonLoadSetting;
        private System.Windows.Forms.Button buttonSaveSetting;
        private System.Windows.Forms.Label label2;
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
        private System.Windows.Forms.Button bindButton;
        private System.Windows.Forms.TabControl tabControl1;
    }


