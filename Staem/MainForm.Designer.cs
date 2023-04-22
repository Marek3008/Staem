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
            this.labelLib = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelStore = new System.Windows.Forms.Label();
            this.labelNick = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelLib
            // 
            this.labelLib.AutoSize = true;
            this.labelLib.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelLib.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelLib.ForeColor = System.Drawing.Color.MidnightBlue;
            this.labelLib.Location = new System.Drawing.Point(12, 18);
            this.labelLib.Name = "labelLib";
            this.labelLib.Size = new System.Drawing.Size(94, 25);
            this.labelLib.TabIndex = 0;
            this.labelLib.Text = "Knižnica";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.MidnightBlue;
            this.panel1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.panel1.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.panel1.Location = new System.Drawing.Point(-19, 46);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(872, 1);
            this.panel1.TabIndex = 1;
            // 
            // labelStore
            // 
            this.labelStore.AutoSize = true;
            this.labelStore.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelStore.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelStore.ForeColor = System.Drawing.Color.MidnightBlue;
            this.labelStore.Location = new System.Drawing.Point(112, 18);
            this.labelStore.Name = "labelStore";
            this.labelStore.Size = new System.Drawing.Size(87, 25);
            this.labelStore.TabIndex = 2;
            this.labelStore.Text = "Obchod";
            // 
            // labelNick
            // 
            this.labelNick.AutoSize = true;
            this.labelNick.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelNick.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelNick.ForeColor = System.Drawing.Color.MidnightBlue;
            this.labelNick.Location = new System.Drawing.Point(690, 18);
            this.labelNick.Name = "labelNick";
            this.labelNick.Size = new System.Drawing.Size(0, 25);
            this.labelNick.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.RoyalBlue;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelNick);
            this.Controls.Add(this.labelStore);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelLib);
            this.Name = "MainForm";
            this.Text = "Staem";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelLib;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelStore;
        private System.Windows.Forms.Label labelNick;
    }
}