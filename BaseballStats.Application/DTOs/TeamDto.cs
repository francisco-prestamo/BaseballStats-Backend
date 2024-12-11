namespace BaseballStats.Application.DTOs
{
    public record TeamDto 
    {
        public long Id { get; init; }
        public string Name { get; init; } = null!;
        public string Initials { get; init; } = null!;
        public string Color { get; init; } = null!;
        public string RepresentedEntity { get; init; } = null!;
    }
}