using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NEGOCIO;

namespace PRESENTACION
{
    public partial class CU_LOGIN : UserControl
    {
        public CU_LOGIN()
        {
            InitializeComponent();
        }

        public delegate void delAddJugador(Jugador jugador);
        public event delAddJugador AñadirJugador;

        public delegate void delRemoveJugador(Jugador jugador);
        public event delRemoveJugador RemoverJugador;

        public delegate bool delVerificarJugador(Jugador jugador);
        public event delVerificarJugador VerificarJugador;

        private Jugador jugadorLogueado = null;

        public Jugador JugadorLogueado
        {
            get { return jugadorLogueado; }
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

            txtUsername.Focus();
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
                if (textBox is TextBox)
                {
                    bool ok = (!string.IsNullOrWhiteSpace(textBox.Text));
                    if (!ok) valido = false;
                    CambiarColor(ok, (TextBox)textBox);
                }
            }


            return valido;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            showLogin();
        }

        public void showLogin()
        {
            label1.Visible = true;
            label2.Visible = true;
            txtUsername.Visible = true;
            txtContraseña.Visible = true;
            button2.Visible = true;
            button1.Visible = false;
            button3.Visible = true;
            lblNombreL.Visible = false;
            lblNombre.Visible = false;
            lblApellidoL.Visible = false;
            lblApellido.Visible = false;
            lblUsernameL.Visible = false;
            lblUsername.Visible = false;
            txtUsername.Focus();
            jugadorLogueado = null;
        }

        public void showUser()
        {
            label1.Visible = false;
            label2.Visible = false;
            txtUsername.Visible = false;
            txtContraseña.Visible = false;
            button2.Visible = true;
            button1.Visible = false;
            button3.Visible = false;
            lblNombreL.Visible = true;
            lblNombre.Visible = true;
            lblApellidoL.Visible = true;
            lblApellido.Visible = true;
            lblUsernameL.Visible = true;
            lblUsername.Visible = true;
        }

        public void hideLogin()
        {
            label1.Visible = false;
            label2.Visible = false;
            txtUsername.Visible = false;
            txtContraseña.Visible = false;
            button1.Visible = true;
            button2.Visible = false;
            button3.Visible = false;
            lblNombreL.Visible = false;
            lblNombre.Visible = false;
            lblApellidoL.Visible = false;
            lblApellido.Visible = false;
            lblUsernameL.Visible = false;
            lblUsername.Visible = false;
            jugadorLogueado = null;
            this.Blanquear();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.RemoverJugador(jugadorLogueado);
            hideLogin();
            
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (Validar())
            {
                Jugador jugador = new Jugador();
                jugador.Credencial = new Credencial();
                jugador.Credencial.Username = txtUsername.Text;
                jugador.Credencial.Contraseña = txtContraseña.Text;

                jugadorLogueado = jugador.Login();

                if (jugadorLogueado != null)
                {
                    bool logueado = this.VerificarJugador(JugadorLogueado);

                    if (!logueado)
                    {
                        mostrarDatosJugador();
                        showUser();
                        this.AñadirJugador(JugadorLogueado);
                    }  else
                    {
                        MessageBox.Show("USUARIO YA LOGUEADO");
                        txtUsername.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("USUARIO Y/O CONTRASEÑA INCORRECTOS");
                    txtUsername.Focus();
                }
                
            }
        }

        private void mostrarDatosJugador()
        {
            lblNombre.Text = jugadorLogueado.Nombre;
            lblApellido.Text = jugadorLogueado.Apellido;
            lblUsername.Text = jugadorLogueado.Credencial.Username;
        }

        private void CU_LOGIN_Load(object sender, EventArgs e)
        {
            hideLogin();
        }
    }
}
