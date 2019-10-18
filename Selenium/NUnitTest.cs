using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Selenium.Extensions;

namespace Selenium
{
    [TestFixture]
    public class NUnitTest
    {
        [TestCase]
        public void Test()
        {
            DirectoryInfo solutionBase = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent;

            DirectoryInfo driversDirectory =
                ((solutionBase?.GetDirectories()) ??
                 throw new InvalidOperationException("Parent solution directory is null."))
                .FirstOrDefault(dir => dir.Name.EqualsIgnoreCase("drivers")) ??
                Directory.CreateDirectory($"{solutionBase.FullName}/Drivers");

            Console.WriteLine("*******************");
            Console.WriteLine(solutionBase?.GetDirectories().ToString());
        }
    }
}