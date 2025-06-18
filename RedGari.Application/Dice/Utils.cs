namespace RedGari.Application.Dice;

public class Utils
{
    public static (int count, int sides) ParseDice(string exp)
    {
        var parts = exp.Split("d");
        return (int.Parse(parts[0]), int.Parse(parts[1]));
    }

    public static IEnumerable<int> GetDiceResult(int count, int sides)
    {
        int[] result = new int[count];
        for(int i = 0; i < count; i++)
        {
            result[i] = Random.Shared.Next(1, sides+1);
        }
        return result;
    }

    public static int Priority(TokenType type)
    {
        switch (type)
        {
            case TokenType.Star or TokenType.Slash:
                return 1;
            case TokenType.Plus or TokenType.Minus:
                return 2;
            default:
                return 3;
        }
    }

}