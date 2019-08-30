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
    public partial class TelaCarros : Form
    {
        private CarroRepository repository = new CarroRepository();
        private int idAlterar = -1;
        public TelaCarros()
        {
            InitializeComponent();
        }

        public void LimpaCampos()
        {
            maskedTextBox2.Clear();
            maskedTextBox1.Clear();
        }
        private void AtualizaTabela()
        {
            dataGridView1.Rows.Clear();
            List<Carro> lista = repository.ObterTodos();
            foreach (Carro carro in lista)
            {
                dataGridView1.Rows.Add(new object[] { carro.Id, carro.Placa });
            }
            LimpaCampos();
        }


        //Metodos para salvar e editar carro
        private void Button1_Click(object sender, EventArgs e)
        {
            string placa = maskedTextBox1.Text.ToUpper(); ;
            if (placa == "   -")
            {
                MessageBox.Show("Digite a placa corretamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (idAlterar == -1)
                {
                    if (repository.VerificaJaCadastrado(placa) == false)
                    {
                        Carro carro = new Carro();
                        carro.Placa = placa;
                        repository.Inserir(carro);
                        AtualizaTabela();
                    }
                    else
                    {
                        MessageBox.Show("Ja existe uma placa cadastrada","Erro",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    Carro carro = new Carro();
                    carro.Id = idAlterar;
                    carro.Placa = placa;
                    repository.Alterar(carro);
                    btnApagar.Enabled = true;
                    btnAlterar.Enabled = true;
                    dataGridView1.Enabled = true;
                    idAlterar = -1;
                    AtualizaTabela();

                }
            }
        }

        private void TelaCarros_Load(object sender, EventArgs e)
        {
            AtualizaTabela();
        }


        //Apagar
        private void Button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Deseja apagar?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
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
                    MessageBox.Show("Você deve selecionar um carro primeiro", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //Botao editar, irá buscar o carro no banco e retornar no maskered
        private void Button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                idAlterar = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                Carro carro = repository.ObterPelaPlaca(dataGridView1.CurrentRow.Cells[1].Value.ToString());
                maskedTextBox1.Text = carro.Placa;
                btnApagar.Enabled = false;
                btnAlterar.Enabled = false;
                dataGridView1.Enabled = false;
            }
            else
            {
                MessageBox.Show("Você deve selecionar um carro primeiro", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                TelaCarros.ActiveForm.Close();
            }
        }

        private void MaskedTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
        public string Placa
        {
            get { return dataGridView1.CurrentRow.Cells[1].Value.ToString(); }
            set { dataGridView1.CurrentRow.Cells[1].Value.ToString() ; }
        }
        //Botao buscar
        private void MaskedTextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (maskedTextBox2.Text.Count() != 8)
                {
                    AtualizaTabela();
                }
                else
                {
                    dataGridView1.Rows.Clear();
                    Carro carro = repository.ObterPelaPlaca(maskedTextBox2.Text);
                    dataGridView1.Rows.Add(new object[] { carro.Id, carro.Placa });
                }
            }
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
