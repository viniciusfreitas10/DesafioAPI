using Produtos.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Produtos.Persistence.Contratos
{
    public interface IProductPersist
    {
        public Task<Produto[]> GetAllProductAsync();
        public Task<Produto> GetProductById(int productId);
    }
}
