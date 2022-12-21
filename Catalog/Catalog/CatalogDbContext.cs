using Microsoft.EntityFrameworkCore;

namespace Catalog;
public class CatalogDbContext : DbContext
{
    public DbSet<ProductModel>? Products { get; set; }
}
