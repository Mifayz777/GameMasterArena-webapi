using GameMasterArena.DataAccess.Utils;

namespace GameMasterArena.DataAccess.Common.Interfaces;

public interface IGetAll<TModel>
{
    public Task<IList<TModel>> GetAllAsync(PaginationParams @params);
}
