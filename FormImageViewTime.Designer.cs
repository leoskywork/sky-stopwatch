namespace SkyStopwatch
{
    partial class FormImageViewTime
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
            this.labelSize = new System.Windows.Forms.Label();
            this.labelMessage = new System.Windows.Forms.Label();
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
            this.groupBoxPresetLocation = new System.Windows.Forms.GroupBox();
            this.labelTip = new System.Windows.Forms.Label();
            this.buttonApplyPreset2 = new System.Windows.Forms.Button();
            this.buttonApplyPreset1 = new System.Windows.Forms.Button();
            this.labelPreset2 = new System.Windows.Forms.Label();
            this.labelPreset1 = new System.Windows.Forms.Label();
            this.labelPreset2Title = new System.Windows.Forms.Label();
            this.labelPreset1Title = new System.Windows.Forms.Label();
            this.buttonSetAsPreset1 = new System.Windows.Forms.Button();
            this.buttonSetAsPreset2 = new System.Windows.Forms.Button();
            this.groupBoxSetting = new System.Windows.Forms.GroupBox();
            this.checkBoxReadTopTime = new System.Windows.Forms.CheckBox();
            this.buttonSaveSetting = new System.Windows.Forms.Button();
            this.numericUpDownDelaySecond = new System.Windows.Forms.NumericUpDown();
            this.labelScanMiddleDelay = new System.Windows.Forms.Label();
            this.checkBoxAutoLock = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOne)).BeginInit();
            this.groupBoxPresetLocation.SuspendLayout();
            this.groupBoxSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDelaySecond)).BeginInit();
            this.SuspendLayout();
            // 
            // labelSize
            // 
            this.labelSize.AutoSize = true;
            this.labelSize.Location = new System.Drawing.Point(31, 34);
            this.labelSize.Name = "labelSize";
            this.labelSize.Size = new System.Drawing.Size(79, 15);
            this.labelSize.TabIndex = 1;
            this.labelSize.Text = "600 x 400";
            // 
            // labelMessage
            // 
            this.labelMessage.AutoSize = true;
            this.labelMessage.Location = new System.Drawing.Point(249, 34);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(167, 15);
            this.labelMessage.TabIndex = 2;
            this.labelMessage.Text = "set image blcok args";
            // 
            // buttonSave
            // 
            this.buttonSave.Enabled = false;
            this.buttonSave.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonSave.Location = new System.Drawing.Point(625, 694);
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
            this.numericUpDownX.Location = new System.Drawing.Point(28, 697);
            this.numericUpDownX.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownX.Name = "numericUpDownX";
            this.numericUpDownX.Size = new System.Drawing.Size(120, 25);
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
            this.numericUpDownY.Location = new System.Drawing.Point(181, 697);
            this.numericUpDownY.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownY.Name = "numericUpDownY";
            this.numericUpDownY.Size = new System.Drawing.Size(120, 25);
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
            this.numericUpDownWidth.Location = new System.Drawing.Point(333, 697);
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
            this.numericUpDownWidth.Size = new System.Drawing.Size(120, 25);
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
            this.numericUpDownHeight.Location = new System.Drawing.Point(484, 697);
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
            this.numericUpDownHeight.Size = new System.Drawing.Size(120, 25);
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
            this.labelX.Location = new System.Drawing.Point(25, 660);
            this.labelX.Name = "labelX";
            this.labelX.Size = new System.Drawing.Size(23, 15);
            this.labelX.TabIndex = 105;
            this.labelX.Text = "X:";
            // 
            // labelY
            // 
            this.labelY.AutoSize = true;
            this.labelY.Location = new System.Drawing.Point(178, 660);
            this.labelY.Name = "labelY";
            this.labelY.Size = new System.Drawing.Size(23, 15);
            this.labelY.TabIndex = 106;
            this.labelY.Text = "Y:";
            // 
            // labelWidth
            // 
            this.labelWidth.AutoSize = true;
            this.labelWidth.Location = new System.Drawing.Point(330, 660);
            this.labelWidth.Name = "labelWidth";
            this.labelWidth.Size = new System.Drawing.Size(55, 15);
            this.labelWidth.TabIndex = 107;
            this.labelWidth.Text = "Width:";
            // 
            // labelHeight
            // 
            this.labelHeight.AutoSize = true;
            this.labelHeight.Location = new System.Drawing.Point(481, 660);
            this.labelHeight.Name = "labelHeight";
            this.labelHeight.Size = new System.Drawing.Size(63, 15);
            this.labelHeight.TabIndex = 108;
            this.labelHeight.Text = "Height:";
            // 
            // pictureBoxOne
            // 
            this.pictureBoxOne.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxOne.BackColor = System.Drawing.Color.LightGray;
            this.pictureBoxOne.Location = new System.Drawing.Point(31, 75);
            this.pictureBoxOne.Name = "pictureBoxOne";
            this.pictureBoxOne.Size = new System.Drawing.Size(573, 549);
            this.pictureBoxOne.TabIndex = 0;
            this.pictureBoxOne.TabStop = false;
            // 
            // groupBoxPresetLocation
            // 
            this.groupBoxPresetLocation.Controls.Add(this.labelTip);
            this.groupBoxPresetLocation.Controls.Add(this.buttonApplyPreset2);
            this.groupBoxPresetLocation.Controls.Add(this.buttonApplyPreset1);
            this.groupBoxPresetLocation.Controls.Add(this.labelPreset2);
            this.groupBoxPresetLocation.Controls.Add(this.labelPreset1);
            this.groupBoxPresetLocation.Controls.Add(this.labelPreset2Title);
            this.groupBoxPresetLocation.Controls.Add(this.labelPreset1Title);
            this.groupBoxPresetLocation.Location = new System.Drawing.Point(632, 67);
            this.groupBoxPresetLocation.Name = "groupBoxPresetLocation";
            this.groupBoxPresetLocation.Size = new System.Drawing.Size(345, 275);
            this.groupBoxPresetLocation.TabIndex = 109;
            this.groupBoxPresetLocation.TabStop = false;
            this.groupBoxPresetLocation.Text = "Location Presets";
            // 
            // labelTip
            // 
            this.labelTip.AutoSize = true;
            this.labelTip.ForeColor = System.Drawing.Color.Red;
            this.labelTip.Location = new System.Drawing.Point(16, 233);
            this.labelTip.Name = "labelTip";
            this.labelTip.Size = new System.Drawing.Size(319, 15);
            this.labelTip.TabIndex = 113;
            this.labelTip.Text = "Click *Save* button after apply preest.";
            // 
            // buttonApplyPreset2
            // 
            this.buttonApplyPreset2.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonApplyPreset2.Location = new System.Drawing.Point(219, 176);
            this.buttonApplyPreset2.Name = "buttonApplyPreset2";
            this.buttonApplyPreset2.Size = new System.Drawing.Size(103, 30);
            this.buttonApplyPreset2.TabIndex = 112;
            this.buttonApplyPreset2.Text = "Apply";
            this.buttonApplyPreset2.UseVisualStyleBackColor = true;
            this.buttonApplyPreset2.Click += new System.EventHandler(this.buttonApplyPreset2_Click);
            // 
            // buttonApplyPreset1
            // 
            this.buttonApplyPreset1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonApplyPreset1.Location = new System.Drawing.Point(219, 82);
            this.buttonApplyPreset1.Name = "buttonApplyPreset1";
            this.buttonApplyPreset1.Size = new System.Drawing.Size(103, 30);
            this.buttonApplyPreset1.TabIndex = 111;
            this.buttonApplyPreset1.Text = "Apply";
            this.buttonApplyPreset1.UseVisualStyleBackColor = true;
            this.buttonApplyPreset1.Click += new System.EventHandler(this.buttonApplyPreset1_Click);
            // 
            // labelPreset2
            // 
            this.labelPreset2.AutoSize = true;
            this.labelPreset2.Location = new System.Drawing.Point(16, 184);
            this.labelPreset2.Name = "labelPreset2";
            this.labelPreset2.Size = new System.Drawing.Size(111, 15);
            this.labelPreset2.TabIndex = 4;
            this.labelPreset2.Text = "20,20,100,100";
            // 
            // labelPreset1
            // 
            this.labelPreset1.AutoSize = true;
            this.labelPreset1.Location = new System.Drawing.Point(16, 90);
            this.labelPreset1.Name = "labelPreset1";
            this.labelPreset1.Size = new System.Drawing.Size(111, 15);
            this.labelPreset1.TabIndex = 3;
            this.labelPreset1.Text = "10,10,100,100";
            // 
            // labelPreset2Title
            // 
            this.labelPreset2Title.AutoSize = true;
            this.labelPreset2Title.Location = new System.Drawing.Point(16, 137);
            this.labelPreset2Title.Name = "labelPreset2Title";
            this.labelPreset2Title.Size = new System.Drawing.Size(71, 15);
            this.labelPreset2Title.TabIndex = 1;
            this.labelPreset2Title.Text = "Preset 2";
            // 
            // labelPreset1Title
            // 
            this.labelPreset1Title.AutoSize = true;
            this.labelPreset1Title.Location = new System.Drawing.Point(16, 43);
            this.labelPreset1Title.Name = "labelPreset1Title";
            this.labelPreset1Title.Size = new System.Drawing.Size(71, 15);
            this.labelPreset1Title.TabIndex = 0;
            this.labelPreset1Title.Text = "Preset 1";
            // 
            // buttonSetAsPreset1
            // 
            this.buttonSetAsPreset1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonSetAsPreset1.Location = new System.Drawing.Point(714, 694);
            this.buttonSetAsPreset1.Name = "buttonSetAsPreset1";
            this.buttonSetAsPreset1.Size = new System.Drawing.Size(126, 30);
            this.buttonSetAsPreset1.TabIndex = 110;
            this.buttonSetAsPreset1.Text = "As Preset 1";
            this.buttonSetAsPreset1.UseVisualStyleBackColor = true;
            this.buttonSetAsPreset1.Click += new System.EventHandler(this.buttonSetAsPreset1_Click);
            // 
            // buttonSetAsPreset2
            // 
            this.buttonSetAsPreset2.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonSetAsPreset2.Location = new System.Drawing.Point(849, 694);
            this.buttonSetAsPreset2.Name = "buttonSetAsPreset2";
            this.buttonSetAsPreset2.Size = new System.Drawing.Size(126, 30);
            this.buttonSetAsPreset2.TabIndex = 111;
            this.buttonSetAsPreset2.Text = "As Preset 2";
            this.buttonSetAsPreset2.UseVisualStyleBackColor = true;
            this.buttonSetAsPreset2.Click += new System.EventHandler(this.buttonSetAsPreset2_Click);
            // 
            // groupBoxSetting
            // 
            this.groupBoxSetting.Controls.Add(this.checkBoxAutoLock);
            this.groupBoxSetting.Controls.Add(this.checkBoxReadTopTime);
            this.groupBoxSetting.Controls.Add(this.buttonSaveSetting);
            this.groupBoxSetting.Controls.Add(this.numericUpDownDelaySecond);
            this.groupBoxSetting.Controls.Add(this.labelScanMiddleDelay);
            this.groupBoxSetting.Location = new System.Drawing.Point(632, 359);
            this.groupBoxSetting.Name = "groupBoxSetting";
            this.groupBoxSetting.Size = new System.Drawing.Size(345, 265);
            this.groupBoxSetting.TabIndex = 112;
            this.groupBoxSetting.TabStop = false;
            this.groupBoxSetting.Text = "Settings";
            // 
            // checkBoxReadTopTime
            // 
            this.checkBoxReadTopTime.AutoSize = true;
            this.checkBoxReadTopTime.Checked = true;
            this.checkBoxReadTopTime.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxReadTopTime.Location = new System.Drawing.Point(19, 84);
            this.checkBoxReadTopTime.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxReadTopTime.Name = "checkBoxReadTopTime";
            this.checkBoxReadTopTime.Size = new System.Drawing.Size(133, 19);
            this.checkBoxReadTopTime.TabIndex = 410;
            this.checkBoxReadTopTime.Text = "Scan top time";
            this.checkBoxReadTopTime.UseVisualStyleBackColor = true;
            this.checkBoxReadTopTime.CheckedChanged += new System.EventHandler(this.checkBoxReadTopTime_CheckedChanged);
            // 
            // buttonSaveSetting
            // 
            this.buttonSaveSetting.Enabled = false;
            this.buttonSaveSetting.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonSaveSetting.Location = new System.Drawing.Point(242, 218);
            this.buttonSaveSetting.Name = "buttonSaveSetting";
            this.buttonSaveSetting.Size = new System.Drawing.Size(80, 30);
            this.buttonSaveSetting.TabIndex = 104;
            this.buttonSaveSetting.Text = "Save";
            this.buttonSaveSetting.UseVisualStyleBackColor = true;
            this.buttonSaveSetting.Click += new System.EventHandler(this.buttonSaveSetting_Click);
            // 
            // numericUpDownDelaySecond
            // 
            this.numericUpDownDelaySecond.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownDelaySecond.Location = new System.Drawing.Point(241, 38);
            this.numericUpDownDelaySecond.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownDelaySecond.Name = "numericUpDownDelaySecond";
            this.numericUpDownDelaySecond.Size = new System.Drawing.Size(81, 25);
            this.numericUpDownDelaySecond.TabIndex = 103;
            this.numericUpDownDelaySecond.ValueChanged += new System.EventHandler(this.numericUpDownDelaySecond_ValueChanged);
            // 
            // labelScanMiddleDelay
            // 
            this.labelScanMiddleDelay.AutoSize = true;
            this.labelScanMiddleDelay.Location = new System.Drawing.Point(16, 43);
            this.labelScanMiddleDelay.Name = "labelScanMiddleDelay";
            this.labelScanMiddleDelay.Size = new System.Drawing.Size(215, 15);
            this.labelScanMiddleDelay.TabIndex = 3;
            this.labelScanMiddleDelay.Text = "Scan middle delay seconds:";
            // 
            // checkBoxAutoLock
            // 
            this.checkBoxAutoLock.AutoSize = true;
            this.checkBoxAutoLock.Location = new System.Drawing.Point(19, 122);
            this.checkBoxAutoLock.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxAutoLock.Name = "checkBoxAutoLock";
            this.checkBoxAutoLock.Size = new System.Drawing.Size(173, 19);
            this.checkBoxAutoLock.TabIndex = 411;
            this.checkBoxAutoLock.Text = "Top time auto lock";
            this.checkBoxAutoLock.UseVisualStyleBackColor = true;
            this.checkBoxAutoLock.CheckedChanged += new System.EventHandler(this.checkBoxAutoLock_CheckedChanged);
            // 
            // FormImageViewTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1011, 753);
            this.Controls.Add(this.groupBoxSetting);
            this.Controls.Add(this.buttonSetAsPreset2);
            this.Controls.Add(this.buttonSetAsPreset1);
            this.Controls.Add(this.groupBoxPresetLocation);
            this.Controls.Add(this.labelHeight);
            this.Controls.Add(this.labelWidth);
            this.Controls.Add(this.labelY);
            this.Controls.Add(this.labelX);
            this.Controls.Add(this.numericUpDownHeight);
            this.Controls.Add(this.numericUpDownWidth);
            this.Controls.Add(this.numericUpDownY);
            this.Controls.Add(this.numericUpDownX);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.labelMessage);
            this.Controls.Add(this.labelSize);
            this.Controls.Add(this.pictureBoxOne);
            this.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.MaximizeBox = false;
            this.Name = "FormImageViewTime";
            this.Text = "Image view";
            this.Load += new System.EventHandler(this.FormImageView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOne)).EndInit();
            this.groupBoxPresetLocation.ResumeLayout(false);
            this.groupBoxPresetLocation.PerformLayout();
            this.groupBoxSetting.ResumeLayout(false);
            this.groupBoxSetting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDelaySecond)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxOne;
        private System.Windows.Forms.Label labelSize;
        private System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.NumericUpDown numericUpDownX;
        private System.Windows.Forms.NumericUpDown numericUpDownY;
        private System.Windows.Forms.NumericUpDown numericUpDownWidth;
        private System.Windows.Forms.NumericUpDown numericUpDownHeight;
        private System.Windows.Forms.Label labelX;
        private System.Windows.Forms.Label labelY;
        private System.Windows.Forms.Label labelWidth;
        private System.Windows.Forms.Label labelHeight;
        private System.Windows.Forms.GroupBox groupBoxPresetLocation;
        private System.Windows.Forms.Button buttonSetAsPreset1;
        private System.Windows.Forms.Label labelPreset1Title;
        private System.Windows.Forms.Label labelPreset2;
        private System.Windows.Forms.Label labelPreset1;
        private System.Windows.Forms.Label labelPreset2Title;
        private System.Windows.Forms.Button buttonApplyPreset2;
        private System.Windows.Forms.Button buttonApplyPreset1;
        private System.Windows.Forms.Button buttonSetAsPreset2;
        private System.Windows.Forms.Label labelTip;
        private System.Windows.Forms.GroupBox groupBoxSetting;
        private System.Windows.Forms.Label labelScanMiddleDelay;
        private System.Windows.Forms.Button buttonSaveSetting;
        private System.Windows.Forms.NumericUpDown numericUpDownDelaySecond;
        private System.Windows.Forms.CheckBox checkBoxReadTopTime;
        private System.Windows.Forms.CheckBox checkBoxAutoLock;
    }
}