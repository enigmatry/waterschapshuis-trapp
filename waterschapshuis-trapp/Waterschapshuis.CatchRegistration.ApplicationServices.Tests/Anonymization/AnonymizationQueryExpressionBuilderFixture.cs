using FluentAssertions;
using NUnit.Framework;
using System;
using Waterschapshuis.CatchRegistration.ApplicationServices.MappingParametrizations;
using Waterschapshuis.CatchRegistration.ApplicationServices.MappingParametrizations.Anonymization;
using Waterschapshuis.CatchRegistration.ApplicationServices.MappingParametrizations.CanBeEdited;
using Waterschapshuis.CatchRegistration.DomainModel.Anonymization;
using Waterschapshuis.CatchRegistration.DomainModel.Identity;
using Waterschapshuis.CatchRegistration.DomainModel.ReportData;
using Waterschapshuis.CatchRegistration.DomainModel.Tests.Identity;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Tests.Anonymization
{
    [Category("unit")]
    public class AnonymizationQueryExpressionBuilderFixture
    {
        private const string NonAnonymizedCreatedBy = "Johhny Mnemonick The Creator";
        private const string NonAnonymizedUpdatedBy = "Johhny Mnemonick The Updated";
        private readonly Guid _currentUserId = Guid.NewGuid();
        private readonly Guid _currentOrganizationId = Guid.NewGuid();
        private readonly DateTimeOffset _anonymizeBefore = DateTimeOffset.Now;
        private readonly Guid _otherUserId = Guid.NewGuid();
        private readonly Guid _otherOrganizationId = Guid.NewGuid();
        private TestAnonymizeEntity _createdBeforeAnonymizeDateThreshold = null!;
        private TestAnonymizeEntity _createdBySameUser = null!;
        private TestAnonymizeEntity _createdByOtherUserFromSameOrganization = null!;
        private TestAnonymizeEntity _createdByOtherUserFromOtherOrganization = null!;
        private OwnReportData _ownReportCreatedBeforeAnonymizeDateThreshold = null!;
        private OwnReportData _ownReportCreatedBySameUser = null!;
        private OwnReportData _ownReportCreatedByOtherUserFromSameOrganization = null!;
        private OwnReportData _ownReportCreatedByOtherUserFromOtherOrganization = null!;

        [SetUp]
        public void Setup()
        {
            _createdBeforeAnonymizeDateThreshold =
                new TestAnonymizeEntity(_anonymizeBefore.AddTicks(-1), _currentUserId, _currentOrganizationId);
            _createdBySameUser =
                new TestAnonymizeEntity(_anonymizeBefore, _currentUserId, _currentOrganizationId);
            _createdByOtherUserFromSameOrganization =
                new TestAnonymizeEntity(_anonymizeBefore, _otherUserId, _currentOrganizationId);
            _createdByOtherUserFromOtherOrganization =
                new TestAnonymizeEntity(_anonymizeBefore, _otherUserId, _otherOrganizationId);

            _ownReportCreatedBeforeAnonymizeDateThreshold =
                new TestOwnReportData(_createdBeforeAnonymizeDateThreshold).OwnReportData;
            _ownReportCreatedBySameUser =
                new TestOwnReportData(_createdBySameUser).OwnReportData;
            _ownReportCreatedByOtherUserFromSameOrganization =
                new TestOwnReportData(_createdByOtherUserFromSameOrganization).OwnReportData;
            _ownReportCreatedByOtherUserFromOtherOrganization =
                new TestOwnReportData(_createdByOtherUserFromOtherOrganization).OwnReportData;
        }

        [Test]
        public void CreatedByAnonymizationFunction_CanSeeNonAnonymized()
        {
            //act
            var result = AnonymizeCreatedBy(_createdBeforeAnonymizeDateThreshold, true);
            result.Should().Be(User.AnonymizedName);

            result = AnonymizeCreatedBy(_createdBySameUser, true);
            result.Should().Be(NonAnonymizedCreatedBy);

            result = AnonymizeCreatedBy(_createdByOtherUserFromSameOrganization, true);
            result.Should().Be(NonAnonymizedCreatedBy);

            result = AnonymizeCreatedBy(_createdByOtherUserFromOtherOrganization, true);
            result.Should().Be(NonAnonymizedCreatedBy);
        }

        [Test]
        public void CreatedByAnonymizationFunction_CannotSeeNonAnonymized()
        {
            //act
            var result = AnonymizeCreatedBy(_createdBeforeAnonymizeDateThreshold, false);
            result.Should().Be(User.AnonymizedName);

            result = AnonymizeCreatedBy(_createdBySameUser, false);
            result.Should().Be(NonAnonymizedCreatedBy);

            result = AnonymizeCreatedBy(_createdByOtherUserFromSameOrganization, false);
            result.Should().Be(NonAnonymizedCreatedBy);

            result = AnonymizeCreatedBy(_createdByOtherUserFromOtherOrganization, false);
            result.Should().Be(User.AnonymizedName);
        }

        [Test]
        public void UpdatedByAnonymizationFunction_CanSeeNonAnonymized()
        {
            //act
            var result = AnonymizeUpdatedBy(_createdBeforeAnonymizeDateThreshold, true);
            result.Should().Be(User.AnonymizedName);

            result = AnonymizeUpdatedBy(_createdBySameUser, true);
            result.Should().Be(NonAnonymizedUpdatedBy);

            result = AnonymizeUpdatedBy(_createdByOtherUserFromSameOrganization, true);
            result.Should().Be(NonAnonymizedUpdatedBy);

            result = AnonymizeUpdatedBy(_createdByOtherUserFromOtherOrganization, true);
            result.Should().Be(NonAnonymizedUpdatedBy);
        }

        [Test]
        public void UpdatedByAnonymizationFunction_CannotSeeNonAnonymized()
        {
            //act
            var result = AnonymizeUpdatedBy(_createdBeforeAnonymizeDateThreshold, false);
            result.Should().Be(User.AnonymizedName);

            result = AnonymizeUpdatedBy(_createdBySameUser, false);
            result.Should().Be(NonAnonymizedUpdatedBy);

            result = AnonymizeUpdatedBy(_createdByOtherUserFromSameOrganization, false);
            result.Should().Be(NonAnonymizedUpdatedBy);

            result = AnonymizeUpdatedBy(_createdByOtherUserFromOtherOrganization, false);
            result.Should().Be(User.AnonymizedName);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void OwnerNameAnonymizationFunction_CanSeeNonAnonymized(bool isSeniorUser)
        {
            //act
            var result = AnonymizeOwnerName(_ownReportCreatedBeforeAnonymizeDateThreshold, true, isSeniorUser);
            result.Should().Be(User.AnonymizedName);

            result = AnonymizeOwnerName(_ownReportCreatedBySameUser, true, isSeniorUser);
            result.Should().Be(NonAnonymizedCreatedBy);

            result = AnonymizeOwnerName(_ownReportCreatedByOtherUserFromSameOrganization, true, isSeniorUser);
            result.Should().Be(NonAnonymizedCreatedBy);

            result = AnonymizeOwnerName(_ownReportCreatedByOtherUserFromOtherOrganization, true, isSeniorUser);
            result.Should().Be(NonAnonymizedCreatedBy);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void OwnerNameAnonymizationFunction_CannotSeeNonAnonymized(bool isSeniorUser)
        {
            //act
            var result = AnonymizeOwnerName(_ownReportCreatedBeforeAnonymizeDateThreshold, false, isSeniorUser);
            result.Should().Be(User.AnonymizedName);

            result = AnonymizeOwnerName(_ownReportCreatedBySameUser, false, isSeniorUser);
            result.Should().Be(NonAnonymizedCreatedBy);

            result = AnonymizeOwnerName(_ownReportCreatedByOtherUserFromSameOrganization, false, isSeniorUser);
            result.Should().Be(isSeniorUser ? NonAnonymizedCreatedBy : User.AnonymizedName);

            result = AnonymizeOwnerName(_ownReportCreatedByOtherUserFromOtherOrganization, false, isSeniorUser);
            result.Should().Be(User.AnonymizedName);
        }

        private string AnonymizeCreatedBy(IAnonymizeCreatedBy entity, bool canSeeNonAnonymized)
        {
            var parameters = new MappingParameters(
                    new AnonymizationProjectToParameters(
                        _anonymizeBefore,
                        _currentUserId,
                        _currentOrganizationId,
                        false,
                        canSeeNonAnonymized),
                    CanBeEditedProjectToParameters.CreateEmpty()
                );

            Func<IAnonymizeCreatedBy, string> anonymizationFunction =
                new CreatedByAnonymizationQueryExpressionBuilder<IAnonymizeCreatedBy>()
                    .Build(parameters)
                    .Compile();

            return anonymizationFunction(entity);
        }

        private string AnonymizeUpdatedBy(IAnonymizeCreatedUpdatedBy entity, bool canSeeNonAnonymized)
        {
            var parameters = new MappingParameters(
                    new AnonymizationProjectToParameters(
                        _anonymizeBefore,
                        _currentUserId,
                        _currentOrganizationId,
                        false,
                        canSeeNonAnonymized),
                    CanBeEditedProjectToParameters.CreateEmpty()
                );

            Func<IAnonymizeCreatedUpdatedBy, string> anonymizationFunction =
                new UpdatedByAnonymizationQueryExpressionBuilder<IAnonymizeCreatedUpdatedBy>()
                    .Build(parameters)
                    .Compile();

            return anonymizationFunction(entity);
        }

        private string AnonymizeOwnerName(OwnReportData entity, bool canSeeNonAnonymized, bool isSeniorUser)
        {
            var parameters = new MappingParameters(
                    new AnonymizationProjectToParameters(
                        _anonymizeBefore,
                        _currentUserId,
                        _currentOrganizationId,
                        isSeniorUser,
                        canSeeNonAnonymized),
                    CanBeEditedProjectToParameters.CreateEmpty()
                );

            Func<OwnReportData, string> anonymizationFunction =
                new OwnerNameAnonymizationQueryExpressionBuilder<OwnReportData>()
                    .Build(parameters)
                    .Compile();

            return anonymizationFunction(entity);
        }

        private class TestAnonymizeEntity : IAnonymizeCreatedUpdatedBy
        {
            public DateTimeOffset CreatedOn { get; }
            public Guid CreatedById { get; }
            public User CreatedBy { get; }
            public DateTimeOffset UpdatedOn { get; }
            public Guid UpdatedById { get; }
            public User UpdatedBy { get; }

            public Guid? LocationOrganizationId => null;

            public TestAnonymizeEntity(DateTimeOffset createdOn, Guid createdById, Guid createdByOrganizationId)
            {
                CreatedOn = UpdatedOn = createdOn;
                CreatedById = UpdatedById = createdById;
                CreatedBy = new UserBuilder().Name(NonAnonymizedCreatedBy).Organization(createdByOrganizationId);
                UpdatedBy = new UserBuilder().Name(NonAnonymizedUpdatedBy).Organization(createdByOrganizationId);
            }
        }

        private class TestOwnReportData
        {
            public OwnReportData OwnReportData { get; }

            public TestOwnReportData(TestAnonymizeEntity entity) :
                this(entity.CreatedOn, entity.CreatedById, entity.CreatedBy.Name, entity.CreatedBy.OrganizationId)
            { }

            public TestOwnReportData(DateTimeOffset? createdOn, Guid? ownerId, string? ownerName, Guid? organizationId)
            {
                OwnReportData = OwnReportData.Create(
                    createdOn,
                    null, null, null, null, null, null, null,
                    ownerId,
                    ownerName,
                    null, null, null, null, null, null,
                    organizationId ?? Guid.Empty,
                    String.Empty, String.Empty, String.Empty, String.Empty, String.Empty,
                    String.Empty, String.Empty, String.Empty, null, null, null, null, string.Empty, String.Empty, null);
            }
        }
    }
}
