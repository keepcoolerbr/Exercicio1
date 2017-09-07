using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Imposto.Core.Domain;
using Imposto.Core.Service;

namespace Testes
{
    [TestClass]
    public class Salvar
    {
        [TestMethod]
        public void TesteSalvar()
        {
            NotaFiscal nf = new NotaFiscal()
            {
                EstadoOrigem = "SP",
                EstadoDestino = "RJ"
            }

            NotaFiscalItemService nfItemService = new NotaFiscalItemService();
        }
    }
}
