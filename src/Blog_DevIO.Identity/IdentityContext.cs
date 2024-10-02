using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog_DevIO.Identity
{
    public class IdentityDBContext : IdentityDbContext
    {
        public IdentityDBContext(DbContextOptions<IdentityDBContext> options) : base(options) { }    
     
    }
}
