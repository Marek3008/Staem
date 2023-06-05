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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.labelLib = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelStore = new System.Windows.Forms.Label();
            this.labelNick = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.mainCena_label = new System.Windows.Forms.Label();
            this.peniazeTick = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            this.labelLib.Click += new System.EventHandler(this.labelLib_Click);
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
            this.labelStore.Location = new System.Drawing.Point(266, 12);
            this.labelStore.Name = "labelStore";
            this.labelStore.Size = new System.Drawing.Size(103, 25);
            this.labelStore.TabIndex = 2;
            this.labelStore.Text = "OBCHOD";
            this.labelStore.Click += new System.EventHandler(this.labelStore_Click);
            this.labelStore.MouseLeave += new System.EventHandler(this.labelPanel_unhover);
            this.labelStore.MouseHover += new System.EventHandler(this.labelPanel_hover);
            // 
            // labelNick
            // 
            this.labelNick.AutoSize = true;
            this.labelNick.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelNick.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelNick.ForeColor = System.Drawing.Color.White;
            this.labelNick.Location = new System.Drawing.Point(1625, 12);
            this.labelNick.Name = "labelNick";
            this.labelNick.Size = new System.Drawing.Size(72, 25);
            this.labelNick.TabIndex = 3;
            this.labelNick.Text = "Marek";
            this.labelNick.MouseLeave += new System.EventHandler(this.labelPanel_unhover);
            this.labelNick.MouseHover += new System.EventHandler(this.labelPanel_hover);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1818, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(40, 40);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // mainCena_label
            // 
            this.mainCena_label.AutoSize = true;
            this.mainCena_label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(62)))), ((int)(((byte)(85)))));
            this.mainCena_label.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mainCena_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.mainCena_label.ForeColor = System.Drawing.Color.Aqua;
            this.mainCena_label.Location = new System.Drawing.Point(1511, 12);
            this.mainCena_label.Name = "mainCena_label";
            this.mainCena_label.Size = new System.Drawing.Size(60, 24);
            this.mainCena_label.TabIndex = 5;
            this.mainCena_label.Text = "0,00 €";
            this.mainCena_label.Click += new System.EventHandler(this.mainCena_label_Click);
            // 
            // peniazeTick
            // 
            this.peniazeTick.Enabled = true;
            this.peniazeTick.Tick += new System.EventHandler(this.peniazeTick_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(40)))), ((int)(((byte)(57)))));
            this.ClientSize = new System.Drawing.Size(1904, 1058);
            this.Controls.Add(this.mainCena_label);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelNick);
            this.Controls.Add(this.labelStore);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelLib);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Staem";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelLib;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelStore;
        private System.Windows.Forms.Label labelNick;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label mainCena_label;
        private System.Windows.Forms.Timer peniazeTick;
    }
}