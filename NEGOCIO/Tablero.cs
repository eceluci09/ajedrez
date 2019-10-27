using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NEGOCIO
{
    public class Tablero
    {
        private static string ARRIBA = "ARRIBA";
        private static string ABAJO = "ABAJO";


        private List<Pieza> piezas;

        public List<Pieza> Piezas
        {
            get { return piezas; }
            set { piezas = value; }
        }

        private List<Celda> celdas;

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


        public Celda getCelda(Pieza pieza)
        {
            Celda celda = (from Celda cel in celdas
                           where cel.Pieza == pieza && pieza.Activa == true
                           select cel).FirstOrDefault();


           return celda;
        }

        public Celda getCelda(Celda celdaActual, Movimiento movimiento)
        {
            Pieza pieza = celdaActual.Pieza;
            if (pieza == null) return null;

            int horizontal = celdaActual.Columna + movimiento.Horizontal;
            int vertical = (pieza.PosicionInicial.Equals(ARRIBA) ? celdaActual.Fila - movimiento.Vertical : celdaActual.Fila + movimiento.Vertical);
            Celda celda = (from Celda cel in celdas
                           where cel.Columna == horizontal && cel.Fila == vertical
                           select cel).FirstOrDefault();

            return celda;

        }

        public bool VerificarCeldaDisponible(Celda celda)
        {
            if (celda == null) return false;

            return (celda.Pieza == null);
        }

        public bool VerificarCeldaDisponible(Celda celda, string color)
        {
            if (celda == null) return false;

            return (celda.Pieza == null || celda.Pieza.Color != color);
        }

        public bool ModificarPosicionPieza(Celda actual, Celda destino, Jugador jug)
        {
            bool coronacion = false;
            Pieza pieza = actual.Pieza;
            actual.Pieza = null;
            Jugador jugadorRival = null;
            Pieza piezaComida = null;
            if (destino.Pieza != null)
            { 
                if (jug.Equals(partida.Jugador1))
                {
                    piezaComida = (from Pieza p in partida.Jugador2.Piezas
                                   where p.Equals(destino.Pieza)
                                   select p).FirstOrDefault();

                    jugadorRival = partida.Jugador2;
                } else
                {
                    piezaComida = (from Pieza p in partida.Jugador1.Piezas
                                         where p.Equals(destino.Pieza)
                                         select p).FirstOrDefault();

                    jugadorRival = partida.Jugador1;
                }

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
            string tipo = (!coronacion) ? this.verificarJaqueOJaqueMate(jug, pieza) : string.Empty;
            if (tipo.Equals("JAQUE")) jugadorRival.PiezaJaque = pieza;

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
                        celdaAMover = this.getCelda(celdaActual, mov);
                    }
                    else
                    {
                        celdaAMover = this.getCelda(celdaAMover, mov);
                    }


                    if (!this.VerificarCeldaDisponible(celdaAMover))
                    {
                        removerMov1 = true;
                    }
                }

                for (int i = 0; i < 3; i++)
                {
                    mov.Vertical = 0;
                    mov.Horizontal = -1;

                    if (celdaAMover == null)
                    {
                        celdaAMover = this.getCelda(celdaActual, mov);
                    }
                    else
                    {
                        celdaAMover = this.getCelda(celdaAMover, mov);
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
                                celdaAMover = this.getCelda(celdaActual, movimientoARealizar);
                            }
                            else
                            {
                                celdaAMover = this.getCelda(celdaAMover, movimientoARealizar);
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
                                celdaAMover = this.getCelda(celdaActual, movimientoARealizar);
                            }
                            else
                            {
                                celdaAMover = this.getCelda(celdaAMover, movimientoARealizar);
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

            Movimiento movimientoTorre = (from Movimiento mt in partida.Movimientos
                                          where mt.Pieza == c.Pieza
                                          select mt).FirstOrDefault();

            if (movimientoRey != null || movimientoTorre != null)
            {
                movimientos.Remove(movAEliminar);
            }

            return movimientos;
        }

        private string verificarJaqueOJaqueMate(Jugador jugUltMov, Pieza piezaUltMov)
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
                reyContrario = (from Pieza rey in partida.Jugador2.Piezas
                                      where rey is Rey && rey.Activa == true
                                      select rey).FirstOrDefault();
            }

            if (reyContrario != null)
            {
                //Verificar jaque mate

                List<Celda> celdasAMoverseProxMov = piezaUltMov.getCeldasDestino(this, this.getCelda(piezaUltMov));

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
                }
            }

            return tipo;
            
        }

        public List<Movimiento> VerificarAmenaza(Rey pieza, List<Movimiento> movimientos)
        {
            Celda celdaActual = this.getCelda(pieza);
            Celda celdaAMover = null;

            foreach (Movimiento mov in movimientos)
            {
                foreach (Pieza p in piezas)
                {
                    if (p.Color != pieza.Color && pieza.Activa == true)
                    {
                        celdaAMover = this.getCelda(celdaActual, mov);
                        foreach (Celda celdaDisp in p.getCeldasDestino(this, this.getCelda(p)))
                        {
                            if (celdaAMover.Equals(celdaDisp))
                            {
                                if(movimientos.Contains(mov))
                                {
                                    movimientos.Remove(mov);
                                }
                                
                            }
                        }
                    }
                }
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
                if (actual.Fila == 8 && destino.Fila == 8)
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
                if (actual.Fila == 1 && destino.Fila == 1)
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
                if(destino.Fila == 1)
                {
                    return true;
                } else
                {
                    return false;
                }
            } else if(pieza.PosicionInicial.Equals(ABAJO))
            {
                if (destino.Fila == 8)
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