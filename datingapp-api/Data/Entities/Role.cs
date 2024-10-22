using Microsoft.AspNetCore.Identity;

namespace datingapp_api.Data.Entities
{
    public class Role : IdentityRole<int>
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
