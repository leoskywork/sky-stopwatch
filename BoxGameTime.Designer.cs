namespace SkyStopwatch
{
    partial class BoxGameTime
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BoxGameTime));
            this.buttonToolBox = new System.Windows.Forms.Button();
            this.labelTimer = new System.Windows.Forms.Label();
            this.timerMain = new System.Windows.Forms.Timer(this.components);
            this.timerAutoRefresh = new System.Windows.Forms.Timer(this.components);
            this.labelTitle = new System.Windows.Forms.Label();
            this.buttonDummyAcceptHighLight = new System.Windows.Forms.Button();
            this.buttonCloseOverlay = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonOCR
            // 
     
            // 
            // buttonToolBox
            // 
            this.buttonToolBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonToolBox.Location = new System.Drawing.Point(248, 45);
            this.buttonToolBox.Margin = new System.Windows.Forms.Padding(4);
            this.buttonToolBox.Name = "buttonToolBox";
            this.buttonToolBox.Size = new System.Drawing.Size(30, 30);
            this.buttonToolBox.TabIndex = 100;
            this.buttonToolBox.Text = "+";
            this.buttonToolBox.UseVisualStyleBackColor = true;
            this.buttonToolBox.Click += new System.EventHandler(this.buttonToolBox_Click);
            // 
            // labelTimer
            // 
            this.labelTimer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelTimer.Font = new System.Drawing.Font("SimSun", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTimer.Location = new System.Drawing.Point(6, 38);
            this.labelTimer.Margin = new System.Windows.Forms.Padding(0);
            this.labelTimer.Name = "labelTimer";
            this.labelTimer.Size = new System.Drawing.Size(129, 40);
            this.labelTimer.TabIndex = 3;
            this.labelTimer.Text = "39:48";
            this.labelTimer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelTimer_MouseDown);
            // 
            // timerMain
            // 
            this.timerMain.Tick += new System.EventHandler(this.timerMain_Tick);
            // 
            // timerAutoRefresh
            // 
            this.timerAutoRefresh.Tick += new System.EventHandler(this.timerAutoRefresh_Tick);
            // 
            // labelTitle
            // 
            this.labelTitle.Font = new System.Drawing.Font("SimSun", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTitle.Location = new System.Drawing.Point(10, 6);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(35, 18);
            this.labelTitle.TabIndex = 5;
            this.labelTitle.Text = "LEO";
            this.labelTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelTitle_MouseDown);
            // 
            // buttonDummyAcceptHighLight
            // 
            this.buttonDummyAcceptHighLight.Location = new System.Drawing.Point(136, 1);
            this.buttonDummyAcceptHighLight.Name = "buttonDummyAcceptHighLight";
            this.buttonDummyAcceptHighLight.Size = new System.Drawing.Size(30, 30);
            this.buttonDummyAcceptHighLight.TabIndex = 10;
            this.buttonDummyAcceptHighLight.Text = "d";
            this.buttonDummyAcceptHighLight.UseVisualStyleBackColor = true;
            // 
            // buttonCloseOverlay
            // 
            this.buttonCloseOverlay.BackgroundImage = global::SkyStopwatch.Properties.Resources.power_off_512_w;
            this.buttonCloseOverlay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonCloseOverlay.FlatAppearance.BorderSize = 0;
            this.buttonCloseOverlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCloseOverlay.Location = new System.Drawing.Point(244, -4);
            this.buttonCloseOverlay.Name = "buttonCloseOverlay";
            this.buttonCloseOverlay.Size = new System.Drawing.Size(30, 30);
            this.buttonCloseOverlay.TabIndex = 20;
            this.buttonCloseOverlay.Text = "x";
            this.buttonCloseOverlay.UseVisualStyleBackColor = true;
            this.buttonCloseOverlay.Click += new System.EventHandler(this.buttonCloseOverlay_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 87);
            this.Controls.Add(this.buttonDummyAcceptHighLight);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.buttonCloseOverlay);
            this.Controls.Add(this.labelTimer);
            this.Controls.Add(this.buttonToolBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LEO";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Main_MouseDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonToolBox;
        private System.Windows.Forms.Label labelTimer;
        private System.Windows.Forms.Timer timerMain;
        private System.Windows.Forms.Timer timerAutoRefresh;
        private System.Windows.Forms.Button buttonCloseOverlay;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Button buttonDummyAcceptHighLight;
    }
}

