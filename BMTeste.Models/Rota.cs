using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMTeste.Domain.Models
{
    public class Rota : RotaBase
    {
        public string Origem { get; set; }
        public string Destino { get; set; }
    }
}
