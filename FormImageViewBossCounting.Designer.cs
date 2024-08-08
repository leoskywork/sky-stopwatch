namespace SkyStopwatch
{
    partial class FormImageViewBossCounting
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
            this.buttonSave = new System.Windows.Forms.Button();
            this.numericUpDownX = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownY = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownWidth = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownHeight = new System.Windows.Forms.NumericUpDown();
            this.labelX = new System.Windows.Forms.Label();
            this.labelY = new System.Windows.Forms.Label();
            this.labelWidth = new System.Windows.Forms.Label();
            this.labelHeight = new System.Windows.Forms.Label();
            this.pictureBoxOne = new System.Windows.Forms.PictureBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.timerScan = new System.Windows.Forms.Timer(this.components);
            this.timerCompare = new System.Windows.Forms.Timer(this.components);
            this.panelImage = new System.Windows.Forms.Panel();
            this.checkBoxAutoSlice = new System.Windows.Forms.CheckBox();
            this.numericUpDownAutoSliceIntervalSeconds = new System.Windows.Forms.NumericUpDown();
            this.checkBoxOneMode = new System.Windows.Forms.CheckBox();
            this.checkBox2SpotsCompare = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOne)).BeginInit();
            this.panelImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAutoSliceIntervalSeconds)).BeginInit();
            this.SuspendLayout();
            // 
            // labelSize
            // 
            this.labelSize.AutoSize = true;
            this.labelSize.Location = new System.Drawing.Point(31, 9);
            this.labelSize.Name = "labelSize";
            this.labelSize.Size = new System.Drawing.Size(79, 15);
            this.labelSize.TabIndex = 1;
            this.labelSize.Text = "600 x 400";
            // 
            // buttonSave
            // 
            this.buttonSave.Enabled = false;
            this.buttonSave.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonSave.Location = new System.Drawing.Point(422, 546);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(80, 30);
            this.buttonSave.TabIndex = 20;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // numericUpDownX
            // 
            this.numericUpDownX.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownX.Location = new System.Drawing.Point(422, 81);
            this.numericUpDownX.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownX.Name = "numericUpDownX";
            this.numericUpDownX.Size = new System.Drawing.Size(80, 25);
            this.numericUpDownX.TabIndex = 101;
            this.numericUpDownX.ValueChanged += new System.EventHandler(this.numericUpDownX_ValueChanged);
            // 
            // numericUpDownY
            // 
            this.numericUpDownY.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownY.Location = new System.Drawing.Point(422, 154);
            this.numericUpDownY.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownY.Name = "numericUpDownY";
            this.numericUpDownY.Size = new System.Drawing.Size(80, 25);
            this.numericUpDownY.TabIndex = 102;
            this.numericUpDownY.ValueChanged += new System.EventHandler(this.numericUpDownY_ValueChanged);
            // 
            // numericUpDownWidth
            // 
            this.numericUpDownWidth.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownWidth.Location = new System.Drawing.Point(422, 248);
            this.numericUpDownWidth.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownWidth.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownWidth.Name = "numericUpDownWidth";
            this.numericUpDownWidth.Size = new System.Drawing.Size(80, 25);
            this.numericUpDownWidth.TabIndex = 103;
            this.numericUpDownWidth.Value = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.numericUpDownWidth.ValueChanged += new System.EventHandler(this.numericUpDownWidth_ValueChanged);
            // 
            // numericUpDownHeight
            // 
            this.numericUpDownHeight.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownHeight.Location = new System.Drawing.Point(422, 318);
            this.numericUpDownHeight.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownHeight.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownHeight.Name = "numericUpDownHeight";
            this.numericUpDownHeight.Size = new System.Drawing.Size(80, 25);
            this.numericUpDownHeight.TabIndex = 104;
            this.numericUpDownHeight.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numericUpDownHeight.ValueChanged += new System.EventHandler(this.numericUpDownHeight_ValueChanged);
            // 
            // labelX
            // 
            this.labelX.AutoSize = true;
            this.labelX.Location = new System.Drawing.Point(419, 54);
            this.labelX.Name = "labelX";
            this.labelX.Size = new System.Drawing.Size(23, 15);
            this.labelX.TabIndex = 105;
            this.labelX.Text = "X:";
            // 
            // labelY
            // 
            this.labelY.AutoSize = true;
            this.labelY.Location = new System.Drawing.Point(419, 127);
            this.labelY.Name = "labelY";
            this.labelY.Size = new System.Drawing.Size(23, 15);
            this.labelY.TabIndex = 106;
            this.labelY.Text = "Y:";
            // 
            // labelWidth
            // 
            this.labelWidth.AutoSize = true;
            this.labelWidth.Location = new System.Drawing.Point(419, 221);
            this.labelWidth.Name = "labelWidth";
            this.labelWidth.Size = new System.Drawing.Size(55, 15);
            this.labelWidth.TabIndex = 107;
            this.labelWidth.Text = "Width:";
            // 
            // labelHeight
            // 
            this.labelHeight.AutoSize = true;
            this.labelHeight.Location = new System.Drawing.Point(419, 291);
            this.labelHeight.Name = "labelHeight";
            this.labelHeight.Size = new System.Drawing.Size(63, 15);
            this.labelHeight.TabIndex = 108;
            this.labelHeight.Text = "Height:";
            // 
            // pictureBoxOne
            // 
            this.pictureBoxOne.BackColor = System.Drawing.Color.LightGray;
            this.pictureBoxOne.Location = new System.Drawing.Point(74, 115);
            this.pictureBoxOne.Name = "pictureBoxOne";
            this.pictureBoxOne.Size = new System.Drawing.Size(226, 268);
            this.pictureBoxOne.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxOne.TabIndex = 0;
            this.pictureBoxOne.TabStop = false;
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(422, 707);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(80, 30);
            this.buttonStart.TabIndex = 109;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // timerScan
            // 
            this.timerScan.Interval = 200;
            this.timerScan.Tick += new System.EventHandler(this.timerScan_Tick);
            // 
            // timerCompare
            // 
            this.timerCompare.Tick += new System.EventHandler(this.timerCompare_Tick);
            // 
            // panelImage
            // 
            this.panelImage.AutoScroll = true;
            this.panelImage.Controls.Add(this.pictureBoxOne);
            this.panelImage.Location = new System.Drawing.Point(22, 51);
            this.panelImage.Name = "panelImage";
            this.panelImage.Size = new System.Drawing.Size(370, 481);
            this.panelImage.TabIndex = 113;
            // 
            // checkBoxAutoSlice
            // 
            this.checkBoxAutoSlice.AutoSize = true;
            this.checkBoxAutoSlice.Location = new System.Drawing.Point(22, 553);
            this.checkBoxAutoSlice.Name = "checkBoxAutoSlice";
            this.checkBoxAutoSlice.Size = new System.Drawing.Size(213, 19);
            this.checkBoxAutoSlice.TabIndex = 115;
            this.checkBoxAutoSlice.Text = "Auto slice in x seconds";
            this.checkBoxAutoSlice.UseVisualStyleBackColor = true;
            this.checkBoxAutoSlice.CheckedChanged += new System.EventHandler(this.checkBoxAux1_CheckedChanged);
            // 
            // numericUpDownAutoSliceIntervalSeconds
            // 
            this.numericUpDownAutoSliceIntervalSeconds.Enabled = false;
            this.numericUpDownAutoSliceIntervalSeconds.Location = new System.Drawing.Point(241, 550);
            this.numericUpDownAutoSliceIntervalSeconds.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.numericUpDownAutoSliceIntervalSeconds.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownAutoSliceIntervalSeconds.Name = "numericUpDownAutoSliceIntervalSeconds";
            this.numericUpDownAutoSliceIntervalSeconds.Size = new System.Drawing.Size(90, 25);
            this.numericUpDownAutoSliceIntervalSeconds.TabIndex = 118;
            this.numericUpDownAutoSliceIntervalSeconds.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.numericUpDownAutoSliceIntervalSeconds.ValueChanged += new System.EventHandler(this.numericUpDownAux1_ValueChanged);
            // 
            // checkBoxOneMode
            // 
            this.checkBoxOneMode.AutoSize = true;
            this.checkBoxOneMode.Location = new System.Drawing.Point(22, 599);
            this.checkBoxOneMode.Name = "checkBoxOneMode";
            this.checkBoxOneMode.Size = new System.Drawing.Size(93, 19);
            this.checkBoxOneMode.TabIndex = 119;
            this.checkBoxOneMode.Text = "One mode";
            this.checkBoxOneMode.UseVisualStyleBackColor = true;
            this.checkBoxOneMode.CheckedChanged += new System.EventHandler(this.checkBoxOneMode_CheckedChanged);
            // 
            // checkBox2SpotsCompare
            // 
            this.checkBox2SpotsCompare.AutoSize = true;
            this.checkBox2SpotsCompare.Checked = true;
            this.checkBox2SpotsCompare.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2SpotsCompare.Location = new System.Drawing.Point(22, 640);
            this.checkBox2SpotsCompare.Name = "checkBox2SpotsCompare";
            this.checkBox2SpotsCompare.Size = new System.Drawing.Size(149, 19);
            this.checkBox2SpotsCompare.TabIndex = 120;
            this.checkBox2SpotsCompare.Text = "2 spots compare";
            this.checkBox2SpotsCompare.UseVisualStyleBackColor = true;
            this.checkBox2SpotsCompare.CheckedChanged += new System.EventHandler(this.checkBox2SpotsCompare_CheckedChanged);
            // 
            // FormImageViewBossCounting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 759);
            this.Controls.Add(this.checkBox2SpotsCompare);
            this.Controls.Add(this.checkBoxOneMode);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.checkBoxAutoSlice);
            this.Controls.Add(this.numericUpDownAutoSliceIntervalSeconds);
            this.Controls.Add(this.panelImage);
            this.Controls.Add(this.labelHeight);
            this.Controls.Add(this.labelWidth);
            this.Controls.Add(this.labelY);
            this.Controls.Add(this.labelX);
            this.Controls.Add(this.numericUpDownHeight);
            this.Controls.Add(this.numericUpDownWidth);
            this.Controls.Add(this.numericUpDownY);
            this.Controls.Add(this.numericUpDownX);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.labelSize);
            this.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MaximizeBox = false;
            this.Name = "FormImageViewBossCounting";
            this.Text = "Boss Counting view";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormImageViewCounting_FormClosing);
            this.Load += new System.EventHandler(this.Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOne)).EndInit();
            this.panelImage.ResumeLayout(false);
            this.panelImage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAutoSliceIntervalSeconds)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxOne;
        private System.Windows.Forms.Label labelSize;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.NumericUpDown numericUpDownX;
        private System.Windows.Forms.NumericUpDown numericUpDownY;
        private System.Windows.Forms.NumericUpDown numericUpDownWidth;
        private System.Windows.Forms.NumericUpDown numericUpDownHeight;
        private System.Windows.Forms.Label labelX;
        private System.Windows.Forms.Label labelY;
        private System.Windows.Forms.Label labelWidth;
        private System.Windows.Forms.Label labelHeight;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Timer timerScan;
        private System.Windows.Forms.Timer timerCompare;
        private System.Windows.Forms.Panel panelImage;
        private System.Windows.Forms.CheckBox checkBoxAutoSlice;
        private System.Windows.Forms.NumericUpDown numericUpDownAutoSliceIntervalSeconds;
        private System.Windows.Forms.CheckBox checkBoxOneMode;
        private System.Windows.Forms.CheckBox checkBox2SpotsCompare;
    }
}