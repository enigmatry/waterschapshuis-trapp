using JetBrains.Annotations;
using MediatR;
using System;
using System.Collections.Generic;
using AutoMapper;
using Waterschapshuis.CatchRegistration.DomainModel.ScheduledJobs;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.ScheduledJobs
{
    public static partial class GetScheduledJobs
    {
        [PublicAPI]
        public class Query : IRequest<Response>
        {
            public ScheduledJobName Name { get; set; }

            public static Query ByJobName(ScheduledJobName name)
            {
                return new Query { Name = name };
            }
        }

        [PublicAPI]
        public class Response
        {
            public Guid? Id { get; set; }
            public ScheduledJobName Name { get; set; }
            public ScheduledJobState State { get; set; }
            public List<string> OutputMessages { get; set; } = new List<string>();
        }

        [UsedImplicitly]
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<ScheduledJob, Response>().ForMember(dest => dest.OutputMessages, 
                    src => src.MapFrom(opt => opt.GetOutputMessages()));
            }
        }
    }
}
