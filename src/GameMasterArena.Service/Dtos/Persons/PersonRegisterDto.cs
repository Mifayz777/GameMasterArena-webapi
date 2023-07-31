using Microsoft.AspNetCore.Http;

namespace GameMasterArena.Service.Dtos.Persons;

public class PersonRegisterDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public IFormFile Image { get; set; } = default!;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

}
