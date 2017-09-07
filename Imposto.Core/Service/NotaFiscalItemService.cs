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
    public class NotaFiscalItemService
    {
        public void PreencherItensNF(Domain.Pedido pedido, NotaFiscal nf)
        {
            NotaFiscalService nfService = new NotaFiscalService();

            foreach (PedidoItem itemPedido in pedido.ItensDoPedido)
            {
                NotaFiscalItem notaFiscalItem = new NotaFiscalItem();

                notaFiscalItem.NomeProduto = itemPedido.NomeProduto;
                notaFiscalItem.CodigoProduto = itemPedido.CodigoProduto;

                //Verificamos a região do destino e aplicamos o desconto se for Sudeste
                notaFiscalItem.Desconto = RegiaoSudeste(nf) ? 10 : 0;

                //Atualizando o valor do item com desconto se o mesmo for da região sudeste, senão apenas mantemos o valor
                notaFiscalItem.Valor = notaFiscalItem.Desconto > 0
                                        ? itemPedido.ValorItemPedido - (itemPedido.ValorItemPedido * (notaFiscalItem.Desconto / 100))
                                        : itemPedido.ValorItemPedido;

                //Método responsável por definir o CFOP baseado na Origem e Destino da NF
                DefinirCFOP(notaFiscalItem, nf);

                //Método responsável pela definição do TipoICMS, BaseICMS e AliquotaICMS
                DefinirICMS(notaFiscalItem, itemPedido, nf);

                //Método responsável pela definição da BaseIPI, ValorIPI e AliquotaIPI
                DefinirIPI(notaFiscalItem, itemPedido);

                //Adicionando os itens do pedido na NF
                nf.ItensDaNotaFiscal.Add(notaFiscalItem);
            }
        }

        public void DefinirCFOP(NotaFiscalItem notaFiscalItem, NotaFiscal notaFiscal)
        {
            //CFOPs para origem SP
            if (notaFiscal.EstadoOrigem == Util.Enumeradores.UF.SP.ToString())
            {
                if (notaFiscal.EstadoDestino == Util.Enumeradores.UF.RJ.ToString())
                {
                    notaFiscalItem.Cfop = "6.000";
                }
                else if (notaFiscal.EstadoDestino == Util.Enumeradores.UF.PE.ToString())
                {
                    notaFiscalItem.Cfop = "6.001";
                }
                else if (notaFiscal.EstadoDestino == Util.Enumeradores.UF.MG.ToString())
                {
                    notaFiscalItem.Cfop = "6.002";
                }
                else if (notaFiscal.EstadoDestino == Util.Enumeradores.UF.PB.ToString())
                {
                    notaFiscalItem.Cfop = "6.003";
                }
                else if (notaFiscal.EstadoDestino == Util.Enumeradores.UF.PR.ToString())
                {
                    notaFiscalItem.Cfop = "6.004";
                }
                else if (notaFiscal.EstadoDestino == Util.Enumeradores.UF.PI.ToString())
                {
                    notaFiscalItem.Cfop = "6.005";
                }
                else if (notaFiscal.EstadoDestino == Util.Enumeradores.UF.RO.ToString())
                {
                    notaFiscalItem.Cfop = "6.006";
                }
                else if (notaFiscal.EstadoDestino == Util.Enumeradores.UF.SE.ToString())
                {
                    notaFiscalItem.Cfop = "6.007";
                }
                else if (notaFiscal.EstadoDestino == Util.Enumeradores.UF.TO.ToString())
                {
                    notaFiscalItem.Cfop = "6.008";
                }
                else if (notaFiscal.EstadoDestino == Util.Enumeradores.UF.SE.ToString())
                {
                    notaFiscalItem.Cfop = "6.009";
                }
                else if (notaFiscal.EstadoDestino == Util.Enumeradores.UF.PA.ToString())
                {
                    notaFiscalItem.Cfop = "6.010";
                }
            }

            //CFOPs para origem MG
            if (notaFiscal.EstadoOrigem == Util.Enumeradores.UF.MG.ToString())
            {
                if (notaFiscal.EstadoDestino == Util.Enumeradores.UF.RJ.ToString())
                {
                    notaFiscalItem.Cfop = "6.000";
                }
                else if (notaFiscal.EstadoDestino == Util.Enumeradores.UF.PE.ToString())
                {
                    notaFiscalItem.Cfop = "6.001";
                }
                else if (notaFiscal.EstadoDestino == Util.Enumeradores.UF.MG.ToString())
                {
                    notaFiscalItem.Cfop = "6.002";
                }
                else if (notaFiscal.EstadoDestino == Util.Enumeradores.UF.PB.ToString())
                {
                    notaFiscalItem.Cfop = "6.003";
                }
                else if (notaFiscal.EstadoDestino == Util.Enumeradores.UF.PR.ToString())
                {
                    notaFiscalItem.Cfop = "6.004";
                }
                else if (notaFiscal.EstadoDestino == Util.Enumeradores.UF.PI.ToString())
                {
                    notaFiscalItem.Cfop = "6.005";
                }
                else if (notaFiscal.EstadoDestino == Util.Enumeradores.UF.RO.ToString())
                {
                    notaFiscalItem.Cfop = "6.006";
                }
                else if (notaFiscal.EstadoDestino == Util.Enumeradores.UF.SE.ToString())
                {
                    notaFiscalItem.Cfop = "6.007";
                }
                else if (notaFiscal.EstadoDestino == Util.Enumeradores.UF.TO.ToString())
                {
                    notaFiscalItem.Cfop = "6.008";
                }
                else if (notaFiscal.EstadoDestino == Util.Enumeradores.UF.SE.ToString())
                {
                    notaFiscalItem.Cfop = "6.009";
                }
                else if (notaFiscal.EstadoDestino == Util.Enumeradores.UF.PA.ToString())
                {
                    notaFiscalItem.Cfop = "6.010";
                }
            }
        }

        public void DefinirICMS(NotaFiscalItem notaFiscalItem, PedidoItem itemPedido, NotaFiscal notaFiscal)
        {
            if (notaFiscal.EstadoDestino == notaFiscal.EstadoOrigem)
            {
                notaFiscalItem.TipoIcms = "60";
                notaFiscalItem.AliquotaIcms = 0.18;
            }
            else
            {
                notaFiscalItem.TipoIcms = "10";
                notaFiscalItem.AliquotaIcms = 0.17;
            }

            if (notaFiscalItem.Cfop == "6.009")
            {
                notaFiscalItem.BaseIcms = itemPedido.ValorItemPedido * 0.90; //redução de base
            }
            else
            {
                notaFiscalItem.BaseIcms = itemPedido.ValorItemPedido;
            }

            //Se o pedido ser do tipo Brinde, devemos alterar as aliquotas
            if (itemPedido.Brinde)
            {
                notaFiscalItem.TipoIcms = "60";
                notaFiscalItem.AliquotaIcms = 0.18;
                notaFiscalItem.ValorIcms = notaFiscalItem.BaseIcms * notaFiscalItem.AliquotaIcms;
            }

            notaFiscalItem.ValorIcms = notaFiscalItem.BaseIcms * notaFiscalItem.AliquotaIcms;
        }

        public void DefinirIPI(NotaFiscalItem notaFiscalItem, PedidoItem itemPedido)
        {
            notaFiscalItem.BaseIPI = notaFiscalItem.Valor;

            if (itemPedido.Brinde)
            {
                notaFiscalItem.AliquotaIPI = 0;
            }
            else
            {
                notaFiscalItem.AliquotaIPI = 10;
            }

            notaFiscalItem.ValorIPI = notaFiscalItem.BaseIPI * notaFiscalItem.AliquotaIPI;
        }

        public bool RegiaoSudeste(NotaFiscal notaFiscal)
        {
            if (notaFiscal.EstadoDestino == Util.Enumeradores.UF.ES.ToString()
                || notaFiscal.EstadoDestino == Util.Enumeradores.UF.MG.ToString()
                || notaFiscal.EstadoDestino == Util.Enumeradores.UF.RJ.ToString()
                || notaFiscal.EstadoDestino == Util.Enumeradores.UF.SP.ToString())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
