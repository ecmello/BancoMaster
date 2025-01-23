namespace BMTeste.Domain.Models.Interface
{
    public interface IRotaRepository
    {
        IEnumerable<Rota> Rotas { get; }
        bool IncluirRota(Rota rota);
    }
}
