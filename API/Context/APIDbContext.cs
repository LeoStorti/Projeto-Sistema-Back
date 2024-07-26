using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Context
{
    public class APIDbContext : DbContext
    {
        public APIDbContext(DbContextOptions<APIDbContext> options) : base(options)
        {
            try
            {
                if (!this.Database.CanConnect())
                {
                    throw new Exception("Não foi possível conectar ao banco de dados.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao conectar ao banco de dados: " + ex.Message);
                throw;
            }
        }

        public DbSet<Fornecedor> CadastroFornecedor { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Vendas> Vendas { get; set; }
        public DbSet<Compras> Compras { get; set; }
        public DbSet<Clientes> CadastroCliente { get; set; }
        public DbSet<Itens_Compra> Itens_Compra { get; set; }
        public DbSet<Itens_Venda> Itens_Venda { get; set; }

        public DbSet<Produtos> Produtos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Itens_Venda>()
                .HasOne(iv => iv.Vendas)
                .WithMany(v => v.Itens_Venda)
                .HasForeignKey(iv => iv.Vendas_Id_FK)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            modelBuilder.Entity<Itens_Venda>()
                .HasOne(iv => iv.Produtos)
                .WithMany(p => p.ItensVenda)
                .HasForeignKey(iv => iv.ProdutoId_FK)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            modelBuilder.Entity<Itens_Compra>()
                 .HasKey(ic => ic.Itens_Compra_PK);

            modelBuilder.Entity<Itens_Compra>()
                .HasOne(ic => ic.Compras)
                .WithMany(c => c.Itens_Compra)
                .HasForeignKey(ic => ic.Compra_Id);

            modelBuilder.Entity<Itens_Compra>()
                .HasOne(ic => ic.Produtos)
                .WithMany()
                .HasForeignKey(ic => ic.ProductId);
        }

    }
}
