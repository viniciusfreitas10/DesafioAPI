using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProEvento.API.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public char EstReg { get; set; } //A - ATIVO || H - HISTÓRICO
        public DateTime DataFabricacao { get; set; }
        public DateTime DataValidade { get; set; }
        public int CodigoFornecedor { get; set; }
        public string DescricaoFornecedor { get; set; }
        public string Cnpj { get; set; }
    }
}
