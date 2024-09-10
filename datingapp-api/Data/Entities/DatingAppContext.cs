using Microsoft.EntityFrameworkCore;

namespace datingapp_api.Data.Entities
{
    public class DatingAppContext : DbContext
    {
        public DatingAppContext(DbContextOptions<DatingAppContext> opt): base(opt)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }
    }
}
