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
    public partial class frmPrincipal : Form
    {
        Bitacora bitacora = new Bitacora();
        List<Jugador> jugadoresLogueados = new List<Jugador>();

        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            frmRegistro registro = new frmRegistro();
            registro.Show();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (jugadoresLogueados.Count == 2)
            {
                frmPartida partida = new frmPartida(this, jugadoresLogueados);
                partida.Show();
                this.Hide();

                cU_LOGIN1.hideLogin();
                cU_LOGIN2.hideLogin();
                jugadoresLogueados.Clear();
            } else
            {
                MessageBox.Show("Debe haber 2 jugadores para comenzar la partida");
            }
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            cU_LOGIN1.AñadirJugador += AñadirJugador;
            cU_LOGIN2.AñadirJugador += AñadirJugador;

            cU_LOGIN1.RemoverJugador += RemoverJugador;
            cU_LOGIN2.RemoverJugador += RemoverJugador;

            cU_LOGIN1.VerificarJugador += VerificarJugador;
            cU_LOGIN2.VerificarJugador += VerificarJugador;

            //Hardcode jugadores

            cU_LOGIN1.Usuario = "eceluci";
            cU_LOGIN1.Contraseña = "eceluci";

            cU_LOGIN2.Usuario = "fdipaola";
            cU_LOGIN2.Contraseña = "fdipaola";
        }

        private void AñadirJugador(Jugador jugador)
        {
            
            bitacora.Escribir(jugador, "INICIO SESION");
            jugadoresLogueados.Add(jugador);
        }

        private void RemoverJugador(Jugador jugador)
        {
            bitacora.Escribir(jugador, "CIERRE DE SESION");
            jugadoresLogueados.Remove(jugador);
        }

        private bool VerificarJugador(Jugador jugador)
        {
            bool logueado = false;
            foreach(Jugador jug in jugadoresLogueados)
            {
                if (jug.Credencial.Username.Equals(jugador.Credencial.Username)) logueado = true;
            }
            return logueado;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            frmBitacora frmBitacora = new frmBitacora();
            frmBitacora.ShowDialog();
        }
    }
}
