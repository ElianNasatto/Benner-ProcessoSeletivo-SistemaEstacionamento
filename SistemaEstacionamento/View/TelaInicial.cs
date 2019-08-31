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

        public string placaEstacionado
        {
            get { return dataGridView1.CurrentRow.Cells[1].Value.ToString(); }
            set { dataGridView1.CurrentRow.Cells[0].Value.ToString(); }
        }


        public TelaInicial()
        {
            InitializeComponent();
        }

        private void BtnEntrada_Click(object sender, EventArgs e)
        {
            TelaEntrada tela = new TelaEntrada();
            tela.Show();
        }

        private void TelaInicial_Load(object sender, EventArgs e)
        {
            AtualizaTabela();
        }

        private void AtualizaTabela()
        {
            dataGridView1.Rows.Clear();
            List<Estacionado> lista = repository.ObterTodos();
            foreach (Estacionado estacionado in lista)
            {
                dataGridView1.Rows.Add(estacionado.IdEstacionado, estacionado.Carro.Placa, estacionado.DataEntrada, estacionado.DataSaida, estacionado.Duracao, estacionado.Preco, estacionado.TempoCobrado, estacionado.ValorPagar);
            }
        }

        private void BtnSaida_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                TelaSaida tela = new TelaSaida(dataGridView1.CurrentRow.Cells[1].Value.ToString());
                tela.Show();
            }
        }

        private void TelaInicial_Activated(object sender, EventArgs e)
        {
            AtualizaTabela();
        }
    }
}


