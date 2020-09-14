namespace Sales.Application.Extensions
{
    public static class NullExtensions
    {
        public static bool IsNull(this object obj) => obj is null;
        public static bool IsNotNull(this object obj) => obj != null;
    }
}
