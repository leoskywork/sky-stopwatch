namespace SkyStopwatch
{
    partial class FormToolBox
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
            this.buttonAddSeconds = new System.Windows.Forms.Button();
            this.buttonOCR = new System.Windows.Forms.Button();
            this.buttonImageView = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonReduceSeconds = new System.Windows.Forms.Button();
            this.checkBoxPopWarning = new System.Windows.Forms.CheckBox();
            this.textBoxTimeSpanNodes = new System.Windows.Forms.TextBox();
            this.buttonSaveTimeNode = new System.Windows.Forms.Button();
            this.groupBoxTimeNode = new System.Windows.Forms.GroupBox();
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
            this.labelSize.Location = new System.Drawing.Point(54, 34);
            this.labelSize.Name = "labelSize";
            this.labelSize.Size = new System.Drawing.Size(79, 15);
            this.labelSize.TabIndex = 1;
            this.labelSize.Text = "600 x 300";
            // 
            // labelMessage
            // 
            this.labelMessage.AutoSize = true;
            this.labelMessage.Location = new System.Drawing.Point(217, 34);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(31, 15);
            this.labelMessage.TabIndex = 2;
            this.labelMessage.Text = "msg";
            // 
            // buttonNewGame
            // 
            this.buttonNewGame.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonNewGame.Location = new System.Drawing.Point(21, 148);
            this.buttonNewGame.Name = "buttonNewGame";
            this.buttonNewGame.Size = new System.Drawing.Size(120, 30);
            this.buttonNewGame.TabIndex = 200;
            this.buttonNewGame.Text = "New Game";
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
            this.buttonTopMost.Location = new System.Drawing.Point(29, 32);
            this.buttonTopMost.Name = "buttonTopMost";
            this.buttonTopMost.Size = new System.Drawing.Size(160, 30);
            this.buttonTopMost.TabIndex = 400;
            this.buttonTopMost.Text = "Toggle top most";
            this.buttonTopMost.UseVisualStyleBackColor = true;
            this.buttonTopMost.Click += new System.EventHandler(this.buttonTopMost_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonClear.Location = new System.Drawing.Point(21, 90);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(120, 30);
            this.buttonClear.TabIndex = 100;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonAddSeconds
            // 
            this.buttonAddSeconds.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonAddSeconds.Location = new System.Drawing.Point(21, 206);
            this.buttonAddSeconds.Name = "buttonAddSeconds";
            this.buttonAddSeconds.Size = new System.Drawing.Size(54, 30);
            this.buttonAddSeconds.TabIndex = 300;
            this.buttonAddSeconds.Text = "+10s";
            this.buttonAddSeconds.UseVisualStyleBackColor = true;
            this.buttonAddSeconds.Click += new System.EventHandler(this.buttonAddSeconds_Click);
            // 
            // buttonOCR
            // 
            this.buttonOCR.BackColor = System.Drawing.Color.SlateBlue;
            this.buttonOCR.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonOCR.ForeColor = System.Drawing.Color.White;
            this.buttonOCR.Location = new System.Drawing.Point(21, 32);
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
            this.buttonImageView.Location = new System.Drawing.Point(26, 24);
            this.buttonImageView.Name = "buttonImageView";
            this.buttonImageView.Size = new System.Drawing.Size(120, 30);
            this.buttonImageView.TabIndex = 401;
            this.buttonImageView.Text = "Image...";
            this.buttonImageView.UseVisualStyleBackColor = true;
            this.buttonImageView.Click += new System.EventHandler(this.buttonImageView_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonReduceSeconds);
            this.groupBox1.Controls.Add(this.buttonOCR);
            this.groupBox1.Controls.Add(this.buttonClear);
            this.groupBox1.Controls.Add(this.buttonAddSeconds);
            this.groupBox1.Controls.Add(this.buttonNewGame);
            this.groupBox1.Location = new System.Drawing.Point(686, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(168, 435);
            this.groupBox1.TabIndex = 402;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "OCR";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonImageView);
            this.groupBox2.Location = new System.Drawing.Point(57, 396);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(599, 73);
            this.groupBox2.TabIndex = 403;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tools";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBoxTimeNode);
            this.groupBox3.Controls.Add(this.checkBoxPopWarning);
            this.groupBox3.Controls.Add(this.buttonTopMost);
            this.groupBox3.Location = new System.Drawing.Point(875, 34);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(284, 435);
            this.groupBox3.TabIndex = 404;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Setting";
            // 
            // buttonReduceSeconds
            // 
            this.buttonReduceSeconds.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonReduceSeconds.Location = new System.Drawing.Point(87, 206);
            this.buttonReduceSeconds.Name = "buttonReduceSeconds";
            this.buttonReduceSeconds.Size = new System.Drawing.Size(54, 30);
            this.buttonReduceSeconds.TabIndex = 301;
            this.buttonReduceSeconds.Text = "-10s";
            this.buttonReduceSeconds.UseVisualStyleBackColor = true;
            this.buttonReduceSeconds.Click += new System.EventHandler(this.buttonReduceSeconds_Click);
            // 
            // checkBoxPopWarning
            // 
            this.checkBoxPopWarning.AutoSize = true;
            this.checkBoxPopWarning.Checked = true;
            this.checkBoxPopWarning.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPopWarning.Location = new System.Drawing.Point(29, 92);
            this.checkBoxPopWarning.Name = "checkBoxPopWarning";
            this.checkBoxPopWarning.Size = new System.Drawing.Size(173, 19);
            this.checkBoxPopWarning.TabIndex = 401;
            this.checkBoxPopWarning.Text = "Monitor time nodes";
            this.checkBoxPopWarning.UseVisualStyleBackColor = true;
            this.checkBoxPopWarning.CheckedChanged += new System.EventHandler(this.checkBoxPopWarning_CheckedChanged);
            // 
            // textBoxTimeSpanNodes
            // 
            this.textBoxTimeSpanNodes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxTimeSpanNodes.Location = new System.Drawing.Point(21, 24);
            this.textBoxTimeSpanNodes.Multiline = true;
            this.textBoxTimeSpanNodes.Name = "textBoxTimeSpanNodes";
            this.textBoxTimeSpanNodes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxTimeSpanNodes.Size = new System.Drawing.Size(176, 80);
            this.textBoxTimeSpanNodes.TabIndex = 403;
            this.textBoxTimeSpanNodes.Text = "10:00\r\n20:00\r\n35:00";
            this.textBoxTimeSpanNodes.TextChanged += new System.EventHandler(this.textBoxTimeSpanNodes_TextChanged);
            // 
            // buttonSaveTimeNode
            // 
            this.buttonSaveTimeNode.Enabled = false;
            this.buttonSaveTimeNode.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonSaveTimeNode.Location = new System.Drawing.Point(115, 112);
            this.buttonSaveTimeNode.Name = "buttonSaveTimeNode";
            this.buttonSaveTimeNode.Size = new System.Drawing.Size(60, 30);
            this.buttonSaveTimeNode.TabIndex = 404;
            this.buttonSaveTimeNode.Text = "Save";
            this.buttonSaveTimeNode.UseVisualStyleBackColor = true;
            this.buttonSaveTimeNode.Click += new System.EventHandler(this.buttonSaveTimeNode_Click);
            // 
            // groupBoxTimeNode
            // 
            this.groupBoxTimeNode.Controls.Add(this.buttonSaveTimeNode);
            this.groupBoxTimeNode.Controls.Add(this.textBoxTimeSpanNodes);
            this.groupBoxTimeNode.Location = new System.Drawing.Point(29, 117);
            this.groupBoxTimeNode.Name = "groupBoxTimeNode";
            this.groupBoxTimeNode.Size = new System.Drawing.Size(226, 158);
            this.groupBoxTimeNode.TabIndex = 406;
            this.groupBoxTimeNode.TabStop = false;
            this.groupBoxTimeNode.Text = "time since game start";
            // 
            // pictureBoxOne
            // 
            this.pictureBoxOne.BackColor = System.Drawing.Color.LightGray;
            this.pictureBoxOne.Location = new System.Drawing.Point(56, 75);
            this.pictureBoxOne.Name = "pictureBoxOne";
            this.pictureBoxOne.Size = new System.Drawing.Size(600, 300);
            this.pictureBoxOne.TabIndex = 0;
            this.pictureBoxOne.TabStop = false;
            // 
            // FormToolBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1191, 491);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelMessage);
            this.Controls.Add(this.labelSize);
            this.Controls.Add(this.pictureBoxOne);
            this.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MaximizeBox = false;
            this.Name = "FormToolBox";
            this.Text = "Tool Box - auto close in  60 sec";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ToolBox_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
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
        private System.Windows.Forms.Button buttonAddSeconds;
        private System.Windows.Forms.Button buttonOCR;
        private System.Windows.Forms.Button buttonImageView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonReduceSeconds;
        private System.Windows.Forms.CheckBox checkBoxPopWarning;
        private System.Windows.Forms.TextBox textBoxTimeSpanNodes;
        private System.Windows.Forms.Button buttonSaveTimeNode;
        private System.Windows.Forms.GroupBox groupBoxTimeNode;
    }
}