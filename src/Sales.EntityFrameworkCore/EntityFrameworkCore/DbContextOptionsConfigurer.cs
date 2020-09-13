using Microsoft.EntityFrameworkCore;

namespace Sales.EntityFrameworkCore
{
    public static class DbContextOptionsConfigurer
    {
        public static void Configure(
            DbContextOptionsBuilder<SalesDbContext> dbContextOptions,
            string connectionString
            )
        {
            /* This is the single point to configure DbContextOptions for SalesDbContext */
            dbContextOptions.UseSqlServer(connectionString);
        }
    }
}
