using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NEGOCIO;

namespace PRESENTACION
{
    public partial class CU_CELDA : UserControl
    {
        public CU_CELDA()
        {
            InitializeComponent();

            this.label1.Visible = false;
        }

        public PictureBox pictureBox
        {
            get { return pictureBox1; }
            set { pictureBox1 = value; }
        }

        public bool Marcado
        {
            get { return label1.Visible; }
            set { label1.Visible = value; }
        }


        private Celda celda;

        public Celda Celda
        {
            get { return celda; }
            set { celda = value; }
        }

        public void AsignarCelda(Celda celda, Panel panel)
        {
            this.celda = celda;
            ActualizarCelda();
            AsignarColor();
            AsignarPosicion(panel);
            
        }

        public void ActualizarCelda()
        {
            if(celda.Pieza != null && celda.Pieza.Activa == true)
            {
                string image = string.Format("{0}_{1}"
                    , celda.Pieza.GetType().Name, celda.Pieza.Color.Name);
                pictureBox1.Image = (Image)Properties.Resources.ResourceManager.GetObject(image.ToLower());
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            } else
            {
                pictureBox1.Image = null;
            }
        }

        private void AsignarPosicion(Panel panel)
        {
            if (panel != null)
            {
                int squareSize = (int)(panel.Width > panel.Height ?
                    Math.Floor((decimal)(panel.Height / 9)) : Math.Floor((decimal)(panel.Width / 9)));
                this.Size = new Size(squareSize, squareSize);
                if (celda != null)
                    Location = new Point(celda.Columna * squareSize, celda.Fila * squareSize);
            }
        }

        private void AsignarColor()
        {
            this.BackColor = celda.Color;
        }

        public void MarcarCeldaDisponible()
        {
            Marcado = true;
        }

        public void DesmarcarCeldaDisponible()
        {
            Marcado = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
