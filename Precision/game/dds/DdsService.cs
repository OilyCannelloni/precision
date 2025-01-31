using Precision.game.elements.cards;
using Precision.game.elements.deal;
using Swan;

namespace Precision.game.dds;

public class DdsService
{
    public IEnumerable<Card> SolveCurrentGameState(Game game)
    {
        var ddsDeal = game.ToDdsDeal();
        var futureTricks = new DdsFutureTricks();
        
        Console.WriteLine(ddsDeal.Stringify());

        try
        {
            var error = DdsWrapper.SolveBoard(ref ddsDeal, -1, 2, 0, ref futureTricks, 0);
            if (error < 0)
            {
                Console.WriteLine(error);
            }
        }
        catch
        {
            
        }
        
        

        
        
        foreach (var card in futureTricks.ToCards())
        {
            yield return card;
        }
    }
}