using BaseballStats.Application.Features.GenerateData;
using BaseballStats.Domain.Interfaces.DataAccess;
using BaseballStats.Domain.Entities;
using BaseballStats.Domain.Entities.Identity;
using FastEndpoints;
using BaseballStats.Domain.Enums;
using Microsoft.IdentityModel.Tokens;
using BaseballStats.Application.Features.GenerateData.DataGenerator;

class GenerateDataCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<GenerateDataCommand, EmptyResponse>
{
    public override Task<EmptyResponse> ExecuteAsync(GenerateDataCommand command, CancellationToken ct = default)
    {
        var random = new Random();

        var users = DataGenerator.GenerateUsers(50);
        System.Console.WriteLine("User count: " + users.Count);
        var nonTechnicalDirectors = users.Where(x => x.Type != UserTypes.TechnicalDirector).ToList();
        var technicalDirectors = users.Where(x => x.Type == UserTypes.TechnicalDirector).ToList();
        System.Console.WriteLine("Technical director count: " + technicalDirectors.Count);
        var teams = DataGenerator.GenerateTeams(30, technicalDirectors.Select(x => x.Id).ToList());
        System.Console.WriteLine("Team count: " + teams.Count);
        var players = DataGenerator.GeneratePlayers(100);
        System.Console.WriteLine("Player count: " + players.Count);
        var (series, seasons) = DataGenerator.GenerateSeriesAndSeasons(10, 4);
        System.Console.WriteLine("Series count: " + series.Count);
        var playerInPositions = DataGenerator.GeneratePlayerInPosition(300, players);
        System.Console.WriteLine("Player in position count: " + playerInPositions.Count);
        var pitchers = DataGenerator.GeneratePitchers(playerInPositions.Where(x => x.Position == PlayerPositions.Pitcher).Select(x => x.PlayerId).Distinct().ToList());
        System.Console.WriteLine("Pitcher count: " + pitchers.Count);
        var playerInSeries = DataGenerator.GeneratePlayerInSeries(players, series, teams, playerInPositions);
        System.Console.WriteLine("Player in series count: " + playerInSeries.Count);
        var games = DataGenerator.GenerateGames(series, teams, playerInSeries);
        System.Console.WriteLine("Game count: " + games.Count);
        var (alignedPlayerInGames, substitutions) = DataGenerator.GenerateAlignmentsAndSubstitutions(players, games, playerInPositions, playerInSeries);
        System.Console.WriteLine("Aligned player in game count: " + alignedPlayerInGames.Count);
        var directionStaffs = DataGenerator.GenerateDirectionStaff(50);
        System.Console.WriteLine("Direction staff count: " + directionStaffs.Count);
        var directionStaffTeams = DataGenerator.GenerateDirectionStaffTeam(50, directionStaffs, teams);
        System.Console.WriteLine("Direction staff team count: " + directionStaffTeams.Count);

        unitOfWork.Repository<PlayerInSeries>().DropWhere(x => true);
        unitOfWork.Repository<RegisteredUser>().DropWhere(x => true);
        unitOfWork.Repository<DirectionStaffTeam>().DropWhere(x => true);
        unitOfWork.Repository<Team>().DropWhere(x => true);
        unitOfWork.Repository<TechnicalDirector>().DropWhere(x => true);
        unitOfWork.Repository<Substitution>().DropWhere(x => true);
        unitOfWork.Repository<AlignedPlayerInGame>().DropWhere(x => true);
        unitOfWork.Repository<Game>().DropWhere(x => true);
        unitOfWork.Repository<Series>().DropWhere(x => true);
        unitOfWork.Repository<PlayerInPosition>().DropWhere(x => true);
        unitOfWork.Repository<Player>().DropWhere(x => true);
        unitOfWork.Repository<Pitcher>().DropWhere(x => true);
        unitOfWork.Repository<Season>().DropWhere(x => true);
        unitOfWork.Repository<DirectionStaff>().DropWhere(x => true);
        unitOfWork.SaveChanges();


        
        unitOfWork.Repository<RegisteredUser>().AddRange(nonTechnicalDirectors);
        unitOfWork.SaveChanges();
        unitOfWork.Repository<TechnicalDirector>().AddRange(technicalDirectors.Select(x => new TechnicalDirector() {Id = x.Id, Password = x.Password, Username = x.Username}));
        unitOfWork.SaveChanges();
        unitOfWork.Repository<Team>().AddRange(teams);
        unitOfWork.SaveChanges();
        unitOfWork.Repository<Player>().AddRange(players);
        unitOfWork.SaveChanges();
        unitOfWork.Repository<Season>().AddRange(seasons);
        unitOfWork.SaveChanges();
        unitOfWork.Repository<Series>().AddRange(series);
        unitOfWork.SaveChanges();
        unitOfWork.Repository<PlayerInPosition>().AddRange(playerInPositions);
        unitOfWork.SaveChanges();
        unitOfWork.Repository<Pitcher>().AddRange(pitchers);
        unitOfWork.SaveChanges();
        unitOfWork.Repository<PlayerInSeries>().AddRange(playerInSeries);
        unitOfWork.SaveChanges();
        unitOfWork.Repository<Game>().AddRange(games);
        unitOfWork.SaveChanges();
        unitOfWork.Repository<AlignedPlayerInGame>().AddRange(alignedPlayerInGames);
        unitOfWork.SaveChanges();
        unitOfWork.Repository<Substitution>().AddRange(substitutions);
        unitOfWork.SaveChanges();
        unitOfWork.Repository<DirectionStaff>().AddRange(directionStaffs);
        unitOfWork.SaveChanges();
        unitOfWork.Repository<DirectionStaffTeam>().AddRange(directionStaffTeams);
        unitOfWork.SaveChanges();

        return Task.FromResult(new EmptyResponse());        
    }
}
