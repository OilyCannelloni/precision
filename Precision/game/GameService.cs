using Precision.game.elements.deal;

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
}