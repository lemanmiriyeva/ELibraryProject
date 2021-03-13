using System.Collections.Generic;
using System.Linq;

namespace ELibrary.Business.Utility
{
    public static class Utilities
    {
        public static bool Run(params bool[] conditions)
        {
            var results= conditions.Where(condition => condition==false).ToList();

            if (results.Count > 0)
            {
                return false;
            }

            return true;
        }

    }
}
