using Imposto.Core.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Imposto.Core.Domain;
using Imposto.Core.Util;

namespace TesteImposto
{
    public partial class FormImposto : Form
    {
        private Pedido pedido = new Pedido();

        public FormImposto()
        {
            InitializeComponent();
            dataGridViewPedidos.AutoGenerateColumns = true;                       
            dataGridViewPedidos.DataSource = GetTablePedidos();
            ResizeColumns();
            CarregaCombos();
        }

        private void ResizeColumns()
        {
            double mediaWidth = dataGridViewPedidos.Width / dataGridViewPedidos.Columns.GetColumnCount(DataGridViewElementStates.Visible);

            for (int i = dataGridViewPedidos.Columns.Count - 1; i >= 0; i--)
            {
                var coluna = dataGridViewPedidos.Columns[i];
                coluna.Width = Convert.ToInt32(mediaWidth);
            }   
        }

        private object GetTablePedidos()
        {
            DataTable table = new DataTable("pedidos");
            table.Columns.Add(new DataColumn("Nome do produto", typeof(string)));
            table.Columns.Add(new DataColumn("Codigo do produto", typeof(string)));
            table.Columns.Add(new DataColumn("Valor", typeof(string)));
            table.Columns.Add(new DataColumn("Brinde", typeof(bool)));
                     
            return table;
        }

        private void CarregaCombos()
        {
            cbEstadoOrigem.Items.Clear();
            cbEstadoDestino.Items.Clear();

            foreach (var item in Enum.GetValues(typeof(Enumeradores.UF)))
            {
                cbEstadoOrigem.Items.Add(item);
                cbEstadoDestino.Items.Add(item);
            }
        }

        private void LimparCampos()
        {
            textBoxNomeCliente.Clear();
            CarregaCombos();
            dataGridViewPedidos.DataSource = GetTablePedidos();
        }

        private bool ValidaCampos()
        {
            DataTable table = (DataTable)dataGridViewPedidos.DataSource;

            if (string.IsNullOrWhiteSpace(textBoxNomeCliente.Text) || string.IsNullOrWhiteSpace(cbEstadoOrigem.Text)
                || string.IsNullOrWhiteSpace(cbEstadoDestino.Text) || table.Rows.Count < 1)
            {
                return false;
            }
            else
            {
                foreach (DataRow row in table.Rows)
                {
                    double ValorItemPedido = 0;
                    double.TryParse(row["Valor"].ToString(), out ValorItemPedido);
                    String CodigoProduto = row["Codigo do produto"].ToString();
                    String NomeProduto = row["Nome do produto"].ToString();

                    if (string.IsNullOrWhiteSpace(CodigoProduto) || string.IsNullOrWhiteSpace(NomeProduto)
                            || ValorItemPedido == 0)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        private void buttonGerarNotaFiscal_Click(object sender, EventArgs e)
        {
            buttonGerarNotaFiscal.Enabled = false;

            if (ValidaCampos())
            {
                NotaFiscalService service = new NotaFiscalService();
                pedido.EstadoOrigem = cbEstadoOrigem.Text;
                pedido.EstadoDestino = cbEstadoDestino.Text;
                pedido.NomeCliente = textBoxNomeCliente.Text;

                DataTable table = (DataTable)dataGridViewPedidos.DataSource;

                foreach (DataRow row in table.Rows)
                {
                    bool brinde;
                    bool.TryParse(row["Brinde"].ToString(), out brinde);

                    pedido.ItensDoPedido.Add(
                        new PedidoItem()
                        {
                            Brinde = brinde,
                            CodigoProduto = row["Codigo do produto"].ToString(),
                            NomeProduto = row["Nome do produto"].ToString(),
                            ValorItemPedido = Convert.ToDouble(row["Valor"].ToString())
                        });
                }

                service.GerarNotaFiscal(pedido);
                LimparCampos();
                MessageBox.Show("Operação efetuada com sucesso");
            }
            else
            {
                MessageBox.Show("Preencha todos os campos");
            }

            buttonGerarNotaFiscal.Enabled = true;
        }

        private void btnGerarReport_Click(object sender, EventArgs e)
        {
            Report report = new Report();
            report.Show();
        }
    }
}
