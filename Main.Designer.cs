namespace SkyStopwatch
{
    partial class Main
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
            this.buttonOCR = new System.Windows.Forms.Button();
            this.buttonTopMost = new System.Windows.Forms.Button();
            this.labelTimerPrefix = new System.Windows.Forms.Label();
            this.labelTimer = new System.Windows.Forms.Label();
            this.timerMain = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // buttonOCR
            // 
            this.buttonOCR.Location = new System.Drawing.Point(217, 19);
            this.buttonOCR.Name = "buttonOCR";
            this.buttonOCR.Size = new System.Drawing.Size(55, 23);
            this.buttonOCR.TabIndex = 0;
            this.buttonOCR.Text = "OCR";
            this.buttonOCR.UseVisualStyleBackColor = true;
            this.buttonOCR.Click += new System.EventHandler(this.buttonOCR_Click);
            // 
            // buttonTopMost
            // 
            this.buttonTopMost.Location = new System.Drawing.Point(278, 19);
            this.buttonTopMost.Name = "buttonTopMost";
            this.buttonTopMost.Size = new System.Drawing.Size(55, 23);
            this.buttonTopMost.TabIndex = 1;
            this.buttonTopMost.Text = "Unpin";
            this.buttonTopMost.UseVisualStyleBackColor = true;
            this.buttonTopMost.Click += new System.EventHandler(this.buttonTopMost_Click);
            // 
            // labelTimerPrefix
            // 
            this.labelTimerPrefix.AutoSize = true;
            this.labelTimerPrefix.Font = new System.Drawing.Font("SimSun", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTimerPrefix.Location = new System.Drawing.Point(2, 9);
            this.labelTimerPrefix.Name = "labelTimerPrefix";
            this.labelTimerPrefix.Size = new System.Drawing.Size(72, 35);
            this.labelTimerPrefix.TabIndex = 2;
            this.labelTimerPrefix.Text = "T +";
            // 
            // labelTimer
            // 
            this.labelTimer.AutoSize = true;
            this.labelTimer.Font = new System.Drawing.Font("SimSun", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTimer.Location = new System.Drawing.Point(77, 9);
            this.labelTimer.Name = "labelTimer";
            this.labelTimer.Size = new System.Drawing.Size(110, 35);
            this.labelTimer.TabIndex = 3;
            this.labelTimer.Text = "39:48";
            // 
            // timerMain
            // 
            this.timerMain.Tick += new System.EventHandler(this.timerMain_Tick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 51);
            this.Controls.Add(this.labelTimer);
            this.Controls.Add(this.labelTimerPrefix);
            this.Controls.Add(this.buttonTopMost);
            this.Controls.Add(this.buttonOCR);
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Text = "LEO Stopwatch";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOCR;
        private System.Windows.Forms.Button buttonTopMost;
        private System.Windows.Forms.Label labelTimerPrefix;
        private System.Windows.Forms.Label labelTimer;
        private System.Windows.Forms.Timer timerMain;
    }
}

