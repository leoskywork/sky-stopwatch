﻿namespace SkyStopwatch
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
            this.timerRefresh = new System.Windows.Forms.Timer(this.components);
            this.labelMessage = new System.Windows.Forms.Label();
            this.buttonKill = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.tableLayoutPanelRight = new System.Windows.Forms.TableLayoutPanel();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonPause = new System.Windows.Forms.Button();
            this.tableLayoutPanelRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerRefresh
            // 
            this.timerRefresh.Tick += new System.EventHandler(this.timerRefresh_Tick);
            // 
            // labelMessage
            // 
            this.labelMessage.BackColor = System.Drawing.SystemColors.Control;
            this.labelMessage.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelMessage.Font = new System.Drawing.Font("SimSun", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelMessage.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelMessage.Location = new System.Drawing.Point(0, 0);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(90, 30);
            this.labelMessage.TabIndex = 0;
            this.labelMessage.Text = "500";
            this.labelMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelMessage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelMessage_MouseDown);
            this.labelMessage.MouseHover += new System.EventHandler(this.labelMessage_MouseHover);
            // 
            // buttonKill
            // 
            this.buttonKill.Location = new System.Drawing.Point(63, 0);
            this.buttonKill.Name = "buttonKill";
            this.buttonKill.Size = new System.Drawing.Size(24, 24);
            this.buttonKill.TabIndex = 222;
            this.buttonKill.Text = "x";
            this.buttonKill.UseVisualStyleBackColor = true;
            this.buttonKill.Click += new System.EventHandler(this.buttonKill_Click);
            // 
            // buttonReset
            // 
            this.buttonReset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonReset.Location = new System.Drawing.Point(8, 5);
            this.buttonReset.Margin = new System.Windows.Forms.Padding(8, 3, 8, 3);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(57, 23);
            this.buttonReset.TabIndex = 3;
            this.buttonReset.Text = "= 0";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // tableLayoutPanelRight
            // 
            this.tableLayoutPanelRight.ColumnCount = 4;
            this.tableLayoutPanelRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelRight.Controls.Add(this.buttonRemove, 2, 1);
            this.tableLayoutPanelRight.Controls.Add(this.buttonAdd, 1, 1);
            this.tableLayoutPanelRight.Controls.Add(this.buttonReset, 0, 1);
            this.tableLayoutPanelRight.Controls.Add(this.buttonPause, 3, 1);
            this.tableLayoutPanelRight.Location = new System.Drawing.Point(93, 0);
            this.tableLayoutPanelRight.Name = "tableLayoutPanelRight";
            this.tableLayoutPanelRight.RowCount = 3;
            this.tableLayoutPanelRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 2F));
            this.tableLayoutPanelRight.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelRight.Size = new System.Drawing.Size(295, 50);
            this.tableLayoutPanelRight.TabIndex = 4;
            // 
            // buttonRemove
            // 
            this.buttonRemove.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonRemove.Location = new System.Drawing.Point(154, 5);
            this.buttonRemove.Margin = new System.Windows.Forms.Padding(8, 3, 8, 3);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(57, 23);
            this.buttonRemove.TabIndex = 5;
            this.buttonRemove.Text = "- 1";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonAdd.Location = new System.Drawing.Point(81, 5);
            this.buttonAdd.Margin = new System.Windows.Forms.Padding(8, 3, 8, 3);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(57, 23);
            this.buttonAdd.TabIndex = 4;
            this.buttonAdd.Text = "+ 1";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonPause
            // 
            this.buttonPause.Location = new System.Drawing.Point(222, 5);
            this.buttonPause.Name = "buttonPause";
            this.buttonPause.Size = new System.Drawing.Size(70, 23);
            this.buttonPause.TabIndex = 6;
            this.buttonPause.Text = "-=-";
            this.buttonPause.UseVisualStyleBackColor = true;
            this.buttonPause.Click += new System.EventHandler(this.buttonPause_Click);
            // 
            // BoxBossCountingSuccinct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(526, 30);
            this.Controls.Add(this.tableLayoutPanelRight);
            this.Controls.Add(this.buttonKill);
            this.Controls.Add(this.labelMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BoxBossCountingSuccinct";
            this.TransparencyKey = System.Drawing.SystemColors.ControlLight;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_FormClosing);
            this.Load += new System.EventHandler(this.Form_Load);
            this.tableLayoutPanelRight.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerRefresh;
        private System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.Button buttonKill;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelRight;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonPause;
    }
}