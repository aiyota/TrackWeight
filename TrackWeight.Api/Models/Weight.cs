namespace TrackWeight.Api.Models;

public class Weight
{
    public int Id { get; set; }
    public string UserId { get; set; } = default!;
    public double Amount { get; set; }
    public DateTime CreatedAt { get; set; }
}
