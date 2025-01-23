using BMTeste.Domain.Models;
using BMTeste.Domain.Models.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace BMTeste.Business
{
    public partial class AplicacaoBusiness
    {
        private readonly IRotaBusiness _rotaBusiness;
        private readonly ISistemaDeArquivos _sistemaDeArquivos;
        private bool EntrarDados = true;
        private bool Execute = true;
        private Rota RotaDesejadaValidada = new Rota();
        public AplicacaoBusiness(IRotaBusiness rotaBusiness, ISistemaDeArquivos sistemaDeArquivos)
        {
            _rotaBusiness = rotaBusiness;
            _sistemaDeArquivos = sistemaDeArquivos;
        }

        public void Inicio()
        {
            while (Execute)
            {
                MenuPrincipal();
            }

            if (_sistemaDeArquivos.ExisteArquivoDados())
            {
                while (EntrarDados)
                {
                    string _entradaRotaDesejada = Perguntar("Digite a rota desejada :", typeof(string), false);
                    string[] DadosEntrada = _entradaRotaDesejada.Split('-');

                    //Write("Digite a rota desejada :");
                    //string[] DadosEntrada = ReadLine().ToUpper().Split('-');

                    (bool ValidarEntrarDados, string MensagemValidarEntrada, Rota RotaDesejadaValidada) _validacaoEntrada = _rotaBusiness.ValidarEntrada(DadosEntrada);
                    EntrarDados = _validacaoEntrada.ValidarEntrarDados;
                    if (EntrarDados)
                    {
                        WriteLine(_validacaoEntrada.MensagemValidarEntrada);
                    }
                    else
                    {
                        RotaDesejadaValidada = _validacaoEntrada.RotaDesejadaValidada;
                    }

                    Trajeto MelhorRota = _rotaBusiness.ObterMelhorRota(RotaDesejadaValidada);

                    var OrigensDoTrajeto = MelhorRota.RotasDoTrajeto.Select(r => r.Origem).ToList().ToArray();
                    var Mensagem = $"Melhor Rota {string.Join(" - ", OrigensDoTrajeto)} - {RotaDesejadaValidada.Destino} ao custo de $ {MelhorRota.custo}";
                    WriteLine(Mensagem);
                }
            }
            else
            {
                WriteLine("ATENCAO:Não foi encontrado em arquivo de dados de Rotas");
            }
        }

    }
}
