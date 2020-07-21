using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Core.Extensions;
using Core.Utility;
using NUnit.Framework;

namespace TemplateTests
{
    [SetUpFixture]
    public class Hooks
    {
        [Key]
        public string Key { get; set; }
        [OneTimeSetUp]
        public void Before()
        {
            Arrays.AsList("").TrueForAll(null);
            Arrays.AsList("").TrueForAll(null);
            Log.SetUp(Assembly.GetExecutingAssembly(), @"/Users/brian/Desktop/Logs");
        }
        
    }
}