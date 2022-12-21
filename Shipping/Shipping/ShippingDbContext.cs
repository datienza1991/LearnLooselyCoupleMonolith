using Microsoft.EntityFrameworkCore;

namespace Shipping
{
    public class ShippingDbContext : DbContext
    {
        public DbSet<Delivery> Deliveries { get; set; }
    }
}