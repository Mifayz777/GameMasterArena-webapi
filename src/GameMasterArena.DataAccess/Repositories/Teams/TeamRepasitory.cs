using Dapper;
using GameMasterArena.DataAccess.Interfaces.Teams;
using GameMasterArena.DataAccess.Repositories.ParticipatingTeams;
using GameMasterArena.DataAccess.Repositories.TeamMembers;
using GameMasterArena.DataAccess.Utils;
using GameMasterArena.DataAccess.ViewModels.Teams;
using GameMasterArena.Domain.Constants;
using GameMasterArena.Domain.Entities.Persons;
using GameMasterArena.Domain.Entities.TeamMembers;
using GameMasterArena.Domain.Entities.Teams;
using GameMasterArena.Domain.Entities.Tournaments;
using Microsoft.VisualBasic;
using Npgsql.Internal.TypeHandlers.DateTimeHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;
using TeamViewModel = GameMasterArena.DataAccess.ViewModels.Teams.TeamViewModel;

namespace GameMasterArena.DataAccess.Repositories.Teams;

public class TeamRepasitory : BaseRepasitory, ITeam
{
  

    public async Task<int> CreateAsync(Team entity)
    {
        try
        {
            var checkLeader = await CheckLeader(entity.Person_id);

            var checkName = await CheckName(entity.Name);
            await _connection.OpenAsync();

            if (checkLeader)
            {
                return 0;
            }
            else
            {
                if (checkName)
                {
                    return 0;
                }
                else
                {
                    string query = "INSERT INTO public.team(person_id, name, image, description) " +
                                    "VALUES (@Person_id, @Name, @Image, @Description);";
                    var result = await _connection.ExecuteAsync(query, entity);

                    query = "SELECT * FROM public.team ORDER BY id desc limit 1;";
                    var team = await _connection.QuerySingleAsync<Team>(query);

                    TeamMember teamMember = new TeamMember()
                    {
                        Team_id = team.Id,
                        Person_id = team.Person_id,
                        Description = team.Description,
                    };

                    TeamMemberRepasitory teamMemberRepasitory = new TeamMemberRepasitory();
                    await teamMemberRepasitory.CreateAsync(teamMember);
                    return result;
                }
               
            }
            
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
            TeamMemberRepasitory teamMemberRepasitory = new TeamMemberRepasitory();
            await teamMemberRepasitory.DeleteTeamAsync(id);
            ParticipatingTeamsRepasitory participatingTeamsRepasitory = new ParticipatingTeamsRepasitory();
            await participatingTeamsRepasitory.DeleteAsync(id);
            string query = "DELETE FROM public.team WHERE id = @Id;";
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

    

    public async Task<IList<TeamViewModel>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT * FROM public.TeamViewModel " +
                $"offset {@params.GetSkipCount()} limit {@params.PageSize}";

            var result = (await _connection.QueryAsync<TeamViewModel>(query)).ToList();
            return result;
        }
        catch 
        {
            return new List<TeamViewModel>();
        }
        finally
        {
            await _connection.CloseAsync();
        }

    }

    public async Task<ViewModels.Teams.TeamViewModel?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM TeamViewModel where teamid=@Id";
            var result = await _connection.QuerySingleAsync<ViewModels.Teams.TeamViewModel>(query, new { Id = id });
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
    
    
    public async Task<int> UpdateAsync(long id, TeamViewModel entity, long leaderId)
    {


        try
        {
            var checkLeader = await CheckLeader(leaderId);
            if (checkLeader)
            {
                return 0;
            }
            else
            {
                await _connection.OpenAsync();
                string query = "UPDATE public.team SET person_id = @Person_id, name=@Name, image=@Image, description=@Description, update_at= now() " +
                    $"WHERE id = {id};";
                var result = await _connection.ExecuteAsync(query, entity);
                return result;
            }

            
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


    public Task<(int ItemsCount, IList<ViewModels.Teams.TeamViewModel>)> SearchAsync(string search, PaginationParams @params)
    {
        throw new NotImplementedException();
    }

    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = "select count(*) from team";
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

    public async Task<bool> CheckAsync(long teamId, long personId)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "select * from team " +
                $"where id = {teamId} and person_id = {personId}";
            var result = await _connection.QuerySingleAsync<long>(query);
            if (result > 0)  return true;
            else return false;
        }
        catch
        {
            return false;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<bool> CheckLeader(long personId)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "select count(*) from team " +
                $"where person_id = {personId}";
            var result = await _connection.QuerySingleAsync<long>(query);
            if (result > 0) return true;
            else return false;
        }
        catch
        {
            return false;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<bool> CheckName(string name)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "select count(*) from team where lower(name) = @Name";
            var result = await _connection.QuerySingleAsync<long>(query, new {Name = name});
            if (result > 0) return true;
            else return false;
        }
        catch
        {
            return false;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }
}
