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

    public CardValue DrawCardFromPlayer(Player player)
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

    public bool IsSlapValid()
    {
        if (discardPile.Count >= 2)
        {
            Rank r1 = discardPile[^1].Rank;
            Rank r2 = discardPile[^2].Rank;

            // Double
            if (r1 == r2)
                return true;

            // Marriage
            if ((r1 == Rank.King && r2 == Rank.Queen) || (r1 == Rank.Queen && r2 == Rank.King))
                return true;
        }

        if (discardPile.Count >= 3)
        {
            Rank r1 = discardPile[^1].Rank;
            Rank r3 = discardPile[^3].Rank;
            Rank rb = discardPile[0].Rank;

            // Sandwich
            if (r1 == r3)
                return true;

            // Top-bottom
            if (r1 == rb)
                return true;

            // Divorce
            if ((r1 == Rank.King && r3 == Rank.Queen) || (r1 == Rank.Queen && r3 == Rank.King))
                return true;
        }

        if (discardPile.Count >= 4)
        {
            Rank r1 = discardPile[^1].Rank;
            Rank r2 = discardPile[^2].Rank;
            Rank r3 = discardPile[^3].Rank;
            Rank r4 = discardPile[^4].Rank;

            // Four in a row
            if (r1 == r2.Increment(1) && r1 == r3.Increment(2) && r1 == r4.Increment(3))
                return true;
            if (r1 == r2.Increment(-1) && r1 == r3.Increment(-2) && r1 == r4.Increment(-3))
                return true;
        }

        return false;
    }
}