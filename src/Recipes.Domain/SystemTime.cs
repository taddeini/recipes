using System;

namespace Recipes.Domain
{
    public class SystemTime
    {
        public static Func<DateTime> Now = () => DateTime.Now;
    }
}
