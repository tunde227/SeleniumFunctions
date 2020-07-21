using System.Data.Entity;
using System.Linq;
using Core.Extensions;

namespace Backend.Database
{
    public abstract class DatabaseConnection : DbContext
    {
        public void Test()
        {
            Arrays.AsList("").FirstOrDefault();
        }
    }
}