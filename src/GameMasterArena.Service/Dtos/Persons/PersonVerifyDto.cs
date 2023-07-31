namespace GameMasterArena.Service.Dtos.PersonsAuth;

public class PersonVerifyDto
{
    public string Email { get; set; } = string.Empty;
    public int Code { get; set; }
}
