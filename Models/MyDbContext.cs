using Microsoft.EntityFrameworkCore;

namespace Projeto_Final.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }
        public DbSet<Consumidor> Consumidor { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<Boleto> Boleto { get; set; }
        public DbSet<CartaoCredito> CartaoCredito { get; set; }
    }
}