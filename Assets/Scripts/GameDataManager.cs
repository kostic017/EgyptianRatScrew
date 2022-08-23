using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class GameDataManager
{
    private readonly Queue<CardValue> player1Cards = new();
    private readonly Queue<CardValue> player2Cards = new();

    public void DealCards()
    {
        List<CardValue> deck = new();
        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                deck.Add(new CardValue(suit, rank));

        for (int n = deck.Count - 1; n > 0; --n)
        {
            int k = Random.Range(0, n + 1);
            (deck[k], deck[n]) = (deck[n], deck[k]);
        }

        bool flag = true;
        foreach (var card in deck)
        {
            if (flag)
                player1Cards.Enqueue(card);
            else
                player2Cards.Enqueue(card);
            flag = !flag;
        }
    }

    public CardValue DrawCard(Player player)
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

    public int GetCardCount(Player player)
    {
        if (player == Player.Player1)
            return player1Cards.Count;
        return player2Cards.Count;
    }
}