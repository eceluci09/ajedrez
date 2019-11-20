namespace PRESENTACION
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
            this.btnSalir = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnReiniciar = new System.Windows.Forms.Button();
            this.btnAbandonar = new System.Windows.Forms.Button();
            this.cU_TURNO2 = new PRESENTACION.CU_TURNO();
            this.cU_TURNO1 = new PRESENTACION.CU_TURNO();
            this.SuspendLayout();
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(664, 12);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(113, 37);
            this.btnSalir.TabIndex = 0;
            this.btnSalir.Text = "SALIR";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Visible = false;
            this.btnSalir.Click += new System.EventHandler(this.Button1_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(581, 566);
            this.panel1.TabIndex = 1;
            // 
            // lblMensaje
            // 
            this.lblMensaje.AutoSize = true;
            this.lblMensaje.BackColor = System.Drawing.Color.Black;
            this.lblMensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensaje.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblMensaje.Location = new System.Drawing.Point(599, 340);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(76, 25);
            this.lblMensaje.TabIndex = 4;
            this.lblMensaje.Text = "label1";
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(602, 476);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(392, 116);
            this.panel2.TabIndex = 5;
            // 
            // btnReiniciar
            // 
            this.btnReiniciar.Location = new System.Drawing.Point(645, 416);
            this.btnReiniciar.Name = "btnReiniciar";
            this.btnReiniciar.Size = new System.Drawing.Size(132, 40);
            this.btnReiniciar.TabIndex = 6;
            this.btnReiniciar.Text = "REINICIAR";
            this.btnReiniciar.UseVisualStyleBackColor = true;
            this.btnReiniciar.Visible = false;
            this.btnReiniciar.Click += new System.EventHandler(this.BtnReiniciar_Click);
            // 
            // btnAbandonar
            // 
            this.btnAbandonar.Location = new System.Drawing.Point(800, 416);
            this.btnAbandonar.Name = "btnAbandonar";
            this.btnAbandonar.Size = new System.Drawing.Size(118, 40);
            this.btnAbandonar.TabIndex = 9;
            this.btnAbandonar.Text = "ABANDONAR";
            this.btnAbandonar.UseVisualStyleBackColor = true;
            this.btnAbandonar.Click += new System.EventHandler(this.Button2_Click);
            // 
            // cU_TURNO2
            // 
            this.cU_TURNO2.BackColor = System.Drawing.Color.Gray;
            this.cU_TURNO2.Location = new System.Drawing.Point(645, 191);
            this.cU_TURNO2.Name = "cU_TURNO2";
            this.cU_TURNO2.Size = new System.Drawing.Size(322, 130);
            this.cU_TURNO2.TabIndex = 8;
            this.cU_TURNO2.Usuario = "label2";
            // 
            // cU_TURNO1
            // 
            this.cU_TURNO1.BackColor = System.Drawing.Color.Gray;
            this.cU_TURNO1.Location = new System.Drawing.Point(645, 55);
            this.cU_TURNO1.Name = "cU_TURNO1";
            this.cU_TURNO1.Size = new System.Drawing.Size(322, 130);
            this.cU_TURNO1.TabIndex = 7;
            this.cU_TURNO1.Usuario = "label2";
            // 
            // frmPartida
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1006, 623);
            this.Controls.Add(this.btnAbandonar);
            this.Controls.Add(this.cU_TURNO2);
            this.Controls.Add(this.cU_TURNO1);
            this.Controls.Add(this.btnReiniciar);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lblMensaje);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnSalir);
            this.Name = "frmPartida";
            this.Text = "frmPartida";
            this.Load += new System.EventHandler(this.FrmPartida_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnReiniciar;
        private CU_TURNO cU_TURNO1;
        private CU_TURNO cU_TURNO2;
        private System.Windows.Forms.Button btnAbandonar;
    }
}
