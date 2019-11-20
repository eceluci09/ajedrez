using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Data;
using NEGOCIO;
using System.IO;

namespace PRESENTACION
{
    public class Bitacora
    {
        /*private static string PATH_BITACORA = "D:\\bitacora.xml";
        private static string PATH_BITACORA_ESQUEMA = "D:\\bitacoraEsquema.xml";
        private static string PATH_MOVIMIENTOS = "D:\\movimientos.xml";
        private static string PATH_MOVIMIENTOS_ESQUEMA = "D:\\movimientosEsquema.xml";
        private static string PATH_BITACORA2 = "D:\\bitacora2.xml";
        private static string PATH_BITACORA_ESQUEMA2 = "D:\\bitacoraEsquema2.xml";
        */

        private static string PATH_BITACORA = @"C:\Users\user\Desktop\facu\bitacora.xml";
        private static string PATH_BITACORA_ESQUEMA = @"C:\Users\user\Desktop\facu\bitacoraEsquema.xml";
        private static string PATH_MOVIMIENTOS = @"C:\Users\user\Desktop\facu\movimientos.xml";
        private static string PATH_MOVIMIENTOS_ESQUEMA = @"C:\Users\user\Desktop\facu\movimientosEsquema.xml";
        private static string PATH_BITACORA2 = @"C:\Users\user\Desktop\facu\bitacora2.xml";
        private static string PATH_BITACORA_ESQUEMA2 = @"C:\Users\user\Desktop\facu\bitacoraEsquema2.xml";


        XmlDocument xml = new XmlDocument();

        public XmlNode Load()
        {
            xml.Load(PATH_BITACORA);
            return xml.DocumentElement;
        }

        public XmlNode LoadIF()
        {
            xml.Load(PATH_BITACORA2);
            return xml.DocumentElement;
        }
        public XmlNode LoadM()
        {
            xml.Load(PATH_MOVIMIENTOS);
            return xml.DocumentElement;
        }

        public XmlNode MostrarPrimero(XmlNode nodoActual)
        {
            if (nodoActual.HasChildNodes)
            {
                return nodoActual.FirstChild;
            }
            return nodoActual;
        }

        public XmlNode MostrarUltimo(XmlNode nodoActual)
        {
            if (nodoActual.HasChildNodes)
            {
                return nodoActual.LastChild;
            }
            return nodoActual;
        }

        public XmlNode MostrarSiguiente(XmlNode nodoActual)
        {
            try
            {
                if(nodoActual.NextSibling != null)
                {
                    return nodoActual.NextSibling;
                }
            }
            catch
            { }
            return nodoActual;
        }

        public XmlNode MostrarAnterior(XmlNode nodoActual)
        {
            try
            {
                if (nodoActual.PreviousSibling != null)
                {
                    return nodoActual.PreviousSibling;
                }
            }
            catch
            { }
            return nodoActual;
        }

        public XmlNode MostrarPadre(XmlNode nodoActual)
        {
            try
            {
                if(nodoActual.ParentNode != null)
                {
                    return nodoActual.ParentNode;
                }
               
            }
            catch
            { }
            return nodoActual;
        }
        public DataSet Leer()
        {
            DataSet ds = new DataSet();
            if (!File.Exists(PATH_BITACORA))
            {
                DataTable tabla = new DataTable("BITACORA");
                tabla.Columns.Add("nombre");
                tabla.Columns.Add("apellido");
                tabla.Columns.Add("usuario");
                tabla.Columns.Add("tipo");
                tabla.Columns.Add("fecha");
                ds.Tables.Add(tabla);

                ds.WriteXml(PATH_BITACORA);
                ds.WriteXmlSchema(PATH_BITACORA_ESQUEMA);


            } else {
                ds.ReadXml(PATH_BITACORA);
                ds.ReadXmlSchema(PATH_BITACORA_ESQUEMA);
            }
            

            
            return ds;
        }

        public void Escribir(Jugador jugador, string tipo)
        {

            DataSet ds = Leer();

            DataRow registro = ds.Tables[0].NewRow();

            registro["nombre"] = jugador.Nombre;
            registro["apellido"] = jugador.Apellido;
            registro["usuario"] = jugador.Credencial.Username;
            registro["tipo"] = tipo;
            registro["fecha"] = DateTime.Now;

            ds.Tables[0].Rows.Add(registro);

            ds.WriteXml(PATH_BITACORA);
            ds.WriteXmlSchema(PATH_BITACORA_ESQUEMA);
        }

        public void Escribir(Partida partida, Jugador jugador, Pieza pieza, Celda celda)
        {
            DataSet ds = LeerM();

            DataRow registro = ds.Tables[0].NewRow();

            registro["partida"] = partida.Id;
            registro["jugador"] = jugador.Credencial.Username;
            registro["pieza"] = pieza.GetType().Name.ToString() + " " + pieza.Color.Name;
            registro["celda"] = "Fila: " + celda.Fila.ToString() + " Columna: " + celda.Columna.ToString();
            registro["fecha"] = DateTime.Now;

            ds.Tables[0].Rows.Add(registro);

            ds.WriteXml(PATH_MOVIMIENTOS);
            ds.WriteXmlSchema(PATH_MOVIMIENTOS_ESQUEMA);
        }

        public DataSet LeerM()
        {
            DataSet ds = new DataSet();
            if (!File.Exists(PATH_MOVIMIENTOS))
            {
                DataTable tabla = new DataTable("MOVIMIENTOS");
                tabla.Columns.Add("partida");
                tabla.Columns.Add("jugador");
                tabla.Columns.Add("pieza");
                tabla.Columns.Add("celda");
                tabla.Columns.Add("fecha");
                ds.Tables.Add(tabla);

                ds.WriteXml(PATH_MOVIMIENTOS);
                ds.WriteXmlSchema(PATH_MOVIMIENTOS_ESQUEMA);
            }
            else
            {
                ds.ReadXml(PATH_MOVIMIENTOS);
                ds.ReadXmlSchema(PATH_MOVIMIENTOS_ESQUEMA);
            }
            return ds;
        }

        public void Escribir(Partida partida, string tipo)
        {
            DataSet ds = LeerIF();

            DataRow registro = ds.Tables[0].NewRow();

            registro["partida"] = partida.Id;
            registro["tipo"] = tipo;
            registro["fecha"] = DateTime.Now;

            ds.Tables[0].Rows.Add(registro);

            ds.WriteXml(PATH_BITACORA2);
            ds.WriteXmlSchema(PATH_BITACORA_ESQUEMA2);
        }

        public DataSet LeerIF()
        {
            DataSet ds = new DataSet();
            if (!File.Exists(PATH_MOVIMIENTOS))
            {
                DataTable tabla = new DataTable("REGISTRO");
                tabla.Columns.Add("partida");
                tabla.Columns.Add("tipo");
                tabla.Columns.Add("fecha");
                ds.Tables.Add(tabla);

                ds.WriteXml(PATH_BITACORA2);
                ds.WriteXmlSchema(PATH_BITACORA_ESQUEMA2);
            }
            else
            {
                ds.ReadXml(PATH_BITACORA2);
                ds.ReadXmlSchema(PATH_BITACORA_ESQUEMA2);
            }
            return ds;
        }
    }
}
