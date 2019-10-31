namespace PRESENTACION
{
    partial class frmPrincipal
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.cU_LOGIN2 = new PRESENTACION.CU_LOGIN();
            this.cU_LOGIN1 = new PRESENTACION.CU_LOGIN();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(663, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 36);
            this.button1.TabIndex = 0;
            this.button1.Text = "REGISTRARSE";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // cU_LOGIN2
            // 
            this.cU_LOGIN2.Location = new System.Drawing.Point(238, 178);
            this.cU_LOGIN2.Name = "cU_LOGIN2";
            this.cU_LOGIN2.Size = new System.Drawing.Size(311, 114);
            this.cU_LOGIN2.TabIndex = 2;
            // 
            // cU_LOGIN1
            // 
            this.cU_LOGIN1.Location = new System.Drawing.Point(238, 58);
            this.cU_LOGIN1.Name = "cU_LOGIN1";
            this.cU_LOGIN1.Size = new System.Drawing.Size(311, 114);
            this.cU_LOGIN1.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(605, 297);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(169, 46);
            this.button2.TabIndex = 3;
            this.button2.Text = "INICIAR PARTIDA";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.cU_LOGIN2);
            this.Controls.Add(this.cU_LOGIN1);
            this.Controls.Add(this.button1);
            this.Name = "frmPrincipal";
            this.Text = "AJEDREZ";
            this.Load += new System.EventHandler(this.FrmPrincipal_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private CU_LOGIN cU_LOGIN1;
        private CU_LOGIN cU_LOGIN2;
        private System.Windows.Forms.Button button2;
    }
}

