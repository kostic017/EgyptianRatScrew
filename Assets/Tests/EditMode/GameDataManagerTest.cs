using NUnit.Framework;

public class GameDataManagerTest
{
    [Test]
    public void GetSlapCombination_Double()
    {
        var gameDataManager = new GameDataManager();
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Spades, Rank.Ace));
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Clubs, Rank.Ace));
        Assert.AreEqual(SlapCombination.Double, gameDataManager.GetSlapCombination());
    }

    [Test]
    public void GetSlapCombination_Marriage_1()
    {
        var gameDataManager = new GameDataManager();
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Spades, Rank.Queen));
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Clubs, Rank.King));
        Assert.AreEqual(SlapCombination.Marriage, gameDataManager.GetSlapCombination());
    }

    [Test]
    public void GetSlapCombination_Marriage_2()
    {
        var gameDataManager = new GameDataManager();
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Spades, Rank.King));
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Clubs, Rank.Queen));
        Assert.AreEqual(SlapCombination.Marriage, gameDataManager.GetSlapCombination());
    }

    [Test]
    public void GetSlapCombination_Sandwich()
    {
        var gameDataManager = new GameDataManager();
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Spades, Rank.Ace));
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Spades, Rank.Two));
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Clubs, Rank.Ace));
        Assert.AreEqual(SlapCombination.Sandwich, gameDataManager.GetSlapCombination());
    }

    [Test]
    public void GetSlapCombination_Divorce_1()
    {
        var gameDataManager = new GameDataManager();
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Spades, Rank.King));
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Spades, Rank.Ace));
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Clubs, Rank.Queen));
        Assert.AreEqual(SlapCombination.Divorce, gameDataManager.GetSlapCombination());
    }

    [Test]
    public void GetSlapCombination_Divorce_2()
    {
        var gameDataManager = new GameDataManager();
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Clubs, Rank.Queen));
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Spades, Rank.Ace));
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Spades, Rank.King));
        Assert.AreEqual(SlapCombination.Divorce, gameDataManager.GetSlapCombination());
    }

    [Test]
    public void GetSlapCombination_ThreeInRow_1()
    {
        var gameDataManager = new GameDataManager();
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Spades, Rank.Ace));
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Clubs, Rank.Two));
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Diamonds, Rank.Three));
        Assert.AreEqual(SlapCombination.ThreeInRow, gameDataManager.GetSlapCombination());
    }

    [Test]
    public void GetSlapCombination_ThreeInRow_2()
    {
        var gameDataManager = new GameDataManager();
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Clubs, Rank.Three));
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Diamonds, Rank.Two));
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Hearts, Rank.Ace));
        Assert.AreEqual(SlapCombination.ThreeInRow, gameDataManager.GetSlapCombination());
    }

    [Test]
    public void GetSlapCombination_ThreeInRow_3()
    {
        var gameDataManager = new GameDataManager();
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Clubs, Rank.King));
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Diamonds, Rank.Ace));
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Hearts, Rank.Two));
        Assert.AreEqual(SlapCombination.ThreeInRow, gameDataManager.GetSlapCombination());
    }

    [Test]
    public void GetSlapCombination_ThreeInRow_4()
    {
        var gameDataManager = new GameDataManager();
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Clubs, Rank.Two));
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Diamonds, Rank.Ace));
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Hearts, Rank.King));
        Assert.AreEqual(SlapCombination.ThreeInRow, gameDataManager.GetSlapCombination());
    }

    [Test]
    public void GetSlapCombination_None()
    {
        var gameDataManager = new GameDataManager();
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Spades, Rank.Ace));
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Spades, Rank.Two));
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Spades, Rank.Four));
        Assert.AreEqual(SlapCombination.None, gameDataManager.GetSlapCombination());
    }
}
