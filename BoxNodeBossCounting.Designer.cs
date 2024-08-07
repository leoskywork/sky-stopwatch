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
            this.panelButtons = new System.Windows.Forms.Panel();
            this.labelKill = new System.Windows.Forms.Label();
            this.labelAddGroup = new System.Windows.Forms.Label();
            this.tableLayoutPanelRight.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerClose
            // 
            this.timerClose.Tick += new System.EventHandler(this.timerClose_Tick);
            // 
            // labelMessage
            // 
            this.labelMessage.BackColor = System.Drawing.SystemColors.Control;
            this.labelMessage.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelMessage.Font = new System.Drawing.Font("SimSun", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelMessage.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelMessage.Location = new System.Drawing.Point(0, 0);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(90, 50);
            this.labelMessage.TabIndex = 0;
            this.labelMessage.Text = "500";
            this.labelMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelMessage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelMessage_MouseDown);
            // 
            // tableLayoutPanelRight
            // 
            this.tableLayoutPanelRight.ColumnCount = 1;
            this.tableLayoutPanelRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelRight.Controls.Add(this.labelTotal, 0, 0);
            this.tableLayoutPanelRight.Controls.Add(this.panelButtons, 0, 1);
            this.tableLayoutPanelRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.tableLayoutPanelRight.Location = new System.Drawing.Point(88, 0);
            this.tableLayoutPanelRight.Name = "tableLayoutPanelRight";
            this.tableLayoutPanelRight.RowCount = 2;
            this.tableLayoutPanelRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelRight.Size = new System.Drawing.Size(80, 50);
            this.tableLayoutPanelRight.TabIndex = 1;
            this.tableLayoutPanelRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tableLayoutPanelRight_MouseDown);
            // 
            // labelTotal
            // 
            this.labelTotal.AutoSize = true;
            this.labelTotal.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelTotal.Location = new System.Drawing.Point(22, 0);
            this.labelTotal.Name = "labelTotal";
            this.labelTotal.Size = new System.Drawing.Size(55, 25);
            this.labelTotal.TabIndex = 0;
            this.labelTotal.Text = "P5-599";
            this.labelTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelTotal.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelTotal_MouseDown);
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.labelAddGroup);
            this.panelButtons.Controls.Add(this.labelKill);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelButtons.Location = new System.Drawing.Point(3, 28);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(74, 19);
            this.panelButtons.TabIndex = 1;
            // 
            // labelKill
            // 
            this.labelKill.AutoSize = true;
            this.labelKill.BackColor = System.Drawing.Color.Tomato;
            this.labelKill.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelKill.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelKill.ForeColor = System.Drawing.Color.Transparent;
            this.labelKill.Location = new System.Drawing.Point(31, 0);
            this.labelKill.Name = "labelKill";
            this.labelKill.Size = new System.Drawing.Size(43, 15);
            this.labelKill.TabIndex = 0;
            this.labelKill.Text = "Kill";
            this.labelKill.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelKill.Click += new System.EventHandler(this.labelKill_Click);
            // 
            // labelAddGroup
            // 
            this.labelAddGroup.AutoSize = true;
            this.labelAddGroup.BackColor = System.Drawing.Color.RoyalBlue;
            this.labelAddGroup.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelAddGroup.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelAddGroup.ForeColor = System.Drawing.Color.Transparent;
            this.labelAddGroup.Location = new System.Drawing.Point(0, 0);
            this.labelAddGroup.Margin = new System.Windows.Forms.Padding(3);
            this.labelAddGroup.Name = "labelAddGroup";
            this.labelAddGroup.Size = new System.Drawing.Size(16, 15);
            this.labelAddGroup.TabIndex = 1;
            this.labelAddGroup.Text = "+";
            this.labelAddGroup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelAddGroup.Click += new System.EventHandler(this.labelAddGroup_Click);
            // 
            // BoxNodeBossCounting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(168, 50);
            this.Controls.Add(this.tableLayoutPanelRight);
            this.Controls.Add(this.labelMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BoxNodeBossCounting";
            this.Text = "Node Warning";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormNodeWarning_FormClosing);
            this.Load += new System.EventHandler(this.FormNodeBossCounting_Load);
            this.tableLayoutPanelRight.ResumeLayout(false);
            this.tableLayoutPanelRight.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.panelButtons.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerClose;
        private System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelRight;
        private System.Windows.Forms.Label labelTotal;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Label labelKill;
        private System.Windows.Forms.Label labelAddGroup;
    }
}