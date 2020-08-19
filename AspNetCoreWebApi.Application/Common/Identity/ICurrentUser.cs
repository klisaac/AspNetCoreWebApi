namespace AspNetCoreWebApi.Application.Common.Identity
{
    public interface ICurrentUser
    {
        string UserName { get; }
    }
}
