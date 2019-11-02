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
        List<Jugador> jugadores;
        Form _principal;
        List<CU_CELDA> celdas = new List<CU_CELDA>();
        public frmPartida(Form principal, List<Jugador> jugadores)
        {
            this._principal = principal;
            this.jugadores = jugadores;
            InitializeComponent();
        }
        Partida partida;
        Tablero tablero;

        CU_CELDA celdaOrigen;
        private void Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            _principal.Show();
            
        }

        private void FrmPartida_Load(object sender, EventArgs e)
        {
            partida = new Partida();
            tablero = new Tablero();
            tablero.Partida = partida;

            foreach(Celda celda in tablero.Celdas)
            {
                CU_CELDA cel = new CU_CELDA();
                cel.AsignarCelda(celda, panel1);

                panel1.Controls.Add(cel);
                celdas.Add(cel);
            }

            partida.Iniciar(jugadores);

            //Asigno piezas al jugador 
            //JUGADOR 1: BLANCAS
            //JUGADOR 2: NEGRAS
            foreach(Pieza pieza in tablero.Piezas)
            {
                if(pieza.Color == Color.White)
                {
                    partida.Jugador1.Piezas.Add(pieza);

                } else if(pieza.Color == Color.Black) {

                    partida.Jugador2.Piezas.Add(pieza);
                }
            }

            partida.AsignarTurno();

            foreach(CU_CELDA control in celdas)
            {
               control.pictureBox.Click += Control_Click;
              
            }



        }

        private void Control_Click(object sender, EventArgs e)
        {
            Jugador jugadorActivo = partida.VerificarJugadorTurnoActual();

            
            CU_CELDA CU_celda = (from CU_CELDA cu_celda in celdas
                                 where (PictureBox)sender == (cu_celda.pictureBox)
                                 select cu_celda).FirstOrDefault();


            if ((partida.VerificarMovimientosJugadorActivo(jugadorActivo) == 1 && (CU_celda.Celda.Pieza is Torre || CU_celda.Marcado)) || 
                (partida.VerificarMovimientosJugadorActivo(jugadorActivo) == 0))
            {


                //Verifica si selecciono una pieza o una celda vacia

                if (CU_celda.Celda.Pieza != null && !CU_celda.Marcado)
                {

                    VerificarMovimientosDisponibles(CU_celda, jugadorActivo);

                }
                else
                {
                    MoverPieza(CU_celda, celdaOrigen, jugadorActivo);
                }
            } else
            {
                LimpiarCeldasDisponibles();
            }

            
        }

        private void MoverPieza(CU_CELDA celdaDestino, CU_CELDA celdaOrigen, Jugador jugadorActivo)
        {
            if (celdaDestino.Marcado)
            {
                Pieza piezaAMover = celdaOrigen.Celda.Pieza;
                Celda celdaActual = tablero.getCelda(piezaAMover);
                jugadorActivo.Mover(piezaAMover, tablero, celdaActual, celdaDestino.Celda);

                LimpiarCeldasDisponibles();

                ActualizarTablero(celdaOrigen, celdaDestino);

                if(!partida.VerificarDobleTurno(jugadorActivo))
                {
                    partida.AsignarTurno();
                }
                
            }

            
        }

        private void ActualizarTablero(CU_CELDA celdaOrigen, CU_CELDA celdaDestino)
        {
            celdaOrigen.ActualizarCelda();
            celdaDestino.ActualizarCelda();
        }

        private void VerificarMovimientosDisponibles(CU_CELDA CU_celda, Jugador jugadorActivo)
        {
            //Limpiar celdas disponibles

            LimpiarCeldasDisponibles();

                     
            CU_CELDA c = (from CU_CELDA cu_celda in celdas
                          where jugadorActivo.Piezas.Contains(CU_celda.Celda.Pieza)
                          select cu_celda).FirstOrDefault();

            if (c != null)
            {
                Pieza pieza = CU_celda.Celda.Pieza;
                Celda celda = CU_celda.Celda;
                List<Celda> celdasDisponibles = pieza.getCeldasDestino(tablero, celda);

                foreach (CU_CELDA control in celdas)
                {
                    if (celdasDisponibles.Contains(control.Celda))
                    {
                        control.MarcarCeldaDisponible();
                    }
                }

                celdaOrigen = CU_celda;
            }
        }

        private void LimpiarCeldasDisponibles()
        {
            List<CU_CELDA> celdasALimpiar = (from CU_CELDA celdaLimpiar in celdas
                                             where celdaLimpiar.Marcado == true
                                             select celdaLimpiar).ToList();

            foreach (CU_CELDA celdaALimpiar in celdasALimpiar)
            {
                celdaALimpiar.DesmarcarCeldaDisponible();
            }
        }
    }
}
