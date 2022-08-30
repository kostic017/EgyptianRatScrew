using System.Collections.Generic;

public class Player
{
    private readonly Queue<CardValue> cards = new();
    
    public string Name { get; }
    public int CardCount => cards.Count;

    public Player(string name)
    {
        Name = name;
    }

    public void AddCard(CardValue card)
    {
        cards.Enqueue(card);
    }

    public CardValue GetCard()
    {
        return cards.Dequeue();
    }
}