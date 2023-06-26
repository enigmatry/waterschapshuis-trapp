using System;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Api.Versioning
{
    public class LatestVersionByNamespaceConvention : VersionByNamespaceConvention
    {
        private const string NamespaceMatch = "Latest";
        private readonly ApiVersion _latestApiVersion;

        public LatestVersionByNamespaceConvention([NotNull] ApiVersion latestApiVersion)
        {
            _latestApiVersion = latestApiVersion ?? throw new ArgumentNullException(nameof(latestApiVersion));
        }

        public override bool Apply(IControllerConventionBuilder controller, ControllerModel controllerModel)
        {
            if (controller == null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            if (controllerModel == null)
            {
                throw new ArgumentNullException(nameof(controllerModel));
            }

            var hasVersionInNamespace = base.Apply(controller, controllerModel);
            if (hasVersionInNamespace)
            {
                return true;
            }

            var hasLatestInNamespace = HasLatestInNamespace(controllerModel.ControllerType.Namespace!);
            if (!hasLatestInNamespace)
            {
                return false;
            }

            var deprecated = controllerModel.Attributes.OfType<ObsoleteAttribute>().Any();

            if (deprecated)
            {
                controller.HasDeprecatedApiVersion(_latestApiVersion!);
            }
            else
            {
                controller.HasApiVersion(_latestApiVersion!);
            }

            return true;
        }

        private bool HasLatestInNamespace(string @namespace)
        {
            return @namespace.Contains($"{NamespaceMatch}.", StringComparison.InvariantCultureIgnoreCase)
                   || @namespace.Contains($".{NamespaceMatch}", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
