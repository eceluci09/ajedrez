using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace PRESENTACION
{
    public partial class frmBitacora : Form
    {
        public frmBitacora()
        {
            InitializeComponent();
        }

        Bitacora bitacora = new Bitacora();
        XmlNode nodoActual;

        private void Button1_Click(object sender, EventArgs e)
        {
            DataSet ds = bitacora.Leer();

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = ds.Tables[0];

            nodoActual = bitacora.Load();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DataSet ds = bitacora.LeerIF();

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = ds.Tables[0];

            nodoActual = bitacora.LoadIF();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            DataSet ds = bitacora.LeerM();

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = ds.Tables[0];

            nodoActual = bitacora.LoadM();
        }

        private void Mostrar(XmlNode nodoActual)
        {
            label1.Text = nodoActual.Value;
            label2.Text = nodoActual.InnerText;
            label3.Text = nodoActual.InnerXml;
            label4.Text = "Tiene hijos " + nodoActual.HasChildNodes.ToString();
            label5.Text = "Tipo de nodo " + nodoActual.NodeType.ToString();
            label6.Text = nodoActual.Name;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if(nodoActual != null)
            {
                nodoActual = bitacora.MostrarPrimero(nodoActual);
                Mostrar(nodoActual);
            }
            
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (nodoActual != null)
            {
                nodoActual = bitacora.MostrarUltimo(nodoActual);
                Mostrar(nodoActual);
            }
            
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            if (nodoActual != null)
            {
                nodoActual = bitacora.MostrarSiguiente(nodoActual);
                Mostrar(nodoActual);
            }
            
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            if (nodoActual != null)
            {
                nodoActual = bitacora.MostrarAnterior(nodoActual);
                Mostrar(nodoActual);
            }
            
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            if (nodoActual != null)
            {
                nodoActual = bitacora.MostrarPadre(nodoActual);
                Mostrar(nodoActual);
            }
            
        }
    }
}
