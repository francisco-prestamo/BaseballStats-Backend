using BaseballStats.Application.Features.GenerateData;
using BaseballStats.Domain.Interfaces.DataAccess;
using BaseballStats.Domain.Entities;
using BaseballStats.Domain.Entities.Identity;
using FastEndpoints;
using BaseballStats.Domain.Enums;
using Microsoft.IdentityModel.Tokens;

class GenerateDataCommandHandler(IUnitOfWork unitOfWork) : CommandHandler<GenerateDataCommand, EmptyResponse>
{
    public override Task<EmptyResponse> ExecuteAsync(GenerateDataCommand command, CancellationToken ct = default)
    {
        
    }
    private List<Team> GenerateTeams(int Amount, List<long> technicalDirectorIds)
    {

        List<string> AnimalNames = [
            "Lions", "Tigers", "Bears", "Wolves", "Sharks", "Eagles", "Falcons", "Hawks", "Panthers", "Jaguars", 
            "Cougars", "Bobcats", "Lynx", "Wolverines", "Badgers", "Coyotes", "Foxes", "Owls", "Hornets", "Wasps", 
            "Scorpions", "Tarantulas", "Spiders", "Vipers", "Pythons", "Cobras", "Rattlesnakes", "Anacondas", "Boas"
        ];

        List<string> Adjectives = [
            "Black", "Red", "Blue", "Green", "Yellow", "Orange", "Purple", "Pink", "White", "Gray", 
            "Silver", "Gold", "Bronze", "Copper", "Platinum", "Titanium", "Steel", "Iron", "Lead", "Mercury", 
            "Nickel", "Tin", "Zinc", "Aluminum", "Cobalt", "Chromium", "Manganese", "Magnesium", "Potassium"
        ];

        List<string> colorNames = [
            "AliceBlue", "AntiqueWhite", "Aqua", "Aquamarine", "Azure", "Beige", "Bisque", "Black", "BlanchedAlmond", "Blue", 
            "BlueViolet", "Brown", "BurlyWood", "CadetBlue", "Chartreuse", "Chocolate", "Coral", "CornflowerBlue", "Cornsilk", "Crimson", 
            "Cyan", "DarkBlue", "DarkCyan", "DarkGoldenRod", "DarkGray", "DarkGreen", "DarkKhaki", "DarkMagenta", "DarkOliveGreen", "DarkOrange", 
            "DarkOrchid", "DarkRed", "DarkSalmon", "DarkSeaGreen", "DarkSlateBlue", "DarkSlateGray", "DarkTurquoise", "DarkViolet", "DeepPink", "DeepSkyBlue", 
            "DimGray", "DodgerBlue", "FireBrick", "FloralWhite", "ForestGreen", "Fuchsia", "Gainsboro", "GhostWhite", "Gold", "GoldenRod", 
            "Gray", "Green", "GreenYellow", "HoneyDew", "HotPink", "IndianRed", "Indigo", "Ivory", "Khaki", "Lavender", 
            "LavenderBlush", "LawnGreen", "LemonChiffon", "LightBlue", "LightCoral", "LightCyan", "LightGoldenRodYellow", "LightGray", "LightGreen", "LightPink", 
            "LightSalmon", "LightSeaGreen", "LightSkyBlue", "LightSlateGray", "LightSteelBlue", "LightYellow", "Lime", "LimeGreen", "Linen", "Magenta", 
            "Maroon", "MediumAquaMarine", "MediumBlue", "MediumOrchid", "MediumPurple", "MediumSeaGreen", "MediumSlateBlue", "MediumSpringGreen", "MediumTurquoise", "MediumVioletRed", 
            "MidnightBlue", "MintCream", "MistyRose", "Moccasin", "NavajoWhite", "Navy", "OldLace", "Olive", "OliveDrab", "Orange", 
            "OrangeRed", "Orchid", "PaleGoldenRod"
        ];

        List<string> CompanyNames = [
            "Acme", "Ajax", "Alcoa", "Altria", "Amazon", "Apple", "Aramark", "AT&T", "Berkshire Hathaway", "Boeing", 
            "Caterpillar", "Chevron", "Cisco", "Coca-Cola", "Comcast", "Dell", "Delta", "Disney", "Dow", "DuPont", 
            "eBay", "ExxonMobil", "FedEx", "Ford", "General Electric", "General Motors", "Google", "Harley-Davidson", "Hewlett-Packard", "Home Depot", 
            "Honeywell", "IBM", "Intel", "Johnson & Johnson", "JPMorgan Chase", "Kellogg", "Kraft", "Kroger", "Lockheed Martin", "Macy's", 
            "McDonald's", "Merck", "Microsoft", "Motorola", "Nestle", "Nike", "Nokia", "Northrop Grumman", "Oracle", "PepsiCo", 
            "Pfizer", "Procter & Gamble", "Qualcomm", "Raytheon", "Safeway", "Sears", "Sony", "Sprint", "Starbucks", "Target", 
            "Texas Instruments", "Time Warner", "Toyota", "United Technologies", "Verizon", "Wal-Mart", "Walt Disney", "Wells Fargo", "Xerox", "Yahoo"
        ];

        var teams = new List<Team>();

        Random random = new Random();

        for (int i = 0; i < Amount; i++)
        {
            var adjective = Adjectives[random.Next(0, Adjectives.Count)];
            var animalName = AnimalNames[random.Next(0, AnimalNames.Count)];
            var companyName = CompanyNames[random.Next(0, CompanyNames.Count)];
            var technincalDirectorId = technicalDirectorIds[random.Next(0, technicalDirectorIds.Count)];
            
            var team = new Team
            {
                Name = "The " + adjective + ' ' + animalName,
                Color = colorNames[random.Next(0, colorNames.Count)],
                Initials = adjective[0].ToString() + animalName[0].ToString(),
                RepresentedEntity = companyName,
                TechnicalDirectorId = technincalDirectorId
            };
            teams.Add(team);
        }

        return teams;
    }

    private List<Player> GeneratePlayers(int Amount)
    {
        var random = new Random();
        var players = new List<Player>();
        for (int i = 0; i < Amount; i++)
        {
            var name = FirstNames[random.Next(0, FirstNames.Count)] + ' ' + LastNames[random.Next(0, LastNames.Count)];
            var age = random.Next(18, 40);
            var yearsOfExperience = random.Next(0, age - 18);
            double? battingAverage = random.Next(10) < 2 ? null : random.NextDouble() * (0.4 - 0.1) + 0.1;

            var player = new Player
            {
                Name = name,
                Age = age,
                YearsOfExperience = yearsOfExperience,
                BattingAverage = battingAverage
            };
            players.Add(player);
        }

        return players;
    }

    private List<PlayerInPosition> GeneratePlayerInPosition(int Amount, List<Player> players)
    {
        var random = new Random();
        var playerInPositions = new List<PlayerInPosition>();
        List<PlayerPositions> validPlayerPositions = [
            PlayerPositions.Pitcher,
            PlayerPositions.Catcher,
            PlayerPositions.CenterField,
            PlayerPositions.LeftField,
            PlayerPositions.RightField,
            PlayerPositions.FirstBase,
            PlayerPositions.SecondBase,
            PlayerPositions.ThirdBase,
            PlayerPositions.ShortStop,
        ];

        var playerSetWithAllPlayers = new HashSet<long>(players.Select(x => x.Id));
        int pos = 0;
        
        while(!playerSetWithAllPlayers.IsNullOrEmpty())
        {
            var playerId = playerSetWithAllPlayers.First();
            var position = validPlayerPositions[pos];
            
            var playerInPosition = new PlayerInPosition
            {
                PlayerId = playerId,
                Position = position
            };
            playerInPositions.Add(playerInPosition);
            playerSetWithAllPlayers.Remove(playerId);
            pos = (pos + 1) % validPlayerPositions.Count;
        }

        for (int i = 0; i < Amount; i++)
        {
            var playerId = players[random.Next(0, players.Count)].Id;
            var position = validPlayerPositions[random.Next(0, validPlayerPositions.Count)];
            
            var playerInPosition = new PlayerInPosition
            {
                PlayerId = playerId,
                Position = position
            };
            playerInPositions.Add(playerInPosition);
        }

        playerInPositions = (
            from pip in playerInPositions
            group pip by new { pip.PlayerId, pip.Position } into g
            select new PlayerInPosition
            {
                PlayerId = g.Key.PlayerId,
                Position = g.Key.Position,
                Effectiveness = random.NextDouble() * (1 - 0.5) + 0.5
            }).ToList();

        return playerInPositions;
    }

    private (List<Series> series, List<Season> seasons) GenerateSeriesAndSeasons(int AmountSeasons, int AmountSeriesPerSeason)
    {
        List<string> seasonsOfTheYear = [
            "Spring", "Summer", "Fall", "Winter"
        ];
        List<string> Locality = [
            "Local", "Regional", "National", "International"
        ];
        List<string> CompetitionNames = [
            "Championship", "Cup", "Tournament", "League", "Series"
        ];

        var random = new Random();
        var seasons = Enumerable.Range(1, AmountSeasons).Select(x => new Season { Id = x }).ToList();
        var series = new List<Series>();
        
        var beginDate = DateTime.Now.Subtract(TimeSpan.FromDays(365*3));
        for (int i = 0; i < AmountSeasons; i++)
        {
            for (int j = 0; j < AmountSeriesPerSeason; j++)
            {
                var seasonId = i + 1;
                var name = seasonsOfTheYear[random.Next(0, seasonsOfTheYear.Count)] + ' ' + Locality[random.Next(0, Locality.Count)] + ' ' + CompetitionNames[random.Next(0, CompetitionNames.Count)];
                var startDate = beginDate.Add(TimeSpan.FromDays(random.Next(365)));
                var endDate = startDate.Add(TimeSpan.FromDays(random.Next(1, 30)));
                var seriesObj = new Series
                {
                    Name = name,
                    SeasonId = seasonId,
                    StartDate = DateOnly.FromDateTime(startDate),
                    EndDate = DateOnly.FromDateTime(endDate)
                };

                series.Add(seriesObj);
            }
        }

        return (series, seasons);
    }

    private List<PlayerInSeries> GeneratePlayerInSeries(List<Player> players, List<Series> series, List<Team> teams, List<PlayerInPosition> allowedPositions)
    {
        const long playersPerTeam = 25;


    }

    private List<string> FirstNames = [
        "James", "John", "Robert", "Michael", "William", "David", "Richard", "Joseph", "Thomas", "Charles", 
        "Christopher", "Daniel", "Matthew", "Anthony", "Mark", "Donald", "Steven", "Paul", "Andrew", "Joshua", 
        "Kenneth", "Kevin", "Brian", "George", "Edward", "Ronald", "Timothy", "Jason", "Jeffrey", "Ryan"
    ];
    private List<string> LastNames = [
        "Smith", "Johnson", "Williams", "Jones", "Brown", "Davis", "Miller", "Wilson", "Moore", "Taylor", 
        "Anderson", "Thomas", "Jackson", "White", "Harris", "Martin", "Thompson", "Garcia", "Martinez", "Robinson", 
        "Clark", "Rodriguez", "Lewis", "Lee", "Walker", "Hall", "Allen", "Young", "Hernandez", "King"
    ];
}
