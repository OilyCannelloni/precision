using Precision.game.elements.deal;

namespace Precision.models.dto;

public class NewGameDto
{
    public string GameId { get; set; } = string.Empty;
    public DealBox DealBox { get; set; } = null!;

    public Trick CurrentTrick { get; set; } = null!;
}