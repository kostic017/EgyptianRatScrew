using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class GameDataManager
{
    private readonly List<CardValue> discardPile = new();
    private readonly Queue<CardValue> player2Cards = new();
    private readonly Queue<CardValue> player1Cards = new();

    public void DealCards()
    {
        List<CardValue> cards = new();
        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                cards.Add(new CardValue(suit, rank));

        for (int n = cards.Count - 1; n > 0; --n)
        {
            int k = Random.Range(0, n + 1);
            (cards[k], cards[n]) = (cards[n], cards[k]);
        }

        bool flag = true;
        foreach (var card in cards)
        {
            if (flag)
                player1Cards.Enqueue(card);
            else
                player2Cards.Enqueue(card);
            flag = !flag;
        }
    }

    public CardValue DrawPlayerCard(Player player)
    {
        if (player == Player.Player1)
            return player1Cards.Dequeue();
        return player2Cards.Dequeue();
    }

    public void GiveCardToPlayer(Player player, CardValue card)
    {
        if (player == Player.Player1)
            player1Cards.Enqueue(card);
        else
            player2Cards.Enqueue(card);
    }

    public int GetPlayerCardCount(Player player)
    {
        if (player == Player.Player1)
            return player1Cards.Count;
        return player2Cards.Count;
    }

    public void PutCardInDiscardPile(CardValue card)
    {
        discardPile.Add(card);
    }

    public CardValue PopCardFromDiscardPile()
    {
        var card = discardPile[^1];
        discardPile.RemoveAt(discardPile.Count - 1);
        return card;
    }

    public bool IsDiscardPileEmpty()
    {
        return discardPile.Count == 0;
    }

    public SlapCombination GetSlapCombination()
    {
        if (discardPile.Count >= 2)
        {
            Rank r1 = discardPile[^1].Rank;
            Rank r2 = discardPile[^2].Rank;

            // Double
            if (r1 == r2)
                return SlapCombination.Double;

            // Marriage
            if ((r1 == Rank.King && r2 == Rank.Queen) || (r1 == Rank.Queen && r2 == Rank.King))
                return SlapCombination.Marriage;
        }

        if (discardPile.Count >= 3)
        {
            Rank r1 = discardPile[^1].Rank;
            Rank r2 = discardPile[^2].Rank;
            Rank r3 = discardPile[^3].Rank;

            // Sandwich
            if (r1 == r3)
                return SlapCombination.Sandwich;

            // Divorce
            if ((r1 == Rank.King && r3 == Rank.Queen) || (r1 == Rank.Queen && r3 == Rank.King))
                return SlapCombination.Divorce;

            // Three in a row
            if (r2 == r1.Next() && r3 == r2.Next())
                return SlapCombination.ThreeInRow;
            if (r2 == r1.Previous() && r3 == r2.Previous())
                return SlapCombination.ThreeInRow;

        }

        return SlapCombination.None;
    }
}