using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;

using Sales.Domain;
using Sales.Domain.Options;
using Sales.EntityFrameworkCore.EntityFrameworkCore;

namespace Sales.EntityFrameworkCore
{
    [DependsOn(
        typeof(SalesCoreModule),
        typeof(SalesDomainModule),
        typeof(AbpEntityFrameworkCoreModule))]
    public class SalesEntityFrameworkCoreModule : AbpModule
    {
        /* Used it tests to skip dbcontext registration, in order to use in-memory database of EF Core */
        public bool SkipDbContextRegistration { get; set; }
        public bool SkipSeed { get; set; }

        public override void PreInitialize()
        {
            if (!SkipDbContextRegistration)
            {
                Configuration.Modules.AbpEfCore().AddDbContext<SalesDbContext>(options =>
                {
                    if (options.ExistingConnection != null)
                    {
                        SalesDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                    }
                    else
                    {
                        SalesDbContextConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                    }
                });
            }
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SalesEntityFrameworkCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            SkipSeed = Configuration.Get<IDatabaseOptions>().SkipSeed;
            if (!SkipSeed)
            {
                SeedHelper.SeedHostDb(IocManager);
            }
        }
    }
}