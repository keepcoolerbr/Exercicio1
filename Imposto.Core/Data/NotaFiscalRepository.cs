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
    public class NotaFiscalRepository : IDisposable
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["Conexao"]);

        public NotaFiscalRepository()
        {
            if (conn.State != System.Data.ConnectionState.Open)
            {
                conn.Open();
            }
        }

        public int SalvarNotaFiscal(NotaFiscal notaFiscal)
        {
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "P_NOTA_FISCAL";

                command.Parameters.AddWithValue("@pId", notaFiscal.Id);
                command.Parameters["@pId"].Direction = System.Data.ParameterDirection.InputOutput;

                command.Parameters.AddWithValue("@pNumeroNotaFiscal", notaFiscal.NumeroNotaFiscal);
                command.Parameters.AddWithValue("@pSerie", notaFiscal.Serie);
                command.Parameters.AddWithValue("@pNomeCliente", notaFiscal.NomeCliente);
                command.Parameters.AddWithValue("@pEstadoDestino", notaFiscal.EstadoDestino);
                command.Parameters.AddWithValue("@pEstadoOrigem", notaFiscal.EstadoOrigem);

                command.ExecuteNonQuery();

                return (int)command.Parameters["@pId"].Value;
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
