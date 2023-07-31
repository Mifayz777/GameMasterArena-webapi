using GameMasterArena.Domain.Entities.Persons;

namespace GetTalim.Service.Interfaces.Persons;

public interface ITokenPersonService
{
    public string GenerateToken(Person person);
}
