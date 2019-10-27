using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NEGOCIO
{
    public class Partida
    {
        private List<Turno> turnos = new List<Turno>();

        public List<Turno> Turnos
        {
            get { return turnos; }
            set { turnos = value; }
        }

        private Jugador jugador1;

        public Jugador Jugador1
        {
            get { return jugador1; }
            set { jugador1 = value; }
        }

        private Jugador jugador2;

        public Jugador Jugador2
        {
            get { return jugador2; }
            set { jugador2 = value; }
        }

        private Jugador ganador;

        public Jugador Ganador
        {
            get { return ganador; }
            set { ganador = value; }
        }


        private List<Movimiento> movimientos;

        public List<Movimiento> Movimientos
        {
            get { return movimientos; }
            set { movimientos = value; }
        }


        private bool activa;

        public bool Activa
        {
            get { return activa; }
            set { activa = value; }
        }

        private bool tablas = false;

        public bool Tablas
        {
            get { return tablas; }
            set { tablas = value; }
        }


        public void AsignarTurno()
        {
            Turno turnoAnterior = (from Turno tur in turnos
                                   where tur.Movimiento != null && tur.Activo == true
                                   select tur).FirstOrDefault();

            Turno turno = new Turno();

            if (turnoAnterior == null)
            {
                jugador1.Turno = turno;
            }
            else
            if (jugador1.Turno == null)
            {
                jugador1.Turno = turno;
                jugador2.Turno = null;
            }
            else
            {
                jugador2.Turno = turno;
                jugador1.Turno = null;
            }

            turnoAnterior.Activo = false;

            turnos.Add(turno);

        }

        public Turno VerificarTurnoActivo()
        {
            Turno actual = (from Turno tur in turnos
                            where tur.Activo == true
                            select tur).FirstOrDefault();

            return actual;
        }

        public Jugador VerificarJugadorTurnoActual(Turno actual)
        {
            if (jugador1.Turno != null && jugador1.Turno.Equals(actual))
            {
                return jugador1;
            }
            if (jugador2.Turno != null && jugador2.Turno.Equals(actual))
            {
                return jugador2;
            }
            return null;

        }

        public bool VerificarDobleTurno(Jugador jugador)
        {
            Pieza p = (from Pieza rey in jugador.Piezas
                 where rey is Rey && rey.Activa == true
                 select rey).FirstOrDefault();

            int cant = jugador.Turno.Movimiento.Count;

            if (cant > 0 && cant < 2 && ((Rey)p).Enroque == true)
            {
                return true;
            }

            return false;
        }

        public void RegistrarMovimiento(Celda actual, Celda destino)
        {
            Movimiento movimiento = new Movimiento();
            movimiento.CeldaOrigen = actual;
            movimiento.CeldaDestino = destino;
            movimiento.Pieza = destino.Pieza;
            Turno turnoActual = this.VerificarTurnoActivo();
            turnoActual.Movimiento.Add(movimiento);
            movimientos.Add(movimiento);
        }

        public void ChequearGanador(string tipo, Jugador jugActual)
        {
            if(tipo.Equals("JAQUE MATE"))
            {
                this.ganador = jugActual;
            }
        }

       

    }
}