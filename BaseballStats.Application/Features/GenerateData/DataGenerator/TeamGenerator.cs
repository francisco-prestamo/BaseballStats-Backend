using BaseballStats.Domain.Entities;

namespace BaseballStats.Application.Features.GenerateData.DataGenerator;

public static partial class DataGenerator
{
    public static List<Domain.Entities.Team> GenerateTeams(int Amount, List<long> technicalDirectorIds)
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

        var teams = new List<Domain.Entities.Team>();

        Random random = new Random(RandomSeed);

        for (int i = 0; i < Amount; i++)
        {
            var adjective = Adjectives[random.Next(0, Adjectives.Count)];
            var animalName = AnimalNames[random.Next(0, AnimalNames.Count)];
            var companyName = CompanyNames[random.Next(0, CompanyNames.Count)];
            var technincalDirectorId = technicalDirectorIds[random.Next(0, technicalDirectorIds.Count)];
            
            var team = new Domain.Entities.Team
            {
                Id = random.Next(),
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
}
