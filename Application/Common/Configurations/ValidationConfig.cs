using System.Reflection;
using Application.Services.Matches.Commands.CreateMatch;
using Application.Services.Matches.Commands.GameOver;
using Application.Services.Matches.Commands.UpdateMatch;
using Application.Services.Players.Commands.CreatePlayer;
using Application.Services.Players.Commands.UpdatePlayer;
using Application.Services.Teams.Commands.CreateTeam;
using Application.Services.Teams.Commands.UpdateTeam;
using Domain.Entities.Players;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Common.Configurations;

public static class ValidationConfig
{
    public static IServiceCollection AddValidations(this IServiceCollection service, Assembly assembly)
    {
        service.AddFluentValidationAutoValidation();
    
        service.AddScoped<IValidator<CreatePlayerCommand> ,CreatePlayerCommandValidator>();
        service.AddScoped<IValidator<UpdatePlayerCommand>, UpdatePlayerCommandValidator>();

        service.AddScoped<IValidator<CreateTeamCommand>, CreateTeamCommandValidator>();
        service.AddScoped<IValidator<UpdateTeamCommand>, UpdateTeamCommandValidator>();
        
        service.AddScoped<IValidator<CreateMatchCommand>, CreateMatchCommandValidator>();
        service.AddScoped<IValidator<UpdateMatchCommand>, UpdateMatchCommandValidator>();
        
        service.AddScoped<IValidator<GameOverCommand>, GameOverCommandValidator>();
        

        return service;
    }
}