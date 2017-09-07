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
    public class ReportNFRepository : IDisposable
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["Conexao"]);

        public ReportNFRepository()
        {
            if (conn.State != System.Data.ConnectionState.Open)
            {
                conn.Open();
            }
        }

        public List<ReportNF> ReportNF()
        {
            List<ReportNF> listReport = new List<ReportNF>();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "P_REL_NOTA_FISCAL";

                SqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    ReportNF reportNF = new ReportNF();
                    reportNF.CFOP = dr["CFOP"].ToString();
                    reportNF.totalBaseICMS = Convert.ToDecimal(dr["ValorBaseICMS"].ToString());
                    reportNF.totalValorICMS = Convert.ToDecimal(dr["ValorICMS"].ToString());
                    reportNF.totalBaseIPI = Convert.ToDecimal(dr["ValorBaseIPI"].ToString());
                    reportNF.totalValorIPI = Convert.ToDecimal(dr["ValorIPI"].ToString());

                    listReport.Add(reportNF);
                }
            }

            return listReport;
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
