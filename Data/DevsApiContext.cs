using DevsApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DevsApi.Data
{
    public class DevsApiContext : IdentityDbContext
    {
        public DevsApiContext(DbContextOptions<DevsApiContext> options) : base(options)
        {
            
        }

        public DbSet<Developer> Developers { get; set; }
        
    }
}