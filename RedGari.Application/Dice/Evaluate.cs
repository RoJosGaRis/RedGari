using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RedGari.Application.Interfaces;
using RedGari.Application.Dice;

namespace RedGari.Application.Dice
{
    internal class Evaluate
    {
        public static RollResult EvaluateRoll(string expression)
        {
            var values = new Stack<double>();
            var details = new List<string>();

            var lexedResults = DiceExpressionLexer.Lex(expression);
            IEnumerable<Token> rollResults = DiceExpressionParser.ParseToRpn(lexedResults);

            foreach (var token in rollResults)
            {
                switch (token.Type)
                {
                    case TokenType.Number:
                        values.Push(Double.Parse(token.Text));
                        break;
                    case TokenType.Dice:
                        (int count, int sides) = Utils.ParseDice(token.Text);
                        IEnumerable<int> result = Utils.GetDiceResult(count, sides);
                        values.Push(result.Sum());
                        details.Add($"{token.Text} -> [{string.Join(", ", result)}]");
                        break;
                    default:
                        double b = values.Pop();
                        double a = values.Pop();
                        double ans = token.Type switch
                        {
                            TokenType.Plus => a + b,
                            TokenType.Minus => a - b,
                            TokenType.Star => a * b,
                            TokenType.Slash => a / b,
                            _ => throw new NotSupportedException()
                        };
                        details.Add($"{a}{token.Text}{b} = {ans}");
                        values.Push(ans);
                        break;
                }
            }

            double total = values.Single();
            return new(total, details);

        }
    }
}
