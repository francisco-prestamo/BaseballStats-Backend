namespace BaseballStats.Application.Features.GenerateData.DataGenerator;

public static partial class DataGenerator
{
    public static List<Domain.Entities.DirectionStaffTeam> GenerateDirectionStaffTeam(int Amount, List<Domain.Entities.DirectionStaff> directionStaffs, List<Domain.Entities.Team> teams)
    {
        var random = new Random(RandomSeed + 1);
        var directionStaffTeams = new List<Domain.Entities.DirectionStaffTeam>();
        foreach (var dst in directionStaffs)
        {
            var cntLeads = random.Next(0, Amount + 1);
            for (int i = 0; i < cntLeads; i++)
            {
                var team = teams[random.Next(0, teams.Count)];
                directionStaffTeams.Add(new Domain.Entities.DirectionStaffTeam()
                {
                    DirectionStaffId = dst.Id,
                    DirectionStaff = dst,
                    TeamId = team.Id,
                    Team = team
                });
            }
        }

        return directionStaffTeams.DistinctBy(x => new {x.DirectionStaffId, x.TeamId}).ToList();
    }
}