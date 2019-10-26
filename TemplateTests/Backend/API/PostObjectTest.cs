using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using Backend.Api.Base;
using Backend.Api.HTTP;
using Core.Extensions;
using log4net;
using MoreLinq;
using NUnit.Framework;

namespace TemplateTests.Backend.API
{
    [TestFixture]
    public sealed class PostObjectTest
    {
        public static readonly ILog Logger = LogManager.GetLogger(typeof(PostObjectTest));

        public class Element : IEquatable<Element>
        {
            public string @Interface { get; set; }
            public string Page { get; set; }
            public int Release { get; set; } = 1;
            public string Name { get; set; }

            public bool Equals(Element other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return string.Equals(Interface, other.Interface, StringComparison.CurrentCultureIgnoreCase) &&
                       string.Equals(Page, other.Page, StringComparison.CurrentCultureIgnoreCase) &&
                       Release == other.Release &&
                       string.Equals(Name, other.Name, StringComparison.CurrentCultureIgnoreCase);
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((Element) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    int hashCode = (Interface != null
                        ? StringComparer.CurrentCultureIgnoreCase.GetHashCode(Interface)
                        : 0);
                    hashCode = (hashCode * 397) ^
                               (Page != null ? StringComparer.CurrentCultureIgnoreCase.GetHashCode(Page) : 0);
                    hashCode = (hashCode * 397) ^ Release;
                    hashCode = (hashCode * 397) ^
                               (Name != null ? StringComparer.CurrentCultureIgnoreCase.GetHashCode(Name) : 0);
                    return hashCode;
                }
            }

            public static bool operator ==(Element left, Element right) => Equals(left, right);

            public static bool operator !=(Element left, Element right) => !Equals(left, right);

            public override string ToString()
            {
                return $"{nameof(Interface)}: {Interface}, {nameof(Page)}: {Page}, {nameof(Release)}: " +
                       $"{Release}, {nameof(Name)}: {Name}";
            }
        }

        [TestCase]
        public void ResponseMapTest()
        {
            var response = new Element()
            {
                Interface = "GAA",
                Name = "FirstName",
                Page = "HomePage",
                Release = 092019
            };

            var response2 = new Element()
            {
                Interface = "GAA",
                Name = "LastName",
                Page = "HomePage",
                Release = 082019
            };

            var responses = new Element()
            {
                Interface = "GAA",
                Name = "Header",
                Page = "HomePage",
                Release = 082019
            };

            var responses2 = new Element()
            {
                Interface = "GAA",
                Name = "Header",
                Page = "HomePage",
                Release = 092019
            };


            var responses3 = new Element()
            {
                Interface = "GAA",
                Name = "Header",
                Page = "HomePage",
                Release = 102019
            };

            var responses4 = new Element()
            {
                Interface = "GAA",
                Name = "HeaderEnter",
                Page = "HomePage",
                Release = 102019
            };


            List<Element> elements = Arrays.AsList(response, response2, responses, responses2, responses3, responses4);
            elements.ForFirst(e => Logger.Debug(e.Release));
            Logger.Debug(responses.ToString());
        }
    }
}