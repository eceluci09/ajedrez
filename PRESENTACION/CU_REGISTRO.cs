using System;
using System.Drawing;
using System.Windows.Forms;
using NEGOCIO;

namespace PRESENTACION
{
    public partial class CU_REGISTRO : UserControl
    {
        public CU_REGISTRO()
        {
            InitializeComponent();
        }

        public void Blanquear()
        {
            foreach (Control textBox in this.Controls)
            {
                if (textBox is TextBox)
                {
                    textBox.Text = string.Empty;
                }
            }

            txtNombre.Focus();
        }


        protected void CambiarColor(bool ok, TextBox textBox)
        {
            if (!ok)
            {
                textBox.BackColor = Color.Coral;
            }
            else
            {
                textBox.BackColor = Color.White;
            }

        }

        public void CambiarColor()
        {
            txtUsername.BackColor = Color.Coral;
            txtUsername.Focus();
        }


            public bool Validar()
        {
            bool valido = true;
            foreach (Control textBox in this.Controls)
            {
                if(textBox is TextBox)
                {
                    bool ok = (!string.IsNullOrWhiteSpace(textBox.Text));
                    if (!ok) valido = false;
                    CambiarColor(ok, (TextBox)textBox);
                }
            }
            

            return valido;
        }

        public Jugador getJugador()
        {
            Jugador jugador = new Jugador();
            jugador.Nombre = txtNombre.Text;
            jugador.Apellido = txtApellido.Text;
            jugador.Credencial = new Credencial();
            jugador.Credencial.Username = txtUsername.Text;
            jugador.Credencial.Contraseña = txtContraseña.Text;

            return jugador;
        }
    }
}
