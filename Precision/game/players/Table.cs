using Precision.models.dto;

namespace Precision.game.players;

public class Table
{
    private List<Player> _players = [];
    private List<Player> _humanPlayers = [];

    public void AddPlayer(Player player)
    {
        if (player is HumanPlayer)
            _humanPlayers.Add(player);
        else
            _players.Add(player);

    }

    public void RemovePlayer(Player player)
    {
        if (player is HumanPlayer)
            _humanPlayers.Remove(player);
        else
            _players.Remove(player);

    }

    public void DispatchDealUpdate(DealUpdateDto dealUpdate)
    {
        foreach (var player in _humanPlayers)
        {
            player.OnDealUpdate(dealUpdate);
        }

        foreach (var player in _players)
        {
            player.OnDealUpdate(dealUpdate);
        }
    }
}