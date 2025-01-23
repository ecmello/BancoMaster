namespace BMTeste.Domain.Models.Interface
{
    public interface IRotaBusiness
    {
        IEnumerable<Rota> Rotas { get; }
        (bool ValidarEntrarDados, string MensagemValidarEntrada, Rota RotaDesejadaValidada) ValidarEntrada(string[] DadosEntrada);

        bool IncluirRota(Rota rota);

        Trajeto ObterMelhorRota(Rota RotaDesejada);
    }
}
