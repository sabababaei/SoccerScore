using Application.Services.Teams.Queries.GetTeamDetail;
using Application.Services.Teams.Queries.GetTeamOfSameStrength;
using Application.Services.Teams.Queries.GetTeamSummery.Queries.GetTeamMatchesInfo;
using Application.Services.Teams.Queries.GetTeamSummery.Queries.GetTeamStatisticalInfoHistory;

namespace Soccer.EndPoint.Models.Teams;

public class TeamSummeryVm
{
    public GetTeamMatchesInfoVm MatchesInfoVm { get; set; }
    public GetTeamStatisticalInfoHistoryChartVm StatisticalInfoHistoryVms { get; set; }
    public ICollection<GetTeamOfSameStrengthVm> SameStrengthVms { get; set; }
    public GetTeamDetailVm TeamDetailVm { get; set; }
}