using Microsoft.EntityFrameworkCore;

namespace FriendCircle.Data
{
    public class MySqlDbContext : DbContext
    {
        public MySqlDbContext(DbContextOptions<MySqlDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }

    public class User
    {
        public string UserId { get; set; }
        public string Name { get; set; }
    }
}