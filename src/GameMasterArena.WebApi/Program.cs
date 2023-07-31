using GameMasterArena.Api.Configurations;
using GameMasterArena.DataAccess.Interfaces.ParticipatingTeams;
using GameMasterArena.DataAccess.Interfaces.Persons;
using GameMasterArena.DataAccess.Interfaces.TeamMembers;
using GameMasterArena.DataAccess.Interfaces.Teams;
using GameMasterArena.DataAccess.Interfaces.Tournaments;
using GameMasterArena.DataAccess.Repositories.ParticipatingTeams;
using GameMasterArena.DataAccess.Repositories.Persons;
using GameMasterArena.DataAccess.Repositories.TeamMembers;
using GameMasterArena.DataAccess.Repositories.Teams;
using GameMasterArena.DataAccess.Repositories.Tournaments;
using GameMasterArena.Service.Interfaces.Auth;
using GameMasterArena.Service.Interfaces.Common;
using GameMasterArena.Service.Interfaces.Notifications;
using GameMasterArena.Service.Interfaces.ParticipatingTeamsService;
using GameMasterArena.Service.Interfaces.Persons;
using GameMasterArena.Service.Interfaces.TeamMemberServices;
using GameMasterArena.Service.Interfaces.TeamServices;
using GameMasterArena.Service.Interfaces.TournamentServices;
using GameMasterArena.Service.Services.Auth;
using GameMasterArena.Service.Services.Common;
using GameMasterArena.Service.Services.Notifications;
using GameMasterArena.Service.Services.ParticipatingTeamsSevice;
using GameMasterArena.Service.Services.Persons;
using GameMasterArena.Service.Services.Students;
using GameMasterArena.Service.Services.TeamMemberService;
using GameMasterArena.Service.Services.TeamService;
using GameMasterArena.Service.Services.TournamentService;
using GetTalim.Service.Interfaces.Persons;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();

builder.ConfigureJwtAuth();
builder.ConfigureSwaggerAuth();


builder.Services.AddScoped<ITeam, TeamRepasitory>();
builder.Services.AddScoped<IParticipatingTeam, ParticipatingTeamsRepasitory>();
builder.Services.AddScoped<IPerson, PersonRepasitory>();
builder.Services.AddScoped<ITeam, TeamRepasitory>();
builder.Services.AddScoped<ITeamMember, TeamMemberRepasitory>();
builder.Services.AddScoped<ITournament, TournamentRepasitory>();

builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<ITournamentService, TournamentService>();
builder.Services.AddScoped<ITeamMemberService, TeamMemberService>();
builder.Services.AddScoped<IParticipatingTeamService, ParticipatingTeamService>();
builder.Services.AddScoped<IPersonService, PersonService>();

builder.Services.AddScoped<IAuthPersonService, AuthPersonService>();
builder.Services.AddScoped<ITokenPersonService, TokenPersonService>();
builder.Services.AddScoped<IMailSender, MailSender>();
builder.Services.AddScoped<IIdentityService, IdentityService>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
