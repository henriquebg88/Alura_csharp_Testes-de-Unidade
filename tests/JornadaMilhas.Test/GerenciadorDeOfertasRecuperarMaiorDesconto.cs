using Bogus;
using JornadaMilhasV1.Gerencidor;
using JornadaMilhasV1.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhas.Test
{
    public class GerenciadorDeOfertasRecuperarMaiorDesconto
    {
        [Fact]
        public void RetornarOfertaNulaDeListaVazia()
        {
            var lista = new List<OfertaViagem>();
            var gerenciadorDeOfertas = new GerenciadorDeOfertas(lista);
            Func<OfertaViagem, bool> filtro = x => x.Rota.Destino == "São Paulo";

            var oferta = gerenciadorDeOfertas.RecuperaMaiorDesconto(filtro);

            Assert.Null(oferta);
        }

        [Fact]
        public void RetornaOfertaEspecificaQuandoDestinoSaoPauloEDesconto40()
        {
            var mockPeriodo = new Faker<Periodo>()
                .CustomInstantiator( f =>
                {
                    DateTime dataInicio = f.Date.Soon();
                    return new Periodo(dataInicio, dataInicio.AddDays(30));
                });
            var rota = new Rota("Curitiba", "Brasília");

            var mockOferta = new Faker<OfertaViagem>()
                .CustomInstantiator(f =>
                
                    new OfertaViagem(
                        rota, 
                        mockPeriodo.Generate(), 
                        f.Random.Int(1, 100) * 100)
                )
                .RuleFor(o => o.Desconto, () => 40)
                .RuleFor(o => o.Ativa, () => true);

            var ofertaEscolhida = new OfertaViagem(
                new Rota("Curitiba", "São Paulo"), 
                mockPeriodo.Generate(), 80)
            {
                Desconto = 40,
                Ativa = true
            };

            var lista = mockOferta.Generate(50);
            lista.Add(ofertaEscolhida);
            lista.AddRange(mockOferta.Generate(50));

            var gerenciadorDeOfertas = new GerenciadorDeOfertas(lista);

            var oferta = gerenciadorDeOfertas.RecuperaMaiorDesconto(x => x.Rota.Destino == "São Paulo");

            var precoEsperado = 40;

            Assert.NotNull(oferta);
            Assert.Equal(precoEsperado, oferta.PrecoDescontado, 0.01);
        }
    }
}
