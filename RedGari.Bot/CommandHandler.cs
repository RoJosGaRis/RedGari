using System.Reflection;
using Discord.API;
using Discord.Commands;
using Discord.WebSocket;

namespace RedGari.Bot
{
    public sealed class CommandHandler
    {
        private readonly DiscordSocketClient    _client;
        private readonly CommandService         _commandService;
        private readonly IServiceProvider       _serviceProvider;

        public CommandHandler (
            DiscordSocketClient client,
            CommandService commandService,
            IServiceProvider serviceProvider
            )
        {
            _client = client;
            _commandService = commandService;
            _serviceProvider = serviceProvider;

            _client.MessageReceived += HandleMessageAsync;

        }

        public async Task InitializeAsync()
        {
            await _commandService.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: _serviceProvider);
        }

        private async Task HandleMessageAsync(SocketMessage msg)
        {
            var message = msg as SocketUserMessage;
            if (message == null) return;

            int argPos = 0;

            if(
                !(
                message.HasCharPrefix('!', ref argPos) 
                || message.HasMentionPrefix(_client.CurrentUser, ref argPos)
                ) 
                || message.Author.IsBot
              )
            {
                return;
            }


            var context = new SocketCommandContext(_client, message);

            await _commandService.ExecuteAsync(
                context:context,
                argPos:argPos,
                services:_serviceProvider);


        }
    }
}
