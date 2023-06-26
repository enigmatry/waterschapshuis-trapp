using FluentAssertions;
using NUnit.Framework;
using System;
using Waterschapshuis.CatchRegistration.ApplicationServices.MappingParametrizations;
using Waterschapshuis.CatchRegistration.ApplicationServices.MappingParametrizations.Anonymization;
using Waterschapshuis.CatchRegistration.ApplicationServices.MappingParametrizations.CanBeEdited;
using Waterschapshuis.CatchRegistration.DomainModel.Catches;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Catches;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Tests.CanBeEdited
{
    [Category("unit")]
    public class CatchCanBeEditedQueryExpressionBuilderFixture
    {
        private readonly DateTimeOffset _currentDateTime = DateTimeOffset.Now;
        private readonly Guid _currentUserId = Guid.NewGuid();
        private readonly Guid _otherUserId = Guid.NewGuid();
        private Catch _catchClosed = null!;
        private Catch _catchCompleted = null!;
        private Catch _catchWritten = null!;


        [SetUp]
        public void SetUp()
        {
            _catchClosed = new CatchBuilder().WithStatus(CatchStatus.Closed);
            _catchCompleted = new CatchBuilder().WithStatus(CatchStatus.Completed);
            _catchWritten = new CatchBuilder().WithStatus(CatchStatus.Written);

        }

        [Test]
        public void CatchCanBeEdited()
        {
            var result = CatchCanBeEdited(_catchWritten);
            result.Should().BeTrue();

            result = CatchCanBeEdited(_catchClosed);
            result.Should().BeFalse();

            result = CatchCanBeEdited(_catchCompleted);
            result.Should().BeFalse();

        }

        private bool CatchCanBeEdited(Catch entity)
        {
            var parameters = new MappingParameters(
                    AnonymizationProjectToParameters.CreateEmpty(),
                    new CanBeEditedProjectToParameters(true, true)
                );

            Func<Catch, bool> catchCanBeEditedFunction =
                new CatchCanBeEditedQueryExpressionBuilder<Catch>()
                    .Build(parameters)
                    .Compile();

            return catchCanBeEditedFunction(entity);
        }
    }
}
