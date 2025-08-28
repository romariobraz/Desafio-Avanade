using Entity.BD;
using Microsoft.EntityFrameworkCore;

namespace Avanade.Contexto
{
    public class VendasContexto : DbContext
    {
        public VendasContexto(DbContextOptions<VendasContexto> options) : base(options)
        {
        }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<PedidoItem> PedidoItens { get; set; }
    }
}
