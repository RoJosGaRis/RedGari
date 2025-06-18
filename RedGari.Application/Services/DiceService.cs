using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using RedGari.Application.Dice;
using RedGari.Application.Interfaces;

namespace RedGari.Application.Services;

public class DiceService:IDiceService
{
    private readonly ILogger<DiceService> _logger;
    private static readonly Regex diceRegex = new(@"(\d+)d(\d+)(\s*[+-]\s*\d+)?");

    public DiceService(ILogger<DiceService> logger)
    {
        _logger = logger;
    }

    public string Roll(string input)
    {
        RollResult result = Evaluate.EvaluateRoll(input);

        return result.ToString();

    }
}
