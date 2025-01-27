using Precision.game.elements.cards;
using Precision.game.elements.deal;
using Precision.models.dto;

namespace Precision.game;

public class GameService
{
    private readonly Dictionary<string, Game> _games = new();

    public string CreateGame(DealBox box)
    {
        var id = Guid.NewGuid().ToString();
        _games[id] = new Game(box);
        return id;
    }

    public Game GetGame(string id)
    {
        return _games[id];
    }

    public void RemoveGame(string id)
    {
        _games.Remove(id);
    }

    public DealUpdateDto? OnCardPlayRequest(string gameId, Card card)
    {
        var game = GetGame(gameId);
        return game.PlayCard(card);
    }
}