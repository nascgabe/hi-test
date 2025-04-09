# (1) Em orientação a objetos,  ́e melhor:

## (a) Usar uma interface ou uma classe abstrata? Por que? Cite exemplos.
-  Depende do contexto. Por exemplo, quando diferentes classes precisam ter o mesmo comportamento sem compartilhar código a interface irá atender melhor como por exemplo, um serviço que faz o disparo de diversos tipos de notificações como SMS, Email, Whatsapp. Nesse caso precisa só de um contrato comum de envio mas com implementações diferentes. Já a classe abstrata atenderá melhor quando várias classes compartilham lógica comum mas possuem regras diferentes. Por exemplo, tipos de conta bancária (Corrente e Poupança), elas compartilham comportamentos como depósito mas tem regras especificas para saque.

## (b) Usar herança ou delegação a outros objetos? Por que? Cite exemplos.
- Também depende do contexto mas na maioria das vezes prefiro a delegação por ser mais flexível e reutilizável que a Herança porque uma classe utiliza outra sem depender muito da estrutura interna da outra. Uma regra interessante para definir quando usar um ou outro é a relação 'é um' ou 'tem um'. Por exemplo, em um sistema de RH desenvolvedores e QA's são tipos de Funcionario e compartilham lógica de salário nesse caso a herança faz mais sentido pois desenvolvedor 'é um' funcionario. Agora um sistema de pedido precisa calcular o frete mas delega isso para classes como FreteCorreios ou FreteTransportadora, que implementam a interface ICalculadoraFrete, nesse caso delegação faz mais sentido porque pedido tem um calculo de frete.

# (2) Um hipermercado muito tradicional descobriu uma fórmula mágica para calcular o preço a ser cobrado por um determinado item, fazendo com que o lucro seja maximizado.
- Implementação em código nesse repositório.

# (3) Um candidato a prefeito quer saber quais ruas ele deve visitar para impactar o maior número de eleitores.
- Implementação em código nesse repositório.

# (4) Esta questão aborda o tratamento de erros orientado a objetos.

## (a) É boa prática definir um tipo específico de exceção que estende da classe Exception? Se sim, em quais casos?
-  Eu considero uma boa prática definir um tipo especifico de exceção que estenda da classe Exception mas isso deve ser feito em situações especificas quando o uso de exceções genéricas como Exception ou ArgumentException não fornece informações claras sobre o erro. Por exemplo, em um sistema bancário lançar uma exceção SaldoInsuficienteException é mais claro do que lançar uma Exception genérica mas para não devemos criar Exceptions para erros genéricos como MyNullException, nesse caso o ArgumentNullException funciona bem. No geral gosto de utilizar OperationResult nos meus retornos para não lançar nenhuma exceção de forma desnecessária ou deixar erros sem tratamento.

## (b) Quando você capturaria uma exceção através de cláusulas try e catch? Por que?
- Eu utilizaria o try e catch quando precisasse tratar erros que possam ocorrer em tempo de execução para evitar a interrupção do funcionamento do sistema. Tem outros artificios que gosto de usar como complemento ao try catch como o padrão de circuit breaker em falhas de comunicação com API externa ou com o banco de dados para garantir a resiliencia da minha aplicação. 

## (c) Em quais situações você lançaria uma exceção? Cite exemplos.
- Eu lançaria exceções em situações onde ocorresse alguma violação de regras de negócio, dados invalidos, alguma operação não autorizada ou falhas de comunicação com recursos externos. É importante avaliar com cuidado cada situação porque podemos acabar utilizando as exceções como controle de fluxo e não é uma boa prática. 

# (5) Considere um web service responsável por crédito e débito em uma conta corrente.
- Analisando a solução da questão posso dizer que atende do ponto de vista funcional mas não garante segurança em cenários de concorrencia. Abaixo vou detalhar as situações que podem acontecer:
-- Múltiplas chamadas simultaneas para debitar e creditar na mesma conta podem gerar inconsistencia de dados.
-- A lógica de verificação conta.podeDebitar(valor) pode ser interrompida entre a verificação e a atualização da conta e outra operação pode alterar o saldo antes de finalizar a primeira.
-- As operações de débito e atualização não são realizadas juntas como uma unica ação. Se uma delas falhar o saldo da conta pode ficar errado.
-- Não tem verificação para valores invalidos que também pode levar a inconsistencia de dados.
- Minhas sugestões para solucionar os pontos acima é utilizar sincronização com o lock do C# para evitar os problemas de concorrencia de maneira simples e rápida, melhorar a atomicidade fazendo uso de transações no banco de dados, e uma validação explicita da entrada para garantir a validade dos dados de entrada.  

# (6) Consulta SQL.
```sql
/* SELECT
  pl.nome AS nome_produto_limpeza,
  a.nome AS nome_alimento,
  ROUND((ee_pl.preco + ee_a.preco) * 0.85, 2) AS preco_kit,
  ROUND(((ee_pl.preco + ee_a.preco) * 0.85) - (ee_pl.custo + ee_a.custo), 2) AS lucro_kit,
  CASE 
    WHEN a.data_validade < pl.data_validade THEN a.data_validade 
    ELSE pl.data_validade 
  END AS data_validade_kit
FROM Produto_Limpeza pl
JOIN Elemento_Estoque ee_pl ON pl.id_elemento_estoque = ee_pl.id
JOIN (
  SELECT id_produto_limpeza
  FROM Pesquisa_Mercado
  GROUP BY id_produto_limpeza
  HAVING AVG(satisfacao) > 70
) pm ON pl.id = pm.id_produto_limpeza
JOIN Alimento a ON 1=1
JOIN Elemento_Estoque ee_a ON a.id_elemento_estoque = ee_a.id
WHERE a.data_validade <= DATE_ADD(CURRENT_DATE(), INTERVAL 5 DAY)
ORDER BY lucro_kit DESC;
 */
