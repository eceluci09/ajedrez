using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace NEGOCIO
{
    public abstract class Pieza
    {
        static int increment = 0;
        public Pieza(string posicionInicial, bool activa, Color color)
        {
            this.posicionInicial = posicionInicial;
            this.activa = activa;
            this.color = color;
            nro = increment++;
        }

        private int nro;

        public int Nro
        {
            get { return nro; }
            set { nro = value; }
        }


        protected Color color;

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        protected List<Celda> celdasDisponibles = new List<Celda>();

        public List<Celda> CeldasDisponibles
        {
            get { return celdasDisponibles; }
            set { celdasDisponibles = value; }
        }

        private List<Celda> celdasDispJaqueRey = new List<Celda>();

        public List<Celda> CeldasDispJaqueRey
        {
            get { return celdasDispJaqueRey; }
            set { celdasDispJaqueRey = value; }
        }


        protected string posicionInicial;

        public string PosicionInicial
        {
            get { return posicionInicial; }
            set { posicionInicial = value; }
        }

        protected bool activa;

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
            if(celdaDestino != null && celdaDestino.Pieza != null && celdaDestino.Pieza is Rey && celdaDestino.Pieza.Color != this.color)
            {
                celdasDispJaqueRey = cDisp;
            }
            return cDisp;
        }

        public List<Celda> PosiblesDestinosLuegoDeComer(Tablero tablero, Celda celdaActual, int incrementoVertical, int incrementoHorizontal)
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

                if (tablero.VerificarCeldaDisponibleLuegoDeComer(celdaDestino, color))
                {
                    cDisp.Add(celdaDestino);
                    celdasDisponibles.Add(celdaDestino);
                }

                piezaOFinalTableroEncontrado = (celdaDestino == null || celdaDestino.Pieza != null);
                horizontal += incrementoHorizontal;
                vertical += incrementoVertical;
            }
            //Verifica solo las celdas disponibles que apuntan al Rey contrario
            if (celdaDestino != null && celdaDestino.Pieza != null && celdaDestino.Pieza is Rey && celdaDestino.Pieza.Color != this.color)
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
                if (celdaDestino != null && celdaDestino.Pieza != null && celdaDestino.Pieza is Rey && celdaDestino.Pieza.Color != this.color)
                {
                    celdasDispJaqueRey.Add(celdaDestino);
                }

            }
            this.celdasDisponibles = cDisp;
            return cDisp;

        }

        public List<Celda> PosiblesDestinosLuegoDeComer(Tablero tablero, Celda celdaActual, List<Movimiento> movimientos)
        {
            List<Celda> cDisp = new List<Celda>();
            foreach (Movimiento movimiento in movimientos)
            {
                Celda celdaDestino = tablero.getCelda(celdaActual, movimiento);

                if (tablero.VerificarCeldaDisponibleLuegoDeComer(celdaDestino, color))
                {
                    cDisp.Add(celdaDestino);
                    celdasDisponibles.Add(celdaDestino);
                }

                //Verifica solo las celdas disponibles que apuntan al Rey contrario
                if (celdaDestino != null && celdaDestino.Pieza != null && celdaDestino.Pieza is Rey && celdaDestino.Pieza.Color != this.color)
                {
                    celdasDispJaqueRey.Add(celdaDestino);
                }

            }
            this.celdasDisponibles = cDisp;
            return cDisp;

        }

        public abstract List<Celda> getCeldasDestino(Tablero tablero, Celda celdaActual);
        public abstract List<Celda> getCeldasDestinoLuegoDeComer(Tablero tablero, Celda celdaActual);

        public bool Mover(Tablero tablero, Celda celdaActual, Celda celdaDestino, Jugador jugador)
        {
            return tablero.ModificarPosicionPieza(celdaActual, celdaDestino, jugador);
        }

        public List<Celda> getMovimientosPermitidosEnJaque(Tablero tablero, Jugador jugador)
        {
            bool coincide = false;
            List<Celda> celdasARemover = new List<Celda>();

                foreach (Celda c in celdasDisponibles)
                {
                    coincide = false;
                    foreach(Pieza piezaJaque in jugador.PiezaJaque)
                    {
                        piezaJaque.getCeldasDestino(tablero, tablero.getCelda(piezaJaque));

                        foreach (Celda celDisp in piezaJaque.celdasDispJaqueRey)
                        {
                            if (c.Equals(celDisp))
                            {
                                coincide = true;
                            }
                        }
                        if (!coincide && tablero.getCelda(piezaJaque) != c)
                        {
                            celdasARemover.Add(c);
                        }
                    }
                    
                }

            foreach (Celda c in celdasARemover)
            {
                celdasDisponibles.Remove(c);
            }

            return celdasDisponibles;
        }


    }
}
