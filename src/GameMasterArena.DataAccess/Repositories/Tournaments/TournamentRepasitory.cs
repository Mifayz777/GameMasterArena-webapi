using Dapper;
using GameMasterArena.DataAccess.Interfaces.Tournaments;
using GameMasterArena.DataAccess.Repositories.ParticipatingTeams;
using GameMasterArena.DataAccess.Repositories.TeamMembers;
using GameMasterArena.DataAccess.Utils;
using GameMasterArena.Domain.Entities.ParticipatingTeams;
using GameMasterArena.Domain.Entities.Tournaments;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace GameMasterArena.DataAccess.Repositories.Tournaments;

public class TournamentRepasitory : BaseRepasitory, ITournament
{
    public async Task<int> CreateAsync(Tournament entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO public.tournament(name, reward, start_at, image, description) " +
                "VALUES (@Name, @Reward, @Start_at, @Image, @Description);";
            var result = await _connection.ExecuteAsync(query, entity);
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
    

    public async Task<int> DeleteAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "DELETE FROM public.tournament WHERE id = @Id;";
            var result = await _connection.ExecuteAsync(query, new { Id = id });
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

    
    public async Task<IList<Tournament>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM public.tournament order by id desc " +
                $"offset {@params.GetSkipCount()} limit {@params.PageSize}";

            var result = (await _connection.QueryAsync<Tournament>(query)).ToList();
            return result;
        }
        catch
        {
            return new List<Tournament>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }
    
    
    public async Task<Tournament?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM tournament where id=@Id";
            var result = await _connection.QuerySingleAsync<Tournament>(query, new { Id = id });
            return result;
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


    public async Task<int> UpdateAsync(long id, Tournament entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "UPDATE public.tournament SET  name=@Name, reward=@Reward, start_at=@Start_at, image=@Image, description=@Description, update_at= now() " +
                $" WHERE id = { id };";
            var result = await _connection.ExecuteAsync(query, entity);
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



    //public async Task<int> ParticipationAsync(ParticipatingTeam entity)
    //{
    //    ParticipatingTeamsRepasitory participatingTeamsRepasitory = new ParticipatingTeamsRepasitory();
    //    var result = await participatingTeamsRepasitory.CreateAsync(entity);
    //    return result;
    //}

    public Task<(int ItemsCount, IList<Tournament>)> SearchAsync(string search, PaginationParams @params)
    {
        throw new NotImplementedException();
    }

    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = "select count(*) from tournament";
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
}

