using FluentAssertions;
using System;
using System.Net.Http;
using Waterschapshuis.CatchRegistration.Core.Helpers;

namespace Waterschapshuis.CatchRegistration.Common.Tests
{
    public static class AssertionExtensions
    {
        public static HttpResponseAssertions Should(this HttpResponseMessage actualValue)
        {
            return new HttpResponseAssertions(actualValue);
        }

        public static void AssertRecordedOn(this DateTimeOffset first, DateTimeOffset second)
        {
            if (first.Date == DateTimeOffset.Now.Date)
            {
                AssertDate(first, second);
            }
            else
            {
                AssertDate(first, second, "dd-MM-yyyy");
            }
        }

        public static void AssertDate(this DateTimeOffset first, DateTimeOffset second, string format = "dd-MM-yyyy mm:HH") =>
            first.SameAs(second, format).Should().BeTrue();
    }
}
