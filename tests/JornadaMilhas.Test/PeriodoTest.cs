using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test
{
    public class PeriodoTest
    {
        [Fact]
        public void RetornaPeriodoInvalidoQuandoDataInicioMenorQueDataFim()
        {
            Periodo periodo = new(new DateTime(2024, 2, 8), new DateTime(2024, 2, 5));

            Assert.False(periodo.EhValido);
        }
    }
}
