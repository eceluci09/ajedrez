using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ACCESOADATOS;

namespace NEGOCIO
{
    public class Partida
    {
        Acceso acceso = new Acceso();

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


        private List<Movimiento> movimientos = new List<Movimiento>();

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

        public void Iniciar(List<Jugador> jugadores)
        {
            jugador1 = jugadores[0];
            jugador2 = jugadores[1];

            this.activa = true;
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

            if(turnoAnterior != null)turnoAnterior.Activo = false;

            turnos.Add(turno);
        }

        public Jugador VerificarJugadorTurnoActual()
        {
            if (jugador1.Turno != null)
            {
                return jugador1;
            }
            else
            if (jugador2.Turno != null)
            {
                return jugador2;
            }
            return null;
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

        public int Alta()
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@idJugador1", jugador1.Id));
            parametros.Add(acceso.CrearParametro("@idJugador2", jugador2.Id));
            if (ganador != null)
            {
                parametros.Add(acceso.CrearParametro("@idJugador2", jugador2.Id));
            } else
            {
                parametros.Add(acceso.CrearParametro("@idGanador", null));
            }

            parametros.Add(acceso.CrearParametro("@tablas", this.tablas == true ? 1 : 0));
            parametros.Add(acceso.CrearParametro("@fecha", DateTime.Now));

            return acceso.Escribir("CREAR_PARTIDA", parametros);
                                 
        }
    }
}
