namespace HiTest
{
    public class Rua
    {
        public string Cep { get; set; }
        public string Nome { get; set; }

        public Rua(string cep, string nome)
        {
            Cep = cep;
            Nome = nome;
        }
    }

    public class Casa
    {
        public Rua Rua { get; set; }
        public int Numero { get; set; }
        public int TotalEleitores { get; set; }

        public Casa(Rua rua, int numero, int totalEleitores)
        {
            Rua = rua;
            Numero = numero;
            TotalEleitores = totalEleitores;
        }
    }

    public class EleitorImpacto
    {
        public static List<Rua> ObterRuasOrdenadas(List<Casa> casas)
        {
            Dictionary<Rua, int> eleitoresPorRua = new Dictionary<Rua, int>();

            foreach (var casa in casas)
            {
                if (eleitoresPorRua.ContainsKey(casa.Rua))
                {
                    eleitoresPorRua[casa.Rua] += casa.TotalEleitores;
                }
                else
                {
                    eleitoresPorRua[casa.Rua] = casa.TotalEleitores;
                }
            }

            return eleitoresPorRua
                .OrderByDescending(x => x.Value)
                .Select(x => x.Key)
                .ToList();
        }
    }
}