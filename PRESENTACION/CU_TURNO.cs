using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRESENTACION
{
    public partial class CU_TURNO : UserControl
    {
        public CU_TURNO()
        {
            InitializeComponent();
        }

        private string usuario;

        public string Usuario
        {
            get { return label2.Text; }
            set { label2.Text = value; }
        }

        private Button pedirTablas;
        public Button PedirTablas
        {
            get { return button1; }
            set { button1 = value; }
        }

        private Button aceptarTablas;

        public Button AceptarTablas
        {
            get { return button2; }
            set { button2 = value; }
        }

        private Button rechazarTablas;

        public Button RechazarTablas
        {
            get { return button3; }
            set { button3 = value; }
        }


        private void CU_TURNO_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.Gray;
        }

        
    }
}
