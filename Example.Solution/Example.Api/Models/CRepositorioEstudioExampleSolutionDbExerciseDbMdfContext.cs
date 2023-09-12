using Microsoft.EntityFrameworkCore;

namespace Example.Api.Models;

public partial class CRepositorioEstudioExampleSolutionDbExerciseDbMdfContext : DbContext
{
    public CRepositorioEstudioExampleSolutionDbExerciseDbMdfContext()
    {
    }

    public CRepositorioEstudioExampleSolutionDbExerciseDbMdfContext(DbContextOptions<CRepositorioEstudioExampleSolutionDbExerciseDbMdfContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.IdProduct).HasName("PK__Product__2E8946D429250EE0");

            entity.ToTable("Product");

            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Image)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.Property(e => e.ProductName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
