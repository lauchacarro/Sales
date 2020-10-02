using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

using Sales.Configuration;
using Sales.Domain.Options;
using Sales.EntityFrameworkCore.EntityFrameworkCore;
using Sales.Web;

namespace Sales.EntityFrameworkCore
{
    /* This class is needed to run EF Core PMC commands. Not used anywhere else */
    public class SalesDbContextFactory : IDesignTimeDbContextFactory<SalesDbContext>
    {
        public SalesDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<SalesDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder(), Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT"));
            var databaseOptoins = configuration.GetSection(nameof(DatabaseOptions)).Get<DatabaseOptions>();


            SalesDbContextConfigurer.Configure(
                builder,
                configuration.GetConnectionString(SalesConsts.ConnectionStringName)
            );

            return new SalesDbContext(databaseOptoins, builder.Options);
        }
    }
}