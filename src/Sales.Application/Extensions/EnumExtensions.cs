using System;

namespace Sales.Application.Extensions
{
    public static class EnumExtensions
    {
        public static T ParseToEnum<T>(this string str) where T : struct, Enum
        {
            return Enum.Parse<T>(str, true);
        }
    }
}
