namespace Waterschapshuis.CatchRegistration.Core
{
    public interface IEntityHasName
    {
        string Name { get; }
    }

    public interface IEntityHasName<out TId> : IEntityHasName
    {
        TId Id { get; }
    }
}
