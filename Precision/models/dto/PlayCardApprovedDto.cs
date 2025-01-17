namespace Precision.models.dto;

public class PlayCardApprovedDto
{
    public required Position ChangedPosition { get; set; }
    public required string PlayedCard { get; set; }
    public required Trick CurrentTrick { get; set; }
    public required Position ActionPlayer { get; set; }
}