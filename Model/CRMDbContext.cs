using Microsoft.EntityFrameworkCore;

namespace Model
{
    public class CRMDbContext : DbContext
    {
        public CRMDbContext(DbContextOptions<CRMDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<SaleChance> SaleChance { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<CusDevPlan> CusDevPlan { get; set; }
        public DbSet<Permission> Permission { get; set; }
        public DbSet<Module> Module { get; set; }
        public DbSet<CusContatct> CusContatct { get; set; }
        public DbSet<CusOrder> CusOrder { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<CusLoss> CusLoss { get; set; }
        public DbSet<CusReprieve> CusReprieve { get; set; }
        public DbSet<CusServer> CusServer { get; set; }
    }
}
