﻿using Model;
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
                dataGridView1.Rows.Add(estacionado.IdEstacionado, estacionado.Carro.Placa, estacionado.DataEntrada);
            }
        }

        private void BtnSaida_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                if ((comboBox1.SelectedIndex == 2) && (dataGridView1.CurrentRow.Cells[3].Value.ToString() != "Nulo"))
                {
                    return;
                }
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
            if (comboBox1.SelectedIndex == 1)
            {
                return;
            }
            else if ((comboBox1.SelectedIndex == 2) && (dataGridView1.CurrentRow.Cells[3].Value.ToString() != "Nulo"))
            {
                return;
            }

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
            btnSaida.Visible = true;
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
                btnSaida.Visible = false;

                foreach (Estacionado estacionadoFechado in repository.ObterTodosFechados())
                {
                    dataGridView1.Rows.Add(estacionadoFechado.IdEstacionado, estacionadoFechado.Carro.Placa, estacionadoFechado.DataEntrada,
                        estacionadoFechado.DataSaida, estacionadoFechado.Duracao,
                        estacionadoFechado.TempoCobrado, estacionadoFechado.Preco.PrecoHora, estacionadoFechado.ValorPagar);
                }
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Columns[3].Visible = true;
                dataGridView1.Columns[4].Visible = true;
                dataGridView1.Columns[5].Visible = true;
                dataGridView1.Columns[6].Visible = true;
                dataGridView1.Columns[7].Visible = true;
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
                    List<Estacionado> lista = repository.ObterTodosPelaPlaca(maskedTextBox1.Text.ToUpper());
                    if (lista == null)
                    {
                        MessageBox.Show("Placa não contem nehuma marcação", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        comboBox1.SelectedIndex = 0;
                    }
                    else
                    {
                        foreach (Estacionado estacionadoPlaca in lista)
                        {
                            if (estacionadoPlaca.Preco == null)
                            {
                                dataGridView1.Rows.Add(estacionadoPlaca.IdEstacionado, estacionadoPlaca.Carro.Placa, estacionadoPlaca.DataEntrada, "Nulo", "Nulo", "Nulo", "Nulo", "Nulo");
                            }
                            else
                            {
                                dataGridView1.Rows.Add(estacionadoPlaca.IdEstacionado, estacionadoPlaca.Carro.Placa, estacionadoPlaca.DataEntrada, estacionadoPlaca.DataSaida, estacionadoPlaca.Duracao, estacionadoPlaca.TempoCobrado, estacionadoPlaca.Preco.PrecoHora, estacionadoPlaca.ValorPagar);
                            }

                        }
                    }
                }
                else
                {
                    MessageBox.Show("Placa invalida", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    comboBox1.SelectedIndex = 0;
                }
            }
        }

        private void TelaInicial_Activated_1(object sender, EventArgs e)
        {
            AtualizaTabela();
            comboBox1.SelectedIndex = 0;
        }

        private void SobreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TelaSobre tela = new TelaSobre();
            tela.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TelaExportar tela = new TelaExportar();
            tela.Show();
        }
    }
}


