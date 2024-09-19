namespace SkyStopwatch
{
    partial class FormBootSetting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.labelSize = new System.Windows.Forms.Label();
            this.labelMessage = new System.Windows.Forms.Label();
            this.buttonNewGame = new System.Windows.Forms.Button();
            this.timerAutoClose = new System.Windows.Forms.Timer(this.components);
            this.buttonTopMost = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonAddFewSeconds = new System.Windows.Forms.Button();
            this.buttonOCR = new System.Windows.Forms.Button();
            this.buttonImageView = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonLockTime = new System.Windows.Forms.Button();
            this.buttonReduceMinute = new System.Windows.Forms.Button();
            this.buttonAddMinute = new System.Windows.Forms.Button();
            this.buttonAddSeconds = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonCountRun = new System.Windows.Forms.Button();
            this.buttonCount = new System.Windows.Forms.Button();
            this.buttonPriceList = new System.Windows.Forms.Button();
            this.checkBoxDebugging = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBoxTopMost = new System.Windows.Forms.CheckBox();
            this.buttonChangeTheme = new System.Windows.Forms.Button();
            this.buttonCloseApp = new System.Windows.Forms.Button();
            this.groupBoxTimeNode = new System.Windows.Forms.GroupBox();
            this.buttonSaveTimeNode = new System.Windows.Forms.Button();
            this.textBoxTimeSpanNodes = new System.Windows.Forms.TextBox();
            this.checkBoxPopWarning = new System.Windows.Forms.CheckBox();
            this.pictureBoxOne = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBoxTimeNode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOne)).BeginInit();
            this.SuspendLayout();
            // 
            // labelSize
            // 
            this.labelSize.AutoSize = true;
            this.labelSize.Location = new System.Drawing.Point(20, 18);
            this.labelSize.Name = "labelSize";
            this.labelSize.Size = new System.Drawing.Size(79, 15);
            this.labelSize.TabIndex = 1;
            this.labelSize.Text = "600 x 300";
            // 
            // labelMessage
            // 
            this.labelMessage.AutoSize = true;
            this.labelMessage.Location = new System.Drawing.Point(183, 18);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(31, 15);
            this.labelMessage.TabIndex = 2;
            this.labelMessage.Text = "msg";
            this.labelMessage.MouseHover += new System.EventHandler(this.labelMessage_MouseHover);
            // 
            // buttonNewGame
            // 
            this.buttonNewGame.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonNewGame.Location = new System.Drawing.Point(20, 129);
            this.buttonNewGame.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonNewGame.Name = "buttonNewGame";
            this.buttonNewGame.Size = new System.Drawing.Size(120, 30);
            this.buttonNewGame.TabIndex = 200;
            this.buttonNewGame.Text = "Restart";
            this.buttonNewGame.UseVisualStyleBackColor = true;
            this.buttonNewGame.Click += new System.EventHandler(this.buttonNewGame_Click);
            // 
            // timerAutoClose
            // 
            this.timerAutoClose.Tick += new System.EventHandler(this.timerAutoClose_Tick);
            // 
            // buttonTopMost
            // 
            this.buttonTopMost.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonTopMost.Location = new System.Drawing.Point(80, 368);
            this.buttonTopMost.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonTopMost.Name = "buttonTopMost";
            this.buttonTopMost.Size = new System.Drawing.Size(100, 30);
            this.buttonTopMost.TabIndex = 400;
            this.buttonTopMost.Text = "x Top most";
            this.buttonTopMost.UseVisualStyleBackColor = true;
            this.buttonTopMost.Click += new System.EventHandler(this.buttonTopMost_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonClear.Location = new System.Drawing.Point(20, 80);
            this.buttonClear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(120, 30);
            this.buttonClear.TabIndex = 100;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonAddFewSeconds
            // 
            this.buttonAddFewSeconds.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonAddFewSeconds.Location = new System.Drawing.Point(21, 181);
            this.buttonAddFewSeconds.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonAddFewSeconds.Name = "buttonAddFewSeconds";
            this.buttonAddFewSeconds.Size = new System.Drawing.Size(53, 30);
            this.buttonAddFewSeconds.TabIndex = 300;
            this.buttonAddFewSeconds.Text = "+2s";
            this.buttonAddFewSeconds.UseVisualStyleBackColor = true;
            this.buttonAddFewSeconds.Click += new System.EventHandler(this.buttonAddFewSeconds_Click);
            // 
            // buttonOCR
            // 
            this.buttonOCR.BackColor = System.Drawing.Color.SlateBlue;
            this.buttonOCR.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonOCR.ForeColor = System.Drawing.Color.White;
            this.buttonOCR.Location = new System.Drawing.Point(21, 32);
            this.buttonOCR.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonOCR.Name = "buttonOCR";
            this.buttonOCR.Size = new System.Drawing.Size(120, 30);
            this.buttonOCR.TabIndex = 7;
            this.buttonOCR.Text = "Run OCR";
            this.buttonOCR.UseVisualStyleBackColor = false;
            this.buttonOCR.Click += new System.EventHandler(this.buttonOCR_Click);
            // 
            // buttonImageView
            // 
            this.buttonImageView.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonImageView.Location = new System.Drawing.Point(27, 25);
            this.buttonImageView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonImageView.Name = "buttonImageView";
            this.buttonImageView.Size = new System.Drawing.Size(95, 30);
            this.buttonImageView.TabIndex = 401;
            this.buttonImageView.Text = "Time...";
            this.buttonImageView.UseVisualStyleBackColor = true;
            this.buttonImageView.Click += new System.EventHandler(this.buttonImageView_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonLockTime);
            this.groupBox1.Controls.Add(this.buttonReduceMinute);
            this.groupBox1.Controls.Add(this.buttonAddMinute);
            this.groupBox1.Controls.Add(this.buttonAddSeconds);
            this.groupBox1.Controls.Add(this.buttonOCR);
            this.groupBox1.Controls.Add(this.buttonClear);
            this.groupBox1.Controls.Add(this.buttonAddFewSeconds);
            this.groupBox1.Controls.Add(this.buttonNewGame);
            this.groupBox1.Location = new System.Drawing.Point(480, 34);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(168, 325);
            this.groupBox1.TabIndex = 402;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "OCR";
            // 
            // buttonLockTime
            // 
            this.buttonLockTime.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonLockTime.Location = new System.Drawing.Point(21, 274);
            this.buttonLockTime.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonLockTime.Name = "buttonLockTime";
            this.buttonLockTime.Size = new System.Drawing.Size(120, 30);
            this.buttonLockTime.TabIndex = 304;
            this.buttonLockTime.Text = "Lock";
            this.buttonLockTime.UseVisualStyleBackColor = true;
            this.buttonLockTime.Click += new System.EventHandler(this.buttonLockTime_Click);
            // 
            // buttonReduceMinute
            // 
            this.buttonReduceMinute.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonReduceMinute.Location = new System.Drawing.Point(87, 220);
            this.buttonReduceMinute.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonReduceMinute.Name = "buttonReduceMinute";
            this.buttonReduceMinute.Size = new System.Drawing.Size(53, 30);
            this.buttonReduceMinute.TabIndex = 303;
            this.buttonReduceMinute.Text = "-1m";
            this.buttonReduceMinute.UseVisualStyleBackColor = true;
            this.buttonReduceMinute.Click += new System.EventHandler(this.buttonReduceMinute_Click);
            // 
            // buttonAddMinute
            // 
            this.buttonAddMinute.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonAddMinute.Location = new System.Drawing.Point(21, 220);
            this.buttonAddMinute.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonAddMinute.Name = "buttonAddMinute";
            this.buttonAddMinute.Size = new System.Drawing.Size(53, 30);
            this.buttonAddMinute.TabIndex = 302;
            this.buttonAddMinute.Text = "+1m";
            this.buttonAddMinute.UseVisualStyleBackColor = true;
            this.buttonAddMinute.Click += new System.EventHandler(this.buttonAddMinute_Click);
            // 
            // buttonAddSeconds
            // 
            this.buttonAddSeconds.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonAddSeconds.Location = new System.Drawing.Point(87, 181);
            this.buttonAddSeconds.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonAddSeconds.Name = "buttonAddSeconds";
            this.buttonAddSeconds.Size = new System.Drawing.Size(53, 30);
            this.buttonAddSeconds.TabIndex = 301;
            this.buttonAddSeconds.Text = "+10s";
            this.buttonAddSeconds.UseVisualStyleBackColor = true;
            this.buttonAddSeconds.Click += new System.EventHandler(this.buttonAddSeconds_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonCountRun);
            this.groupBox2.Controls.Add(this.buttonCount);
            this.groupBox2.Controls.Add(this.buttonPriceList);
            this.groupBox2.Controls.Add(this.checkBoxDebugging);
            this.groupBox2.Controls.Add(this.buttonImageView);
            this.groupBox2.Location = new System.Drawing.Point(23, 380);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(625, 72);
            this.groupBox2.TabIndex = 403;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tools";
            // 
            // buttonCountRun
            // 
            this.buttonCountRun.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonCountRun.Location = new System.Drawing.Point(469, 25);
            this.buttonCountRun.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonCountRun.Name = "buttonCountRun";
            this.buttonCountRun.Size = new System.Drawing.Size(54, 30);
            this.buttonCountRun.TabIndex = 405;
            this.buttonCountRun.Text = "Run";
            this.buttonCountRun.UseVisualStyleBackColor = true;
            this.buttonCountRun.Click += new System.EventHandler(this.buttonCountRun_Click);
            // 
            // buttonCount
            // 
            this.buttonCount.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonCount.Location = new System.Drawing.Point(376, 25);
            this.buttonCount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonCount.Name = "buttonCount";
            this.buttonCount.Size = new System.Drawing.Size(95, 30);
            this.buttonCount.TabIndex = 404;
            this.buttonCount.Text = "Count...";
            this.buttonCount.UseVisualStyleBackColor = true;
            this.buttonCount.Click += new System.EventHandler(this.buttonCount_Click);
            // 
            // buttonPriceList
            // 
            this.buttonPriceList.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonPriceList.Location = new System.Drawing.Point(260, 25);
            this.buttonPriceList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonPriceList.Name = "buttonPriceList";
            this.buttonPriceList.Size = new System.Drawing.Size(95, 30);
            this.buttonPriceList.TabIndex = 403;
            this.buttonPriceList.Text = "Price...";
            this.buttonPriceList.UseVisualStyleBackColor = true;
            this.buttonPriceList.Click += new System.EventHandler(this.buttonPriceList_Click);
            // 
            // checkBoxDebugging
            // 
            this.checkBoxDebugging.AutoSize = true;
            this.checkBoxDebugging.Location = new System.Drawing.Point(140, 32);
            this.checkBoxDebugging.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxDebugging.Name = "checkBoxDebugging";
            this.checkBoxDebugging.Size = new System.Drawing.Size(101, 19);
            this.checkBoxDebugging.TabIndex = 402;
            this.checkBoxDebugging.Text = "Debugging";
            this.checkBoxDebugging.UseVisualStyleBackColor = true;
            this.checkBoxDebugging.CheckedChanged += new System.EventHandler(this.checkBoxDebugging_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBoxTopMost);
            this.groupBox3.Controls.Add(this.buttonChangeTheme);
            this.groupBox3.Controls.Add(this.buttonCloseApp);
            this.groupBox3.Controls.Add(this.groupBoxTimeNode);
            this.groupBox3.Controls.Add(this.checkBoxPopWarning);
            this.groupBox3.Controls.Add(this.buttonTopMost);
            this.groupBox3.Location = new System.Drawing.Point(669, 34);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Size = new System.Drawing.Size(284, 419);
            this.groupBox3.TabIndex = 404;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Setting";
            // 
            // checkBoxTopMost
            // 
            this.checkBoxTopMost.AutoSize = true;
            this.checkBoxTopMost.Checked = true;
            this.checkBoxTopMost.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxTopMost.Location = new System.Drawing.Point(29, 39);
            this.checkBoxTopMost.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxTopMost.Name = "checkBoxTopMost";
            this.checkBoxTopMost.Size = new System.Drawing.Size(93, 19);
            this.checkBoxTopMost.TabIndex = 408;
            this.checkBoxTopMost.Text = "Top most";
            this.checkBoxTopMost.UseVisualStyleBackColor = true;
            this.checkBoxTopMost.CheckedChanged += new System.EventHandler(this.checkBoxTopMost_CheckedChanged);
            // 
            // buttonChangeTheme
            // 
            this.buttonChangeTheme.Location = new System.Drawing.Point(29, 90);
            this.buttonChangeTheme.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonChangeTheme.Name = "buttonChangeTheme";
            this.buttonChangeTheme.Size = new System.Drawing.Size(227, 30);
            this.buttonChangeTheme.TabIndex = 404;
            this.buttonChangeTheme.Text = "Change theme - 0";
            this.buttonChangeTheme.UseVisualStyleBackColor = true;
            this.buttonChangeTheme.Click += new System.EventHandler(this.buttonChangeTheme_Click);
            // 
            // buttonCloseApp
            // 
            this.buttonCloseApp.BackColor = System.Drawing.Color.OrangeRed;
            this.buttonCloseApp.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonCloseApp.ForeColor = System.Drawing.Color.White;
            this.buttonCloseApp.Location = new System.Drawing.Point(155, 32);
            this.buttonCloseApp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonCloseApp.Name = "buttonCloseApp";
            this.buttonCloseApp.Size = new System.Drawing.Size(100, 30);
            this.buttonCloseApp.TabIndex = 407;
            this.buttonCloseApp.Text = "KILL";
            this.buttonCloseApp.UseVisualStyleBackColor = false;
            this.buttonCloseApp.Click += new System.EventHandler(this.buttonCloseApp_Click);
            // 
            // groupBoxTimeNode
            // 
            this.groupBoxTimeNode.Controls.Add(this.buttonSaveTimeNode);
            this.groupBoxTimeNode.Controls.Add(this.textBoxTimeSpanNodes);
            this.groupBoxTimeNode.Location = new System.Drawing.Point(29, 175);
            this.groupBoxTimeNode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxTimeNode.Name = "groupBoxTimeNode";
            this.groupBoxTimeNode.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxTimeNode.Size = new System.Drawing.Size(227, 158);
            this.groupBoxTimeNode.TabIndex = 406;
            this.groupBoxTimeNode.TabStop = false;
            this.groupBoxTimeNode.Text = "time since game start";
            // 
            // buttonSaveTimeNode
            // 
            this.buttonSaveTimeNode.Enabled = false;
            this.buttonSaveTimeNode.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonSaveTimeNode.Location = new System.Drawing.Point(115, 112);
            this.buttonSaveTimeNode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSaveTimeNode.Name = "buttonSaveTimeNode";
            this.buttonSaveTimeNode.Size = new System.Drawing.Size(60, 30);
            this.buttonSaveTimeNode.TabIndex = 404;
            this.buttonSaveTimeNode.Text = "Save";
            this.buttonSaveTimeNode.UseVisualStyleBackColor = true;
            this.buttonSaveTimeNode.Click += new System.EventHandler(this.buttonSaveTimeNode_Click);
            // 
            // textBoxTimeSpanNodes
            // 
            this.textBoxTimeSpanNodes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxTimeSpanNodes.Location = new System.Drawing.Point(21, 24);
            this.textBoxTimeSpanNodes.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxTimeSpanNodes.Multiline = true;
            this.textBoxTimeSpanNodes.Name = "textBoxTimeSpanNodes";
            this.textBoxTimeSpanNodes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxTimeSpanNodes.Size = new System.Drawing.Size(175, 80);
            this.textBoxTimeSpanNodes.TabIndex = 403;
            this.textBoxTimeSpanNodes.Text = "10:00\r\n20:00\r\n35:00";
            this.textBoxTimeSpanNodes.TextChanged += new System.EventHandler(this.textBoxTimeSpanNodes_TextChanged);
            // 
            // checkBoxPopWarning
            // 
            this.checkBoxPopWarning.AutoSize = true;
            this.checkBoxPopWarning.Checked = true;
            this.checkBoxPopWarning.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPopWarning.Location = new System.Drawing.Point(29, 150);
            this.checkBoxPopWarning.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxPopWarning.Name = "checkBoxPopWarning";
            this.checkBoxPopWarning.Size = new System.Drawing.Size(173, 19);
            this.checkBoxPopWarning.TabIndex = 401;
            this.checkBoxPopWarning.Text = "Monitor time nodes";
            this.checkBoxPopWarning.UseVisualStyleBackColor = true;
            this.checkBoxPopWarning.CheckedChanged += new System.EventHandler(this.checkBoxPopWarning_CheckedChanged);
            // 
            // pictureBoxOne
            // 
            this.pictureBoxOne.BackColor = System.Drawing.Color.LightGray;
            this.pictureBoxOne.Location = new System.Drawing.Point(21, 59);
            this.pictureBoxOne.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBoxOne.Name = "pictureBoxOne";
            this.pictureBoxOne.Size = new System.Drawing.Size(400, 300);
            this.pictureBoxOne.TabIndex = 0;
            this.pictureBoxOne.TabStop = false;
            // 
            // FormBootSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 476);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelMessage);
            this.Controls.Add(this.labelSize);
            this.Controls.Add(this.pictureBoxOne);
            this.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "FormBootSetting";
            this.Text = "Tool Box - auto close in  60 sec";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ToolBox_FormClosing);
            this.Load += new System.EventHandler(this.FormToolBox_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBoxTimeNode.ResumeLayout(false);
            this.groupBoxTimeNode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOne)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxOne;
        private System.Windows.Forms.Label labelSize;
        private System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.Button buttonNewGame;
        private System.Windows.Forms.Timer timerAutoClose;
        private System.Windows.Forms.Button buttonTopMost;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonAddFewSeconds;
        private System.Windows.Forms.Button buttonOCR;
        private System.Windows.Forms.Button buttonImageView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonAddSeconds;
        private System.Windows.Forms.CheckBox checkBoxPopWarning;
        private System.Windows.Forms.TextBox textBoxTimeSpanNodes;
        private System.Windows.Forms.Button buttonSaveTimeNode;
        private System.Windows.Forms.GroupBox groupBoxTimeNode;
        private System.Windows.Forms.CheckBox checkBoxDebugging;
        private System.Windows.Forms.Button buttonChangeTheme;
        private System.Windows.Forms.Button buttonAddMinute;
        private System.Windows.Forms.Button buttonReduceMinute;
        private System.Windows.Forms.Button buttonCloseApp;
        private System.Windows.Forms.Button buttonPriceList;
        private System.Windows.Forms.CheckBox checkBoxTopMost;
        private System.Windows.Forms.Button buttonCount;
        private System.Windows.Forms.Button buttonCountRun;
        private System.Windows.Forms.Button buttonLockTime;
    }
}