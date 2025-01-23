using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTeste.Domain.Models.Interface
{
    public interface IRotaRepository
    {
        IEnumerable<Rota> Rotas { get; }
        bool IncluirRota(Rota rota);
    }
}
