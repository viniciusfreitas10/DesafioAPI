using Microsoft.EntityFrameworkCore;
using Produtos.Domain;
using Produtos.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Produtos.Persistence.Contratos;
using Produtos.Persistence.Context;

namespace ProEventos.Persistence
{
    public class ProductPersist : IProductPersist
    {
        private readonly ProductContext _context;

        public ProductPersist(ProductContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<Produto[]> GetAllProductAsync()
        {
            try
            {
                IQueryable<Produto> query = _context.Produtos;

                query = query
                    .AsNoTracking().
                    OrderBy(e => e.Id);

                return await query.ToArrayAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }
        
        public async Task<Produto> GetProductById(int productId)
        {
            try
            {
                IQueryable<Produto> query = _context.Produtos;

                query = query
                    .Where(e => e.Id == productId)
                    .AsNoTracking()
                    .OrderBy(e => e.Id);

                return await query.FirstOrDefaultAsync();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
