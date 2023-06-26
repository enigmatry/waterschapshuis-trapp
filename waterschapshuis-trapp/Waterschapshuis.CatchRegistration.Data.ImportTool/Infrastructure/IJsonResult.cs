namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Infrastructure
{
    public interface IJsonResult
    {
        T Parse<T>();
        string Stringify();
    }
}
