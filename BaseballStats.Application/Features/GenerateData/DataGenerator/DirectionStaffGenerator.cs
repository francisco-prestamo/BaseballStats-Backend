namespace BaseballStats.Application.Features.GenerateData.DataGenerator;

public static partial class DataGenerator
{
    public static List<Domain.Entities.DirectionStaff> GenerateDirectionStaff(int Amount)
    {
        var random = new Random(RandomSeed + 1);
        var directionStaffs = new List<Domain.Entities.DirectionStaff>();
        for (int i = 0; i < Amount; i++)
        {
            var name = FirstNames[random.Next(0, FirstNames.Count)] + ' ' + LastNames[random.Next(0, LastNames.Count)];
            var dst = new Domain.Entities.DirectionStaff()
            {
                Id = random.NextInt64(),
                Name = name
            };
            directionStaffs.Add(dst);
        }

        return directionStaffs;
    }
}