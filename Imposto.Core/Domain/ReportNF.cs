using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Core.Domain
{
    public class ReportNF
    {
        public string CFOP { get; set; }
        public decimal totalBaseICMS { get; set; }
        public decimal totalValorICMS { get; set; }
        public decimal totalBaseIPI { get; set; }
        public decimal totalValorIPI { get; set; }
    }
}
