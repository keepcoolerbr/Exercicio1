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
    public class NotaFiscalService
    {
        public void GerarNotaFiscal(Domain.Pedido pedido)
        {
            NotaFiscal notaFiscal = new NotaFiscal();
            notaFiscal.EmitirNotaFiscal(pedido);

            NotaFiscalItemService notaFiscalItemService = new NotaFiscalItemService();
            notaFiscalItemService.PreencherItensNF(pedido, notaFiscal);

            //Método responsável pela geração da classe em XML utilizando Serializer
            if (GerarXMLNotaFiscal(notaFiscal))
            {
                NotaFiscalRepository repo = new NotaFiscalRepository();
                notaFiscal.Id = repo.SalvarNotaFiscal(notaFiscal);

                //Atualizando os itens da NF com o ID salvo no BD
                notaFiscal.ItensDaNotaFiscal.ForEach(q => q.IdNotaFiscal = notaFiscal.Id);

                NotaFiscalItemRepository repoItem = new NotaFiscalItemRepository();
                repoItem.SalvarItensNF(notaFiscal.ItensDaNotaFiscal);
            }
        }

        public bool GerarXMLNotaFiscal(NotaFiscal notaFiscal)
        {
            string path = ConfigurationManager.AppSettings["pathXML"] + "xmlNotaFiscal.xml";
            FileStream file = File.Create(path);

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(NotaFiscal));

                serializer.Serialize(file, notaFiscal);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                file.Close();
            }
        }
    }
}
