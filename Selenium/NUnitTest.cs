using System;
using System.IO;
using System.Linq;
using Core.Extensions;
using NUnit.Framework;

namespace Selenium
{
    [TestFixture]
    public class NUnitTest
    {
        [TestCase]
        public void Test()
        {
            var solutionBase = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent;

            var driversDirectory =
                (solutionBase?.GetDirectories() ??
                 throw new InvalidOperationException("Parent solution directory is null."))
                .FirstOrDefault(dir => dir.Name.EqualsIgnoreCase("drivers")) ??
                Directory.CreateDirectory($"{solutionBase.FullName}/Drivers");

            Console.WriteLine("*******************");
            Console.WriteLine(string.Join(",", solutionBase.GetDirectories().ToList()));
        }
    }
}