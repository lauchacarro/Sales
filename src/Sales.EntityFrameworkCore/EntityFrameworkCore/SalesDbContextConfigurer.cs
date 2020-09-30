using System.Data.Common;

using Microsoft.EntityFrameworkCore;

namespace Sales.EntityFrameworkCore.EntityFrameworkCore
{
    public static class SalesDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<SalesDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString, builder =>
            {
                builder.CommandTimeout(60 * 1000);
                //builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(5), null);
            });
        }
        public static void Configure(DbContextOptionsBuilder<SalesDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection, builder =>
            {
                builder.CommandTimeout(60 * 1000);
                //builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(5), null);
            });
        }
    }
}
