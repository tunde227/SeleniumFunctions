using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Backend.Api.Base;
using Backend.Api.HTTP;
using NUnit.Framework;

namespace TemplateTests.Backend.API
{
    [TestFixture]
    public sealed class PostObjectTest
    {
        [TestCase]
        public void ResponseMapTest()
        {
            ResponseMap responses = new ResponseMap()
            {
                {"1", "Test 1"},
                {"2", "Test 2"},
                {"3", "Test 3"},
                {"4", "Test 4"},
                {"5", "Test 5"}
            };
            Console.WriteLine(responses.ToString());
        }
    }
}