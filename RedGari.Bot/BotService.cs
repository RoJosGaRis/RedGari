using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RedGari.Application.Interfaces;
using RedGari.Application.Services;

namespace RedGari.Bot
{
    public class BotService:BackgroundService
    {
        private readonly DiscordSocketClient _client;
        private readonly ILogger _logger;
        private readonly CommandHandler _handler;

        private readonly string _token;

        public BotService(DiscordSocketClient client, ILogger<BotService> logger, CommandHandler handler, IOptions<Settings> settings)
        {
            _client = client;
            _logger = logger;
            _handler = handler;
            _token = settings.Value.Token ?? 
                throw new InvalidOperationException("Discord token missing in configuration");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _client.Log += log =>
            {
                _logger.LogInformation(log.ToString());
                return Task.CompletedTask;
            };

            await _client.LoginAsync(TokenType.Bot, _token);
            await _client.StartAsync();
            await _handler.InitializeAsync();


            await Task.Delay(-1, stoppingToken);
        }

       
    }
}
