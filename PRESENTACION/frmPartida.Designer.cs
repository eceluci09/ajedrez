﻿namespace PRESENTACION
{
    partial class frmPartida
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
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cU_TURNO1 = new PRESENTACION.CU_TURNO();
            this.cU_TURNO2 = new PRESENTACION.CU_TURNO();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(675, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 37);
            this.button1.TabIndex = 0;
            this.button1.Text = "SALIR";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(581, 566);
            this.panel1.TabIndex = 1;
            // 
            // cU_TURNO1
            // 
            this.cU_TURNO1.BackColor = System.Drawing.Color.Gray;
            this.cU_TURNO1.Location = new System.Drawing.Point(645, 90);
            this.cU_TURNO1.Name = "cU_TURNO1";
            this.cU_TURNO1.Size = new System.Drawing.Size(306, 111);
            this.cU_TURNO1.TabIndex = 2;
            this.cU_TURNO1.Usuario = "label2";
            // 
            // cU_TURNO2
            // 
            this.cU_TURNO2.BackColor = System.Drawing.Color.Gray;
            this.cU_TURNO2.Location = new System.Drawing.Point(645, 196);
            this.cU_TURNO2.Name = "cU_TURNO2";
            this.cU_TURNO2.Size = new System.Drawing.Size(306, 110);
            this.cU_TURNO2.TabIndex = 3;
            this.cU_TURNO2.Usuario = "label2";
            // 
            // frmPartida
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 606);
            this.Controls.Add(this.cU_TURNO2);
            this.Controls.Add(this.cU_TURNO1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button1);
            this.Name = "frmPartida";
            this.Text = "frmPartida";
            this.Load += new System.EventHandler(this.FrmPartida_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private CU_TURNO cU_TURNO1;
        private CU_TURNO cU_TURNO2;
    }
}