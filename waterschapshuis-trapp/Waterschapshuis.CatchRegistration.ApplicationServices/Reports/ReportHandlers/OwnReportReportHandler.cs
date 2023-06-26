using AutoMapper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Waterschapshuis.CatchRegistration.ApplicationServices.MappingParametrizations;
using Waterschapshuis.CatchRegistration.Core.Data;
using Waterschapshuis.CatchRegistration.DomainModel.ReportData;
using System.Collections;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Reports.ReportHandlers
{
    [UsedImplicitly]
    public class OwnReportReportHandler : IDevExtremeReportHandler
    {
        private readonly IMapper _mapper;
        private readonly IMappingParametrizationService _mappingParametrizationService;
        private readonly IRoleAwareReadOnlyRepository<OwnReportData> _ownReportDataRepository;

        public OwnReportReportHandler(
            IMapper mapper,
            IMappingParametrizationService mappingParametrizationService,
            IRoleAwareReadOnlyRepository<OwnReportData> ownReportDataRepository)
        {
            _mapper = mapper;
            _mappingParametrizationService = mappingParametrizationService;
            _ownReportDataRepository = ownReportDataRepository;
        }

        public async Task<LoadResult> GetReport(DataSourceLoadOptionsBase request, CancellationToken cancellationToken)
        {
            var queryable = _ownReportDataRepository
                .QueryAll()
                .ProjectToWithMappingParameters<OwnReportData, Response.Item>(_mapper, _mappingParametrizationService);

            var measurements = request.GroupSummary?
                .Where(x => x.Selector!= null)
                .Select(x=> x.Selector).ToList();

            if (measurements != null && measurements.Any())
            {
                if (request.Filter != null && request.Filter.Contains("or"))
                {
                    var existingGroupedFilter = GroupFilterExpressionsIntoOneItem(request.Filter);
                    request.Filter.Clear();
                    request.Filter.Add(existingGroupedFilter);
                }
                request.Filter?.Add("and");
                request.Filter?.Add(GetFilterExpressionsForSumaryValuesBiggerThanZero(measurements));
            }

            LoadResult? loadResult = await DataSourceLoader.LoadAsync(queryable, request, cancellationToken);

            return loadResult;
        }

        private JArray GroupFilterExpressionsIntoOneItem(IList filter)
        {
            var existingFilters = new JArray();
            foreach (object? item in filter)
            {
                existingFilters.Add(item);
            } 
            return existingFilters;
        }

        private static JArray GetFilterExpressionsForSumaryValuesBiggerThanZero(IReadOnlyList<string> measurements)
        {
            var measurementFilters = new JArray();
            for (int i = 0; i < measurements.Count; i++)
            {
                measurementFilters.Add(new JArray { measurements[i], ">", 0 });
                if (i < measurements.Count - 1)
                    measurementFilters.Add("or");
            }
            return measurementFilters;
        }

        public class Response : LoadResult
        {
            public class Item
            {
                public int? RecordedOnYear { get; set; }
                public int? NumberOfCatches { get; set; }
                public int? NumberOfByCatches { get; set; }
                public int? NumberOfCatchesPreviousYear { get; set; }
                public int? NumberOfByCatchesPreviousYear { get; set; }
                public int? NumberOfTraps { get; set; }
                public string TrappingTypeName { get; set; } = String.Empty;
                public string TrapTypeName { get; set; } = String.Empty;
                public string CatchTypeCategoryName { get; set; } = String.Empty;
                public string CatchTypeName { get; set; } = String.Empty;
                public string OrganizationName { get; set; } = String.Empty;
                public string WaterAuthorityName { get; set; } = String.Empty;
                public string RayonName { get; set; } = String.Empty;
                public string CatchAreaName { get; set; } = String.Empty;
                public string SubAreaName { get; set; } = String.Empty;
                public string ProvinceName { get; set; } = String.Empty;
                public string HourSquareName { get; set; } = String.Empty;
                public string Period { get; set; } = String.Empty;
                public string Week { get; set; } = String.Empty;
                public string OwnerName { get; set; } = String.Empty;
                public double? Hours { get; set; }
                public double? HoursPreviousYear { get; set; }
                public int? CatchingNights { get; set; }
                public int? KmWaterway { get; set; }
                public string? FieldTestName { get; set; }
                public int? YearAndPeriod { get; set; }
                public string TimeRegistrationType { get; set; } = String.Empty;
                public string? VersionRegionalLayout { get; set; } = String.Empty;
                public double? HoursOther { get; set; }
                public string TrapStatus { get; set; } = String.Empty;
            }

            [PublicAPI]
            public class MappingProfile : Profile
            {
                public MappingProfile()
                {
                    // https://docs.automapper.org/en/stable/Queryable-Extensions.html#parameterization
                    var parameters = MappingParameters.CreateEmpty();

                    CreateMap<OwnReportData, Item>()
                        .ForMember(dest => dest.CatchTypeCategoryName, opt => opt.MapFrom(src =>
                            src.IsByCatch.HasValue ? (src.IsByCatch.Value ? "Bijvangst" : "Vangst") : String.Empty))
                        .ForMember(dest => dest.OwnerName, opt => opt.MapAnonymizedOwnerName(parameters))
                        .ForMember(dest => dest.YearAndPeriod, opt => opt.MapFrom(src =>
                            (src.RecordedOnYear.HasValue && src.Period.HasValue) ? src.RecordedOnYear * 100 + src.Period : null));
                }
            }
        }
    }
}
