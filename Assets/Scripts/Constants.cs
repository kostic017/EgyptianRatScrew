using System.ComponentModel;

public static class Constants
{
    public const float ZOffset = 0.01f;
    public const string CardBack = "back_blue3";
}

public enum Player
{
    Player1,
    Player2,
};

public enum Suit
{
    [Description("spades")] Spades,
    [Description("clubs")] Clubs,
    [Description("diamonds")] Diamonds,
    [Description("hearts")] Hearts,
}

public enum Rank
{
    [Description("A")] Ace,
    [Description("2")] Two,
    [Description("3")] Three,
    [Description("4")] Four,
    [Description("5")] Five,
    [Description("6")] Six,
    [Description("7")] Seven,
    [Description("8")] Eight,
    [Description("9")] Nine,
    [Description("10")] Ten,
    [Description("J")] Jack,
    [Description("Q")] Queen,
    [Description("K")] King,
}

public enum SlapCombination
{
    [Description("Invalid slap")] None,
    [Description("Double")] Double,
    [Description("Marriage")] Marriage,
    [Description("Sandwich")] Sandwich,
    [Description("Top-Bottom")] TopBottom,
    [Description("Divorce")] Divorce,
    [Description("Four in a Row")] FourInRow
}