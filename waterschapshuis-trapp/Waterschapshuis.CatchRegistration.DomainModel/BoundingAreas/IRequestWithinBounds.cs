namespace Waterschapshuis.CatchRegistration.DomainModel.BoundingAreas
{
    public interface IRequestWithinBounds
    {
        public BoundingBox? BoundingBox { get; set; }
    }
}
