using System;
using System.Drawing;

namespace Staem
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.labelLib = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelStore = new System.Windows.Forms.Label();
            this.labelNick = new System.Windows.Forms.Label();
            this.labelpokus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelLib
            // 
            this.labelLib.AutoSize = true;
            this.labelLib.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelLib.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelLib.ForeColor = System.Drawing.Color.White;
            this.labelLib.Location = new System.Drawing.Point(25, 12);
            this.labelLib.Name = "labelLib";
            this.labelLib.Size = new System.Drawing.Size(105, 24);
            this.labelLib.TabIndex = 0;
            this.labelLib.Text = "KNIŽNICA";
            this.labelLib.MouseLeave += new System.EventHandler(this.labelPanel_unhover);
            this.labelLib.MouseHover += new System.EventHandler(this.labelPanel_hover);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.panel1.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.panel1.Location = new System.Drawing.Point(0, 46);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1920, 5);
            this.panel1.TabIndex = 1;
            // 
            // labelStore
            // 
            this.labelStore.AutoSize = true;
            this.labelStore.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelStore.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelStore.ForeColor = System.Drawing.Color.White;
            this.labelStore.Location = new System.Drawing.Point(177, 12);
            this.labelStore.Name = "labelStore";
            this.labelStore.Size = new System.Drawing.Size(103, 25);
            this.labelStore.TabIndex = 2;
            this.labelStore.Text = "OBCHOD";
            this.labelStore.MouseLeave += new System.EventHandler(this.labelPanel_unhover);
            this.labelStore.MouseHover += new System.EventHandler(this.labelPanel_hover);
            // 
            // labelNick
            // 
            this.labelNick.AutoSize = true;
            this.labelNick.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelNick.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelNick.ForeColor = System.Drawing.Color.White;
            this.labelNick.Location = new System.Drawing.Point(1730, 12);
            this.labelNick.Name = "labelNick";
            this.labelNick.Size = new System.Drawing.Size(72, 25);
            this.labelNick.TabIndex = 3;
            this.labelNick.Text = "Marek";
            this.labelNick.MouseLeave += new System.EventHandler(this.labelPanel_unhover);
            this.labelNick.MouseHover += new System.EventHandler(this.labelPanel_hover);
            // 
            // labelpokus
            // 
            this.labelpokus.AutoSize = true;
            this.labelpokus.Location = new System.Drawing.Point(13, 96);
            this.labelpokus.Name = "labelpokus";
            this.labelpokus.Size = new System.Drawing.Size(35, 13);
            this.labelpokus.TabIndex = 4;
            this.labelpokus.Text = "label1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(40)))), ((int)(((byte)(57)))));
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.labelpokus);
            this.Controls.Add(this.labelNick);
            this.Controls.Add(this.labelStore);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelLib);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Staem";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelLib;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelStore;
        private System.Windows.Forms.Label labelNick;
        private System.Windows.Forms.Label labelpokus;
    }
}