using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace NEGOCIO
{
    public class Peon : Pieza
    {
        public Peon(string posicionInicial, bool activa, Color color) : base(posicionInicial, activa, color)
        {
            this.posicionInicial = posicionInicial;
            this.activa = activa;
            this.color = color;
        }

        public override List<Celda> getCeldasDestino(Tablero tablero, Celda celdaActual)
        {
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
            Celda celdaHayPeonContrario = null;
            //Peon al paso
            if ((this.PosicionInicial.Equals("ARRIBA") && celdaActual.Fila == 4) || (this.PosicionInicial.Equals("ABAJO") && celdaActual.Fila == 5))
            {
                movimiento.Horizontal = -1;
                movimiento.Vertical = 0;
                celdaHayPeonContrario = tablero.getCelda(celdaActual, movimiento);
            }
            if(celdaHayPeonContrario != null && celdaHayPeonContrario.Pieza!= null && celdaHayPeonContrario.Pieza is Peon && celdaHayPeonContrario.Pieza.Color != this.color)
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
            if ((this.PosicionInicial.Equals("ARRIBA") && celdaActual.Fila == 4) || (this.PosicionInicial.Equals("ABAJO") && celdaActual.Fila == 5))
            {
                movimiento.Horizontal = 1;
                movimiento.Vertical = 0;
                celdaHayPeonContrario = tablero.getCelda(celdaActual, movimiento);
            }
            if (celdaHayPeonContrario != null && celdaHayPeonContrario.Pieza != null && celdaHayPeonContrario.Pieza is Peon && celdaHayPeonContrario.Pieza.Color != this.color)
            {
                celdasDisponibles.Add(celdaDestino);
            }

            return celdasDisponibles;

        }
    }
}