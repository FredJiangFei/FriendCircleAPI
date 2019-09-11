using System;
using System.Linq;

namespace FriendCircle.Data
{
    public class DbInitializer
    {
        public static void Initialize(MySqlDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return;
            }

            var users = new User[]
            {
                new User{
                    UserId = Guid.NewGuid().ToString(),
                    Name = "Fred"
                },
            };

            context.Users.AddRange(users);

            context.SaveChanges();
        }
    }
}
