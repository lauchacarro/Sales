using Abp.Dependency;

namespace Sales.Domain.Options
{

    public interface IDatabaseOptions : ISingletonDependency
    {
        public string Schema { get; set; }
        public bool SkipSeed { get; set; }
    }

    public class DatabaseOptions : IDatabaseOptions
    {
        public string Schema { get; set; }
        public bool SkipSeed { get; set; }
    }
}
