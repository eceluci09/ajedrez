using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ACCESOADATOS;

namespace NEGOCIO
{
    public class Jugador
    {
        Acceso acceso = new Acceso();
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private List<Pieza> piezas = new List<Pieza>();

        public List<Pieza> Piezas
        {
            get { return piezas; }
            set { piezas = value; }
        }

        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        private string apellido;

        public string Apellido
        {
            get { return apellido; }
            set { apellido = value; }
        }

        private Credencial credencial;

        public Credencial Credencial
        {
            get { return credencial; }
            set { credencial = value; }
        }


        private Turno turno;

        public Turno Turno
        {
            get { return turno; }
            set { turno = value; }
        }

        private int partidasGanadas;

        public int PartidasGanadas
        {
            get { return partidasGanadas; }
            set { partidasGanadas = value; }
        }

        private int partidasEmpatadas;

        public int PartidasEmpatadas
        {
            get { return partidasEmpatadas; }
            set { partidasEmpatadas = value; }
        }

        private int partidasPerdidas;

        public int PartidasPerdidas
        {
            get { return partidasPerdidas; }
            set { partidasPerdidas = value; }
        }

        private int tiempoJugado;

        public int TiempoJugado
        {
            get { return tiempoJugado; }
            set { tiempoJugado = value; }
        }






        private List<Partida> partidasJugadas = new List<Partida>();

        public List<Partida> PartidasJugadas
        {
            get { return partidasJugadas; }
            set { partidasJugadas = value; }
        }

        private List<Pieza> piezaJaque = new List<Pieza>();

        public List<Pieza> PiezaJaque
        {
            get { return piezaJaque; }
            set { piezaJaque = value; }
        }

        private List<Pieza> piezasCoronacion = new List<Pieza>();

        public List<Pieza> PiezasCoronacion
        {
            get { return piezasCoronacion; }
            set { piezasCoronacion = value; }
        }

        private bool pidioTablas = false;

        public bool PidioTablas
        {
            get { return pidioTablas; }
            set { pidioTablas = value; }
        }


        public void Mover(Pieza pieza, Tablero tablero, Celda celdaActual, Celda celdaDestino)
        {
            Pieza existePieza = (from Pieza p in piezas
                                 where p == pieza
                                 select p).FirstOrDefault();
            if (existePieza != null)
            {
                if(pieza.Mover(tablero, celdaActual, celdaDestino, this))
                {
                    piezasCoronacion.Clear();
                    Caballo caballo = new Caballo(pieza.PosicionInicial, true, pieza.Color);
                    piezasCoronacion.Add(caballo);
                    Reina reina = new Reina(pieza.PosicionInicial, true, pieza.Color);
                    piezasCoronacion.Add(reina);
                    Alfil alfil = new Alfil(pieza.PosicionInicial, true, pieza.Color);
                    piezasCoronacion.Add(alfil);
                    Torre torre = new Torre(pieza.PosicionInicial, true, pieza.Color);
                    piezasCoronacion.Add(torre);
                }
                
            }
        }

        public void SeleccionarPiezaCorona(Tablero tablero, Pieza pieza, Celda destino)
        {
            tablero.IntercambiarPieza(pieza, destino, this);
        }

        public void OfrecerTablas()
        {
            this.pidioTablas = true;
        }

        public void AceptarTablas(Tablero tablero)
        {
            tablero.FinalizarPorTablas();
        }

       public int Alta()
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@nombre", this.nombre));
            parametros.Add(acceso.CrearParametro("@apellido", this.apellido));
            parametros.Add(acceso.CrearParametro("@username", this.credencial.Username));
            parametros.Add(acceso.CrearParametro("@contraseña", this.credencial.Contraseña));

            return acceso.Escribir("CREAR_JUGADOR", parametros);
        }

        public int ActualizarTiempoJuego(int tiempoJugado)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@idJugador", this.id));
            parametros.Add(acceso.CrearParametro("@tiempo", tiempoJugado));

            return acceso.Escribir("ACTUALIZAR_TIEMPO_JUEGO", parametros);
        }
        public int ActualizarPartidasGanadas()
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@idJugador", this.id));

            return acceso.Escribir("ACTUALIZAR_PARTIDAS_GANADAS", parametros);
        }

        public int ActualizarPartidasEmpatadas()
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@idJugador", this.id));

            return acceso.Escribir("ACTUALIZAR_PARTIDAS_EMPATADAS", parametros);
        }

        public int ActualizarPartidasPerdidas()
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@idJugador", this.id));

            return acceso.Escribir("ACTUALIZAR_PARTIDAS_PERDIDAS", parametros);
        }

        public bool ValidarUsuario()
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@username", this.credencial.Username));

            return acceso.Validar("VALIDAR_USUARIO", parametros);
        }

        public Jugador Login()
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@username", this.credencial.Username));
            parametros.Add(acceso.CrearParametro("@contraseña", this.credencial.Contraseña));

            DataTable tabla = acceso.Leer("VALIDAR_LOGIN", parametros);

            if(tabla.Rows.Count > 0)
            {
                DataRow registro = tabla.Rows[0];
                this.id = int.Parse(registro["idJugador"].ToString());
                this.nombre = registro["nombre"].ToString();
                this.apellido = registro["apellido"].ToString();

                return this;
            }

            return null;
        }

        public static List<Jugador> Listar()
        {
            Acceso acceso = new Acceso();
            List<Jugador> jugadores = new List<Jugador>();
            DataTable tabla = acceso.Leer("LEER_JUGADOR", null);

            foreach(DataRow registro in tabla.Rows)
            {
                Jugador jugador = new Jugador();
                jugador.id = int.Parse(registro["idJugador"].ToString());
                jugador.nombre = registro["nombre"].ToString();
                jugador.apellido = registro["apellido"].ToString();
                jugador.partidasGanadas = int.Parse(registro["cantPartidasGanadas"].ToString());
                jugador.partidasEmpatadas = int.Parse(registro["cantPartidasEmpatadas"].ToString());
                jugador.partidasPerdidas = int.Parse(registro["cantPartidasPerdidas"].ToString());
                jugador.tiempoJugado = int.Parse(registro["minutosJugados"].ToString());

                jugadores.Add(jugador);
            }

            return jugadores;
        }

        public override string ToString()
        {
            return nombre.ToString() + " " + apellido.ToString();
        }







    }
}
