using Discord;
using Discord.WebSocket;
using Discord.Commands;

public class MudaeService
{
    private readonly DiscordSocketClient _client;
    private readonly CommandService _commands;
    public MudaeService(DiscordSocketClient client, CommandService commands)
    {
        _commands = commands;
        _client = client;
    }

    public Task LoadLogger() {

        _client.MessageReceived += LogMudaeResponse;

        return Task.CompletedTask;
    }

    public Task LogMudaeResponse(SocketMessage messageParam) {

        //doesn't process command if it's a system message or is empty
        var message = messageParam as SocketUserMessage;
        if (message == null || message.Content == null) return Task.CompletedTask;

        //tracks where the prefix ends and where command begins
        int argPos = 0;

        //doesn't process command if prefix doesn't match, user doesn't match (has to be me), or if the user is a bot.
        if (!message.HasCharPrefix('$', ref argPos) || 
            message.Author.Id != 283810729033990147 ||
            message.Author.IsBot)
            return Task.CompletedTask;
        
        return Task.CompletedTask;
    }
}