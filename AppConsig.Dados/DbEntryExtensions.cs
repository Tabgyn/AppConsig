using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace AppConsig.Dados
{
    public static class DbEntryExtensions
    {
        public static object GetPrimaryKeyValues(this DbEntityEntry dbEntry, IEnumerable<string> columnNames)
        {
            var enumerableNames = columnNames as string[] ?? columnNames.ToArray();

            if (enumerableNames.Count() == 1)
            {
                return dbEntry.GetDatabaseValues().GetValue<object>(enumerableNames.First());
            }

            if (enumerableNames.Count() <= 1)
                throw new KeyNotFoundException("key not found for " + dbEntry.Entity.GetType().FullName);

            var output = "[";

            output += string.Join(",",
                enumerableNames.Select(colName => dbEntry.GetDatabaseValues().GetValue<object>(colName)));

            output += "]";

            return output;
        }
    }
}