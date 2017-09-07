# Exercicio1

Report de Atividades

Referente as altera��es descritas no documento �Teste-NetShoes� foi detectada a necessidade de cria��o de um Windows form simples para salvar informa��es pertinentes a NFe. O Form cont�m os seguintes campos: Nome, UF Origem, UF Destino, Nome Produto, C�digo Produto, Valor e uma flag Brinde.
A primeira etapa do desenvolvimento consistiu em efetuar a serializa��o da classe NotaFiscal para que todas as informa��es contidas no objeto fossem convertidas para XML, inclusive os itens da NotaFiscal. Foi criada uma chave no appconfig para definir o path onde o XML dever� ser salvo seguindo instru��es contidas no documento.
Na classe NotaFiscalRepository foram adicionados os m�todos para salvar a NotaFiscal.  Foi criada a classe NotaFiscalItemRepository para salvar os Itens utilizando as procedures anexadas no projeto. Por se tratar de uma aplica��o simples os m�todos foram implementados com SqlConnection e SqlCommand para execu��o das chamadas, a interface IDisposable foi implementada garantindo o fechamento das conex�es ap�s utiliza��o.
Novos campos foram criados na aplica��o, com os seguintes nomes: BaseIPI, AliquotaIPI, ValorIPI e Desconto, todos com suas regras de preenchimento de acordo com a documenta��o fornecida. Tamb�m foi criado o campo Valor na tabela NotaFiscalItens, este campo existia na aplica��o mas n�o era salvo no BD, portanto, tratava-se de um bug na aplica��o.
Conforme documenta��o foi implementado o relat�rio P_REL_NOTA_FISCAL que exibe a soma dos campos solicitados, agrupados por CFOP.
Foram realizadas algumas melhorias como a limpeza dos campos ao gerar o XML e tamb�m a valida��o do preenchimento das informa��es. Os campos relacionados a UF foram substitu�dos por combos para facilitar o preenchimento.
Para preenchimento e valida��o das informa��es referentes a UF foi criada uma classe auxiliar com um enum para facilitar a visualiza��o dos estados em quest�o, al�m de ser utilizado para o preenchimento das combos.
Os m�todos foram remanejados separando um pouco a responsabilidade do preenchimento das informa��es, devido ao tamanho do projeto n�o achei necess�rio a separa��o em camadas, mas em uma poss�vel refatora��o seria interessante.
Por fim, foram detectados 2 bugs na aplica��o, um deles referente a alimenta��o das UFs de destino e origem que estavam trocados e outro referente aos itens da nota que n�o eram adicionados na lista da classe NotaFiscal, ambos foram corrigidos.
N�o foi realizada a implementa��o de testes unit�rios para a aplica��o.  

