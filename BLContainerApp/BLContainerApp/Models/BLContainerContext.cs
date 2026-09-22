// Trabalho Prático 02 - CBTSWE2 (ADS 671)
// Dupla: João do Valle Seixas Paula e André Luiz Nascimento de Andrade

using Microsoft.EntityFrameworkCore;

namespace BLContainerApp.Models
{
    public class BLContainerContext : DbContext
    {
        public BLContainerContext(DbContextOptions<BLContainerContext> options) : base(options)
        {
        }

        public DbSet<BL> BLs { get; set; }
        public DbSet<Container> Containers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BL>().ToTable("BL");
            modelBuilder.Entity<Container>().ToTable("Container");

            // Relacionamento: 1 BL para 0..N Containers.
            // BLId é obrigatório (Required) -> um Container sempre pertence a um BL.
            // Restrict impede excluir um BL que ainda possua Containers associados.
            modelBuilder.Entity<Container>()
                .HasOne(c => c.BL)
                .WithMany(b => b.Containers)
                .HasForeignKey(c => c.BLId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
