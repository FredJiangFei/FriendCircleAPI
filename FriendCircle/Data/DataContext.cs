using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FriendCircle.Data
{
    public class MySqlDbContext : DbContext
    {
        public MySqlDbContext(DbContextOptions<MySqlDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        // public DbSet<Relation> Relations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Relation>()
                .HasKey(x => new { x.UserId, x.FriendId });
        }
    }

    public class User
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public ICollection<Relation> Friends { get; set; } = new List<Relation>();
    }

    public class Relation
    {
        public string UserId { get; set; }
        public string FriendId { get; set; }

        public User Me { get; set; }
        public User Friend { get; set; }
    }
}