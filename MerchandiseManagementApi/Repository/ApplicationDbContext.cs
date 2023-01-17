using MerchandiseManagementApi.Dto.Source;
using Microsoft.EntityFrameworkCore;
#pragma warning disable CS8618

namespace MerchandiseManagementApi.Repository;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder
            .Entity<ProductDto>()
            .HasKey(product => product.Id);

        builder
            .Entity<CategoryDto>()
            .HasKey(category => category.Id);

        builder
            .Entity<ProductDto>()
            .HasOne(product => product.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);
        
        base.OnModelCreating(builder);
    }

    public DbSet<ProductDto> Products { get; set; }
    public DbSet<CategoryDto> Categories { get; set; }
}