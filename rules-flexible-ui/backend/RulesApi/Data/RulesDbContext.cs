using Microsoft.EntityFrameworkCore;
using RulesApi.Models;

namespace RulesApi.Data
{
    public class RulesDbContext : DbContext
    {
        public RulesDbContext(DbContextOptions<RulesDbContext> options) : base(options) { }
        public DbSet<Rule> Rules => Set<Rule>();
    }
}
