using BlazorApp3.Shared;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp3.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Skills> Skills { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
