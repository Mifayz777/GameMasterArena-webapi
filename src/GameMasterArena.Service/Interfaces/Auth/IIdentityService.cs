namespace GameMasterArena.Service.Interfaces.Auth;

public interface IIdentityService
{
    public long Id { get; }

    public string FirstName { get; }

    public string LastName { get; }

    public string Email { get; }

}
