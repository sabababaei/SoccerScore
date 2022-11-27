using System.Dynamic;
using System.Text.Json.Nodes;
using Application.Services.Matches.Commands.CreateMatch;
using Application.Services.Matches.Commands.GameOver;
using Application.Services.Matches.Queries.GetMatchDetail;
using Application.Services.Matches.Queries.GetMatchList;
using Application.Services.Teams.Queries.GetTeamList;
using Application.Services.Teams.Queries.GetTeamOfSameStrength;
using Domain.Entities.Matches;
 
using Domain.Entities.Teams;
using Domain.Enumarations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Newtonsoft.Json;
using Soccer.EndPoint.Models.Matches;
using Soccer.EndPoint.Models.Teams;
  
namespace Soccer.EndPoint.Controllers;

public class MatchController : BaseController
{
    
    public IActionResult Index()
    {
        var list = Mediator.Send(new GetMatchListQuery()).Result
            .Select(m => new MatchListVm()
            {
                Id = m.Id,
                Status = m.Status,
                MatchTeamsName = m.MatchTeamsName,
                EndDate = m.EndDate,
                StartDate = m.StartDate,
                Importance = m.Importance,
                MatchType = m.MatchType
                
            }).ToList();
        return View(list);
    }

    public IActionResult New()
    {
        var teams = new SelectList(Mediator.Send(new GetTeamListQuery()).Result, "Id", "TeamName");
       
        return View(new NewMatchesVm()
        {
            TeamsList = teams
        });
    }

    [HttpPost]
    public IActionResult New([FromBody] string input)
    {
        NewMatchesVm entity =
            JsonConvert.DeserializeObject<NewMatchesVm>(input);
        var createMatchCommand = new CreateMatchCommand()
        {
            MatchTeams = entity.Teams.Select(p => new MatchTeam()
            {
                Team = new Team() { Id = p.Id }
            }).ToList(),
            Status = (entity.StartDate <= DateTime.Now ? MatchStatus.Doing : MatchStatus.UnDone),
            StartDate = entity.StartDate,
            MatchType = entity.MatchTypes,
            Importance = Enum.Parse<MatchTypeImportance>(entity.MatchTypes.ToString())
        };
        var result = Mediator.Send(createMatchCommand).Result;

        if (result.Succeeded)
        {
            return Json(Url.Action("Index"));
        }
        else
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }

            return Json(Url.Action("New", entity));
        }
    }

    public IActionResult SuggestTeam([FromBody] Int32 TeamId)
    { 
        var result = Mediator.Send(new GetTeamOfSameStrengthQuery()
        {
            Id = TeamId
        }).Result;

        IEnumerable<TeamOfSameStrengthVm> list=null ;
        if (result.Succeeded)
        {
            list=result.Data.Select(t => new TeamOfSameStrengthVm()
            {
                Id = t.Id,
                Rank = t.Rank,
                Rate = t.Rate,
                MatchesCount = t.MatchesCount,
                TeamName = t.TeamName,
                TeamPlayers = t.TeamPlayers,
                Power = t.Power
            });
        }
       
        return PartialView("ShowTeamOfSameStrength", list);
         
    }

    public IActionResult GameOver(int Id)
    {
        var entity = Mediator.Send(new GetMatchDetailQuery()
        {
            Id = Id
        }).Result;
        var model = new GameOverVm()
        {
            Id = entity.Id,
            EndDate = entity.EndDate,
            MatchTeams = entity.MatchTeams
        };
        return View(model);
         
    }

    [HttpPost]
    public IActionResult GameOver([FromBody] string input)
    {
         GameOverVm entity = JsonConvert.DeserializeObject<GameOverVm>(input);
         var gameOverCommand = new GameOverCommand()
         {
             Id = entity.Id,
             EndDate = entity.EndDate,
             MatchTeams = entity.MatchTeams
             
         };
         var result = Mediator.Send(gameOverCommand).Result;
         if (result.Succeeded)
         {
         return Json(Url.Action("Index"));
         }
         else
         {
             foreach (var error in result.Errors)
             {
                 ModelState.AddModelError("", error);
             }

             return Json(Url.Action("GameOver", entity));
         }
    }

   
}