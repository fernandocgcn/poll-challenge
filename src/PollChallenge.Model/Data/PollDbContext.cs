using Microsoft.EntityFrameworkCore;
using PollChallenge.Model.Entities;
using System.Reflection;

namespace PollChallenge.Model.Data
{
    public class PollDbContext : DbContext
    {
        public PollDbContext(DbContextOptions<PollDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Poll> Polls { get; set; }
        public DbSet<Option> Options { get; set; }
    }
}
