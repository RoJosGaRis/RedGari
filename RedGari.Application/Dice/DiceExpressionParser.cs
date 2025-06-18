namespace RedGari.Application.Dice
{
    internal class DiceExpressionParser
    {
        
        public static IEnumerable<Token> ParseToRpn(IEnumerable<Token> tokens)
        {
            var result = new List<Token>();
            var operators = new Stack<Token>();

            foreach (var token in tokens)
            {
                switch (token.Type)
                {
                    case TokenType.Number or TokenType.Dice:
                        result.Add(token);
                        break;
                    case TokenType.LParen:
                        operators.Push(token);
                        break;
                    case TokenType.RParen:
                        while (operators.Peek().Type != TokenType.LParen && operators.Any())
                        {
                            result.Add(operators.Pop());
                        }
                        if(operators.Any()) operators.Pop();
                        break;
                    default:
                        while (
                            operators.Any()
                            &&
                            operators.Peek().Type != TokenType.LParen
                            &&
                            Utils.Priority(token.Type) >= Utils.Priority(operators.Peek().Type)
                            )
                        {
                            result.Add(operators.Pop());
                        }
                        operators.Push(token);
                        break;
                }
            }
            while(operators.Any())
            {
                result.Add(operators.Pop());
            }
            return result;
        }
    }
}
