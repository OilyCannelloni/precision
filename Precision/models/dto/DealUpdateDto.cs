using Precision.game.elements.cards;
using Precision.game.elements.deal;

namespace Precision.models.dto;

public class DealUpdateDto
{
    public string GameId { get; set; } = string.Empty;
    public required Position ChangedPosition { get; set; }
    public required Card PlayedCard { get; set; }
    public required Trick CurrentTrick { get; set; }
    public required Position ActionPlayer { get; set; }
}