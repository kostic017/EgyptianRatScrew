using NUnit.Framework;

public class DiscardPileTest
{
    [Test]
    public void GetSlapCombination_Double()
    {
        var discardPile = new DiscardPile();
        discardPile.AddCard(new CardValue(Suit.Spades, Rank.Ace));
        discardPile.AddCard(new CardValue(Suit.Clubs, Rank.Ace));
        Assert.AreEqual(SlapCombination.Double, discardPile.GetSlapCombination());
    }

    [Test]
    public void GetSlapCombination_Marriage_1()
    {
        var discardPile = new DiscardPile();
        discardPile.AddCard(new CardValue(Suit.Spades, Rank.Queen));
        discardPile.AddCard(new CardValue(Suit.Clubs, Rank.King));
        Assert.AreEqual(SlapCombination.Marriage, discardPile.GetSlapCombination());
    }

    [Test]
    public void GetSlapCombination_Marriage_2()
    {
        var discardPile = new DiscardPile();
        discardPile.AddCard(new CardValue(Suit.Spades, Rank.King));
        discardPile.AddCard(new CardValue(Suit.Clubs, Rank.Queen));
        Assert.AreEqual(SlapCombination.Marriage, discardPile.GetSlapCombination());
    }

    [Test]
    public void GetSlapCombination_Sandwich()
    {
        var discardPile = new DiscardPile();
        discardPile.AddCard(new CardValue(Suit.Spades, Rank.Ace));
        discardPile.AddCard(new CardValue(Suit.Spades, Rank.Two));
        discardPile.AddCard(new CardValue(Suit.Clubs, Rank.Ace));
        Assert.AreEqual(SlapCombination.Sandwich, discardPile.GetSlapCombination());
    }

    [Test]
    public void GetSlapCombination_Divorce_1()
    {
        var discardPile = new DiscardPile();
        discardPile.AddCard(new CardValue(Suit.Spades, Rank.King));
        discardPile.AddCard(new CardValue(Suit.Spades, Rank.Ace));
        discardPile.AddCard(new CardValue(Suit.Clubs, Rank.Queen));
        Assert.AreEqual(SlapCombination.Divorce, discardPile.GetSlapCombination());
    }

    [Test]
    public void GetSlapCombination_Divorce_2()
    {
        var discardPile = new DiscardPile();
        discardPile.AddCard(new CardValue(Suit.Clubs, Rank.Queen));
        discardPile.AddCard(new CardValue(Suit.Spades, Rank.Ace));
        discardPile.AddCard(new CardValue(Suit.Spades, Rank.King));
        Assert.AreEqual(SlapCombination.Divorce, discardPile.GetSlapCombination());
    }

    [Test]
    public void GetSlapCombination_ThreeInRow_1()
    {
        var discardPile = new DiscardPile();
        discardPile.AddCard(new CardValue(Suit.Spades, Rank.Ace));
        discardPile.AddCard(new CardValue(Suit.Clubs, Rank.Two));
        discardPile.AddCard(new CardValue(Suit.Diamonds, Rank.Three));
        Assert.AreEqual(SlapCombination.ThreeInRow, discardPile.GetSlapCombination());
    }

    [Test]
    public void GetSlapCombination_ThreeInRow_2()
    {
        var discardPile = new DiscardPile();
        discardPile.AddCard(new CardValue(Suit.Clubs, Rank.Three));
        discardPile.AddCard(new CardValue(Suit.Diamonds, Rank.Two));
        discardPile.AddCard(new CardValue(Suit.Hearts, Rank.Ace));
        Assert.AreEqual(SlapCombination.ThreeInRow, discardPile.GetSlapCombination());
    }

    [Test]
    public void GetSlapCombination_ThreeInRow_3()
    {
        var discardPile = new DiscardPile();
        discardPile.AddCard(new CardValue(Suit.Clubs, Rank.King));
        discardPile.AddCard(new CardValue(Suit.Diamonds, Rank.Ace));
        discardPile.AddCard(new CardValue(Suit.Hearts, Rank.Two));
        Assert.AreEqual(SlapCombination.ThreeInRow, discardPile.GetSlapCombination());
    }

    [Test]
    public void GetSlapCombination_ThreeInRow_4()
    {
        var discardPile = new DiscardPile();
        discardPile.AddCard(new CardValue(Suit.Clubs, Rank.Two));
        discardPile.AddCard(new CardValue(Suit.Diamonds, Rank.Ace));
        discardPile.AddCard(new CardValue(Suit.Hearts, Rank.King));
        Assert.AreEqual(SlapCombination.ThreeInRow, discardPile.GetSlapCombination());
    }

    [Test]
    public void GetSlapCombination_None()
    {
        var discardPile = new DiscardPile();
        discardPile.AddCard(new CardValue(Suit.Spades, Rank.Ace));
        discardPile.AddCard(new CardValue(Suit.Spades, Rank.Two));
        discardPile.AddCard(new CardValue(Suit.Spades, Rank.Four));
        Assert.AreEqual(SlapCombination.None, discardPile.GetSlapCombination());
    }
}
