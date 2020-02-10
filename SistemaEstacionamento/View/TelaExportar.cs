using ClosedXML.Excel;
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
    public partial class TelaExportar : Form
    {
        public TelaExportar()
        {
            InitializeComponent();
        }

        private void ExportarPlacas(List<Carro> lista)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("Placa");

            foreach (var carro in lista)
            {
                dt.Rows.Add(carro.Placa);

            }
            ds.Tables.Add(dt);

            XLWorkbook wb = new XLWorkbook();
            wb.Worksheets.Add(ds.Tables[0], ds.Tables[0].TableName);

            string AppLocation = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase) + "\\2019.xlsx";

            wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            wb.Style.Font.Bold = true;

            String path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            wb.SaveAs(path + @"\Placas.xlsx", false);
            MessageBox.Show("Arquivo salvo em documentos", "Salvo com sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ExportarPrecos(List<Preco> lista)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("Valor");
            dt.Columns.Add("Data Inicial");
            dt.Columns.Add("Data Final");

            foreach (var preco in lista)
            {
                dt.Rows.Add(preco.PrecoHora, preco.DataInicial, preco.DataFinal);
            }

            ds.Tables.Add(dt);

            XLWorkbook wb = new XLWorkbook();
            wb.Worksheets.Add(ds.Tables[0], ds.Tables[0].TableName);

            string AppLocation = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase) + "\\2019.xlsx";

            wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            wb.Style.Font.Bold = true;
            String path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            wb.SaveAs(path + @"\Precos.xlsx", false);
            MessageBox.Show("Arquivo salvo em documentos","Salvo com sucesso",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (cbOpcao.SelectedIndex)
            {
                case 0:
                    CarroRepository repository = new CarroRepository();
                    ExportarPlacas(repository.ObterTodos());
                    break;
                case 1:
                    PrecoRepository repository2 = new PrecoRepository();
                    ExportarPrecos(repository2.ObterTodos());
                    break;
            }

        }
    }
}
