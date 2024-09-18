using Microsoft.EntityFrameworkCore;
using _03_WEB_beforetest.Modules;

namespace _03_WEB_beforetest.Data
{
    public class ComponentsDbContext:DbContext
    {
        public ComponentsDbContext(DbContextOptions<ComponentsDbContext> options) : base(options) 
        { 
        }

        public DbSet<PCComponents> Components {  get; set; }
        public DbSet<Distributors> Distributors { get; set; }

    }
}
