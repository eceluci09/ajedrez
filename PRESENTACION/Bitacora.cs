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

        XmlDocument xml = new XmlDocument();

        public DataSet Leer()
        {
            DataSet ds = new DataSet();
            if (!File.Exists("D:\\bitacora.xml"))
            {
                DataTable tabla = new DataTable("BITACORA");
                tabla.Columns.Add("nombre");
                tabla.Columns.Add("apellido");
                tabla.Columns.Add("usuario");
                tabla.Columns.Add("tipo");
                tabla.Columns.Add("fecha");
                ds.Tables.Add(tabla);

                ds.WriteXml("D:\\bitacora.xml");
                ds.WriteXmlSchema("D:\\bitacoraEsquema.xml");


            }
            else
            {
                ds.ReadXml("D:\\bitacora.xml");
                ds.ReadXmlSchema("D:\\bitacoraEsquema.xml");

            }
            return ds;
        }

        public void Escribir(Jugador jugador, string tipo)
        {

        }



    }
}
