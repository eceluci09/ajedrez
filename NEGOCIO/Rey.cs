using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace NEGOCIO
{
    public class Rey : Pieza, IMOVIMIENTOFIJO
    {
        public Rey(string posicionInicial, bool activa, Color color) : base(posicionInicial, activa, color)
        {
            this.posicionInicial = posicionInicial;
            this.activa = activa;
            this.color = color;
        }
        private List<Movimiento> movimientos;

        public List<Movimiento> Movimientos
        {
            get { return movimientos; }
            set { movimientos = value; }
        }

        private bool enroque = false;

        public bool Enroque
        {
            get { return enroque; }
            set { enroque = value; }
        }


        public List<Movimiento> cargarMovimientos()
        {
            movimientos.Add(new Movimiento() { Vertical = 1, Horizontal = -1 });
            movimientos.Add(new Movimiento() { Vertical = 1, Horizontal = 0 });
            movimientos.Add(new Movimiento() { Vertical = 1, Horizontal = 1 });
            movimientos.Add(new Movimiento() { Vertical = 0, Horizontal = -1 });
            movimientos.Add(new Movimiento() { Vertical = 0, Horizontal = 1 });
            movimientos.Add(new Movimiento() { Vertical = -1, Horizontal = -1 });
            movimientos.Add(new Movimiento() { Vertical = -1, Horizontal = 0 });
            movimientos.Add(new Movimiento() { Vertical = -1, Horizontal = 1 });
            //Enroque
            movimientos.Add(new Movimiento() { Vertical = 0, Horizontal = 2 });
            movimientos.Add(new Movimiento() { Vertical = 0, Horizontal = -2 });

            return movimientos;
        }

        public override List<Celda> getCeldasDestino(Tablero tablero, Celda celdaActual)
        {
            List<Movimiento> movDisp = tablero.VerificarEnroque(this, movimientos);
            movDisp = tablero.VerificarAmenaza(this, movDisp);
            return base.PosiblesDestinos(tablero, celdaActual, movDisp);
        }
    }
}