using System;
using NUnit.Framework;

namespace Selenium
{
    [TestFixture]
    public class NUnitTest
    {
        [TestCase]
        public void Test()
        {
            Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}