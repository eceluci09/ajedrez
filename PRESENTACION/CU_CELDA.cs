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

        Graphics graphics;

        private Celda celda;

        public Celda Celda
        {
            get { return celda; }
            set { celda = value; }
        }

        public void AsignarCelda(Celda celda, Panel panel)
        {
            this.celda = celda;
            UbicarPieza();
            AsignarColor();
            AsignarPosicion(panel);
            
        }

        private void UbicarPieza()
        {
            if(celda.Pieza != null)
            {
                string image = string.Format("{0}_{1}"
                    , celda.Pieza.GetType().Name, celda.Pieza.Color.Name);
                pictureBox1.Image = (Image)Properties.Resources.ResourceManager.GetObject(image.ToLower());
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
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

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            this.label1.Visible = true;
        }
    }
}
