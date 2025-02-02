using BaseballStats.Domain.Enums;

namespace BaseballStats.Application.Features.GenerateData.DataGenerator;

public static partial class DataGenerator
{
    private const int RandomSeed = 1;

    private static List<PlayerPositions> ValidPlayerPositions = [
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
    private static List<string> FirstNames = [
        "James", "John", "Robert", "Michael", "William", "David", "Richard", "Joseph", "Thomas", "Charles", 
        "Christopher", "Daniel", "Matthew", "Anthony", "Mark", "Donald", "Steven", "Paul", "Andrew", "Joshua", 
        "Kenneth", "Kevin", "Brian", "George", "Edward", "Ronald", "Timothy", "Jason", "Jeffrey", "Ryan"
    ];
    private static List<string> LastNames = [
        "Smith", "Johnson", "Williams", "Jones", "Brown", "Davis", "Miller", "Wilson", "Moore", "Taylor", 
        "Anderson", "Thomas", "Jackson", "White", "Harris", "Martin", "Thompson", "Garcia", "Martinez", "Robinson", 
        "Clark", "Rodriguez", "Lewis", "Lee", "Walker", "Hall", "Allen", "Young", "Hernandez", "King"
    ];
}