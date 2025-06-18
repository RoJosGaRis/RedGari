using Discord.Commands;
using RedGari.Application.Interfaces;

namespace RedGari.Bot.Modules
{
    public class RollModule : ModuleBase<SocketCommandContext>
    {
        private readonly IDiceService _dice;

        public RollModule(IDiceService dice)
        {
            _dice = dice;
        }

        [Command("roll")]
        [Alias("r")]
        [Summary("Roll dice, e.g. 3d6+2 or 1d20*(4-1)")]
        public Task RollAsync([Remainder] string expression)
        {
            var result = _dice.Roll(expression);
            return ReplyAsync(result);
        }
    }
}
