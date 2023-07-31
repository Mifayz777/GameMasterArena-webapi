using Dapper;
using GameMasterArena.DataAccess.Interfaces.Persons;
using GameMasterArena.DataAccess.Utils;
using GameMasterArena.DataAccess.ViewModels.ParticipatingTeams;
using GameMasterArena.DataAccess.ViewModels.Persons;
using GameMasterArena.Domain.Entities.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace GameMasterArena.DataAccess.Repositories.Persons;

public class PersonRepasitory : BaseRepasitory, IPerson
{
    public async Task<IList<PersonViewModel>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM public.PersonViewModel order by id desc " +
                $"offset {@params.GetSkipCount()} limit {@params.PageSize}";

            var result = (await _connection.QueryAsync<PersonViewModel>(query)).ToList();
            return result;
        }
        catch
        {
            return new List<PersonViewModel>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }



    public async Task<int> UpdateAsync(long id, PersonViewModel person)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "UPDATE public.person " +
                "SET  first_name=@FirstName, last_name=@LastName, image=@Image, " +
                "description=@Description,  update_at=now() " +
                $"WHERE id = {id};";
            var result = await _connection.ExecuteAsync(query, person);
            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }



   

    public async Task<int> CreateAsync(Person person)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO public.person(first_name, last_name, email, password, salt, image, description)" +
                " VALUES (@FirstName, @LastName, @Email, @Password, @Salt, @Image, @Description);";
            var result = await _connection.ExecuteAsync(query, person);
            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }



    public Task<(int ItemsCount, IList<PersonViewModel>)> SearchAsync(string search, PaginationParams @params)
    {
        throw new NotImplementedException();
    }

    public async Task<Person?> GetByEmailAsync(string email)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT * FROM public.person where email = @Email;";

            var data = await _connection.QuerySingleAsync<Person>(query, new { Email = email });
            return data;
        }
        catch
        {
            return null;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = "select count(*) from person";
            var result = await _connection.QuerySingleAsync<long>(query);
            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<PersonViewModel?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT * FROM public.person where id = @Id;";

            var data = await _connection.QuerySingleAsync<PersonViewModel>(query, new { Id = id });
            return data;
        }
        catch
        {
            return null;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }
}
