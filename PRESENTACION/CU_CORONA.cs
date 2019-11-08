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
    public partial class CU_CORONA : UserControl
    {
        private Pieza piezaCorona;
        public Pieza PiezaCorona
        {
            get { return piezaCorona; }
            set { piezaCorona = value; }
        }

        private Pieza pieza;
        public Pieza Pieza
        {
            get { return pieza; }
            set { pieza = value; }
        }

        private Jugador jugador;

        public Jugador Jugador
        {
            get { return jugador; }
            set { jugador = value; }
        }


        public PictureBox pictureBox
        {
            get { return pictureBox1; }
            set { pictureBox1 = value; }
        }

        public CU_CORONA()
        {
            InitializeComponent();
        }

        public void AsignarPieza(Pieza piezaCorona, Pieza pieza, Jugador jugador, int posicionX, Panel panel)
        {
            this.jugador = jugador;
            this.piezaCorona = piezaCorona;
            this.pieza = pieza;

            MostrarPieza();
            this.BackColor = Color.FromArgb(145, 29, 19);
            AsignarPosicion(panel, posicionX);
            
            
            
        }

        private void MostrarPieza()
        {
           
           string image = string.Format("{0}_{1}", piezaCorona.GetType().Name, piezaCorona.Color.Name);
                pictureBox1.Image = (Image)Properties.Resources.ResourceManager.GetObject(image.ToLower());
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void AsignarPosicion(Panel panel, int posicionX)
        {
            if (panel != null)
            {
                int squareSize = (int)(panel.Width > panel.Height ?
                    Math.Floor((decimal)(panel.Height / 4)) : Math.Floor((decimal)(panel.Width / 4)));
                this.Size = new Size(squareSize * 3, panel.Height);
                Location = new Point((squareSize * 3)*posicionX, squareSize);
            }
        }

    }
}
