using System;
using System.Collections.Generic;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.DomainModel.ReportTemplates;
using Waterschapshuis.CatchRegistration.DomainModel.Templates;

namespace Waterschapshuis.CatchRegistration.BackOffice.Api.Features.Reports
{
    public static partial class GetReportTemplates
    {
        [PublicAPI]
        public class Query : IRequest<Response> { }

        [PublicAPI]
        public class Response
        {
            public IEnumerable<Item> Items { get; set; } = new List<Item>();

            [PublicAPI]
            public class Item
            {
                /// <summary>
                /// GUID of the report template
                /// </summary>
                public Guid Id { get; set; }

                /// <summary>
                /// Name of the template
                /// </summary>
                public string Title { get; set; } = String.Empty;

                /// <summary>
                /// Name of the report group (Vangstrapportage, Heatmap, Speurkaart)
                /// </summary>
                public string Group { get; set; } = String.Empty;

                /// <summary>
                /// Endpoint route of the template
                /// </summary>
                public string RouteUri { get; set; } = String.Empty;

                /// <summary>
                /// Report type (DevExtreme, Geo)
                /// </summary>
                public ReportTemplateType Type { get; set; }

                /// <summary>
                /// Key
                /// </summary>
                public string Key { get; set; } = String.Empty;

                /// <summary>
                /// Name of the export file
                /// </summary>
                public string ExportFileName { get; set; } = String.Empty;

                /// <summary>
                /// Content which is used to build the template
                /// </summary>
                public string Content { get; set; } = String.Empty;

                /// <summary>
                /// Indicator whether template is active
                /// </summary>
                public bool Active { get; set; }

                /// <summary>
                /// Indicator whether template is exported
                /// </summary>
                public bool Exported { get; set; }

                /// <summary>
                /// Chart type selected
                /// </summary>
                public ChartType ChartType { get; set; } = ChartType.None;
            }

            [UsedImplicitly]
            public class MappingProfile : Profile
            {
                public MappingProfile() => CreateMap<ReportTemplate, Item>();
            }
        }
    }
}
