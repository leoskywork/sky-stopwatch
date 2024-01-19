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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.buttonOCR = new System.Windows.Forms.Button();
            this.buttonToolBox = new System.Windows.Forms.Button();
            this.labelTimer = new System.Windows.Forms.Label();
            this.timerMain = new System.Windows.Forms.Timer(this.components);
            this.timerAutoRefresh = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // buttonOCR
            // 
            this.buttonOCR.Location = new System.Drawing.Point(171, 17);
            this.buttonOCR.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOCR.Name = "buttonOCR";
            this.buttonOCR.Size = new System.Drawing.Size(70, 30);
            this.buttonOCR.TabIndex = 0;
            this.buttonOCR.Text = "OCR";
            this.buttonOCR.UseVisualStyleBackColor = true;
            this.buttonOCR.Click += new System.EventHandler(this.buttonOCR_Click);
            // 
            // buttonToolBox
            // 
            this.buttonToolBox.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonToolBox.Location = new System.Drawing.Point(248, 17);
            this.buttonToolBox.Margin = new System.Windows.Forms.Padding(4);
            this.buttonToolBox.Name = "buttonToolBox";
            this.buttonToolBox.Size = new System.Drawing.Size(30, 30);
            this.buttonToolBox.TabIndex = 1;
            this.buttonToolBox.Text = "P";
            this.buttonToolBox.UseVisualStyleBackColor = true;
            this.buttonToolBox.Click += new System.EventHandler(this.buttonToolBox_Click);
            // 
            // labelTimer
            // 
            this.labelTimer.AutoSize = true;
            this.labelTimer.Font = new System.Drawing.Font("SimSun", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTimer.Location = new System.Drawing.Point(13, 6);
            this.labelTimer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTimer.Name = "labelTimer";
            this.labelTimer.Size = new System.Drawing.Size(134, 44);
            this.labelTimer.TabIndex = 3;
            this.labelTimer.Text = "39:48";
            // 
            // timerMain
            // 
            this.timerMain.Tick += new System.EventHandler(this.timerMain_Tick);
            // 
            // timerAutoRefresh
            // 
            this.timerAutoRefresh.Tick += new System.EventHandler(this.timerAutoRefresh_Tick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 56);
            this.Controls.Add(this.labelTimer);
            this.Controls.Add(this.buttonToolBox);
            this.Controls.Add(this.buttonOCR);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LEO";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOCR;
        private System.Windows.Forms.Button buttonToolBox;
        private System.Windows.Forms.Label labelTimer;
        private System.Windows.Forms.Timer timerMain;
        private System.Windows.Forms.Timer timerAutoRefresh;
    }
}

