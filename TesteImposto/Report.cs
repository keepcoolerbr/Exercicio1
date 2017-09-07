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

namespace TesteImposto
{
    public partial class Report : Form
    {
        public Report()
        {
            InitializeComponent();
            gridReport.AutoGenerateColumns = true;
            gridReport.DataSource = new ReportNFService().ReportNF();
            ResizeColumns();
            DefinirNomeColunas();
        }

        private void ResizeColumns()
        {
            double mediaWidth = gridReport.Width / gridReport.Columns.GetColumnCount(DataGridViewElementStates.Visible);

            for (int i = gridReport.Columns.Count - 1; i >= 0; i--)
            {
                var coluna = gridReport.Columns[i];
                coluna.Width = Convert.ToInt32(mediaWidth);
            }
        }

        private void DefinirNomeColunas()
        {
            gridReport.Columns["CFOP"].HeaderText = "CFOP ";
            gridReport.Columns["totalBaseICMS"].HeaderText = "Valor Total da Base de ICMS";
            gridReport.Columns["totalValorICMS"].HeaderText = "Valor Total do ICMS";
            gridReport.Columns["totalBaseIPI"].HeaderText = "Valor Total da Base de IPI";
            gridReport.Columns["totalValorIPI"].HeaderText = "Valor Total do IPI";
        }
    }
}
