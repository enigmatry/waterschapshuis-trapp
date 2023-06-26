namespace Waterschapshuis.CatchRegistration.Core
{
    public abstract class EntityWithTypedId<TId> : Entity
    {
        public TId Id { get; set; } = default!;
    }
}
