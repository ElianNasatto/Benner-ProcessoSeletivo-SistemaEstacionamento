using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace View
{
    public partial class TelaSaida : Form
    {
        public TelaSaida()
        {
            InitializeComponent();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            TelaPrecos tela = new TelaPrecos();
            tela.Show();
        }
    }
}
