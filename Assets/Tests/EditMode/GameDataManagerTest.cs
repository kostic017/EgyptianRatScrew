using NUnit.Framework;

public class GameDataManagerTest
{
    [Test]
    public void IsSlapValid_Double()
    {
        var gameDataManager = new GameDataManager();
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Spades, Rank.Ace));
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Clubs, Rank.Ace));
        Assert.True(gameDataManager.IsSlapValid());
    }

    [Test]
    public void IsSlapValid_Marriage_1()
    {
        var gameDataManager = new GameDataManager();
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Spades, Rank.Queen));
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Clubs, Rank.King));
        Assert.True(gameDataManager.IsSlapValid());
    }

    [Test]
    public void IsSlapValid_Marriage_2()
    {
        var gameDataManager = new GameDataManager();
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Spades, Rank.King));
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Clubs, Rank.Queen));
        Assert.True(gameDataManager.IsSlapValid());
    }

    [Test]
    public void IsSlapValid_Sandwich()
    {
        var gameDataManager = new GameDataManager();
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Spades, Rank.Ace));
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Spades, Rank.Two));
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Clubs, Rank.Ace));
        Assert.True(gameDataManager.IsSlapValid());
    }

    [Test]
    public void IsSlapValid_TopBottom()
    {
        var gameDataManager = new GameDataManager();
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Spades, Rank.Ace));
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Spades, Rank.Two));
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Spades, Rank.Three));
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Clubs, Rank.Ace));
        Assert.True(gameDataManager.IsSlapValid());
    }

    [Test]
    public void IsSlapValid_Divorce_1()
    {
        var gameDataManager = new GameDataManager();
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Spades, Rank.King));
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Spades, Rank.Ace));
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Clubs, Rank.Queen));
        Assert.True(gameDataManager.IsSlapValid());
    }

    [Test]
    public void IsSlapValid_Divorce_2()
    {
        var gameDataManager = new GameDataManager();
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Clubs, Rank.Queen));
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Spades, Rank.Ace));
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Spades, Rank.King));
        Assert.True(gameDataManager.IsSlapValid());
    }

    [Test]
    public void IsSlapValid_FourInRow_1()
    {
        var gameDataManager = new GameDataManager();
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Spades, Rank.Ace));
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Clubs, Rank.Two));
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Diamonds, Rank.Three));
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Hearts, Rank.Four));
        Assert.True(gameDataManager.IsSlapValid());
    }

    [Test]
    public void IsSlapValid_FourInRow_2()
    {
        var gameDataManager = new GameDataManager();
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Spades, Rank.Four));
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Clubs, Rank.Three));
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Diamonds, Rank.Two));
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Hearts, Rank.Ace));
        Assert.True(gameDataManager.IsSlapValid());
    }

    [Test]
    public void IsSlapValid_FourInRow_3()
    {
        var gameDataManager = new GameDataManager();
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Spades, Rank.King));
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Clubs, Rank.Ace));
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Diamonds, Rank.Two));
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Hearts, Rank.Three));
        Assert.True(gameDataManager.IsSlapValid());
    }

    [Test]
    public void IsSlapValid_FourInRow_4()
    {
        var gameDataManager = new GameDataManager();
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Spades, Rank.Three));
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Clubs, Rank.Two));
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Diamonds, Rank.Ace));
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Hearts, Rank.King));
        Assert.True(gameDataManager.IsSlapValid());
    }

    [Test]
    public void IsSlapValid_Invalid()
    {
        var gameDataManager = new GameDataManager();
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Spades, Rank.Ace));
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Clubs, Rank.Two));
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Diamonds, Rank.Three));
        gameDataManager.PutCardInDiscardPile(new CardValue(Suit.Hearts, Rank.Five));
        Assert.False(gameDataManager.IsSlapValid());
    }
}
