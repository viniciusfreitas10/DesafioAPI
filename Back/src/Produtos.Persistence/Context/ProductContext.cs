
using Microsoft.EntityFrameworkCore;
using Produtos.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Produtos.Persistence.Context
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) 
            :  base(options) {}
        public DbSet<Produto> Produtos { get; set; }
        
    }
}
