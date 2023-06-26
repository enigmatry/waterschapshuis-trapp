using System;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;

namespace Waterschapshuis.CatchRegistration.External.Api.Features.Catches
{
    public static partial class GetCatch
    {
        private static string Unknown = "Onbekend";

        [PublicAPI]
        public class Query : IRequest<Response>
        {
            /// <summary>
            /// ID of catch
            /// </summary>
            public Guid Id { get; set; }

            public Query(Guid id)
            {
                Id = id;
            }
        }

        [PublicAPI]
        public class Response
        {
            /// <summary>
            /// Catch
            /// </summary>
            public CatchItem? Catch { get; set; }

            public Response(CatchItem? @catch)
            {
                Catch = @catch;
            }
        }

        [PublicAPI]
        public class CatchItem
        {
            /// <summary>
            /// ID of catch
            /// </summary>
            public Guid Id { get; set; }

            /// <summary>
            /// Determines whether catch is by catch
            /// </summary>
            public bool IsByCatch { get; set; }

            /// <summary>
            /// Number of catches
            /// </summary>
            public int NumberOfCatches { get; set; }

            /// <summary>
            /// Longitude (X coordinate) of trap where catch is registered
            /// </summary>
            public double Longitude { get; set; }

            /// <summary>
            /// Latitude (Y coordinate) of trap where catch is registered
            /// </summary>
            public double Latitude { get; set; }

            /// <summary>
            /// Created on
            /// </summary>
            public DateTimeOffset CreatedOn { get; set; }

            /// <summary>
            /// Name of user who created this catch
            /// </summary>
            public string CreatedBy { get; set; } = String.Empty;

            /// <summary>
            /// 'ByCatch' or 'Catch' Catch type
            /// </summary>
            public string CatchType { get; set; } = String.Empty;

            /// <summary>
            /// Catch type name
            /// </summary>
            public string CatchTypeName { get; set; } = String.Empty;

            /// <summary>
            /// Catch type age
            /// </summary>
            public string Age { get; set; } = String.Empty;

            /// <summary>
            /// Catch type gender
            /// </summary>
            public string Gender { get; set; } = String.Empty;

            /// <summary>
            /// Trapping type
            /// </summary>
            public string TrappingType { get; set; } = String.Empty;

            /// <summary>
            /// ID of trap
            /// </summary>
            public Guid TrapId { get; set; } = Guid.Empty;

            /// <summary>
            /// Trap type
            /// </summary>
            public string TrapType { get; set; } = String.Empty;

            /// <summary>
            /// Organization
            /// </summary>
            public string OrganizationName { get; set; } = String.Empty;

            /// <summary>
            /// Water Authority
            /// </summary>
            public string WaterAuthorityName { get; set; } = String.Empty;

            /// <summary>
            /// Rayon
            /// </summary>
            public string RayonName { get; set; } = String.Empty;
           
            /// <summary>
            /// Catch Area
            /// </summary>
            public string CatchAreaName { get; set; } = String.Empty;

            /// <summary>
            /// Sub Area
            /// </summary>
            public string SubAreaName { get; set; } = String.Empty;

            /// <summary>
            /// Hour Square (Atlas Block)
            /// </summary>
            public string HourSquareName { get; set; } = String.Empty;
        }
        
        [UsedImplicitly]
        public class CatchMappingProfile : Profile
        {
            public CatchMappingProfile(){}
            public CatchMappingProfile(bool hasPrivatePermission)
            {
                CreateMap<Catch, CatchItem>()
                    .ForMember(dest => dest.NumberOfCatches, opts => opts.MapFrom(src => src.Number))
                    .ForMember(dest => dest.IsByCatch, opts => opts.MapFrom(src => src.CatchType.IsByCatch))
                    .ForMember(dest => dest.CreatedBy, opts => opts.MapFrom(src => hasPrivatePermission ? src.CreatedBy.Name : "Geanonimiseerd"))
                    .ForMember(dest => dest.TrapType, opt => opt.MapFrom(src => src.Trap.TrapType.Name))
                    .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Trap.Location.X))
                    .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Trap.Location.Y))
                    .ForMember(dest => dest.TrappingType, opt => opt.MapFrom(src => src.Trap.TrapType.TrappingType.Name))
                    .ForMember(dest => dest.CatchType, opt => opt.MapFrom(src => src.CatchType.IsByCatch ? "Bijvangst" : "Vangst"))
                    .ForMember(dest => dest.CatchTypeName, opt => opt.MapFrom(src => src.CatchType.Name))
                    .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.CatchType.IsByCatch ? Unknown : GetCatchAge(src.CatchType.Name)))
                    .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.CatchType.IsByCatch ? Unknown : GetCatchGender(src.CatchType.Name)))
                    .ForMember(dest => dest.OrganizationName, opt => opt.MapFrom(src => src.Trap.SubAreaHourSquare.SubArea.WaterAuthority.Organization.Name))
                    .ForMember(dest => dest.WaterAuthorityName, opt => opt.MapFrom(src => src.Trap.SubAreaHourSquare.SubArea.WaterAuthority.Name))
                    .ForMember(dest => dest.RayonName, opt => opt.MapFrom(src => src.Trap.SubAreaHourSquare.SubArea.CatchArea.Rayon.Name))
                    .ForMember(dest => dest.CatchAreaName, opt => opt.MapFrom(src => src.Trap.SubAreaHourSquare.SubArea.CatchArea.Name))
                    .ForMember(dest => dest.SubAreaName, opt => opt.MapFrom(src => src.Trap.SubAreaHourSquare.SubArea.Name))
                    .ForMember(dest => dest.HourSquareName, opt => opt.MapFrom(src => src.Trap.SubAreaHourSquare.HourSquare.Name));
            }
        }

        private static string GetCatchAge(string catchName)
        {
            return CatchNameHasEnoughInfo(catchName) ? catchName.Split(' ')[2] : Unknown;
        }

        private static string GetCatchGender(string catchName)
        {
            return CatchNameHasEnoughInfo(catchName) ? catchName.Split(' ')[1] : Unknown;
        }

        private static bool CatchNameHasEnoughInfo(string catchName)
        {
            return catchName.Split(' ').Length > 2;
        }
    }
}
