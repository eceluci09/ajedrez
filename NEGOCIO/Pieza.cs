﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NEGOCIO
{
    public abstract class Pieza
    {
        protected string color;

        public string Color
        {
            get { return color; }
            set { color = value; }
        }

        protected List<Celda> celdasDisponibles;

        public List<Celda> CeldasDisponibles
        {
            get { return celdasDisponibles; }
            set { celdasDisponibles = value; }
        }

        private List<Celda> celdasDispJaqueRey;

        public List<Celda> CeldasDispJaqueRey
        {
            get { return celdasDispJaqueRey; }
            set { celdasDispJaqueRey = value; }
        }


        private string posicionInicial;

        public string PosicionInicial
        {
            get { return posicionInicial; }
            set { posicionInicial = value; }
        }

        private bool activa;

        public bool Activa
        {
            get { return activa; }
            set { activa = value; }
        }


        public List<Celda> PosiblesDestinos(Tablero tablero, Celda celdaActual, int incrementoVertical, int incrementoHorizontal)
        {
            List<Celda> cDisp = new List<Celda>();
            bool piezaOFinalTableroEncontrado = false;
            int horizontal = incrementoHorizontal;
            int vertical = incrementoVertical;
            Celda celdaDestino = null;

            while (!piezaOFinalTableroEncontrado)
            {
                Movimiento movimiento = new Movimiento();
                movimiento.Horizontal = horizontal;
                movimiento.Vertical = vertical;

                celdaDestino = tablero.getCelda(celdaActual, movimiento);

                if(tablero.VerificarCeldaDisponible(celdaDestino, color))
                {
                    cDisp.Add(celdaDestino);
                    celdasDisponibles.Add(celdaDestino);
                }

                piezaOFinalTableroEncontrado = (celdaDestino == null || celdaDestino.Pieza != null);
                horizontal += incrementoHorizontal;
                vertical += incrementoVertical;
            }
            //Verifica solo las celdas disponibles que apuntan al Rey contrario
            if(celdaDestino.Pieza != null && celdaDestino.Pieza is Rey && celdaDestino.Pieza.Color != this.color)
            {
                celdasDispJaqueRey = cDisp;
            }
            return cDisp;
        }

        public List<Celda> PosiblesDestinos(Tablero tablero, Celda celdaActual, List<Movimiento> movimientos)
        {
            List<Celda> cDisp = new List<Celda>();
            foreach (Movimiento movimiento in movimientos)
            {
                Celda celdaDestino = tablero.getCelda(celdaActual, movimiento);

                if (tablero.VerificarCeldaDisponible(celdaDestino, color))
                {
                    cDisp.Add(celdaDestino);
                    celdasDisponibles.Add(celdaDestino);
                }

                //Verifica solo las celdas disponibles que apuntan al Rey contrario
                if (celdaDestino.Pieza != null && celdaDestino.Pieza is Rey && celdaDestino.Pieza.Color != this.color)
                {
                    celdasDispJaqueRey.Add(celdaDestino);
                }

            }
            return cDisp;

        }

        public abstract List<Celda> getCeldasDestino(Tablero tablero, Celda celdaActual);

        public bool Mover(Tablero tablero, Celda celdaActual, Celda celdaDestino, Jugador jugador)
        {
            return tablero.ModificarPosicionPieza(celdaActual, celdaDestino, jugador);
        }

        public void getMovimientosPermitidosEnJaque(Tablero tablero, Jugador jugador)
        {
            bool coincide = false;
            if (jugador.PiezaJaque is Caballo)
            {
                celdasDisponibles.Clear();
            }
            else
            {
                foreach (Celda c in celdasDisponibles)
                {
                    coincide = false;
                    jugador.PiezaJaque.getCeldasDestino(tablero, tablero.getCelda(jugador.PiezaJaque));
                    foreach (Celda celDisp in jugador.PiezaJaque.celdasDispJaqueRey)
                    {
                        if (c.Equals(celDisp))
                        {
                            coincide = true;
                        }
                    }
                    if (!coincide && tablero.getCelda(jugador.PiezaJaque) != c)
                    {
                        celdasDisponibles.Remove(c);
                    }
                }
            }
        }

    }
}