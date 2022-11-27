using Application.Interfaces;
using MediatR;

namespace Application.Services.WeeklyReport.Queries.RankingBarChart;

public class RankingBarChartQuery:IRequest<RankingBarChartVm>
{
    public class Handler:IRequestHandler<RankingBarChartQuery , RankingBarChartVm>
    {
        private readonly IDataBaseContext _context;

        public Handler( IDataBaseContext context)
        {
            
            _context = context;
        }
        public async Task<RankingBarChartVm> Handle(RankingBarChartQuery request, CancellationToken cancellationToken)
        {
            var teamsName = string.Join(",", _context.Teams.Select(t => t.TeamName));
            var teamsRank = string.Join(",", _context.Teams.Select(t => t.Rank));
            return new RankingBarChartVm()
            {
                TeamsName = teamsName,
                TeamsRank = teamsRank
            };
        }
    }
}