namespace TrackWeight.Api.Models;

public class CalorieRecord
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public int Calories { get; set; }
    public DateTime CreatedAt { get; set; }
}
