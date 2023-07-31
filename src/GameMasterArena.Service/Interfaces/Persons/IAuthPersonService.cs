using GameMasterArena.Service.Dtos.Persons;

namespace GameMasterArena.Service.Interfaces.Persons;
public interface IAuthPersonService
{
    public Task<(bool Result, int CashedMinutes)> RegisterAsync(PersonRegisterDto registerDto);
    public Task<(bool Result, int CashedVerificationMinutes)> SendCodeForRegisterAsync(string mail);
    public Task<(bool Result, string Token)> VerifyRegisterAsync(string mail, int code);
    public Task<(bool Result, string Token)> LoginAsync(PersonLoginDto loginDto);
}
