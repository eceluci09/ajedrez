﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace NEGOCIO
{
    public class Torre : Pieza
    {
        public Torre(string posicionInicial, bool activa, Color color) : base(posicionInicial, activa, color)
        {
            this.posicionInicial = posicionInicial;
            this.activa = activa;
            this.color = color;
        }

        public override List<Celda> getCeldasDestino(Tablero tablero, Celda celdaActual)
        {
            celdasDisponibles.Clear();
            Celda celda = this.VerificarSiPuedeEnrocar(tablero);
            if (celda != null)
            {
                celdasDisponibles.Add(celda);
            }
            else
            {
                celdasDisponibles.AddRange(base.PosiblesDestinos(tablero, celdaActual, 0, -1));
                celdasDisponibles.AddRange(base.PosiblesDestinos(tablero, celdaActual, 0, 1));
                celdasDisponibles.AddRange(base.PosiblesDestinos(tablero, celdaActual, 1, 0));
                celdasDisponibles.AddRange(base.PosiblesDestinos(tablero, celdaActual, -1, 0));
            }

            return celdasDisponibles;
        }

        public override List<Celda> getCeldasDestinoLuegoDeComer(Tablero tablero, Celda celdaActual)
        {
            celdasDisponibles.Clear();
            celdasDisponibles.AddRange(base.PosiblesDestinosLuegoDeComer(tablero, celdaActual, 0, -1));
            celdasDisponibles.AddRange(base.PosiblesDestinosLuegoDeComer(tablero, celdaActual, 0, 1));
            celdasDisponibles.AddRange(base.PosiblesDestinosLuegoDeComer(tablero, celdaActual, 1, 0));
            celdasDisponibles.AddRange(base.PosiblesDestinosLuegoDeComer(tablero, celdaActual, -1, 0));

            return celdasDisponibles;
        }

        public Celda VerificarSiPuedeEnrocar(Tablero tablero)
        {
            Celda c = tablero.getCelda(this);

            Movimiento movimientoTorre = (from Movimiento mt in tablero.Partida.Movimientos
                                          where mt.Pieza == c.Pieza
                                          select mt).FirstOrDefault();
            Movimiento movimiento = new Movimiento();
            movimiento.Horizontal = 0;
            movimiento.Vertical = -1;
            bool startPosition = false;
            if (tablero.getCelda(c, movimiento) == null && movimientoTorre == null)
            {
                startPosition = true;
            }

            if(startPosition)
            {
                movimiento.Vertical = 0;
                movimiento.Horizontal = 2;

                Celda cel = tablero.getCelda(c, movimiento);

                List<Movimiento> movimientoRey = null;

                if (cel != null && cel.Pieza is Rey)
                {
                    movimientoRey = (from Movimiento m in tablero.Partida.Movimientos
                                                where m.Pieza == cel.Pieza
                                                select m).ToList();
                }


                if (cel != null)
                {

                    if (cel.Pieza != null && cel.Pieza is Rey && cel.Pieza.Color == this.color && movimientoRey.Count == 1)
                    {
                        movimiento.Horizontal = 3;
                        cel = tablero.getCelda(c, movimiento);
                        if (cel.Pieza == null)
                        {
                            return cel;
                        }
                    }
                } else
                {
                    movimiento.Vertical = 0;
                    movimiento.Horizontal = -1;

                    cel = tablero.getCelda(c, movimiento);

                    if (cel != null && cel.Pieza is Rey)
                    {
                        movimientoRey = (from Movimiento m in tablero.Partida.Movimientos
                                         where m.Pieza == cel.Pieza
                                         select m).ToList();
                    }

                    if (cel.Pieza != null && cel.Pieza is Rey && cel.Pieza.Color == this.color && movimientoRey.Count == 1)
                    {
                        movimiento.Horizontal = -2;
                        cel = tablero.getCelda(c, movimiento);
                        if (cel.Pieza == null)
                        {
                            return cel;
                        }
                    }
                }

            }

            return null;
        }
    }
}