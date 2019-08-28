using Repository.Repositories;
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
    public partial class TelaPrecos : Form
    {
        private PrecoRepository repository = new PrecoRepository();
        private int idAalterar = -1;
        public TelaPrecos()
        {
            InitializeComponent();
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            string valor = maskedTextBox1.Text;
            DateTime dataInicial = dateTimePicker1.Value;
            DateTime dataFinal = dateTimePicker2.Value;

            if (valor == "R$  .")
            {
                MessageBox.Show("Digite o valor da hora","Erro",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }

            if (idAalterar == -1)
            {
                
            }

        }
    }
}
