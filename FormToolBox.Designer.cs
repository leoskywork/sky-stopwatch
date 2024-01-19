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
            this.pictureBoxOne = new System.Windows.Forms.PictureBox();
            this.labelSize = new System.Windows.Forms.Label();
            this.labelMessage = new System.Windows.Forms.Label();
            this.buttonNewGame = new System.Windows.Forms.Button();
            this.timerAutoClose = new System.Windows.Forms.Timer(this.components);
            this.buttonTopMost = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonAddSeconds = new System.Windows.Forms.Button();
            this.buttonOCR = new System.Windows.Forms.Button();
            this.buttonImageView = new System.Windows.Forms.Button();
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
            this.buttonNewGame.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonNewGame.Location = new System.Drawing.Point(733, 147);
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
            this.buttonTopMost.Location = new System.Drawing.Point(733, 291);
            this.buttonTopMost.Name = "buttonTopMost";
            this.buttonTopMost.Size = new System.Drawing.Size(120, 30);
            this.buttonTopMost.TabIndex = 400;
            this.buttonTopMost.Text = "Pin On/Off";
            this.buttonTopMost.UseVisualStyleBackColor = true;
            this.buttonTopMost.Click += new System.EventHandler(this.buttonTopMost_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonClear.Location = new System.Drawing.Point(733, 75);
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
            this.buttonAddSeconds.Location = new System.Drawing.Point(733, 219);
            this.buttonAddSeconds.Name = "buttonAddSeconds";
            this.buttonAddSeconds.Size = new System.Drawing.Size(120, 30);
            this.buttonAddSeconds.TabIndex = 300;
            this.buttonAddSeconds.Text = "Add 10s";
            this.buttonAddSeconds.UseVisualStyleBackColor = true;
            this.buttonAddSeconds.Click += new System.EventHandler(this.buttonAddSeconds_Click);
            // 
            // buttonOCR
            // 
            this.buttonOCR.BackColor = System.Drawing.Color.SlateBlue;
            this.buttonOCR.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonOCR.ForeColor = System.Drawing.Color.White;
            this.buttonOCR.Location = new System.Drawing.Point(536, 34);
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
            this.buttonImageView.Location = new System.Drawing.Point(733, 356);
            this.buttonImageView.Name = "buttonImageView";
            this.buttonImageView.Size = new System.Drawing.Size(120, 30);
            this.buttonImageView.TabIndex = 401;
            this.buttonImageView.Text = "Image...";
            this.buttonImageView.UseVisualStyleBackColor = true;
            this.buttonImageView.Click += new System.EventHandler(this.buttonImageView_Click);
            // 
            // FormToolBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(918, 454);
            this.Controls.Add(this.buttonImageView);
            this.Controls.Add(this.buttonOCR);
            this.Controls.Add(this.buttonAddSeconds);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonTopMost);
            this.Controls.Add(this.buttonNewGame);
            this.Controls.Add(this.labelMessage);
            this.Controls.Add(this.labelSize);
            this.Controls.Add(this.pictureBoxOne);
            this.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MaximizeBox = false;
            this.Name = "FormToolBox";
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
        private System.Windows.Forms.Button buttonOCR;
        private System.Windows.Forms.Button buttonImageView;
    }
}