namespace SkyStopwatch
{
    partial class BoxMessage
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
            this.timerClose = new System.Windows.Forms.Timer(this.components);
            this.labelMessage = new System.Windows.Forms.Label();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.buttonConfirm = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.tableLayoutPanelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerClose
            // 
            this.timerClose.Tick += new System.EventHandler(this.timerClose_Tick);
            // 
            // labelMessage
            // 
            this.labelMessage.BackColor = System.Drawing.Color.CornflowerBlue;
            this.labelMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelMessage.Font = new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelMessage.ForeColor = System.Drawing.Color.White;
            this.labelMessage.Location = new System.Drawing.Point(3, 0);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(234, 30);
            this.labelMessage.TabIndex = 0;
            this.labelMessage.Text = "msg";
            this.labelMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 3;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanelMain.Controls.Add(this.buttonConfirm, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.labelMessage, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.buttonClose, 1, 0);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 1;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(360, 30);
            this.tableLayoutPanelMain.TabIndex = 1;
            // 
            // buttonConfirm
            // 
            this.buttonConfirm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonConfirm.Location = new System.Drawing.Point(243, 3);
            this.buttonConfirm.Name = "buttonConfirm";
            this.buttonConfirm.Size = new System.Drawing.Size(74, 24);
            this.buttonConfirm.TabIndex = 2;
            this.buttonConfirm.Text = "OK";
            this.buttonConfirm.UseVisualStyleBackColor = true;
            this.buttonConfirm.Click += new System.EventHandler(this.buttonConfirm_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonClose.Location = new System.Drawing.Point(323, 3);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(34, 24);
            this.buttonClose.TabIndex = 1;
            this.buttonClose.Text = "X";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // BoxMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 30);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BoxMessage";
            this.Text = "msg";
            this.TransparencyKey = System.Drawing.SystemColors.Control;
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerClose;
        private System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonConfirm;
    }
}