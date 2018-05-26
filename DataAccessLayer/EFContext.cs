using Park.Victor.Grein.Benner.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Park.Victor.Grein.Benner.DataAccessLayer
{
    public class EFContext : DbContext
    {
        public EFContext() : base("EFConnectionString") {}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); //Remove uma Convention do EntityFramework que coloca plural nas tabelas
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Movimentacao> Movimentacoes { get; set; }
        public DbSet<Preco> Precos { get; set; }
        
    }
}