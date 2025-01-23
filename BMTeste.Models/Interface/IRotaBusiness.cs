using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

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
