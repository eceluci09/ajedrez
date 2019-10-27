using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NEGOCIO
{
    public class Reina : Pieza
    {
        public override List<Celda> getCeldasDestino(Tablero tablero, Celda celdaActual)
        {
            celdasDisponibles.AddRange(base.PosiblesDestinos(tablero, celdaActual, 1, -1));
            celdasDisponibles.AddRange(base.PosiblesDestinos(tablero, celdaActual, 1, 0));
            celdasDisponibles.AddRange(base.PosiblesDestinos(tablero, celdaActual, 1, 1));
            celdasDisponibles.AddRange(base.PosiblesDestinos(tablero, celdaActual, 0, -1));
            celdasDisponibles.AddRange(base.PosiblesDestinos(tablero, celdaActual, 0, 1));
            celdasDisponibles.AddRange(base.PosiblesDestinos(tablero, celdaActual, -1, -1));
            celdasDisponibles.AddRange(base.PosiblesDestinos(tablero, celdaActual, -1, 0));
            celdasDisponibles.AddRange(base.PosiblesDestinos(tablero, celdaActual, -1, 1));

            return celdasDisponibles;
        }
    }
}