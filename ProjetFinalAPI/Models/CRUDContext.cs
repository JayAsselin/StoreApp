using Microsoft.EntityFrameworkCore;

namespace ProjetFinalAPI.Models
{
    public class CRUDContext:DbContext
    {
        public CRUDContext(DbContextOptions<CRUDContext> options)
            : base(options)
        {

        }

        public DbSet<SmartDevice> SmartDevices { get; set; } = null!;
        public DbSet<Invoice> Invoices { get; set; } = null!;
    }
}
