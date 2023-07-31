namespace GameMasterArena.Service.Dtos.Security;

public class PersonVerificationDto
{
    public int Code { get; set; }

    public int Attempt { get; set; }

    public DateTime CreatedAt { get; set; }
}
