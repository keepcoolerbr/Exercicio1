# Exercicio1

Report de Atividades

Referente as alterações descritas no documento “Teste-NetShoes” foi detectada a necessidade de criação de um Windows form simples para salvar informações pertinentes a NFe. O Form contém os seguintes campos: Nome, UF Origem, UF Destino, Nome Produto, Código Produto, Valor e uma flag Brinde.
A primeira etapa do desenvolvimento consistiu em efetuar a serialização da classe NotaFiscal para que todas as informações contidas no objeto fossem convertidas para XML, inclusive os itens da NotaFiscal. Foi criada uma chave no appconfig para definir o path onde o XML deverá ser salvo seguindo instruções contidas no documento.
Na classe NotaFiscalRepository foram adicionados os métodos para salvar a NotaFiscal.  Foi criada a classe NotaFiscalItemRepository para salvar os Itens utilizando as procedures anexadas no projeto. Por se tratar de uma aplicação simples os métodos foram implementados com SqlConnection e SqlCommand para execução das chamadas, a interface IDisposable foi implementada garantindo o fechamento das conexões após utilização.
Novos campos foram criados na aplicação, com os seguintes nomes: BaseIPI, AliquotaIPI, ValorIPI e Desconto, todos com suas regras de preenchimento de acordo com a documentação fornecida. Também foi criado o campo Valor na tabela NotaFiscalItens, este campo existia na aplicação mas não era salvo no BD, portanto, tratava-se de um bug na aplicação.
Conforme documentação foi implementado o relatório P_REL_NOTA_FISCAL que exibe a soma dos campos solicitados, agrupados por CFOP.
Foram realizadas algumas melhorias como a limpeza dos campos ao gerar o XML e também a validação do preenchimento das informações. Os campos relacionados a UF foram substituídos por combos para facilitar o preenchimento.
Para preenchimento e validação das informações referentes a UF foi criada uma classe auxiliar com um enum para facilitar a visualização dos estados em questão, além de ser utilizado para o preenchimento das combos.
Os métodos foram remanejados separando um pouco a responsabilidade do preenchimento das informações, devido ao tamanho do projeto não achei necessário a separação em camadas, mas em uma possível refatoração seria interessante.
Por fim, foram detectados 2 bugs na aplicação, um deles referente a alimentação das UFs de destino e origem que estavam trocados e outro referente aos itens da nota que não eram adicionados na lista da classe NotaFiscal, ambos foram corrigidos.
Não foi realizada a implementação de testes unitários para a aplicação.  

