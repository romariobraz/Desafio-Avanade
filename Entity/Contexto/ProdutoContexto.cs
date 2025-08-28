using Entity.BD;
using Microsoft.EntityFrameworkCore;

namespace Avanade.Contexto
{
    public class ProdutoContexto : DbContext
    {
        public ProdutoContexto(DbContextOptions<ProdutoContexto> options) : base(options)
        {
        }
        public DbSet<Produto> Produtos { get; set; }
    }
}
