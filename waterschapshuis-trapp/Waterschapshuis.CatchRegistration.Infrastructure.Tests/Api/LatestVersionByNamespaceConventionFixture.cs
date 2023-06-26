using System;
using System.Reflection;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using NUnit.Framework;
using Waterschapshuis.CatchRegistration.Infrastructure.Api.Versioning;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Tests.Api
{
    [Category("unit")]
    public class LatestVersionByNamespaceConventionFixture
    {
        private readonly ApiVersion _latestApiVersion = ApiVersion.Parse("1.0");

        [Test]
        [TestCase("Api.Features.Latest")]
        [TestCase("Api.Features.Latest.Users")]
        [TestCase("Latest.Api.Features")]
        public void ShouldInferLatestVersionFromNamespace(string @namespace)
        {
            var controllerType = new TestType(@namespace);
            var attributes = Array.Empty<object>();
            var controllerModel = new ControllerModel(controllerType.GetTypeInfo(), attributes);
            var controller = A.Fake<IControllerConventionBuilder>();
            var convention = new LatestVersionByNamespaceConvention(_latestApiVersion);

            var result = convention.Apply(controller, controllerModel);

            result.Should().BeTrue();
            A.CallTo(() => controller.HasApiVersion(_latestApiVersion)).MustHaveHappenedOnceExactly();
        }


        [Test]
        [TestCase("Api.Features.v1_1.Controllers", "1.1")]
        [TestCase("Api.Features.v2.Controllers", "2.0")]
        public void ShouldInferVersionNumberFromNamespace(string @namespace, string version)
        {
            var apiVersion = ApiVersion.Parse(version);
            var controllerType = new TestType(@namespace);
            var attributes = Array.Empty<object>();
            var controllerModel = new ControllerModel(controllerType.GetTypeInfo(), attributes);
            var controller = A.Fake<IControllerConventionBuilder>();
            var convention = new LatestVersionByNamespaceConvention(_latestApiVersion);

            var result = convention.Apply(controller, controllerModel);

            result.Should().BeTrue();
            A.CallTo(() => controller.HasApiVersion(apiVersion)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void ShouldIgnoreUnmatchedNamespace()
        {
            var controllerType = new TestType("Api.Features.Controllers");
            var attributes = Array.Empty<object>();
            var controllerModel = new ControllerModel(controllerType.GetTypeInfo(), attributes);
            var controller = A.Fake<IControllerConventionBuilder>();
            var convention = new LatestVersionByNamespaceConvention(_latestApiVersion);

            var result = convention.Apply(controller, controllerModel);

            result.Should().BeFalse();
        }

        private sealed class TestType : TypeDelegator
        {
            internal TestType(string @namespace) => Namespace = @namespace;

            public override string Namespace { get; }
        }
    }
}
