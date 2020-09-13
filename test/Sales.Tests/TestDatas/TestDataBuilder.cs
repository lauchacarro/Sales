using Sales.EntityFrameworkCore;

namespace Sales.Tests.TestDatas
{
    public class TestDataBuilder
    {
        private readonly SalesDbContext _context;

        public TestDataBuilder(SalesDbContext context)
        {
            _context = context;
        }

        public void Build()
        {
            //create test data here...
        }
    }
}