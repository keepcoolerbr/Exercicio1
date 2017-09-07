using Imposto.Core.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Core.Data
{
    public class NotaFiscalItemRepository : IDisposable
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["Conexao"]);

        public NotaFiscalItemRepository()
        {
            if (conn.State != System.Data.ConnectionState.Open)
            {
                conn.Open();
            }
        }

        public void SalvarItensNF(List<NotaFiscalItem> listNotaFiscalItem)
        {
            foreach (var notaFiscalItem in listNotaFiscalItem)
            {
                using (SqlCommand command = new SqlCommand())
                {

                    command.Connection = conn;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "P_NOTA_FISCAL_ITEM";

                    command.Parameters.AddWithValue("@pId", notaFiscalItem.Id);
                    command.Parameters.AddWithValue("@pIdNotaFiscal", notaFiscalItem.IdNotaFiscal);
                    command.Parameters.AddWithValue("@pCfop", notaFiscalItem.Cfop);
                    command.Parameters.AddWithValue("@pTipoIcms", notaFiscalItem.TipoIcms);
                    command.Parameters.AddWithValue("@pBaseIcms", notaFiscalItem.BaseIcms);
                    command.Parameters.AddWithValue("@pAliquotaIcms", notaFiscalItem.AliquotaIcms);
                    command.Parameters.AddWithValue("@pValorIcms", notaFiscalItem.ValorIcms);
                    command.Parameters.AddWithValue("@pNomeProduto", notaFiscalItem.NomeProduto);
                    command.Parameters.AddWithValue("@pCodigoProduto", notaFiscalItem.CodigoProduto);
                    command.Parameters.AddWithValue("@BaseIPI", notaFiscalItem.BaseIPI);
                    command.Parameters.AddWithValue("@AliquotaIPI", notaFiscalItem.AliquotaIPI);
                    command.Parameters.AddWithValue("@ValorIPI", notaFiscalItem.ValorIPI);
                    command.Parameters.AddWithValue("@Valor", notaFiscalItem.Valor);
                    command.Parameters.AddWithValue("@Desconto", notaFiscalItem.Desconto);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Dispose()
        {
            if (conn.State != System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }
}
