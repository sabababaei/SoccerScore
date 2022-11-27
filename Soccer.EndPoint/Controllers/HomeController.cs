using System.Diagnostics;
using Application.Services.WeeklyReport.Queries.BestTeam;
using Application.Services.WeeklyReport.Queries.FewestAndMostGoals;
using Application.Services.WeeklyReport.Queries.LastAndNumberOfMatch;
using Application.Services.WeeklyReport.Queries.MatchesInProgress;
using Application.Services.WeeklyReport.Queries.RankingBarChart;
using Microsoft.AspNetCore.Mvc;
using Soccer.EndPoint.Models;
using Soccer.EndPoint.Models.WeeklyReport;


namespace Soccer.EndPoint.Controllers;

public class HomeController : BaseController
{
    
    public IActionResult Index()
    {
        WeeklyReportVm model = new WeeklyReportVm()
        {
            BestTeamVm = CreatePartial_BestTeamVm(),
            MatchesInProgressVm = CreatePartial_MatchesInProgressVm(),
            FewestAndMostGoalsVm = CreatePartial_FewestAndMostGoalsVm(),
            LastAndNumberOfMatchVm = CreatePartial_LastAndNumberOfMatchVm() ,
            RankingBarChartVm = CreatePartial_RankingBarChartVm()
        };
         
        return View(model);
    }

    private RankingBarChartVm CreatePartial_RankingBarChartVm()
    {
        var result=Mediator.Send(new RankingBarChartQuery()).Result;
        return result;
    }

    private MatchesInProgressVm CreatePartial_MatchesInProgressVm()
    {
        var result=Mediator.Send(new MatchesInProgressQuery()).Result;
        return result;
        
    }

    private LastAndNumberOfMatchVm CreatePartial_LastAndNumberOfMatchVm()
    {
        var result=Mediator.Send(new LastAndNumberOfMatchQuery()).Result;
        return result;
    }

    private FewestAndMostGoalsVm CreatePartial_FewestAndMostGoalsVm()
    {
        var result=Mediator.Send(new FewestAndMostGoalsQuery()).Result;
        return result;
    }

    private BestTeamVm CreatePartial_BestTeamVm()
    {
        var result=Mediator.Send(new BestTeamQuery()).Result;
        return result;
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult PlayersList()
    {
        return RedirectToAction("Index", "Player");
       
    }

    public IActionResult TeamsList()
    {
        return RedirectToAction("Index", "Team");
    }

    public IActionResult MatchesList()
    {
        return RedirectToAction("Index", "Match");
    }

    public IActionResult NewPlayer()
    {
        return RedirectToAction("New", "Player");
    }

    public IActionResult NewTeam()
    {
        return RedirectToAction("New", "Team");
    }

    public IActionResult NewMatch()
    {
        return RedirectToAction("New", "Match");
    }
}