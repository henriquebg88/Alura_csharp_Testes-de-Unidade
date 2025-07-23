using JornadaMilhasV1.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhas.Test
{
    public class OfertaViagemDesconto
    {
        [Fact]
        public void RetornaPrecoAtualizadoQuandoAplicadoDesconto()
        {
            Rota rotaTeste = new("origem", "destino");
            Periodo periodoTeste = new(DateTime.Parse("2024-01-02"), DateTime.Parse("2024-01-05"));
            double precoOriginal = 100.00;
            double desconto = 20.00;
            double precoComDesconto = precoOriginal - desconto;

            OfertaViagem oferta = new(rotaTeste, periodoTeste, precoOriginal);

            oferta.Desconto = desconto;

            Assert.Equal(precoComDesconto, oferta.PrecoDescontado);
        }

        [Theory]
        [InlineData(-10, 100)]
        [InlineData(0, 100)]
        [InlineData(20, 80)]
        [InlineData(80, 30)]
        [InlineData(100, 30)]
        [InlineData(200, 30)]
        public void ValidaDesconto(double desconto, double precoComDesconto)
        {
            Rota rotaTeste = new("origem", "destino");
            Periodo periodoTeste = new(DateTime.Parse("2024-01-02"), DateTime.Parse("2024-01-05"));
            double precoOriginal = 100.00;

            OfertaViagem oferta = new(rotaTeste, periodoTeste, precoOriginal);

            oferta.Desconto = desconto;

            Assert.Equal(precoComDesconto, oferta.PrecoDescontado, 0.01);
        }
    }
}
