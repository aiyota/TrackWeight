namespace TrackWeight.Api.Models;

public class WeightRecord
{
    public int Id { get; set; }
    public Guid UserId { get; set; } = default!;
    public double Weight { get; set; }
    public DateTime CreatedAt { get; set; }
}
