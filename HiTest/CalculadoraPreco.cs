namespace HiTest
{
    public class Item
    {
        public string Nome { get; set; }
        public double Custo { get; set; }
        public int Validade { get; set; }
        public double Volume { get; set; }
        public bool NecessitaRefrigeracao { get; set; }

        public Item(string nome, double custo, int validade, double volume, bool necessitaRefrigeracao)
        {
            Nome = nome;
            Custo = custo;
            Validade = validade;
            Volume = volume;
            NecessitaRefrigeracao = necessitaRefrigeracao;
        }
    }

    public class ItemPriceCalculator
    {
        public static double CalcularPreco(Item item)
        {
            double custoAjustado = item.Custo;
            custoAjustado += item.Volume * 0.5;
            if (item.NecessitaRefrigeracao)
                custoAjustado += 2.0;

            return Hi.formulaMagica(custoAjustado, item.Validade);
        }
    }

    public static class Hi
    {
        public static double formulaMagica(double custo, int validade)
        {
            return custo * Math.Log(validade + 1);
        }
    }
}