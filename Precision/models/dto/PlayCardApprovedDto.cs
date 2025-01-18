namespace Precision.models.dto;

public class PlayCardApprovedDto
{
    public required Position ChangedPosition { get; set; }
    public required Card PlayedCard { get; set; }
    public required Trick CurrentTrick { get; set; }
    public required Position ActionPlayer { get; set; }
}