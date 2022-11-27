using Application.Interfaces;
using Domain.Entities.Matches;
using Domain.Entities.Players;
using Domain.Entities.Teams;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Persistence;

public class DataBaseContext:DbContext,IDataBaseContext
{
    public DataBaseContext(DbContextOptions<DataBaseContext> options):base(options)
    {
    }

     
    public DbSet<Player> Players { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Match> Matches { get; set; }

    public DbSet<MatchTeam> MatchTeams { get; set; }
    public DbSet<TeamStatisticalInfoHistory> TeamRates { get; set; }
    public DatabaseFacade DbTransaction()
    {
        return this.Database;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Match>()
            .HasMany<MatchTeam>(m=>m.MatchTeams)
            .WithOne(t=>t.Match)
            ;
        modelBuilder.Entity<Match>()
            .Property(p => p.EndDate)
            .IsRequired(false);
         

        modelBuilder.Entity<Team>()
            .HasMany<Player>(t => t.Players)
            .WithMany(p => p.Teams)
            ;
        modelBuilder.Entity<Team>()
            .HasMany<MatchTeam>(t => t.MatchTeams)
            .WithOne(m=>m.Team);
        
        
        base.OnModelCreating(modelBuilder);
    }
}