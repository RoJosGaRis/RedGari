using System.Text.Json;

namespace RedGari.Application.Dice;
public record RollResult(double Total, IEnumerable<string> Steps)
{
    public override string ToString()
    {
        return $"{string.Join("\n", Steps)}\nTotal: {Total}";
    }
}

public enum TokenType
{
    Number,
    Dice,
    Plus,
    Minus,
    Star,
    Slash,
    LParen,
    RParen
}

public record Token(TokenType Type, string Text);
