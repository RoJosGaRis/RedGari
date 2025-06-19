using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace RedGari.Tests.Bot;

[Collection("Discord integration")] // isolates rate-limit pressure
public sealed class DiscordConnectionTests : IAsyncLifetime
{
    private readonly DiscordSocketClient _client;
    private readonly string? _token;
    public DiscordConnectionTests()
    {
        // The bare minimum gateway intents for a “ready” handshake.
        var cfg = new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.Guilds
        };
        _client = new DiscordSocketClient(cfg);

        // Configuration pipeline identical to app:
        _token = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .AddUserSecrets<DiscordConnectionTests>()
            .Build()["Discord:Token"];
    }

    [Fact(DisplayName = "Bot logs in & reaches READY")]
    [Trait("Category", "Integration")]
    public async Task Connects_to_Discord_and_is_ready()
    {
        Skip.If(string.IsNullOrWhiteSpace(_token),
            "Discord token not configured; skipping integration test");

        var ready = new TaskCompletionSource();

        _client.Ready += () =>
        {
            ready.TrySetResult();
            return Task.CompletedTask;
        };

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(15));

        await _client.LoginAsync(TokenType.Bot, _token!, true);
        await _client.StartAsync();

        // ─── Wait for Ready or timeout ──────────────────
        var completed = await Task.WhenAny(ready.Task, Task.Delay(-1, cts.Token));
        Assert.True(ready.Task.IsCompleted, "Client never reached READY state");

        await _client.StopAsync();
    }

    // xUnit IAsyncLifetime
    public Task InitializeAsync() => Task.CompletedTask;
    public async Task DisposeAsync() => await _client.DisposeAsync();
}