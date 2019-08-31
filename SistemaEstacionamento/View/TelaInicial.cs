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
            List<Estacionado> lista = repository.ObterTodosAberto();
            foreach (Estacionado estacionado in lista)
            {
                dataGridView1.Rows.Add(estacionado.IdEstacionado, estacionado.Carro.Placa, estacionado.DataEntrada, estacionado.DataSaida, estacionado.Duracao, estacionado.Preco, estacionado.TempoCobrado, estacionado.ValorPagar);
            }
        }

        private void BtnSaida_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                TelaSaida tela = new TelaSaida(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value));
                tela.Show();
            }
        }

        private void TelaInicial_Activated(object sender, EventArgs e)
        {
            ///AtualizaTabela();
        }

        private void DataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                TelaSaida tela = new TelaSaida(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value));
                tela.Show();
            }
        }

        private void MarcarEntradaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TelaEntrada tela = new TelaEntrada();
            tela.ShowDialog();
        }

        private void CarrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TelaCarros tela = new TelaCarros();
            tela.ShowDialog();
        }

        private void PreçosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TelaPrecos tela = new TelaPrecos();
            tela.ShowDialog();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            maskedTextBox1.Visible = false;
            label5.Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;

            if (comboBox1.SelectedIndex == 0)
            {
                AtualizaTabela();
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Columns[3].Visible = true;
                dataGridView1.Columns[4].Visible = true;
                dataGridView1.Columns[5].Visible = true;
                dataGridView1.Columns[6].Visible = true;
                dataGridView1.Columns[7].Visible = true;
                 
                foreach (Estacionado estacionado in repository.ObterTodosFechados())
                {
                    dataGridView1.Rows.Add(estacionado.IdEstacionado, estacionado.Carro.Placa, estacionado.DataEntrada, estacionado.DataSaida, estacionado.Duracao,"estacionado.TempoCobrado", estacionado.Preco.PrecoHora, estacionado.ValorPagar);
                }
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                maskedTextBox1.Visible = true;
                label5.Visible = true;
            }
        }

        //Campo buscar por placa
        private void MaskedTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (maskedTextBox1.Text.Length < 9)
                {
                    dataGridView1.Rows.Clear();
                    Estacionado estacionado = repository.ObterPelaPlaca(maskedTextBox1.Text);
                    if (estacionado == null)
                    {
                        MessageBox.Show("Placa não contem nehuma marcação", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        comboBox1.SelectedIndex = 0;
                    }
                    else
                    {
                        dataGridView1.Rows.Add(estacionado.IdEstacionado, estacionado.Carro.Placa, estacionado.DataEntrada, estacionado.DataSaida, estacionado.Duracao, estacionado.Preco, estacionado.TempoCobrado, estacionado.ValorPagar);
                    }
                }
                else
                {
                    MessageBox.Show("Placa invalida", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    comboBox1.SelectedIndex = 0;
                }
            }
        }

        private void TelaInicial_Load_1(object sender, EventArgs e)
        {

        }
    }
}


