using Imposto.Core.Data;
using Imposto.Core.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Imposto.Core.Service
{
    public class ReportNFService
    {
        public List<ReportNF> ReportNF()
        {
            ReportNFRepository repository = new ReportNFRepository();

            return repository.ReportNF();
        }
    }
}
