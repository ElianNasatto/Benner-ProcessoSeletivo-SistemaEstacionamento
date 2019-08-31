using Model;
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
    public partial class TelaEntrada : Form
    {
        private EstacionadoRepository repository = new EstacionadoRepository();
        private static Carro carro = new Carro();
        public TelaEntrada()
        {
            InitializeComponent();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Estacionado estacionado = new Estacionado();
            estacionado.IdCarro = carro.Id;
            estacionado.DataEntrada = dateTimePicker1.Value;
            estacionado.DataSaida = DateTime.Now;
            bool inseriu = repository.Inserir(estacionado);
            if (inseriu == true)
            {
                MessageBox.Show("Adicionado com sucesso","Adicionado",MessageBoxButtons.OK,MessageBoxIcon.Information);
                maskedTextBox1.Clear();
                dateTimePicker1.Value = DateTime.Now;
            }
            else
            {
                MessageBox.Show("Ocorreu um erro ao adicionar, por favor tente novamente ou entre em contato com o suporte","Erro",MessageBoxButtons.OK,MessageBoxIcon.Error);
                maskedTextBox1.Clear();
                dateTimePicker1.Value = DateTime.Now;
            }

        }


        //Botao placa
        private void Button1_Click(object sender, EventArgs e)
        {
            TelaCarros tela = new TelaCarros();
            tela.ShowDialog();
            CarroRepository repositoryCarro = new CarroRepository();
            carro = repositoryCarro.ObterPelaPlaca(tela.Placa);
            maskedTextBox1.Text = carro.Placa;
        }

        private void TelaEntrada_Load(object sender, EventArgs e)
        {
            dateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm";
            dateTimePicker1.Value = DateTime.Now;
        }

    }
}
