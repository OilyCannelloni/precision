using NUnit.Framework;
using Precision.deals;
using Precision.game;
using Precision.game.dds;
using Precision.game.elements.deal;
using Swan;
using Contract = Precision.game.elements.deal.Contract;

namespace PrecisionTests.cppdll;

public class TestSolveBoard
{
    [Test]
    public void Test_SolveBoard()
    {
        var game = new Game("xxx", new DealBox(3, new DealService().GetRandomDeal()));

        var ddsDeal = game.ToDdsDeal();
        Console.WriteLine(game.CurrentDealState.ToString());
        Console.WriteLine(ddsDeal.Stringify());

        var ddsFutureTricks = new DdsFutureTricks();
        DdsWrapper.SolveBoard(ref ddsDeal, -1, 2, 0, ref ddsFutureTricks, 0);
        
        Console.WriteLine(ddsFutureTricks.Stringify());
    }

    [Test]
    public void Test_SolveBoardService()
    {
        var game = new Game("xxx", new DealBox(4, new DealService().GetRandomDeal()));
        game.Contract = new Contract("6d");

        var cards = new DdsService().SolveCurrentGameState(game).ToList();
        Console.WriteLine(game.CurrentDealState);
        Console.WriteLine(string.Join(" ", cards));
    }

    [Test]
    public void Test_SolveBoardService_PlayedCard()
    {
        var game = new Game("xxx", new DealBox(4, new DealService().GetRandomDeal()));
        game.Contract = new Contract("6d");
        
        var card = game.DealerHand().GetRandomCard();
        game.PlayCard(card);
        Console.WriteLine(card);
        Console.WriteLine(game.CurrentDealState);
        
        var cards = new DdsService().SolveCurrentGameState(game).ToList();
        Console.WriteLine(game.CurrentDealState);
        Console.WriteLine(string.Join(" ", cards));
    }
}