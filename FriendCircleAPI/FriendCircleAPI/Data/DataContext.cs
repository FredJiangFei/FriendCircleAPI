using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FriendCircleAPI.Data
{
    public class MySqlDbContext : DbContext
    {
        public MySqlDbContext(DbContextOptions<MySqlDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Relation> Relations { get; set; }
        public DbSet<Moment> Moments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Relation>()
            .HasKey(x => new { x.UserId, x.FriendId });

            modelBuilder.Entity<Relation>()
            .HasOne(e => e.User)
            .WithMany(e => e.Friends)
            .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Relation>()
            .HasOne(e => e.Friend)
            .WithMany(e => e.FriendOf)
            .HasForeignKey(e => e.FriendId);

            modelBuilder.Entity<User>()
            .HasMany(c => c.Moments)
            .WithOne(e => e.User);
        }
    }

    public class User
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Relation> Friends { get; set; } = new List<Relation>();
        public virtual ICollection<Relation> FriendOf { get; set; } = new List<Relation>();
        public virtual ICollection<Moment> Moments { get; set; } = new List<Moment>();
    }

    public class Relation
    {
        public string UserId { get; set; }
        public string FriendId { get; set; }

        public User User { get; set; }
        public User Friend { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }

    public class Moment
    {
        public User User { get; set; }
        public string MomentId { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}