namespace HiTest
{
    class Program
    {
        static void Main(string[] args)
        {
            bool continuar = true;

            while (continuar)
            {
                Console.WriteLine("Escolha uma das opções abaixo:");
                Console.WriteLine("1 - Calcular preços de itens do estoque");
                Console.WriteLine("2 - Ordenar ruas pelo total de eleitores");
                Console.WriteLine("0 - Sair");
                var escolha = Console.ReadLine();

                switch (escolha)
                {
                    case "1":
                        TestarQuestao1();
                        break;
                    case "2":
                        TestarQuestao2();
                        break;
                    case "0":
                        continuar = false;
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
                Console.WriteLine();
            }
        }

        static void TestarQuestao1()
        {
            Console.WriteLine("Calcular preços de itens do estoque:");
            bool adicionarMaisItens = true;

            while (adicionarMaisItens)
            {
                Console.Write("Digite o nome do item: ");
                string nome = Console.ReadLine();

                Console.Write("Digite o custo do item (em reais): ");
                double custo = Convert.ToDouble(Console.ReadLine());

                Console.Write("Digite a validade do item (em dias): ");
                int validade = Convert.ToInt32(Console.ReadLine());

                Console.Write("Digite o volume do item (em metros cúbicos): ");
                double volume = Convert.ToDouble(Console.ReadLine());

                Console.Write("O item precisa de refrigeração? (s/n): ");
                bool necessitaRefrigeracao = Console.ReadLine()?.ToLower() == "s";

                var item = new Item(nome, custo, validade, volume, necessitaRefrigeracao);
                double preco = ItemPriceCalculator.CalcularPreco(item);

                Console.WriteLine($"Preço calculado para o item '{item.Nome}': R$ {preco:F2}");

                Console.WriteLine("Deseja adicionar outro item? (s/n): ");
                adicionarMaisItens = Console.ReadLine()?.ToLower() == "s";
            }
        }

        static void TestarQuestao2()
        {
            Console.WriteLine("Ordenar ruas pelo total de eleitores:");

            var rua1 = new Rua("12345-678", "Rua das Flores");
            var rua2 = new Rua("23456-789", "Rua dos Lírios");
            var rua3 = new Rua("34567-890", "Rua dos Cravos");

            var casas = new List<Casa>
            {
                new Casa(rua1, 1, 5),
                new Casa(rua1, 2, 8),
                new Casa(rua2, 3, 12),
                new Casa(rua3, 4, 7),
                new Casa(rua3, 5, 3),
                new Casa(rua2, 6, 10)
            };

            var ruasOrdenadas = EleitorImpacto.ObterRuasOrdenadas(casas);

            Console.WriteLine("Ruas ordenadas pelo total de eleitores:");
            foreach (var rua in ruasOrdenadas)
            {
                Console.WriteLine($"{rua.Nome} - {rua.Cep}");
            }
        }
    }
}