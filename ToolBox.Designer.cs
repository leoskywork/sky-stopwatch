namespace SkyStopwatch
{
    partial class ToolBox
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
            this.pictureBoxOne = new System.Windows.Forms.PictureBox();
            this.labelSize = new System.Windows.Forms.Label();
            this.labelMessage = new System.Windows.Forms.Label();
            this.buttonNewGame = new System.Windows.Forms.Button();
            this.timerAutoClose = new System.Windows.Forms.Timer(this.components);
            this.buttonTopMost = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonAddSeconds = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOne)).BeginInit();
            this.SuspendLayout();
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
            // labelSize
            // 
            this.labelSize.AutoSize = true;
            this.labelSize.Location = new System.Drawing.Point(66, 34);
            this.labelSize.Name = "labelSize";
            this.labelSize.Size = new System.Drawing.Size(79, 15);
            this.labelSize.TabIndex = 1;
            this.labelSize.Text = "600 x 300";
            // 
            // labelMessage
            // 
            this.labelMessage.AutoSize = true;
            this.labelMessage.Location = new System.Drawing.Point(189, 34);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(31, 15);
            this.labelMessage.TabIndex = 2;
            this.labelMessage.Text = "msg";
            // 
            // buttonNewGame
            // 
            this.buttonNewGame.Location = new System.Drawing.Point(733, 75);
            this.buttonNewGame.Name = "buttonNewGame";
            this.buttonNewGame.Size = new System.Drawing.Size(120, 30);
            this.buttonNewGame.TabIndex = 3;
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
            this.buttonTopMost.Location = new System.Drawing.Point(733, 217);
            this.buttonTopMost.Name = "buttonTopMost";
            this.buttonTopMost.Size = new System.Drawing.Size(120, 30);
            this.buttonTopMost.TabIndex = 4;
            this.buttonTopMost.Text = "Pin On/Off";
            this.buttonTopMost.UseVisualStyleBackColor = true;
            this.buttonTopMost.Click += new System.EventHandler(this.buttonTopMost_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(733, 288);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(120, 30);
            this.buttonClear.TabIndex = 5;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonAddSeconds
            // 
            this.buttonAddSeconds.Location = new System.Drawing.Point(733, 146);
            this.buttonAddSeconds.Name = "buttonAddSeconds";
            this.buttonAddSeconds.Size = new System.Drawing.Size(120, 30);
            this.buttonAddSeconds.TabIndex = 6;
            this.buttonAddSeconds.Text = "Add 10s";
            this.buttonAddSeconds.UseVisualStyleBackColor = true;
            this.buttonAddSeconds.Click += new System.EventHandler(this.buttonAddSeconds_Click);
            // 
            // ToolBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(918, 454);
            this.Controls.Add(this.buttonAddSeconds);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonTopMost);
            this.Controls.Add(this.buttonNewGame);
            this.Controls.Add(this.labelMessage);
            this.Controls.Add(this.labelSize);
            this.Controls.Add(this.pictureBoxOne);
            this.Name = "ToolBox";
            this.Text = "TestBox - auto close in  60 sec";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ToolBox_FormClosing);
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
    }
}