using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ACCESOADATOS
{
    public class Acceso
    {
        SqlConnection conexion;

        public void Abrir()
        {
            conexion = new SqlConnection();
            conexion.ConnectionString = "initial Catalog=Ajedrez; Integrated Security=SSPI; Data source=localhost";
            conexion.Open();
        }

        public void Cerrar()
        {
            conexion.Close();
            conexion.Dispose();
            conexion = null;
            GC.Collect();
        }

        private SqlCommand CrearComando(string nombre, List<SqlParameter> parametros)
        {
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = nombre;
            if (parametros != null && parametros.Count > 0)
            {
                comando.Parameters.AddRange(parametros.ToArray());
            }

            return comando;
        }


        public DataTable Leer(string nombre, List<SqlParameter> parametros)
        {
            Abrir();
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = CrearComando(nombre, parametros);

            DataTable tabla = new DataTable();

            adaptador.Fill(tabla);

            adaptador = null;
            Cerrar();
            return tabla;
        }

        public int Escribir(string nombre, List<SqlParameter> parametros)
        {
            Abrir();
            SqlCommand comando = CrearComando(nombre, parametros);
            int filas = 0;

            try
            {
                filas = comando.ExecuteNonQuery();
            }
            catch(Exception ex)
            { filas = -1; }
            Cerrar();
            return filas;
        }

        public int ValidarPartida(string nombre, List<SqlParameter> parametros)
        {
            Abrir();
            SqlCommand comando = CrearComando(nombre, parametros);
            int idUltimo = 0;

            try
            {
                idUltimo = int.Parse(comando.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {  }
            Cerrar();
            return idUltimo + 1;
        }

        public bool Validar(string nombre, List<SqlParameter> parametros)
        {
            Abrir();
            SqlCommand comando = CrearComando(nombre, parametros);
            bool existe = false;

            try
            {
                existe = int.Parse(comando.ExecuteScalar().ToString()) == 1;
            }
            catch(Exception ex)
            {
                existe = false;
            }
            Cerrar();
            return existe;
        }

        public SqlParameter CrearParametro(string nombre, DateTime valor)
        {
            SqlParameter parametro = new SqlParameter();
            parametro.ParameterName = nombre;
            parametro.Value = valor;
            parametro.DbType = DbType.DateTime;

            return parametro;
        }
        public SqlParameter CrearParametro(string nombre, int valor)
        {
            SqlParameter parametro = new SqlParameter();
            parametro.ParameterName = nombre;
            parametro.Value = valor;
            parametro.DbType = DbType.Int32;

            return parametro;
        }

        public SqlParameter CrearParametro(string nombre, float valor)
        {
            SqlParameter parametro = new SqlParameter();
            parametro.ParameterName = nombre;
            parametro.Value = valor;
            parametro.DbType = DbType.Single;

            return parametro;
        }
        public SqlParameter CrearParametro(string nombre, string valor)
        {
            SqlParameter parametro = new SqlParameter();
            parametro.ParameterName = nombre;
            parametro.Value = valor;
            parametro.DbType = DbType.String;

            return parametro;
        }
    }
}
