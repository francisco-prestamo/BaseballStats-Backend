namespace BaseballStats.Application.DTOs
{
    public record TeamDto 
    {
        public long id { get; init; }
        public string name { get; init; } = null!;
        public string initials { get; init; } = null!;
        public string color { get; init; } = null!;
        public string representedEntity { get; init; } = null!;
    }
}