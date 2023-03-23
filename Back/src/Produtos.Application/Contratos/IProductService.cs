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
        Task<Produto> AddProduct(Produto model);
        Task<Produto> UpdateProduct(int productId, Produto model);
        Task<bool> DeleteProduct(int productId);
        Task<Produto[]> GetAllProductAsync();
        Task<Produto> GetProductById(int productId);
    }
}
