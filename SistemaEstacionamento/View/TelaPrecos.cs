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
    public partial class TelaPrecos : Form
    {
        private PrecoRepository repository = new PrecoRepository();
        public TelaPrecos()
        {
            InitializeComponent();
        }

        public int idPreco
        {
            get { return Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value); }
            set { Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value); }
        }

        private int idAlterar = -1;
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

            if (dataInicial.Date == dataFinal.Date)
            {
                MessageBox.Show("As datas fora colocadas para o mesmo dia, por favor selecione um periodo diferente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning) ;
                return;
            }

            if (idAlterar == -1)
            {
                if (repository.VerificaJaCadastrado(dataInicial, dataFinal) == false)
                {


                    Preco preco = new Preco();
                    preco.DataInicial = dataInicial;
                    preco.DataFinal = dataFinal;
                    valor = valor.Replace("R$", "");
                    preco.PrecoHora = Convert.ToDecimal(valor);
                    preco.RegistroAtivo = true;
                    repository.Inserir(preco);
                    LimpaCampos();
                    AtualizaTabela();
                }
                else
                {
                    MessageBox.Show("Ja existe um preco cadastrado nesse periodo","Erro",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    
                }
            }
            else
            {
                Preco preco = new Preco();
                preco.IdPreco = idAlterar;
                preco.DataInicial = dataInicial;
                preco.DataFinal = dataFinal;
                valor = valor.Replace("R$", "");
                preco.PrecoHora = Convert.ToDecimal(valor);
                preco.RegistroAtivo = true;
                repository.Alterar(preco);

                idAlterar = -1;
                btnApagar.Enabled = true;
                dataGridView1.Enabled = true;
                AtualizaTabela();
                LimpaCampos();
            }

        }

        private void LimpaCampos()
        {
            maskedTextBox1.Clear();
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
        }

        private void AtualizaTabela()
        {
            dataGridView1.Rows.Clear();
            List<Preco> lista = repository.ObterTodos();
            foreach (Preco preco  in lista)
            {
                dataGridView1.Rows.Add(new object[] { preco.IdPreco, preco.PrecoHora, preco.DataInicial, preco.DataFinal });
            }
        }

        private void TelaPrecos_Load(object sender, EventArgs e)
        {
            AtualizaTabela();
        }

        private void BtnApagar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Realmente deseja apagar?","Aviso",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                    repository.Apagar(id);
                    AtualizaTabela();
                }
                else
                {
                    MessageBox.Show("Você não selecionou um preço para apagar","Erro",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            }
        }

        private void BtnAlterar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count >0 )
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                Preco preco = repository.ObterPeloId(id);
                dateTimePicker1.Value = preco.DataInicial;
                dateTimePicker2.Value = preco.DataFinal;
                string precoMenor = preco.PrecoHora.ToString();
                if (precoMenor.Length <5)
                {
                    precoMenor = "0" + precoMenor;
                }
                maskedTextBox1.Text = precoMenor;
                idAlterar = id;
                btnApagar.Enabled = false;
                dataGridView1.Enabled = false;
            }
            else
            {
                MessageBox.Show("Você não selecionou um registro para alterar","Erro",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            

           
        }

        private void DataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                TelaPrecos.ActiveForm.Close();
            }
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
