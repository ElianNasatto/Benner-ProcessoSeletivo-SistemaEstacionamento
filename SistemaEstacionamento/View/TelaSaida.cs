﻿using Model;
using Repository.Repositories;
using System;
using System.Windows.Forms;

namespace View
{
    public partial class TelaSaida : Form
    {
        private EstacionadoRepository repository = new EstacionadoRepository();
        Preco preco = new Preco();
        Estacionado estacionado = new Estacionado();

        //Recebe como parametro da tela inicial a placa e busca no banco informações do estacionamento
        public TelaSaida(int idEstacionado)
        {
            InitializeComponent();
            estacionado = repository.ObterPeloId(idEstacionado);
            textBox1.Text = estacionado.Carro.Placa;
            dateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm";
            dateTimePicker2.CustomFormat = "yyyy-MM-dd HH:mm";

            dateTimePicker1.Value = estacionado.DataEntrada;
            dateTimePicker2.Value = DateTime.Now;

        }

        //Abre a tela de preco
        private void Button2_Click(object sender, EventArgs e)
        {
            TelaPrecos tela = new TelaPrecos();
            tela.ShowDialog();
            PrecoRepository repositoryPreco = new PrecoRepository();
            preco = repositoryPreco.ObterPeloId(tela.idPreco);

            //Se o valor da hora for menor que 10, ele ira acrescentar um 0 antes do numero devido a mascara
            string precoMenor = preco.PrecoHora.ToString();
            if (precoMenor.Length < 5)
            {
                precoMenor = "0" + precoMenor;
            }
            maskedTextBox1.Text = precoMenor;

        }

        //Botão salvar
        private void Button3_Click(object sender, EventArgs e)
        {
            estacionado.Duracao = textBox2.Text;
            if (estacionado.Duracao == "")
            {
                MessageBox.Show("Deve ser feito o calculo altes de salvar, selecione o preço a data de entrada e depois a de saida e precione enter","Erro",MessageBoxButtons.OK,MessageBoxIcon.Error);
                dateTimePicker2.Focus();
                return;
            }
            estacionado.DataEntrada = dateTimePicker1.Value;
            estacionado.DataSaida = dateTimePicker2.Value;
            estacionado.IdPreco = preco.IdPreco;
            estacionado.RegistroAtivo = false;
            bool fechado = repository.Alterar(estacionado);
            if (fechado == true)
            {
                MessageBox.Show("Saida marcada", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TelaSaida.ActiveForm.Close();
            }
            else
            {
                MessageBox.Show("Ocorreu um erro ao marcar a saida, tente novamente ou contate o suporte", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TelaSaida.ActiveForm.Close();
            }

        }




        //Calcula a duração
        private void DateTimePicker2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (maskedTextBox1.Text == "R$  ,")
                {
                    MessageBox.Show("Selecione primeiro o preço", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    button2.Focus();
                }
                else
                {
                    string tempo = (dateTimePicker2.Value - dateTimePicker1.Value).ToString();
                    if (tempo.Contains("-"))
                    {
                        MessageBox.Show("Você selecionou o horario de saida antes da entrada", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dateTimePicker2.Focus();
                    }
                    else
                    {
                        textBox2.Text = tempo;
                        DateTime tempoDate = Convert.ToDateTime(tempo);
                        decimal precoMeiaHora = preco.PrecoHora / 2;

                        int hora = (tempoDate.Hour);
                        int minutos = (tempoDate.Minute);

                        int quantidadeMeiaHora = hora * 2;

                        if ((minutos > 0) && (minutos <= 30))
                        {
                            quantidadeMeiaHora++;
                        }
                        else if (minutos > 30)
                        {
                            quantidadeMeiaHora += 2;

                        }

                        estacionado.Duracao = tempo;
                        if (quantidadeMeiaHora % 2 == 0)
                        {
                            estacionado.TempoCobrado = (quantidadeMeiaHora / 2).ToString();
                        }
                        else
                        {
                            estacionado.TempoCobrado = ((quantidadeMeiaHora / 2).ToString()+ ":30");
                        }
                        estacionado.ValorPagar = quantidadeMeiaHora * precoMeiaHora;
                        textBox4.Text = estacionado.ValorPagar.ToString();
                        textBox3.Text = estacionado.TempoCobrado;

                    }
                }
            }

        }

        private void MaskedTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }


}
