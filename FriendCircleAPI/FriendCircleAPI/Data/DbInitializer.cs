namespace FriendCircleAPI.Data
{
    public class DbInitializer
    {
        public static void Initialize(MySqlDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}
