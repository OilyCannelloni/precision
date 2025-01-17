namespace Precision.models.dto;

public class DealUpdateDto
{
    public Position ChangedPosition { get; set; }
    public string ChangedCard { get; set; } = string.Empty;
    public List<string> CurrentDealMiddle { get; set; } = [];
}