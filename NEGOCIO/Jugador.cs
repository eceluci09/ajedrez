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
                    bool caballoElegido = false;
                    bool alfilElegido = false;
                    bool torreElegida = false;
                    bool damaElegida = false;
                    //Saco los peones y el rey como piezas coronadoras
                    foreach (Pieza piezaCorona in piezas)
                    {
                        if(piezaCorona is Caballo && !caballoElegido)
                        {
                            caballoElegido = true;
                            piezasCoronacion.Add(piezaCorona);
                        }
                        if (piezaCorona is Alfil && !alfilElegido)
                        {
                            alfilElegido = true;
                            piezasCoronacion.Add(piezaCorona);
                        }
                        if (piezaCorona is Torre && !torreElegida)
                        {
                            torreElegida = true;
                            piezasCoronacion.Add(piezaCorona);
                        }
                        if (piezaCorona is Reina && !damaElegida)
                        {
                            damaElegida = true;
                            piezasCoronacion.Add(piezaCorona);
                        }
                    }
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

        public int ActualizarPartidasGanadas()
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@idJugador", this.id));

            return acceso.Escribir("ACTUALIZAR_PARTIDAS_GANADAS", parametros);
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
                this.nombre = registro["nombre"].ToString();
                this.apellido = registro["apellido"].ToString();

                return this;
            }

            return null;
        }







    }
}
