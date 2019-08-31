using Model;
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
        public TelaSaida(string placa)
        {
            InitializeComponent();
            estacionado = repository.ObterPelaPlaca(placa);
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
            maskedTextBox1.Text = preco.PrecoHora.ToString();
        }

        //Botão salvar
        private void Button3_Click(object sender, EventArgs e)
        {

        }

       

       
        //Calcula a duração
        private void DateTimePicker2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string tempo = (dateTimePicker2.Value - dateTimePicker1.Value).ToString();
                if (tempo.Contains("-"))
                {
                    MessageBox.Show("Você selecionou o horario de saida antes da entrada", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dateTimePicker2.Focus();
                }
                else
                {
                    textBox2.Text = tempo.ToString();
                    DateTime tempoDate = Convert.ToDateTime(tempo);
                    decimal minutosHora = (tempoDate.Hour * 60);
                    int minutos = tempoDate.Minute;
                    textBox4.Text = ( (minutosHora+minutos) * (preco.PrecoHora * 60)).ToString();
                    

                }
            }

        }
    }


}
