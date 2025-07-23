using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JornadaMilhasV1.Validador;

namespace JornadaMilhasV1.Modelos;

public class OfertaViagem: Valida
{
    private double precoDescontado;

    const int DESCONTO_MAXIMO = 70;
    public int Id { get; set; }
    public Rota Rota { get; set; } 
    public Periodo Periodo { get; set; }
    public double Preco { get; set; }
    public double Desconto { get; set; }
    public double PrecoDescontado 
    {
        get
        {

            if (Desconto < 0) return Preco;
            if (Preco - Desconto <= Preco * (100 - DESCONTO_MAXIMO)/100) return Preco * (100 - DESCONTO_MAXIMO)/100;
            return Preco - Desconto;
        }
    }
    public bool Ativa { get; set; } = true;


    public OfertaViagem(Rota rota, Periodo periodo, double preco)
    {
        Rota = rota;
        Periodo = periodo;
        Preco = preco;
        Validar();
    }

    public override string ToString()
    {
        return $"Origem: {Rota.Origem}, Destino: {Rota.Destino}, Data de Ida: {Periodo.DataInicial.ToShortDateString()}, Data de Volta: {Periodo.DataFinal.ToShortDateString()}, Preço: {Preco:C}";
    }

    protected override void Validar()
    {
        if (!Periodo.EhValido)
            Erros.RegistrarErro(Periodo.Erros.Sumario);

        if (Rota == null || Periodo == null)
            Erros.RegistrarErro("A oferta de viagem não possui rota ou período válidos.");
        
        if (Preco <= 0)
            Erros.RegistrarErro("O preço da oferta de viagem deve ser maior que zero.");

    }
}
