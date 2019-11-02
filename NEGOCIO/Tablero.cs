﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace NEGOCIO
{
    public class Tablero
    {
        public Tablero()
        {
            this.Iniciar();
        }
        private static string ARRIBA = "ARRIBA";
        private static string ABAJO = "ABAJO";

        public delegate void delInformarJaque(Rey contrario);
        public event delInformarJaque InformarJaque;

        public delegate void delInformarJaqueMate(Rey contrario);
        public event delInformarJaque InformarJaqueMate;

        private List<Pieza> piezas = new List<Pieza>();

        public List<Pieza> Piezas
        {
            get { return piezas; }
            set { piezas = value; }
        }

        private List<Celda> celdas = new List<Celda>();

        public List<Celda> Celdas
        {
            get { return celdas; }
            set { celdas = value; }
        }

        private Partida partida;

        public Partida Partida
        {
            get { return partida; }
            set { partida = value; }
        }

        public void Iniciar()
        {
            CrearCeldas();
            CrearPiezas();
        }

        private void CrearPiezas()
        {
            //Blancas

            Torre torre1B = new Torre(ABAJO, true, Color.White);
            getCelda(8, 1).Pieza = torre1B;

            piezas.Add(torre1B);

            Caballo caballo1B = new Caballo(ABAJO, true, Color.White);
            getCelda(8, 2).Pieza = caballo1B;

            piezas.Add(caballo1B);

            Alfil alfil1B = new Alfil(ABAJO, true, Color.White);
            getCelda(8, 3).Pieza = alfil1B;

            piezas.Add(alfil1B);

            Reina reinaB = new Reina(ABAJO, true, Color.White);
            getCelda(8, 4).Pieza = reinaB;

            piezas.Add(reinaB);

            Rey reyB = new Rey(ABAJO, true, Color.White);
            getCelda(8, 5).Pieza = reyB;

            piezas.Add(reyB);

            Alfil alfil2B = new Alfil(ABAJO, true, Color.White);
            getCelda(8, 6).Pieza = alfil2B;

            piezas.Add(alfil2B);

            Caballo caballo2B = new Caballo(ABAJO, true, Color.White);
            getCelda(8, 7).Pieza = caballo2B;

            piezas.Add(caballo2B);

            Torre torre2B = new Torre(ABAJO, true, Color.White);
            getCelda(8, 8).Pieza = torre2B;

            piezas.Add(torre2B);

            for (int i = 0; i < 8; i++)
            {
                Peon peon = new Peon(ABAJO, true, Color.White);
                getCelda(7, i + 1).Pieza = peon;
                piezas.Add(peon);
            }

            //Negras

            Torre torre1N = new Torre(ARRIBA, true, Color.Black);
            getCelda(1, 1).Pieza = torre1N;

            piezas.Add(torre1N);

            Caballo caballo1N = new Caballo(ARRIBA, true, Color.Black);
            getCelda(1, 2).Pieza = caballo1N;

            piezas.Add(caballo1N);

            Alfil alfil1N = new Alfil(ARRIBA, true, Color.Black);
            getCelda(1, 3).Pieza = alfil1N;

            piezas.Add(alfil1N);

            Reina reinaN = new Reina(ARRIBA, true, Color.Black);
            getCelda(1, 4).Pieza = reinaN;

            piezas.Add(reinaN);

            Rey reyN = new Rey(ARRIBA, true, Color.Black);
            getCelda(1, 5).Pieza = reyN;

            piezas.Add(reyN);

            Alfil alfil2N = new Alfil(ARRIBA, true, Color.Black);
            getCelda(1, 6).Pieza = alfil2N;

            piezas.Add(alfil2N);

            Caballo caballo2N = new Caballo(ARRIBA, true, Color.Black);
            getCelda(1, 7).Pieza = caballo2N;

            piezas.Add(caballo2N);

            Torre torre2N = new Torre(ARRIBA, true, Color.Black);
            getCelda(1, 8).Pieza = torre2N;

            piezas.Add(torre2N);

            for (int i = 0; i < 8; i++)
            {
                Peon peon = new Peon(ARRIBA, true, Color.Black);
                getCelda(2, i + 1).Pieza = peon;
                piezas.Add(peon);
            }


        }

        private void CrearCeldas()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Celda celda = new Celda();
                    celda.Color = (i + j) % 2 == 0 ? Color.FromArgb(145, 29, 19) : Color.FromArgb(233, 255, 95);
                    celda.Fila = i + 1;
                    celda.Columna = j + 1;
                    celdas.Add(celda);
                }
            }
        }

        public Celda getCelda(Pieza pieza)
        {
            Celda celda = (from Celda cel in celdas
                           where cel.Pieza != null && cel.Pieza.Nro == pieza.Nro && pieza.Activa == true
                           select cel).FirstOrDefault();


           return celda;
        }

        public Celda getCelda(int fila, int columna)
        {
            Celda celda = (from Celda cel in celdas
                           where cel.Fila == fila && cel.Columna == columna
                           select cel).FirstOrDefault();


            return celda;
        }

        public Celda getCelda(Celda celdaActual, Movimiento movimiento)
        {
            try
            {
                if (celdaActual == null) return null;
                Pieza pieza = celdaActual.Pieza;
                if (pieza == null) return null;

                int horizontal = celdaActual.Columna + movimiento.Horizontal;
                int vertical = (pieza.PosicionInicial.Equals(ARRIBA) ? celdaActual.Fila + movimiento.Vertical : celdaActual.Fila - movimiento.Vertical);
                Celda celda = (from Celda cel in celdas
                               where cel.Columna == horizontal && cel.Fila == vertical
                               select cel).FirstOrDefault();

                return celda;
            }
            catch (StackOverflowException ex)
            {
                
            }

            return null;

        }

        public bool VerificarCeldaDisponible(Celda celda)
        {
            if (celda == null) return false;

            return (celda.Pieza == null);
        }

        public bool VerificarCeldaDisponible(Celda celda, Color color)
        {
            if (celda == null) return false;

            return (celda.Pieza == null || celda.Pieza.Color != color);
        }

        public bool ModificarPosicionPieza(Celda actual, Celda destino, Jugador jug)
        {
            bool coronacion = false;
            Pieza pieza = actual.Pieza;
            Jugador jugadorRival = null;
            Pieza piezaComida = null;
            
                if (jug.Equals(partida.Jugador1))
                {
                    if (destino.Pieza != null)
                    {
                        piezaComida = (from Pieza p in partida.Jugador2.Piezas
                                       where p.Equals(destino.Pieza)
                                       select p).FirstOrDefault();
                    }

                    jugadorRival = partida.Jugador2;
                } else
                {
                    if (destino.Pieza != null)
                    {
                        piezaComida = (from Pieza p in partida.Jugador1.Piezas
                                       where p.Equals(destino.Pieza)
                                       select p).FirstOrDefault();
                    }

                    jugadorRival = partida.Jugador1;
                }

            if (destino.Pieza != null)
            {

                piezaComida.Activa = false;

                piezaComida = (from Pieza p in piezas
                               where p.Equals(piezaComida)
                               select p).FirstOrDefault();

                piezaComida.Activa = false;
            }
            
            //Verifica si el peon comio con el peon al paso
            if (pieza is Peon)
            {
                Movimiento mov = new Movimiento();
                mov.Horizontal = 0;
                mov.Vertical = -1;

                Celda celdaAnterior = this.getCelda(actual, mov);

                if(celdaAnterior.Pieza != null && celdaAnterior.Pieza is Peon && celdaAnterior.Pieza.Color != pieza.Color)
                {
                    piezaComida = (from Pieza p in piezas
                                   where p.Equals(celdaAnterior.Pieza)
                                   select p).FirstOrDefault();

                    piezaComida.Activa = false;
                }

                coronacion = this.VerificarCoronacion((Peon)pieza, destino);

            }

            destino.Pieza = pieza;
            partida.RegistrarMovimiento(actual, destino);
            this.VerificarMovimiento(actual, destino, pieza, jug);
            actual.Pieza = null;
            jugadorRival.PiezaJaque.Clear();
            string tipo = (!coronacion) ? this.verificarJaqueOJaqueMate(jug, pieza, jugadorRival) : string.Empty;
            partida.ChequearGanador(tipo, jug);

            return coronacion;
        }

        public void IntercambiarPieza(Pieza pieza, Celda destino, Jugador jug)
        {
            Pieza piezaIntercambiada = null;
            Jugador jugadorRival = null;
            if(destino.Pieza != null)
            {
                if (jug.Equals(partida.Jugador1))
                {
                    piezaIntercambiada = (from Pieza p in partida.Jugador2.Piezas
                                   where p.Equals(destino.Pieza)
                                   select p).FirstOrDefault();

                    jugadorRival = partida.Jugador2;
                }
                else
                {
                    piezaIntercambiada = (from Pieza p in partida.Jugador1.Piezas
                                   where p.Equals(destino.Pieza)
                                   select p).FirstOrDefault();

                    jugadorRival = partida.Jugador1;
                }

                piezaIntercambiada.Activa = false;

                piezaIntercambiada = (from Pieza p in piezas
                               where p.Equals(piezaIntercambiada)
                               select p).FirstOrDefault();

                piezaIntercambiada.Activa = false;
            }
            destino.Pieza = pieza;
            partida.RegistrarMovimiento(null, destino);

        }

        public List<Movimiento> VerificarEnroque(Rey pieza, List<Movimiento> movimientos)
        {
            Celda celdaAMover = null;
            Celda celdaActual = this.getCelda(pieza);
            Movimiento mov1 = movimientos.Where(m => m.Vertical == 0 && m.Horizontal == 2).FirstOrDefault();
            Movimiento mov2 = movimientos.Where(m => m.Vertical == 0 && m.Horizontal == -2).FirstOrDefault();
            Movimiento movimientoRey = (from Movimiento m in partida.Movimientos
                             where m.Pieza == pieza
                             select m).FirstOrDefault();

            Movimiento mov = new Movimiento();
            mov.Vertical = 0;
            mov.Horizontal = -4;

            movimientos = this.VerificarMovimiento(celdaActual, mov, movimientoRey, movimientos, mov2);

            mov.Vertical = 0;
            mov.Horizontal = 3;

            movimientos = this.VerificarMovimiento(celdaActual, mov, movimientoRey, movimientos, mov1);

            //Verifica que no haya piezas de por medio

            if (movimientos.Contains(mov1) || movimientos.Contains(mov2))
            {
                bool removerMov1 = false;
                bool removerMov2 = false;
                for (int i = 0; i < 2; i++)
                {
                    mov.Vertical = 0;
                    mov.Horizontal = 1;

                    if (celdaAMover == null)
                    {
                        celdaAMover = this.getCelda(celdaActual.Fila, celdaActual.Columna + mov.Horizontal);
                    }
                    else
                    {
                        celdaAMover = this.getCelda(celdaAMover.Fila, celdaAMover.Columna + mov.Horizontal);
                    }


                    if (!this.VerificarCeldaDisponible(celdaAMover))
                    {
                        removerMov1 = true;
                    }
                }
                celdaAMover = null;
                for (int i = 0; i < 3; i++)
                {
                    mov.Vertical = 0;
                    mov.Horizontal = -1;

                    if (celdaAMover == null)
                    {
                        celdaAMover = this.getCelda(celdaActual.Fila, celdaActual.Columna + mov.Horizontal);
                    }
                    else
                    {
                        celdaAMover = this.getCelda(celdaAMover.Fila, celdaAMover.Columna + mov.Horizontal);
                    }


                    if (!this.VerificarCeldaDisponible(celdaAMover))
                    {
                        removerMov2 = true;
                    }
                }

                if (removerMov1) movimientos.Remove(mov1);
                if (removerMov2) movimientos.Remove(mov2);
            }

            //Verificar si ya realizo un movimiento


            if (movimientos.Contains(mov1) || movimientos.Contains(mov2))
            {
                foreach (Pieza p in piezas)
                {
                    if (p.Color != pieza.Color && pieza.Activa == true)
                    {
                        Celda celda = this.getCelda(p);

                        //Verifica si el rey esta en jaque actualmente
                        foreach (Celda celdaDisp in p.getCeldasDestino(this, celda))
                        {
                            if (celdaActual.Equals(celdaDisp))
                            {
                                movimientos.Remove(mov1);
                                movimientos.Remove(mov2);
                            }
                        }

                        //Verifica si el rey esta amenazado al moverse
                        for (int i = 0; i < 2; i++)
                        {
                            Movimiento movimientoARealizar = new Movimiento();
                            movimientoARealizar.Vertical = 0;
                            movimientoARealizar.Horizontal = 1;
                            if (celdaAMover == null)
                            {
                                celdaAMover = this.getCelda(celdaActual.Fila, celdaActual.Columna + movimientoARealizar.Horizontal);
                            }
                            else
                            {
                                celdaAMover = this.getCelda(celdaAMover.Fila, celdaAMover.Columna + movimientoARealizar.Horizontal);
                            }

                            foreach (Celda celdaDisp in p.getCeldasDestino(this, celda))
                            {
                                if (celdaAMover.Equals(celdaDisp))
                                {
                                    movimientos.Remove(mov1);
                                }
                            }
                        }

                        celdaAMover = null;
                        for (int i = 0; i < 2; i++)
                        {
                            Movimiento movimientoARealizar = new Movimiento();
                            movimientoARealizar.Vertical = 0;
                            movimientoARealizar.Horizontal = -1;
                            if (celdaAMover == null)
                            {
                                celdaAMover = this.getCelda(celdaActual.Fila, celdaActual.Columna + movimientoARealizar.Horizontal);
                            }
                            else
                            {
                                celdaAMover = this.getCelda(celdaAMover.Fila, celdaAMover.Columna + movimientoARealizar.Horizontal);
                            }
                            foreach (Celda celdaDisp in p.getCeldasDestino(this, celda))
                            {
                                if (celdaAMover.Equals(celdaDisp))
                                {
                                    movimientos.Remove(mov2);
                                }
                            }
                        }
                    }
                }
            }

            return movimientos;
        }

        private List<Movimiento> VerificarMovimiento(Celda celdaActual, Movimiento mov, Movimiento movimientoRey, List<Movimiento> movimientos, Movimiento movAEliminar)
        {
            Celda c = this.getCelda(celdaActual, mov);

            if (c == null) return movimientos;

            Movimiento movimientoTorre = (from Movimiento mt in partida.Movimientos
                                          where mt.Pieza == c.Pieza
                                          select mt).FirstOrDefault();

            if (movimientoRey != null || movimientoTorre != null)
            {
                movimientos.Remove(movAEliminar);
            }

            return movimientos;
        }

        private string verificarJaqueOJaqueMate(Jugador jugUltMov, Pieza piezaUltMov, Jugador jugadorRival)
        {
            string tipo = string.Empty;
            Pieza reyContrario = null;
            if (jugUltMov.Equals(partida.Jugador1))
            {
                reyContrario = (from Pieza rey in partida.Jugador2.Piezas
                                      where rey is Rey && rey.Activa == true
                                      select rey).FirstOrDefault();
            }
            else
            {
                reyContrario = (from Pieza rey in partida.Jugador1.Piezas
                                      where rey is Rey && rey.Activa == true
                                      select rey).FirstOrDefault();
            }

            if (reyContrario != null)
            {
                Celda celdaCoincidente = null;
                bool amenaza = false;
                Celda celdaActualRey = this.getCelda(reyContrario);

                List<Celda> celdaAMoverse = reyContrario.getCeldasDestino(this, celdaActualRey);

                foreach (Pieza pieza in piezas)
                {
                    if (piezaUltMov.Color == pieza.Color && pieza.Activa == true && !(pieza is Rey))
                    {
                        List<Celda> celdasAMover = pieza.getCeldasDestino(this, this.getCelda(pieza));
                        celdaCoincidente = (from Celda c in celdasAMover
                                            where c.Equals(celdaActualRey)
                                            select c).FirstOrDefault();

                        if (celdaCoincidente != null)
                        {
                            jugadorRival.PiezaJaque.Add(pieza);
                            amenaza = true;
                        }

                    }
                }

                if(amenaza)
                {
                    if(celdaAMoverse.Count > 0)
                    {
                        tipo = "JAQUE";
                        this.InformarJaque((Rey)reyContrario);
                    } else
                    {
                        tipo = "JAQUE MATE";
                        this.InformarJaqueMate((Rey)reyContrario);
                    }
                }



                //Verificar jaque mate
                /*List<Celda> celdasAMoverseProxMov = null;
                if (piezaUltMov is Rey)
                {
                    celdasAMoverseProxMov = ((Rey)piezaUltMov).getCeldasDestinoSinAmenazaNiEnroque(this, this.getCelda(piezaUltMov));
                }
                else
                {
                    celdasAMoverseProxMov = piezaUltMov.getCeldasDestino(this, this.getCelda(piezaUltMov));
                }
                

                Celda celdaActualRey = this.getCelda(reyContrario);

                if (celdasAMoverseProxMov.Contains(celdaActualRey))
                {
                    tipo = "JAQUE MATE";

                    //Verificar jaque

                    List<Celda> celdaAMoverse = reyContrario.getCeldasDestino(this, celdaActualRey);

                    foreach (Celda celdaDisp in celdaAMoverse)
                    {
                        Celda celdaCoincidente = null;

                        foreach (Pieza pieza in piezas)
                        {
                            if (piezaUltMov.Color == pieza.Color && pieza.Activa == true)
                            {
                                List<Celda> celdasAMover = pieza.getCeldasDestino(this, this.getCelda(pieza));
                                celdaCoincidente = (from Celda c in celdasAMover
                                                    where c.Equals(celdaDisp)
                                                    select c).FirstOrDefault();

                            }
                        }
                        if (celdaCoincidente == null)
                        {
                            tipo = "JAQUE";
                        }
                    }
                }*/
            }

            return tipo;
            
        }

        public List<Movimiento> VerificarAmenaza(Rey pieza, List<Movimiento> movimientos, bool esElContrario)
        {
            Celda celdaActual = this.getCelda(pieza);
            Celda celdaAMover = null;
            
            List<Movimiento> movi = new List<Movimiento>();
            foreach (Movimiento mov in movimientos)
            {
                celdaAMover = this.getCelda(celdaActual, mov);

                if (celdaAMover != null)
                {
                    foreach (Pieza p in piezas)
                    {
                        if (p.Color != pieza.Color && pieza.Activa == true)
                        {
                            List<Celda> cel = new List<Celda>();
                            if (p is Rey && !esElContrario)
                            {
                                cel = ((Rey)p).getCeldasDestino(this, this.getCelda(p), true);
                            }
                            else
                            {
                                if (p is Peon && !esElContrario)
                                {
                                    cel = ((Peon)p).getCeldasDestinoRey(this, this.getCelda(p));
                                }
                                else
                                {
                                    if(!(p is Rey) && !(p is Peon) && !esElContrario)
                                    {
                                        cel = p.getCeldasDestino(this, this.getCelda(p));
                                    }
                                                                   
                                }
                            }
                            foreach (Celda celdaDisp in cel)
                            {
                                if (celdaAMover.Equals(celdaDisp))
                                {
                                    if (!movi.Contains(mov))
                                    {
                                        movi.Add(mov);
                                    }

                                }
                            }

                        }
                    }

                }
            }

            foreach(Movimiento m in movi)
            {
                movimientos.Remove(m);
            }

            return movimientos;
        }

        public void VerificarMovimiento(Celda actual, Celda destino, Pieza pieza, Jugador jugador)
        {
            Pieza p = null;
            if (jugador.Equals(partida.Jugador1))
            {
                p = (from Pieza rey in partida.Jugador1.Piezas
                     where rey is Rey && rey.Activa == true
                     select rey).FirstOrDefault();

            }
            else
            {
                p = (from Pieza rey in partida.Jugador2.Piezas
                     where rey is Rey && rey.Activa == true
                     select rey).FirstOrDefault();
            }

            if (pieza.PosicionInicial.Equals(ARRIBA) && pieza is Rey)
            {
                if (actual.Fila == 1 && destino.Fila == 1)
                {
                    if((actual.Columna + 2 == destino.Columna) || (actual.Columna - 2 == destino.Columna))
                    {
                        ((Rey)p).Enroque = true;

                    } else
                    {
                        ((Rey)p).Enroque = false;
                    }
                } else
                {
                    ((Rey)p).Enroque = false;
                }
            } else if(pieza.PosicionInicial.Equals(ABAJO) && pieza is Rey)
            {
                if (actual.Fila == 8 && destino.Fila == 8)
                {
                    if ((actual.Columna + 2 == destino.Columna) || (actual.Columna - 2 == destino.Columna))
                    {
                        ((Rey)p).Enroque = true;
                    } else
                    {
                        ((Rey)p).Enroque = false;
                    }
                } else
                {
                    ((Rey)p).Enroque = false;
                }
            } else
            {
                ((Rey)p).Enroque = false;
            }


        }

        public bool VerificarCoronacion(Peon pieza, Celda destino)
        {
            if(pieza.PosicionInicial.Equals(ARRIBA))
            {
                if(destino.Fila == 8)
                {
                    return true;
                } else
                {
                    return false;
                }
            } else if(pieza.PosicionInicial.Equals(ABAJO))
            {
                if (destino.Fila == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        public void FinalizarPorTablas()
        {
            partida.Tablas = true;
        }

    }
}