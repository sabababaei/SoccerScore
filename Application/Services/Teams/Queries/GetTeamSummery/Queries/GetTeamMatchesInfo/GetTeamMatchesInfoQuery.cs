using Application.Common.Models;
using Application.Interfaces;
using Application.Services.Teams.Queries.GetTeamSummery.Queries.GetTeamStatisticalInfoHistory;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Teams.Queries.GetTeamSummery.Queries.GetTeamMatchesInfo;

public class GetTeamMatchesInfoQuery:IRequest<Result< GetTeamMatchesInfoVm>>
{
    public int Id { get; set; }   
    public class Handler:IRequestHandler<GetTeamMatchesInfoQuery ,Result< GetTeamMatchesInfoVm>>
    {
        private readonly IDataBaseContext _context;

        public Handler( IDataBaseContext context)
        {
          
            _context = context;
        }
        public async Task<Result<GetTeamMatchesInfoVm>> Handle(GetTeamMatchesInfoQuery request, CancellationToken cancellationToken)
        {
            var list
                =await _context.MatchTeams.Where(m => m.Team.Id == request.Id).ToListAsync(cancellationToken);
            return new Result<GetTeamMatchesInfoVm>(true, new[] { "" },
                new GetTeamMatchesInfoVm()
                {
                    Id = request.Id,
                    MatchesCount = list.Count,
                    NumberOfGoal = list.Sum(t => t.NumberOfGoal)
                });
            
           
        }
    }
}