using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NEGOCIO;

namespace PRESENTACION
{
    public partial class frmRegistro : Form
    {
        public frmRegistro()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if(cU_REGISTRO1.Validar())
            {
                Jugador jugador = cU_REGISTRO1.getJugador();
                if (!this.UsuarioExistente(jugador))
                {
                    this.RegistrarUsuario(jugador);
                } else
                {
                    MessageBox.Show("EL USUARIO YA SE ENCUENTRA EN USO");
                    cU_REGISTRO1.CambiarColor();
                }
            }
            
        }

        public bool UsuarioExistente(Jugador jugador)
        {
            return jugador.ValidarUsuario();
        }

        public void RegistrarUsuario(Jugador jugador)
        {
            if (jugador.Alta() > 0)
            {
                cU_REGISTRO1.Blanquear();
                MessageBox.Show("JUGADOR REGISTRADO CORRECTAMENTE");
            }
            else
            {
                MessageBox.Show("HA OCURRIDO UN ERROR AL REGISTRAR AL USUARIO");
            }
        }
    }
}
