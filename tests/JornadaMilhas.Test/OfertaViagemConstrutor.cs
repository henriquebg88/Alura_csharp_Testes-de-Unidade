using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test
{
    public class OfertaViagemConstrutor
    {
        private Periodo periodoValido { get; set; }
        private Rota rotaValida { get; set; }

        public OfertaViagemConstrutor()
        {
            rotaValida = new("OrigemTeste", "DestinoTeste");
            periodoValido = new(new DateTime(2024, 2, 1), new DateTime(2024, 2, 5));
        }

        [Theory]
        [InlineData("", null, "2024-01-01", "2024-01-02", 0, false)]
        [InlineData("OrigemTeste", "DestinoTeste", "2024-02-01", "2024-02-05", 100, true)]
        [InlineData(null, "São Paulo", "2024-01-01", "2024-01-02", -1, false)]
        [InlineData("Vitória", "São Paulo", "2024-01-01", "2024-01-01", 0, false)]
        [InlineData("Rio de Janeiro", "São Paulo", "2024-01-01", "2024-01-02", -500, false)]
        public void Validacoes(string origem, string destino, string dataIda, string dataVolta, double preco, bool validacao)
        {
            Rota rotaTeste = new(origem, destino);
            Periodo periodoTeste = new( DateTime.Parse(dataIda), DateTime.Parse(dataVolta));

            OfertaViagem oferta = new(rotaTeste, periodoValido, preco);

            Assert.Equal(validacao, oferta.EhValido);
        }

        [Fact]
        public void RetornaOfertaValidaQuandoDadosValidos()
        {
            double preco = 100;

            var validacao = true;

            OfertaViagem oferta = new(rotaValida, periodoValido, preco);

            Assert.Equal(validacao, oferta.EhValido);
        }

        [Fact]
        public void RetornaOfertaInvalidaQuandoRotaNula()
        {
            Rota rotaNula = null;
            double preco = 100;

            OfertaViagem oferta = new(rotaNula, periodoValido, preco);

            Assert.Contains("A oferta de viagem não possui rota ou período válidos.", oferta.Erros.Sumario);
            Assert.False(oferta.EhValido);
        }

        [Fact]
        public void RetornaOfertaInvalidaQuandoPrecoNegativo()
        {
            double preco = -10;

            OfertaViagem oferta = new(rotaValida, periodoValido, preco);

            Assert.False(oferta.EhValido);
        }
    }
}