using System.Text.RegularExpressions;

namespace RedGari.Application.Dice;

internal class DiceExpressionLexer
{
    public static IEnumerable<Token> Lex(string input)
    {
        var diceRegex = new Regex(@"\s*(\d+d\d+|\d+|[()+\-*\/])", RegexOptions.Compiled);

        foreach (Match match in diceRegex.Matches(input))
        {
            string value = match.Groups[1].Value;
            yield return value switch
            {
                "+" => new(TokenType.Plus, value),
                "-" => new(TokenType.Minus, value),
                "*" => new(TokenType.Star, value),
                "/" => new(TokenType.Slash, value),
                "(" => new(TokenType.LParen, value),
                ")" => new(TokenType.RParen, value),
                _ when value.Contains("d") => new(TokenType.Dice, value),
                _ => new(TokenType.Number, value)
            };
        }
    }
}