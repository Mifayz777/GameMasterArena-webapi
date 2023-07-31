using GameMasterArena.Service.Interfaces.Common;

namespace GameMasterArena.Service.Services.Common;

public class ShortStorageService : IShortStorageService
{
    public IDictionary<string, string> KeyValuePairs { get; set; }
	public ShortStorageService()
	{
		KeyValuePairs = new Dictionary<string, string>();
	}
}
