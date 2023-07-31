using GameMasterArena.DataAccess.Common.Interfaces;
using GameMasterArena.DataAccess.ViewModels.Persons;
using GameMasterArena.DataAccess.ViewModels.Teams;
using GameMasterArena.Domain.Entities.Persons;
using GameMasterArena.Domain.Entities.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMasterArena.DataAccess.Interfaces.Persons;

public interface IPerson: IGetAll<PersonViewModel>, ISearchable<PersonViewModel>
{
    public Task<int> UpdateAsync(long id, PersonViewModel person);

    public Task<int> CreateAsync(Person person);

    public Task<Person?> GetByEmailAsync(string email);

    public Task<long> CountAsync();

    public Task<PersonViewModel?> GetByIdAsync(long id);

}
