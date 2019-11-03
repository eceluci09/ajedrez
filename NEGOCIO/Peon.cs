using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace NEGOCIO
{
    public class Peon : Pieza
    {
        private Peon comerAlPaso;

        public Peon ComerAlPaso
        {
            get { return comerAlPaso; }
            set { comerAlPaso = value; }
        }

        public Peon(string posicionInicial, bool activa, Color color) : base(posicionInicial, activa, color)
        {
            this.posicionInicial = posicionInicial;
            this.activa = activa;
            this.color = color;
        }

        public override List<Celda> getCeldasDestino(Tablero tablero, Celda celdaActual)
        {
            celdasDisponibles.Clear();
            Movimiento movimiento = new Movimiento();
            movimiento.Horizontal = 0;
            movimiento.Vertical = -2;
            bool startPosition = false;
            if (tablero.getCelda(celdaActual, movimiento) == null)
            {
                startPosition = true;
            }

            movimiento.Horizontal = 0;
            movimiento.Vertical = 1;

            Celda celdaDestino = tablero.getCelda(celdaActual, movimiento);

            if (celdaDestino != null && celdaDestino.Pieza == null)
            {
                celdasDisponibles.Add(celdaDestino);

                if (startPosition)
                {
                    movimiento.Horizontal = 0;
                    movimiento.Vertical = 2;

                    celdaDestino = tablero.getCelda(celdaActual, movimiento);
                    if (celdaDestino != null && celdaDestino.Pieza == null)
                    {
                        celdasDisponibles.Add(celdaDestino);
                    }
                }
            }

            movimiento.Horizontal = -1;
            movimiento.Vertical = 1;

            celdaDestino = tablero.getCelda(celdaActual, movimiento);
            if (celdaDestino != null && celdaDestino.Pieza != null && celdaDestino.Pieza.Color != color)
            {
                celdasDisponibles.Add(celdaDestino);
            }


            movimiento.Horizontal = 1;
            movimiento.Vertical = 1;

            celdaDestino = tablero.getCelda(celdaActual, movimiento);
            if (celdaDestino != null && celdaDestino.Pieza != null && celdaDestino.Pieza.Color != color)
            {
                celdasDisponibles.Add(celdaDestino);
            }
            //Peon al paso

            if (comerAlPaso != null)
            {
                Celda celdaPeonAComer = tablero.getCelda(comerAlPaso);

                Movimiento movi = new Movimiento();
                movi.Horizontal = 0;
                movi.Vertical = -1;

                Celda celdaDisp = tablero.getCelda(celdaPeonAComer, movi);

                celdasDisponibles.Add(celdaDisp);

                comerAlPaso = null;
            }
            /*if ((this.PosicionInicial.Equals("ARRIBA") && celdaActual.Fila == 5) || (this.PosicionInicial.Equals("ABAJO") && celdaActual.Fila == 4))
            {
                movimiento.Horizontal = -1;
                movimiento.Vertical = 0;
                celdaHayPeonContrario = tablero.getCelda(celdaActual, movimiento);
            }
            if(celdaHayPeonContrario != null && celdaHayPeonContrario.Pieza!= null && celdaHayPeonContrario.Pieza is Peon && celdaHayPeonContrario.Pieza.Color != this.color)
            {
                celdasDisponibles.Add(celdaDestino);
            }

            if ((this.PosicionInicial.Equals("ARRIBA") && celdaActual.Fila == 5) || (this.PosicionInicial.Equals("ABAJO") && celdaActual.Fila == 4))
            {
                movimiento.Horizontal = 1;
                movimiento.Vertical = 0;
                celdaHayPeonContrario = tablero.getCelda(celdaActual, movimiento);
            }
            if (celdaHayPeonContrario != null && celdaHayPeonContrario.Pieza != null && celdaHayPeonContrario.Pieza is Peon && celdaHayPeonContrario.Pieza.Color != this.color)
            {
                celdasDisponibles.Add(celdaDestino);
            }*/

            return celdasDisponibles;

        }


        public List<Celda> getCeldasDestinoRey(Tablero tablero, Celda celdaActual)
        {
            celdasDisponibles.Clear();
            Movimiento movimiento = new Movimiento();
            movimiento.Horizontal = -1;
            movimiento.Vertical = 1;

            if(celdaActual == null)
            {

            }

            Celda celdaDestino = tablero.getCelda(celdaActual, movimiento);
            if (celdaDestino != null && celdaDestino.Pieza == null)
            {
                celdasDisponibles.Add(celdaDestino);
            }

            movimiento.Horizontal = 1;
            movimiento.Vertical = 1;

            celdaDestino = tablero.getCelda(celdaActual, movimiento);
            if (celdaDestino != null && celdaDestino.Pieza == null)
            {
                celdasDisponibles.Add(celdaDestino);
            }

            return celdasDisponibles;

        }

        public override List<Celda> getCeldasDestinoLuegoDeComer(Tablero tablero, Celda celdaActual)
        {
            throw new NotImplementedException();
        }
    }
}