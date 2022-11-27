using Application.Common.Models;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Teams.Queries.GetTeamSummery.Queries.GetTeamStatisticalInfoHistory;

public class GetTeamStatisticalInfoHistoryQuery:IRequest<Result<GetTeamStatisticalInfoHistoryChartVm>>
{
    public int Id { get; set; }   
    public class Handler:IRequestHandler<GetTeamStatisticalInfoHistoryQuery ,Result< GetTeamStatisticalInfoHistoryChartVm>>
    {
        private readonly IDataBaseContext _context;

        public Handler( IDataBaseContext context)
        {
          
            _context = context;
        }

        public async Task<Result<GetTeamStatisticalInfoHistoryChartVm>> Handle(
            GetTeamStatisticalInfoHistoryQuery request, CancellationToken cancellationToken)
        {
            var list
                = await _context.TeamRates.Include(t => t.Team)
                    .Where(t => t.Team.Id == request.Id)
                    .ToListAsync(cancellationToken);
            
            if (list != null)
            {
                var teamsName = string.Join(",", list.Select(t => t.Team.TeamName));
                var teamsRank = string.Join(",", list.Select(t => t.LastRank));
                var teamsPower = string.Join(",", list.Select(t => t.LastPower));
                var teamsRate = string.Join(",", list.Select(t => t.LastRate));
                var dates = string.Join(",", list.Select(t => t.LastDate.Value.ToShortDateString()));
                var result = new GetTeamStatisticalInfoHistoryChartVm()
                
                {
                    Id = request.Id,
                    LastDate =dates,
                    LastPower = teamsPower,
                    LastRank = teamsRank,
                    LastRate = teamsRate,
                    TeamsName = teamsName
                };
                
                return new Result<GetTeamStatisticalInfoHistoryChartVm>(true, new[] { "" }, result);
            }
            else
            { return new Result<GetTeamStatisticalInfoHistoryChartVm>(false, new[] { "" }
                ,null );
            }

        }
    }
}