﻿namespace SkyStopwatch
{
    partial class FormImageViewPrice
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
            this.buttonStop = new System.Windows.Forms.Button();
            this.textBoxPriceList = new System.Windows.Forms.TextBox();
            this.labelPriceMessage = new System.Windows.Forms.Label();
            this.timerScan = new System.Windows.Forms.Timer(this.components);
            this.timerCompare = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOne)).BeginInit();
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
            this.buttonSave.Location = new System.Drawing.Point(492, 527);
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
            this.numericUpDownX.Location = new System.Drawing.Point(492, 113);
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
            this.numericUpDownY.Location = new System.Drawing.Point(492, 205);
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
            this.numericUpDownWidth.Location = new System.Drawing.Point(492, 358);
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
            this.numericUpDownHeight.Location = new System.Drawing.Point(492, 442);
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
            this.labelX.Location = new System.Drawing.Point(489, 76);
            this.labelX.Name = "labelX";
            this.labelX.Size = new System.Drawing.Size(23, 15);
            this.labelX.TabIndex = 105;
            this.labelX.Text = "X:";
            // 
            // labelY
            // 
            this.labelY.AutoSize = true;
            this.labelY.Location = new System.Drawing.Point(489, 168);
            this.labelY.Name = "labelY";
            this.labelY.Size = new System.Drawing.Size(23, 15);
            this.labelY.TabIndex = 106;
            this.labelY.Text = "Y:";
            // 
            // labelWidth
            // 
            this.labelWidth.AutoSize = true;
            this.labelWidth.Location = new System.Drawing.Point(489, 321);
            this.labelWidth.Name = "labelWidth";
            this.labelWidth.Size = new System.Drawing.Size(55, 15);
            this.labelWidth.TabIndex = 107;
            this.labelWidth.Text = "Width:";
            // 
            // labelHeight
            // 
            this.labelHeight.AutoSize = true;
            this.labelHeight.Location = new System.Drawing.Point(489, 405);
            this.labelHeight.Name = "labelHeight";
            this.labelHeight.Size = new System.Drawing.Size(63, 15);
            this.labelHeight.TabIndex = 108;
            this.labelHeight.Text = "Height:";
            // 
            // pictureBoxOne
            // 
            this.pictureBoxOne.BackColor = System.Drawing.Color.LightGray;
            this.pictureBoxOne.Location = new System.Drawing.Point(22, 79);
            this.pictureBoxOne.Name = "pictureBoxOne";
            this.pictureBoxOne.Size = new System.Drawing.Size(454, 481);
            this.pictureBoxOne.TabIndex = 0;
            this.pictureBoxOne.TabStop = false;
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(22, 599);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(80, 30);
            this.buttonStart.TabIndex = 109;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(131, 599);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(80, 30);
            this.buttonStop.TabIndex = 110;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // textBoxPriceList
            // 
            this.textBoxPriceList.Location = new System.Drawing.Point(34, 642);
            this.textBoxPriceList.Multiline = true;
            this.textBoxPriceList.Name = "textBoxPriceList";
            this.textBoxPriceList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxPriceList.Size = new System.Drawing.Size(530, 281);
            this.textBoxPriceList.TabIndex = 111;
            // 
            // labelPriceMessage
            // 
            this.labelPriceMessage.AutoSize = true;
            this.labelPriceMessage.Location = new System.Drawing.Point(249, 607);
            this.labelPriceMessage.Name = "labelPriceMessage";
            this.labelPriceMessage.Size = new System.Drawing.Size(47, 15);
            this.labelPriceMessage.TabIndex = 112;
            this.labelPriceMessage.Text = "ready";
            // 
            // timerScan
            // 
            this.timerScan.Tick += new System.EventHandler(this.timerScan_Tick);
            // 
            // timerCompare
            // 
            this.timerCompare.Tick += new System.EventHandler(this.timerCompare_Tick);
            // 
            // FormImageViewPrice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 935);
            this.Controls.Add(this.labelPriceMessage);
            this.Controls.Add(this.textBoxPriceList);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonStart);
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
            this.MaximizeBox = false;
            this.Name = "FormImageViewPrice";
            this.Text = "Image view";
            this.Load += new System.EventHandler(this.FormImageViewPrice_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOne)).EndInit();
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
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.TextBox textBoxPriceList;
        private System.Windows.Forms.Label labelPriceMessage;
        private System.Windows.Forms.Timer timerScan;
        private System.Windows.Forms.Timer timerCompare;
    }
}