using System;
using System.Threading.Tasks;

using Abp.TestBase;

using Sales.EntityFrameworkCore;
using Sales.Tests.TestDatas;

namespace Sales.Tests
{
    public class SalesTestBase : AbpIntegratedTestBase<SalesTestModule>
    {
        public SalesTestBase()
        {
            UsingDbContext(context => new TestDataBuilder(context).Build());
        }

        protected virtual void UsingDbContext(Action<SalesDbContext> action)
        {
            using (var context = LocalIocManager.Resolve<SalesDbContext>())
            {
                action(context);
                context.SaveChanges();
            }
        }

        protected virtual T UsingDbContext<T>(Func<SalesDbContext, T> func)
        {
            T result;

            using (var context = LocalIocManager.Resolve<SalesDbContext>())
            {
                result = func(context);
                context.SaveChanges();
            }

            return result;
        }

        protected virtual async Task UsingDbContextAsync(Func<SalesDbContext, Task> action)
        {
            using (var context = LocalIocManager.Resolve<SalesDbContext>())
            {
                await action(context);
                await context.SaveChangesAsync(true);
            }
        }

        protected virtual async Task<T> UsingDbContextAsync<T>(Func<SalesDbContext, Task<T>> func)
        {
            T result;

            using (var context = LocalIocManager.Resolve<SalesDbContext>())
            {
                result = await func(context);
                context.SaveChanges();
            }

            return result;
        }
    }
}
