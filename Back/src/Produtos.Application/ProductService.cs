using Produtos.Domain;
using Produtos.Application.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Produtos.Persistence.Contratos;

namespace Produtos.Application
{
    public class ProductService : IProductService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IProductPersist _productPersist;

        public ProductService(IGeralPersist geralPersist, IProductPersist productPersist)
        {
            _geralPersist = geralPersist;
            _productPersist = productPersist;
        }

        public async Task<Produto> AddProduct(Produto model)
        {
            try
            {
                _geralPersist.add<Produto>(model);
                if (await _geralPersist.SaveChangesAsync())
                {
                    return await _productPersist.GetProductById(model.Id);
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception("AddProduct - Erro ao tentar adicionar um novo produto " + e.Message);
            }
        }

        public async Task<Produto> UpdateProduct(int productId, Produto model)
        {
            try
            {
                var product = await _productPersist.GetProductById(productId);
                if (product == null) return null;

                model.Id = product.Id;

                _geralPersist.Update(model);

                if (await _geralPersist.SaveChangesAsync())
                {
                    return await _productPersist.GetProductById(model.Id);
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception($"UpdateProduct: Erro ao tentar atualizar o produto de id: {productId} - " + e.Message);
            }
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            try
            {
                var product = await _productPersist.GetProductById(productId);

                if (product == null) throw new Exception("DeleteProduct: Produto para delete não encontrado.");

                _geralPersist.Delete<Produto>(product);

                return await _geralPersist.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception("DeleteProduct - Erro ao tentar deletar o produto " + e.Message);
            }
        }

        public async Task<Produto[]> GetAllProductAsync()
        {
            try
            {
                var products = await _productPersist.GetAllProductAsync();
                if (products == null) return null;

                return products;
            }
            catch (Exception e)
            {
                throw new Exception("GetAllProductAsync - Erro ao recuperar os produtos: " + e.Message);
            }
        }

        public async Task<Produto> GetProductById(int productId)
        {
            try
            {
                var product = await _productPersist.GetProductById(productId);
                if (product == null) return null;

                return product;
            }
            catch (Exception e)
            {
                throw new Exception($"GetProductById - Erro ao recuperar o produto de id: {productId} - " + e.Message);
            }
        }
    }
}
