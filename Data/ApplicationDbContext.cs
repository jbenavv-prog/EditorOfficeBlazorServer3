using Microsoft.EntityFrameworkCore;
using EditorOfficeBlazorServer3.Models;

namespace EditorOfficeBlazorServer3.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
