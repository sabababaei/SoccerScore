using Application.Common.Exceptions;
using Application.Interfaces;
using Domain.Entities.Matches;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace Application.Services.Matches.Queries.GetMatchDetail;

public class GetMatchDetailQuery:IRequest<GetMatchDetailVm>
{
    public int Id { get; set; }   
    public class Handler:IRequestHandler<GetMatchDetailQuery , GetMatchDetailVm>
    {
      
        private readonly IDataBaseContext _context;

        public Handler( IDataBaseContext context)
        {
            
            _context = context;
        }
        public async Task<GetMatchDetailVm> Handle(GetMatchDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = _context.Matches.Include(m=>m.MatchTeams)
                .ThenInclude(m=>m.Team)
                .SingleOrDefault(p => p.Id == request.Id);
            if (entity==null)
            {
                throw new NotFoundException(nameof(Match), request.Id);
            }
            var result= new GetMatchDetailVm()
            {
                Id = entity.Id,
                Status = entity.Status,
                EndDate = entity.EndDate,
                StartDate = entity.StartDate,
                MatchTeams = entity.MatchTeams,
                MatchType = entity.MatchType,
                Importance = entity.Importance
            };
            
            return result;


        }
    }
}