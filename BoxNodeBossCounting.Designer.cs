namespace SkyStopwatch
{
    partial class BoxNodeBossCounting
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
            this.tableLayoutPanelRight = new System.Windows.Forms.TableLayoutPanel();
            this.labelTotal = new System.Windows.Forms.Label();
            this.labelKill = new System.Windows.Forms.Label();
            this.tableLayoutPanelRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerClose
            // 
            this.timerClose.Tick += new System.EventHandler(this.timerClose_Tick);
            // 
            // labelMessage
            // 
            this.labelMessage.BackColor = System.Drawing.Color.Tomato;
            this.labelMessage.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelMessage.Font = new System.Drawing.Font("SimSun", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelMessage.ForeColor = System.Drawing.Color.White;
            this.labelMessage.Location = new System.Drawing.Point(0, 0);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(110, 50);
            this.labelMessage.TabIndex = 0;
            this.labelMessage.Text = "500";
            this.labelMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelMessage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelMessage_MouseDown);
            // 
            // tableLayoutPanelRight
            // 
            this.tableLayoutPanelRight.ColumnCount = 1;
            this.tableLayoutPanelRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelRight.Controls.Add(this.labelTotal, 0, 0);
            this.tableLayoutPanelRight.Controls.Add(this.labelKill, 0, 1);
            this.tableLayoutPanelRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.tableLayoutPanelRight.Location = new System.Drawing.Point(104, 0);
            this.tableLayoutPanelRight.Name = "tableLayoutPanelRight";
            this.tableLayoutPanelRight.RowCount = 2;
            this.tableLayoutPanelRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelRight.Size = new System.Drawing.Size(66, 50);
            this.tableLayoutPanelRight.TabIndex = 1;
            this.tableLayoutPanelRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tableLayoutPanelRight_MouseDown);
            // 
            // labelTotal
            // 
            this.labelTotal.AutoSize = true;
            this.labelTotal.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelTotal.Location = new System.Drawing.Point(8, 0);
            this.labelTotal.Name = "labelTotal";
            this.labelTotal.Size = new System.Drawing.Size(55, 25);
            this.labelTotal.TabIndex = 0;
            this.labelTotal.Text = "P5-599";
            this.labelTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelTotal.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelTotal_MouseDown);
            // 
            // labelKill
            // 
            this.labelKill.AutoSize = true;
            this.labelKill.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelKill.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelKill.ForeColor = System.Drawing.Color.Tomato;
            this.labelKill.Location = new System.Drawing.Point(20, 25);
            this.labelKill.Name = "labelKill";
            this.labelKill.Size = new System.Drawing.Size(43, 25);
            this.labelKill.TabIndex = 1;
            this.labelKill.Text = "KILL";
            this.labelKill.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelKill.Click += new System.EventHandler(this.labelKill_Click);
            // 
            // BoxNodeBossCounting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(170, 50);
            this.Controls.Add(this.tableLayoutPanelRight);
            this.Controls.Add(this.labelMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BoxNodeBossCounting";
            this.Text = "Node Warning";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormNodeWarning_FormClosing);
            this.Load += new System.EventHandler(this.FormNodeBossCounting_Load);
            this.tableLayoutPanelRight.ResumeLayout(false);
            this.tableLayoutPanelRight.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerClose;
        private System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelRight;
        private System.Windows.Forms.Label labelTotal;
        private System.Windows.Forms.Label labelKill;
    }
}