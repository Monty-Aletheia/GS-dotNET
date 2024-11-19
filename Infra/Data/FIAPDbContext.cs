using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data
{
    public class FIAPDbContext(DbContextOptions<FIAPDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }

    }
}
