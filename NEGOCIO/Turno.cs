using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NEGOCIO
{
    public class Turno
    {
        static int increment = 0;
        public Turno()
        {
            nro = ++increment;
            activo = true;
        }
        private int nro;

        public int Nro
        {
            get { return nro; }
            set { nro = value; }
        }


        private List<Movimiento> movimientos = new List<Movimiento>();

        public List<Movimiento> Movimiento
        {
            get { return movimientos; }
            set { movimientos = value; }
        }

        private bool activo;

        public bool Activo
        {
            get { return activo; }
            set { activo = value; }
        }



    }
}