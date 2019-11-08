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
        public List<Jugador> jugadores;
        Form _principal;
        List<CU_CELDA> celdas = new List<CU_CELDA>();
        List<CU_CORONA> coronas = new List<CU_CORONA>();
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
            this.Reiniciar(jugadores);

        }

        private void Reiniciar(List<Jugador> jug)
        {
            partida = null;
            tablero = null;
            partida = new Partida();
            tablero = new Tablero();
            tablero.Partida = partida;

            foreach (Celda celda in tablero.Celdas)
            {
                CU_CELDA cel = new CU_CELDA();
                cel.AsignarCelda(celda, panel1);

                panel1.Controls.Add(cel);
                celdas.Add(cel);
            }

            partida.Iniciar(jug);

            //Asigno piezas al jugador 
            //JUGADOR 1: BLANCAS
            //JUGADOR 2: NEGRAS
            foreach (Pieza pieza in tablero.Piezas)
            {
                if (pieza.Color == Color.White)
                {
                    partida.Jugador1.Piezas.Add(pieza);

                }
                else if (pieza.Color == Color.Black)
                {

                    partida.Jugador2.Piezas.Add(pieza);
                }
            }

            cU_TURNO1.Usuario = partida.Jugador1.Credencial.Username;
            cU_TURNO2.Usuario = partida.Jugador2.Credencial.Username;
            partida.AsignarTurno();

            MarcarJugadorTurnoActivo();



            foreach (CU_CELDA control in celdas)
            {
                control.pictureBox.Click += Control_Click;

            }

            tablero.InformarJaque += Tablero_InformarJaque;
            tablero.InformarJaqueMate += Tablero_InformarJaqueMate;

            lblMensaje.Text = string.Empty;

            btnReiniciar.Visible = false;
        }

        private void MarcarJugadorTurnoActivo()
        {
            Jugador jugadorActivo = partida.VerificarJugadorTurnoActual();
            if(jugadorActivo.Credencial.Username.Equals(cU_TURNO1.Usuario))
            {
                cU_TURNO1.BackColor = Color.DarkTurquoise;
                cU_TURNO2.BackColor = Color.Gray;
            } else
            {
                cU_TURNO2.BackColor = Color.DarkTurquoise;
                cU_TURNO1.BackColor = Color.Gray;
            }
        }

        private void Tablero_InformarJaqueMate(Rey contrario, Jugador jugador)
        {
            lblMensaje.Text = "¡JAQUE MATE AL REY " + contrario.Color.Name + "!";
            lblMensaje.Text += "\n\n";
            lblMensaje.Text += "GANADOR = " + jugador.Credencial.Username;
            partida.SetGanador(jugador);
        }

        private void Tablero_InformarJaque(Rey contrario)
        {
            lblMensaje.Text = "¡JAQUE AL REY " + contrario.Color.Name + "!";
        }

        private void Control_Click(object sender, EventArgs e)
        {
            
            Jugador jugadorActivo = partida.VerificarJugadorTurnoActual();

            if (partida.Ganador == null && !partida.Tablas)
            {
                if (jugadorActivo.PiezasCoronacion.Count == 0)
                {



                    CU_CELDA CU_celda = (from CU_CELDA cu_celda in celdas
                                         where (PictureBox)sender == (cu_celda.pictureBox)
                                         select cu_celda).FirstOrDefault();


                    if ((partida.VerificarMovimientosJugadorActivo(jugadorActivo) == 1 && (CU_celda.Celda.Pieza is Torre || CU_celda.Marcado)) ||
                        (partida.VerificarMovimientosJugadorActivo(jugadorActivo) == 0))
                    {

                        //Verifica si selecciono una pieza o una celda vacia

                        if (CU_celda.Celda.Pieza != null && !CU_celda.Marcado)
                        {
                            lblMensaje.Text = string.Empty;
                            VerificarMovimientosDisponibles(CU_celda, jugadorActivo);

                        }
                        else
                        {
                            MoverPieza(CU_celda, celdaOrigen, jugadorActivo);
                        }
                    }
                    else
                    {
                        LimpiarCeldasDisponibles();
                    }
                }
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
                if (jugadorActivo.PiezasCoronacion.Count > 0)
                {
                    int posicionX = 0;
                    foreach (Pieza piezaCorona in jugadorActivo.PiezasCoronacion)
                    {
                        CU_CORONA corona = new CU_CORONA();
                        corona.AsignarPieza(piezaCorona, piezaAMover, jugadorActivo, posicionX, panel2);
                        
                        panel2.Controls.Add(corona);
                        coronas.Add(corona);
                        corona.pictureBox.Click += PiezaCorona_Click;
                        posicionX++;
                    }
                } else
                {
                    ActualizarTablero();

                    if (!partida.VerificarDobleTurno(jugadorActivo) && partida.Ganador == null)
                    {
                        partida.AsignarTurno();
                        MarcarJugadorTurnoActivo();
                    }

                    if(partida.Ganador != null || partida.Tablas)
                    {
                        partida.Alta();
                        partida.Ganador.ActualizarPartidasGanadas();
                        if(partida.Ganador.Equals(partida.Jugador1))
                        {
                            partida.Jugador2.ActualizarPartidasPerdidas();
                        } else
                        {
                            partida.Jugador1.ActualizarPartidasPerdidas();
                        }
                        if(partida.Tablas)
                        {
                            partida.Jugador1.ActualizarPartidasEmpatadas();
                            partida.Jugador2.ActualizarPartidasEmpatadas();
                        }
                        btnReiniciar.Visible = true;
                    }
                }

                

                
                
            }

            
        }

        private void PiezaCorona_Click(object sender, EventArgs e)
        {
            CU_CORONA CU_corona = (from CU_CORONA cu_corona in coronas
                                 where (PictureBox)sender == (cu_corona.pictureBox)
                                 select cu_corona).FirstOrDefault();

            tablero.IntercambiarPieza(CU_corona.PiezaCorona, tablero.getCelda(CU_corona.Pieza), CU_corona.Jugador);
            Jugador jugadorActivo = partida.VerificarJugadorTurnoActual();
            jugadorActivo.PiezasCoronacion.Clear();
            ActualizarTablero();
            partida.AsignarTurno();
            MarcarJugadorTurnoActivo();
            panel2.Controls.Clear();

        }

        private void ActualizarTablero()
        {
            foreach(CU_CELDA celda in celdas)
            {
                celda.ActualizarCelda();
            }
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

                if (jugadorActivo.PiezaJaque != null && !(pieza is Rey)) {
                celdasDisponibles = pieza.getMovimientosPermitidosEnJaque(tablero, jugadorActivo);
                }

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

        private void BtnReiniciar_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            celdas.Clear();
            coronas.Clear();
            
            this.Reiniciar(partida.recargarJugadoresPartida());
        }
    }
}
