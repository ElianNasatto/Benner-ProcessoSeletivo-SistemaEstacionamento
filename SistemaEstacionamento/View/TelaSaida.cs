using Model;
using Repository.Repositories;
using System;
using System.Windows.Forms;

namespace View
{
    public partial class TelaSaida : Form
    {
        private EstacionadoRepository repository = new EstacionadoRepository();
        static Preco preco = new Preco();


        public TelaSaida()
        {
            InitializeComponent();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            TelaPrecos tela = new TelaPrecos();
            tela.ShowDialog();
            PrecoRepository repositoryPreco = new PrecoRepository();
            preco = repositoryPreco.ObterPeloId(tela.idPreco);
            maskedTextBox1.Text = preco.PrecoHora.ToString();
        }

        private void Button3_Click(object sender, EventArgs e)
        {

        }

        
    }


}
