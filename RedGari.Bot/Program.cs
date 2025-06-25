using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RedGari.Application.Services;
using RedGari.Application.Interfaces;
using RedGari.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;



namespace RedGari.Bot
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            IHostBuilder builder = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((ctx, cfg) =>
                {
                    cfg.AddUserSecrets<Program>();
                });
            await builder.ConfigureServices((ctx,services) =>
            {
                services.AddDbContext<CharacterDbContext>(options =>
                    options.UseNpgsql(ctx.Configuration.GetConnectionString("Postgres")));
                services.Configure<Settings>(ctx.Configuration.GetSection("Discord"));
                //Console.WriteLine(ctx.Configuration.GetSection("Discord").ToString());
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