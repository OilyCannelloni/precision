using Precision.algorithm;
using Precision.game.dds;
using Precision.game.elements.cards;
using Precision.game.elements.deal;


namespace Precision.game.players;

public class BotStrategy(Position position)
{
    private DdsService _ddsService = new();
    private Position _position = position;

    private List<BotStrategyFilterAction> _filterPipeline = [];
    private BotStrategyPickAction _pickAction = BotStrategyPickAction.Random;

    public BotStrategy SetPickAction(BotStrategyPickAction action)
    {
        _pickAction = action;
        return this;
    }

    public BotStrategy AddFilter(BotStrategyFilterAction action)
    {
        _filterPipeline.Add(action);
        return this;
    }

    public Card PickCard(List<Card> cards, Game game)
    {
        foreach (var action in _filterPipeline)
        {
            cards = TakeFilterAction(cards, game, action);
        }
        return TakePickAction(cards, _pickAction);
    }
    
    private List<Card> FilterLegal(List<Card> cards, Game game)
    {
        var trickSuit = game.CurrentTrick.LeadCard()?.Suit;
        if (trickSuit == null)
            return cards;
        if (game.CurrentDealState[_position][(Suit)trickSuit].IsEmpty())
            return cards;
        return cards.Where(c => c.Suit == trickSuit).ToList();
    }
    
    private List<Card> FilterDoubleDummy(List<Card> cards, Game game)
    {
        var ddCards = _ddsService.SolveCurrentGameState(game).ToList();
        Console.WriteLine($"DD: {ddCards.Print()}");
        return ddCards;
    }

    private List<Card> FilterWinners(List<Card> cards, Game game)
    {
        var filteredCards = new List<Card>();
        foreach (var card in cards)
        {
            var lhoCards = game.CurrentDealState[_position.Next()];
            var rhoCards = game.CurrentDealState[_position.Previous()];
            var trumps = game.Contract?.Suit ?? throw new NullReferenceException("No contract");

            if (trumps != Suit.NT && (CanRuff(lhoCards, card.Suit) || CanRuff(rhoCards, card.Suit)))
                continue;
            
            // Winner is when neither opponent has a higher card (partner can have one)
            if (lhoCards[card.Suit].Value < card.IntValue && rhoCards[card.Suit].Value < card.IntValue)
            {
                filteredCards.Add(card);
            }
            continue;

            // If suit can be ruffed it contains no winners
            bool CanRuff(Hand opponent, Suit suit)
            {
                if (!opponent[suit].IsEmpty()) return false;
                return !opponent[trumps].IsEmpty();
            }
        }

        Console.WriteLine($"Winners: {filteredCards.Print()}");    
        
        // If there are no winners, return the previous card list
        if (filteredCards.Count == 0)
            return cards;
        return filteredCards;
    } 
    
    private Card LowestCard(List<Card> cards)
    {
        return cards.MinBy(c => c.IntValue) ?? throw new ArgumentException("Empty card sequence.");
    }

    private Card RandomCard(List<Card> cards)
    {
        return cards[Random.Shared.Next(cards.Count)];
    }

    private List<Card> TakeFilterAction(List<Card> cards, Game game, BotStrategyFilterAction action)
    {
        
        return action switch
        {
            BotStrategyFilterAction.DoubleDummy => FilterDoubleDummy(cards, game),
            BotStrategyFilterAction.CashWinners => FilterWinners(cards, game),
            BotStrategyFilterAction.Legal => FilterLegal(cards, game),
            _ => throw new ArgumentOutOfRangeException(nameof(action), action, null)
        };
    }

    private Card TakePickAction(List<Card> cards, BotStrategyPickAction action)
    {
        return action switch
        {
            BotStrategyPickAction.Lowest => LowestCard(cards),
            BotStrategyPickAction.Random => RandomCard(cards),
            _ => throw new ArgumentOutOfRangeException(nameof(action), action, null)
        };
    }
}