using BMTeste.Domain.Models;
using BMTeste.Domain.Models.Interface;

namespace BMTeste.Application.Business
{
    public class RotaBusiness : IRotaBusiness
    {
        private static string _ENTRADA_INCORRETA_ = "Entrada não reconhecida! Utilize o formato XXX-XXX para informar a rota desejada";
        private static string _LOCALIDADE_INEXISTENTE_ = "Não foi encontrada a origem e/ou destino no cadastro de rotas";

        private readonly IRotaRepository _rotaRepository;
        public IEnumerable<Rota> Rotas { get { return _rotaRepository.Rotas; } }

        public RotaBusiness(IRotaRepository rotaRepository) 
        {
            _rotaRepository = rotaRepository;
        }

        public bool IncluirRota(Rota rota)
        {
            return _rotaRepository.IncluirRota(rota);
        }

        public Trajeto ObterMelhorRota(Rota rotaDesejada)
        {
            Trajeto MelhorRota = null;
            IEnumerable<Trajeto> completas = new List<Trajeto>();
            IEnumerable<Rota> origens = Rotas.Where(r => r.Origem == rotaDesejada.Origem);
            IEnumerable<Rota> conexoes = [];
            IEnumerable<string> localidadesJaAlcancadas;

            foreach (var origem in origens)
                completas = completas.Append(new Trajeto(origem));

            //liveloop
            foreach (var rota in completas)
            {
                localidadesJaAlcancadas = [];

                //Fique no loop até encontrar correspondencia ou um fluxo circular
                while (!rota.Finalizada)
                {
                    //encontrada a correspondencia
                    if (rota.RotasDoTrajeto.Last().Destino == rotaDesejada.Destino)
                    {
                        rota.Finalizada = true;
                        rota.Sucesso = true;
                        continue;
                    }

                    //deteccao de fluxo circular
                    if (localidadesJaAlcancadas.Contains<string>(rotaDesejada.Destino))
                    {
                        rota.Finalizada = true;
                        rota.Sucesso = false;
                        continue;
                    }
                    localidadesJaAlcancadas = localidadesJaAlcancadas.Append(rota.RotasDoTrajeto.Last().Destino);

                    //localizar conexoes
                    conexoes = _rotaRepository.Rotas.Where(r => r.Origem == rota.RotasDoTrajeto.Last().Destino);

                    //identificar e enfileirar conexoes alternativas
                    if (conexoes.Count() > 1)
                    {
                        //retirar a primeira ocorrencia pertencente a esta rota
                        conexoes = conexoes.Take(1);

                        //incluir as demais ocorrencias à novas rotas completas a serem abertas
                        foreach (var conexao in conexoes)
                        {
                            IEnumerable<Trajeto> conexoesAlternativas = new List<Trajeto>
                        {
                            rota,
                            new Trajeto(conexao)
                        };
                            completas = completas.Concat(conexoesAlternativas);
                        };
                    }
                    else // existe apenas uma conexao disponivel
                    {
                        rota.RotasDoTrajeto = rota.RotasDoTrajeto.Append(conexoes.First());
                    }
                }
            }

            //extraindo Rotas completas e com destino correspondente
            var RotasExistentes = completas.Where(c => c.Sucesso = true && c.Finalizada == true);
            //escolhendo a mais barata das rotas existente se existir.
            if (RotasExistentes.ToList().Count() > 0)
            {
                MelhorRota = RotasExistentes.OrderBy(m => m.custo).First();
            }
            return MelhorRota;
        }

        public (bool ValidarEntrarDados, string MensagemValidarEntrada, Rota RotaDesejadaValidada) ValidarEntrada(string[] DadosEntrada)
        {
            bool ReentrarDados = true;// principio do privilégio mínimo
            string MensagemValidarEntrada = string.Empty;
            Rota rotaDesejadaValidada = new Rota();

            if (DadosEntrada != null && DadosEntrada.Length == 2)
            {
                if (ValidarLocalidades(DadosEntrada))
                {
                    ReentrarDados = false;
                    rotaDesejadaValidada = new Rota { Origem = DadosEntrada[0], Destino = DadosEntrada[1] };
                }
                else
                {
                    MensagemValidarEntrada = _LOCALIDADE_INEXISTENTE_;
                }
            }
            else
            {
                MensagemValidarEntrada = _ENTRADA_INCORRETA_;
            }

            return (ReentrarDados, MensagemValidarEntrada, rotaDesejadaValidada);

        }

        private bool ValidarLocalidades(string[] rotaDesejada)
        {
            bool result = false;

            if (rotaDesejada[0] != rotaDesejada[1])
            {
                var localidadesCadastradas = _rotaRepository.Rotas.Select(r => r.Origem).ToArray().Concat(
                                             _rotaRepository.Rotas.Select(r => r.Destino).ToArray()).Distinct();
                result = localidadesCadastradas.Contains(rotaDesejada[0]) && localidadesCadastradas.Contains(rotaDesejada[1]);
            }

            return result;
        }

    }
}
