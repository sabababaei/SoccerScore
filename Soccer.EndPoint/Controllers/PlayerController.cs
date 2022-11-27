using Application.Services.Players.Commands.CreatePlayer;
using Application.Services.Players.Queries.GetPlayerList;
using Microsoft.AspNetCore.Mvc;
using Soccer.EndPoint.Models.Players;

namespace Soccer.EndPoint.Controllers;

public class PlayerController : BaseController
{
    // GET
    public IActionResult Index()
    {
        var list = Mediator.Send(new GetPlayerListQuery()).Result
            .Select(p=>new PlayerListVm()
            {
                Id = p.Id ,
                PlayerName = p.PlayerName
            }).ToList();
        return View(list);
    }

    public IActionResult Edit()
    {
        throw new NotImplementedException();
    }

    public IActionResult Delete()
    {
        throw new NotImplementedException();
    }

    public IActionResult New()
    {
        return View();
    }

    [HttpPost]
    public IActionResult New(NewPlayerVm entity)
    {
        var result = Mediator.Send(new CreatePlayerCommand() { PlayerName = entity.PlayerName }).Result;
        if (result.Succeeded)
        {
            return RedirectToAction("Index");
        }
        else
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }

            return View(entity);

        }
    }
}