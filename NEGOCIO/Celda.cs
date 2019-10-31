using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEGOCIO
{
    public class Celda
    {
        private Color color;

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        private int fila;

        public int Fila
        {
            get { return fila; }
            set { fila = value; }
        }

        private int columna;

        public int Columna
        {
            get { return columna; }
            set { columna = value; }
        }

        private Pieza pieza;

        public Pieza Pieza
        {
            get { return pieza; }
            set { pieza = value; }
        }

    }
}
