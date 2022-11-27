using Domain.Entities.Matches;
using Domain.Entities.Players;
using Domain.Entities.Teams;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Application.Interfaces;

public interface IDataBaseContext
{
    DbSet<Player> Players { get; set; }
    DbSet<Team> Teams { get; set; }
    DbSet<Match> Matches { get; set; }
     DbSet<MatchTeam> MatchTeams { get; set; }
     DbSet<TeamStatisticalInfoHistory> TeamRates { get; set; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    DatabaseFacade DbTransaction();
     

}