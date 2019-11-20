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
    public partial class frmEstadisticas : Form
    {
        public frmEstadisticas()
        {
            InitializeComponent();
        }

        private void FrmEstadisticas_Load(object sender, EventArgs e)
        {
            listBox1.DataSource = null;
            listBox1.DataSource = Jugador.Listar();
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Jugador jugador = (Jugador)listBox1.SelectedItem;
            if (jugador != null)
            {
                label2.Text = "CANTIDAD PARTIDAS GANADAS: " + jugador.PartidasGanadas;
                label3.Text = "CANTIDAD PARTIDAS EMPATADAS: " + jugador.PartidasEmpatadas;
                label4.Text = "CANTIDAD PARTIDAS PERDIDAS: " + jugador.PartidasPerdidas;

                int partidasJugadas = jugador.PartidasGanadas + jugador.PartidasEmpatadas + jugador.PartidasPerdidas;

                float porcentaje = 0;

                if (partidasJugadas == 0)
                {
                    porcentaje = 0;
                } else
                {
                    porcentaje = (jugador.PartidasGanadas * 100) / partidasJugadas;
                }
                

                label5.Text = "PORCENTAJE DE VICTORIAS: " + porcentaje + "%";

                label6.Text = "MINUTOS JUGADOS: " + jugador.TiempoJugado;

            }


        }
    }
}
