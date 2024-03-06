using LivrariaAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace LivrariaAPI.Context
{
    public class LivrariaContext : DbContext
    {
        public LivrariaContext(DbContextOptions<LivrariaContext> option)
             : base(option)
        {

        }

        public DbSet<Product> Products => Set<Product>();
    }
}
