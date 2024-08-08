namespace SkyStopwatch
{
    partial class BoxBossCountingSuccinct
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
            this.buttonKill = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.tableLayoutPanelRight = new System.Windows.Forms.TableLayoutPanel();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.tableLayoutPanelRight.SuspendLayout();
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
            this.labelMessage.MouseHover += new System.EventHandler(this.labelMessage_MouseHover);
            // 
            // buttonKill
            // 
            this.buttonKill.Location = new System.Drawing.Point(109, 12);
            this.buttonKill.Name = "buttonKill";
            this.buttonKill.Size = new System.Drawing.Size(75, 23);
            this.buttonKill.TabIndex = 2;
            this.buttonKill.Text = "Kill";
            this.buttonKill.UseVisualStyleBackColor = true;
            this.buttonKill.Click += new System.EventHandler(this.buttonKill_Click);
            // 
            // buttonReset
            // 
            this.buttonReset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonReset.Location = new System.Drawing.Point(12, 13);
            this.buttonReset.Margin = new System.Windows.Forms.Padding(12, 3, 12, 3);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(65, 23);
            this.buttonReset.TabIndex = 3;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // tableLayoutPanelRight
            // 
            this.tableLayoutPanelRight.ColumnCount = 3;
            this.tableLayoutPanelRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanelRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanelRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tableLayoutPanelRight.Controls.Add(this.buttonRemove, 2, 1);
            this.tableLayoutPanelRight.Controls.Add(this.buttonAdd, 1, 1);
            this.tableLayoutPanelRight.Controls.Add(this.buttonReset, 0, 1);
            this.tableLayoutPanelRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.tableLayoutPanelRight.Location = new System.Drawing.Point(201, 0);
            this.tableLayoutPanelRight.Name = "tableLayoutPanelRight";
            this.tableLayoutPanelRight.RowCount = 3;
            this.tableLayoutPanelRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanelRight.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelRight.Size = new System.Drawing.Size(270, 50);
            this.tableLayoutPanelRight.TabIndex = 4;
            // 
            // buttonRemove
            // 
            this.buttonRemove.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonRemove.Location = new System.Drawing.Point(190, 13);
            this.buttonRemove.Margin = new System.Windows.Forms.Padding(12, 3, 12, 3);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(68, 23);
            this.buttonRemove.TabIndex = 5;
            this.buttonRemove.Text = "- 1";
            this.buttonRemove.UseVisualStyleBackColor = true;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonAdd.Location = new System.Drawing.Point(101, 13);
            this.buttonAdd.Margin = new System.Windows.Forms.Padding(12, 3, 12, 3);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(65, 23);
            this.buttonAdd.TabIndex = 4;
            this.buttonAdd.Text = "+ 1";
            this.buttonAdd.UseVisualStyleBackColor = true;
            // 
            // BoxBossCountingSuccinct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 50);
            this.Controls.Add(this.tableLayoutPanelRight);
            this.Controls.Add(this.buttonKill);
            this.Controls.Add(this.labelMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BoxBossCountingSuccinct";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormNodeWarning_FormClosing);
            this.Load += new System.EventHandler(this.FormNodeBossCounting_Load);
            this.tableLayoutPanelRight.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerClose;
        private System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.Button buttonKill;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelRight;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonAdd;
    }
}