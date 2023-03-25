using Produtos.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Produtos.Application.Contratos
{
    public interface IProductService
    {
        public Task<Produto> AddProduct(Produto model);
        public Task<Produto> UpdateProduct(int productId, Produto model);
        public Task<bool> DeleteProduct(int productId);
        public Task<Produto[]> GetAllProductAsync();
        public Task<Produto> GetProductById(int productId);
    }
}
