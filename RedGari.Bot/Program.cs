using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RedGari.Application.Services;
using RedGari.Application.Interfaces;



namespace RedGari.Bot
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            IHostBuilder builder = Host.CreateDefaultBuilder(args);
            await builder.ConfigureServices((ctx,services) =>
            {
                services.Configure<Settings>(ctx.Configuration.GetSection("Discord"));
                services.AddHostedService<BotService>();
                services.AddSingleton<DiscordSocketClient>();
                services.AddSingleton<CommandService>();
                services.AddSingleton<CommandHandler>();
                services.AddSingleton<IDiceService, DiceService>();
            }).
            RunConsoleAsync();
        }
    }
}