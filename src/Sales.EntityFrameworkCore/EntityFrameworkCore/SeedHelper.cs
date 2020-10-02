using System;
using System.Transactions;

using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore.Uow;

using Microsoft.EntityFrameworkCore;

namespace Sales.EntityFrameworkCore.EntityFrameworkCore
{
    public static class SeedHelper
    {
        public static void SeedHostDb(IIocResolver iocResolver)
        {
            WithDbContext<SalesDbContext>(iocResolver, SeedHostDb);
        }
        public static void SeedHostDb(SalesDbContext context)
        {
            context.Database.Migrate();
        }
        private static void WithDbContext<TDbContext>(IIocResolver iocResolver, Action<TDbContext> contextAction)
            where TDbContext : Microsoft.EntityFrameworkCore.DbContext
        {
            using var uowManager = iocResolver.ResolveAsDisposable<IUnitOfWorkManager>();
            using var uow = uowManager.Object.Begin(TransactionScopeOption.Suppress);
            var context = uowManager.Object.Current.GetDbContext<TDbContext>();
            contextAction(context);
            uow.Complete();
        }
    }
}
