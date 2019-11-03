using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace NEGOCIO
{
    public class Caballo : Pieza, IMOVIMIENTOFIJO
    {
        private List<Movimiento> movimientos = new List<Movimiento>();

        public Caballo(string posicionInicial, bool activa, Color color) : base(posicionInicial, activa, color)
        {
            this.posicionInicial = posicionInicial;
            this.activa = activa;
            this.color = color;
            this.cargarMovimientos();
        }

        public List<Movimiento> Movimientos
        {
            get { return movimientos; }
            set { movimientos = value; }
        }

        public List<Movimiento> cargarMovimientos()
        {
            movimientos.Add(new Movimiento { Vertical = 1, Horizontal = -2 });
            movimientos.Add(new Movimiento { Vertical = 2, Horizontal = -1 });
            movimientos.Add(new Movimiento { Vertical = 2, Horizontal = 1 });
            movimientos.Add(new Movimiento { Vertical = 1, Horizontal = 2 });
            movimientos.Add(new Movimiento { Vertical = -1, Horizontal = -2 });
            movimientos.Add(new Movimiento { Vertical = -2, Horizontal = -1 });
            movimientos.Add(new Movimiento { Vertical = -2, Horizontal = 1 });
            movimientos.Add(new Movimiento { Vertical = -1, Horizontal = 2 });

            return movimientos;
        }

        public override List<Celda> getCeldasDestino(Tablero tablero, Celda celdaActual)
        {
            return base.PosiblesDestinos(tablero, celdaActual, movimientos);
        }

        public override List<Celda> getCeldasDestinoLuegoDeComer(Tablero tablero, Celda celdaActual)
        {
            return base.PosiblesDestinosLuegoDeComer(tablero, celdaActual, movimientos);
        }
    }
}