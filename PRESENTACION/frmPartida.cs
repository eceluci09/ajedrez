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
    public partial class frmPartida : Form
    {
        Form _principal;
        public frmPartida(Form principal, List<Jugador> jugadores)
        {
            this._principal = principal;
            InitializeComponent();
        }
        Partida partida;
        Tablero tablero;
        private void Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            _principal.Show();
            
        }

        private void FrmPartida_Load(object sender, EventArgs e)
        {
            partida = new Partida();
            tablero = new Tablero();
            List<CU_CELDA> celdas = new List<CU_CELDA>();

            foreach(Celda celda in tablero.Celdas)
            {
                CU_CELDA cel = new CU_CELDA();
                cel.AsignarCelda(celda, panel1);

                panel1.Controls.Add(cel);
                celdas.Add(cel);
            }

        }
    }
}
