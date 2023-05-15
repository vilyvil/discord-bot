using Discord;
using Discord.WebSocket;
using Discord.Commands;

public class MudaeCommandHandler {

    private readonly DiscordSocketClient _client;
    private readonly MudaeService _mudae;
    public MudaeCommandHandler(DiscordSocketClient client, MudaeService mudae)
    {
        _mudae = mudae;
        _client = client;
    }

    public Task LoadLogger() {

        _client.MessageReceived += HandleMCommandAsync;

        return Task.CompletedTask;
    }

    public async Task HandleMCommandAsync(SocketMessage messageParam) {

        //doesn't process message if it's a system message or is empty
        var message = messageParam as SocketUserMessage;
        if (message == null || message.Content == null) return; 

        //tracks where the prefix ends and where command begins
        int argPos = 0;

        //doesn't process message if it does not have mudae prefix or is sent by a bot
        if (!message.HasCharPrefix('$', ref argPos) || 
            message.Content.Length == 1 ||
            message.Author.IsBot)
            return;
        
        //add a check for if message has already been logged today


        var context = new SocketCommandContext(_client, message);

        await _mudae.LogResponseAsync(context);
        Console.WriteLine(context.Message.Content);
    }
}