using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using static System.Text.RegularExpressions.RegexOptions;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Api.Versioning
{
    public class VersionByNamespaceConvention : IControllerConvention
    {
        /// <summary>
        /// Applies a controller convention given the specified builder and model.
        /// </summary>
        /// <param name="controller">The <see cref="IControllerConventionBuilder">builder</see> used to apply conventions.</param>
        /// <param name="controllerModel">The <see cref="ControllerModel">model</see> to build conventions from.</param>
        /// <returns>True if any conventions were applied to the <paramref name="controllerModel">controller model</paramref>;
        /// otherwise, false.</returns>
        public virtual bool Apply(IControllerConventionBuilder controller, ControllerModel controllerModel)
        {
            if (controller == null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            if (controllerModel == null)
            {
                throw new ArgumentNullException(nameof(controllerModel));
            }

            var text = GetRawApiVersion(controllerModel.ControllerType.Namespace!);

            if (!ApiVersion.TryParse(text, out var apiVersion))
            {
                return false;
            }

            var deprecated = controllerModel.Attributes.OfType<ObsoleteAttribute>().Any();

            if (deprecated)
            {
                controller.HasDeprecatedApiVersion(apiVersion!);
            }
            else
            {
                controller.HasApiVersion(apiVersion!);
            }

            return true;
        }

        public static string? GetRawApiVersion(string @namespace)
        {
            // 'v' | 'V' : [<year> '-' <month> '-' <day>] : [<major[.minor]>] : [<status>]
            // ex: v2018_04_01_1_1_Beta
            const string pattern = @"[^\.]?[vV](\d{4})?_?(\d{2})?_?(\d{2})?_?(\d+)?_?(\d*)_?([a-zA-Z][a-zA-Z0-9]*)?[\.$]?";

            var match = Regex.Match(@namespace, pattern, Singleline);
            var rawApiVersions = new List<string>();
            var text = new StringBuilder();

            while (match.Success)
            {
                ExtractDateParts(match, text);
                ExtractNumericParts(match, text);
                ExtractStatusPart(match, text);

                if (text.Length > 0)
                {
                    rawApiVersions.Add(text.ToString());
                }

                text.Clear();
                match = match.NextMatch();
            }

            return rawApiVersions.Count switch
            {
                0 => default,
                1 => rawApiVersions[0],
                _ => throw new InvalidOperationException(
                    $"Multiple API versions were inferred from the namespace '{@namespace}'")
            };
        }

        private static void ExtractDateParts(Match match, StringBuilder text)
        {
            var year = match.Groups[1];
            var month = match.Groups[2];
            var day = match.Groups[3];

            if (!year.Success || !month.Success || !day.Success)
            {
                return;
            }

            text.Append(year.Value);
            text.Append('-');
            text.Append(month.Value);
            text.Append('-');
            text.Append(day.Value);
        }

        private static void ExtractNumericParts(Match match, StringBuilder text)
        {
            var major = match.Groups[4];

            if (!major.Success)
            {
                return;
            }

            if (text.Length > 0)
            {
                text.Append('.');
            }

            text.Append(major.Value);

            var minor = match.Groups[5];

            if (!minor.Success)
            {
                return;
            }

            text.Append('.');

            if (minor.Length > 0)
            {
                text.Append(minor.Value);
            }
            else
            {
                text.Append('0');
            }
        }

        private static void ExtractStatusPart(Match match, StringBuilder text)
        {
            var status = match.Groups[6];

            if (status.Success && text.Length > 0)
            {
                text.Append('-');
                text.Append(status.Value);
            }
        }
    }
}
