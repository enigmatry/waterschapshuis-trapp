using FluentValidation;
using JetBrains.Annotations;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Core.Data;

namespace Waterschapshuis.CatchRegistration.DomainModel.ReportTemplates.Commands
{
    public static class CreateReportTemplateExport
    {
        [PublicAPI]
        public class Command : IRequest<Result>
        {
            /// <summary>
            /// Endpoint of the report
            /// </summary>
            public string ReportUri { get; set; } = String.Empty;

            /// <summary>
            /// Name for this report template
            /// </summary>
            public string TemplateTitle { get; set; } = String.Empty;

            /// <summary>
            /// Content report is build of
            /// </summary>
            public string TemplateContent { get; set; } = String.Empty;

            /// <summary>
            /// Chart type selected
            /// </summary>
            public ChartType ChartType { get; set; } = ChartType.None;
        }

        [PublicAPI]
        public class Result
        {
            public Guid Id { get; set; }
        }

        [UsedImplicitly]
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.ReportUri).NotEmpty();
                RuleFor(x => x.TemplateTitle).NotEmpty().MaximumLength(100);
                RuleFor(x => x.TemplateContent).NotEmpty();
            }
        }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Command, Result>
        {
            private IRepository<ReportTemplate> _reportTemplateRepository;

            public RequestHandler(IRepository<ReportTemplate> reportTemplateRepository)
            {
                _reportTemplateRepository = reportTemplateRepository;
            }

            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var originReportTemplate = _reportTemplateRepository
                    .QueryAll()
                    .Single(x => !x.Exported && x.RouteUri == request.ReportUri);

                var reportTemplateExport = ReportTemplate.CreateExport(
                    originReportTemplate, 
                    request.TemplateTitle, 
                    request.TemplateContent,
                    request.ChartType);

                _reportTemplateRepository.Add(reportTemplateExport);

                return await Task.FromResult(new Result { Id = reportTemplateExport.Id });
            }
        }
    }
}
