using Model;
using Repository;
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
    public partial class TelaInicial : Form
    {
        private EstacionadoRepository repository = new EstacionadoRepository();
        public TelaInicial()
        {
            InitializeComponent();
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {

        }

        private void BtnEntrada_Click(object sender, EventArgs e)
        {
            TelaEntrada tela = new TelaEntrada();
            tela.Show();
        }

        private void TelaInicial_Load(object sender, EventArgs e)
        {
            List<Estacionado> lista = repository.ObterTodos();
            foreach (Estacionado estacionado in lista)
            {
                dataGridView1.Rows.Add(new object[] {estacionado.IdEstacionado,estacionado.Carro.Placa,estacionado.DataEntrada,estacionado.DataSaida,estacionado.Duracao,estacionado.Preco,estacionado.TempoCobrado,estacionado.ValorPagar });
            }
        }

        private void BtnSaida_Click(object sender, EventArgs e)
        {
            TelaSaida tela = new TelaSaida();
            tela.Show();
        }
    }
}


