using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NEGOCIO
{
    public class Movimiento
    {
        private Pieza pieza;

        public Pieza Pieza
        {
            get { return pieza; }
            set { pieza = value; }
        }

        private Tablero tablero;

        public Tablero Tablero
        {
            get { return tablero; }
            set { tablero = value; }
        }


        private int vertical;

        public int Vertical
        {
            get { return vertical; }
            set { vertical = value; }
        }

        private int horizontal;

        public int Horizontal
        {
            get { return horizontal; }
            set { horizontal = value; }
        }

        private Celda celdaOrigen;

        public Celda CeldaOrigen
        {
            get { return celdaOrigen; }
            set { celdaOrigen = value; }
        }

        private Celda celdaDestino;

        public Celda CeldaDestino
        {
            get { return celdaDestino; }
            set { celdaDestino = value; }
        }


    }
}