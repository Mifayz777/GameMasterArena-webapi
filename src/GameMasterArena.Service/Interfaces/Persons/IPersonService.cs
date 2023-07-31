using GameMasterArena.DataAccess.Utils;
using GameMasterArena.DataAccess.ViewModels.Persons;
using GameMasterArena.DataAccess.ViewModels.Teams;
using GameMasterArena.Service.Dtos.Persons;
using GameMasterArena.Service.Dtos.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMasterArena.Service.Interfaces.Persons;

public interface IPersonService
{
    

    public Task<IList<PersonViewModel>> GetAllAsync(PaginationParams @params);

    public Task<PersonViewModel> GetByIdAsync(long teamId);

    public Task<bool> UpdateAsync(PersonUpdateDto dto);

    public Task<long> CountAsync();
}
