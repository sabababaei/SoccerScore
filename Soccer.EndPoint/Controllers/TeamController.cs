using System.Text.Json;
using Application.Services.Players.Queries.GetPlayerList;
using Application.Services.Teams.Commands.CreateTeam;
using Application.Services.Teams.Queries.GetTeamDetail;
using Application.Services.Teams.Queries.GetTeamList;
using Application.Services.Teams.Queries.GetTeamOfSameStrength;
using Application.Services.Teams.Queries.GetTeamSummery.Queries.GetTeamMatchesInfo;
using Application.Services.Teams.Queries.GetTeamSummery.Queries.GetTeamStatisticalInfoHistory;
using Domain.Entities.Players;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;
using Soccer.EndPoint.Models.Players;
using Soccer.EndPoint.Models.Teams;

namespace Soccer.EndPoint.Controllers;

public class TeamController : BaseController
{
    // GET
    public IActionResult Index()
    {
        var list = Mediator.Send(new GetTeamListQuery()).Result
            .Select(p => new TeamListVm()
            {
                Id = p.Id,
                Players = p.TeamPlayers.Select(a => new PlayerListVm()
                {
                    Id = a.Id,
                    PlayerName = a.PlayerName
                }).ToList(),
                Rank = p.Rank,
                TeamName = p.TeamName
            }).ToList();
        return View(list);
    }

    [HttpGet]
    public IActionResult New()
    {
        var players = new SelectList(Mediator.Send(new GetPlayerListQuery()).Result, "Id", "PlayerName");

        return View(new NewTeamVm()
        {
            PlayersList = players
        });
    }

    [HttpPost]
    public IActionResult New([FromBody]string input)
    {
        NewTeamVm entity = JsonSerializer.Deserialize<NewTeamVm>(input);
       
        var result = Mediator.Send(new CreateTeamCommand()
        {
            Players = entity.Players.Select(p => new Player()
            {
                Id = p.PlayerId
            }).ToList(),
            TeamName = entity.TeamName
        }).Result;
        if (result.Succeeded)
        {
            return Json( Url.Action("Index"));
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

    public IActionResult Edit()
    {
        throw new NotImplementedException();
    }

    public IActionResult Delete()
    {
        throw new NotImplementedException();
    }

    

    public IActionResult Summery(int teamId)
    {
        var teamStatisticalInfoHistory = Mediator.Send(new GetTeamStatisticalInfoHistoryQuery()
        {
            Id = teamId
        }).Result;
        var teamSameStrength = Mediator.Send(new GetTeamOfSameStrengthQuery()
        {
            Id = teamId
        }).Result;
        var teamDetail = Mediator.Send(new GetTeamDetailQuery()
        {
            Id = teamId
        }).Result;
        var teamMatchInfo = Mediator.Send(new GetTeamMatchesInfoQuery()
        {
            Id = teamId
        }).Result;
        var model = new TeamSummeryVm()
        {
            MatchesInfoVm = teamMatchInfo.Data,
            SameStrengthVms = teamSameStrength.Data,
            TeamDetailVm = teamDetail,
            StatisticalInfoHistoryVms = teamStatisticalInfoHistory.Data
        };
        return View(model);
    }
}