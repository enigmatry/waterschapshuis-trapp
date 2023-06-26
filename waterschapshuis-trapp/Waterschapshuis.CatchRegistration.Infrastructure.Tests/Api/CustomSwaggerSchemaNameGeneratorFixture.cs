using FluentAssertions;
using NUnit.Framework;
using Waterschapshuis.CatchRegistration.Infrastructure.Api.Swagger;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Tests.Api
{
    [Category("unit")]
    public class CustomSwaggerSchemaNameGeneratorFixture
    {
        private CustomSwaggerSchemaNameGenerator _generator = null!;

        [SetUp]
        public void Setup()
        {
            _generator = new CustomSwaggerSchemaNameGenerator();
        }

        [Test]
        public void TestGenerateWithInnerType()
        {
            var result = _generator.Generate(typeof(OuterType.InnerType));

            result.Should().Be("CustomSwaggerSchemaNameGeneratorFixtureOuterTypeInnerType");
        }

        [Test]
        public void TestGenerateWithNonInnerType()
        {
            // just use any class that is not an inner class
            var result = _generator.Generate(typeof(CustomSwaggerSchemaNameGeneratorFixture));

            result.Should().Be("CustomSwaggerSchemaNameGeneratorFixture");
        }

        private static class OuterType
        {
            public class InnerType
            {
            }
        }
    }
}
