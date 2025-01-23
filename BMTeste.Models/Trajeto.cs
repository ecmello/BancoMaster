namespace BMTeste.Domain.Models
{
    public class Trajeto
    {
        public IEnumerable<Rota> RotasDoTrajeto { get; set; }
        public decimal custo { get { return RotasDoTrajeto.Sum(r => r.Valor); } }
        public bool Sucesso { get; set; }
        public bool Finalizada { get; set; }
        public Trajeto(Rota? rota = null)
        {
            RotasDoTrajeto = new List<Rota>();
            if (rota != null) RotasDoTrajeto = RotasDoTrajeto.Append(rota);
        }

        public Trajeto(IEnumerable<Rota>? rotas = null)
        {
            RotasDoTrajeto = new List<Rota>();
            if (rotas != null) RotasDoTrajeto = RotasDoTrajeto.Concat(rotas);
        }
    }
}
