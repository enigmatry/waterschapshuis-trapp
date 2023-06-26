using AutoMapper;
using System;
using System.Linq.Expressions;
using Waterschapshuis.CatchRegistration.ApplicationServices.MappingParametrizations.Anonymization;
using Waterschapshuis.CatchRegistration.ApplicationServices.MappingParametrizations.CanBeEdited;
using Waterschapshuis.CatchRegistration.DomainModel.Anonymization;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.ReportData;
using Waterschapshuis.CatchRegistration.DomainModel.Traps;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.MappingParametrizations
{
    public static class MemberConfigurationExpressionExtensions
    {
        public static void MapAnonymizedCreatedBy<TSource, TDestination, TMember>(
            this IMemberConfigurationExpression<TSource, TDestination, TMember> configurationExpression,
            MappingParameters parameters)
            where TSource : IAnonymizeCreatedBy
        {
            Expression<Func<TSource, string>> anonymizeExpression = 
                new CreatedByAnonymizationQueryExpressionBuilder<TSource>().Build(parameters);
            configurationExpression.MapFrom(anonymizeExpression);
        }

        public static void MapAnonymizedUpdatedBy<TSource, TDestination, TMember>(
            this IMemberConfigurationExpression<TSource, TDestination, TMember> configurationExpression,
            MappingParameters parameters)
            where TSource : IAnonymizeCreatedUpdatedBy
        {
            Expression<Func<TSource, string>> anonymizeExpression = 
                new UpdatedByAnonymizationQueryExpressionBuilder<TSource>().Build(parameters);
            configurationExpression.MapFrom(anonymizeExpression);
        }

        public static void MapAnonymizedTrapUpdatedBy<TSource, TDestination, TMember>(
            this IMemberConfigurationExpression<TSource, TDestination, TMember> configurationExpression,
            MappingParameters parameters)
            where TSource : Trap
        {
            Expression<Func<TSource, string>> anonymizeExpression = 
                new UpdatedByTrapAnonymizationQueryExpressionBuilder<TSource>().Build(parameters);
            configurationExpression.MapFrom(anonymizeExpression);
        }

        public static void MapAnonymizedOwnerName<TSource, TDestination, TMember>(
            this IMemberConfigurationExpression<TSource, TDestination, TMember> configurationExpression,
            MappingParameters parameters)
            where TSource : OwnReportData
        {
            Expression<Func<TSource, string>> anonymizeExpression = 
                new OwnerNameAnonymizationQueryExpressionBuilder<TSource>().Build(parameters);
            configurationExpression.MapFrom(anonymizeExpression);
        }

        public static void MapCatchCanBeEdited<TSource, TDestination, TMember>(
            this IMemberConfigurationExpression<TSource, TDestination, TMember> configurationExpression,
            MappingParameters parameters)
            where TSource : Catch
        {
            Expression<Func<TSource, bool>> canBeEditedExpression =
                new CatchCanBeEditedQueryExpressionBuilder<TSource>().Build(parameters);

            configurationExpression.MapFrom(canBeEditedExpression);
        }
    }
}
