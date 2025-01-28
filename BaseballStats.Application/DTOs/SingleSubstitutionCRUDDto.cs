namespace BaseballStats.Application.DTOs;

public class SingleSubstitutionCRUDDto
{
    public long Id { get; set; }
    public long TeamId { get; set; }
    public long PlayerInId { get; set; }
    public long PlayerOutId { get; set; }
    public TimeSpan Time { get; set; }

}