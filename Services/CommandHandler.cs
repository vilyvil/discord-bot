using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System.Reflection;

public class CommandHandler {

    private readonly DiscordSocketClient _client;
    private readonly CommandService _commands;

    public CommandHandler(DiscordSocketClient client, CommandService commands)
    {
        _commands = commands;
        _client = client;
    }

    public async Task LoadCommandsAsync()
    {
        //hooks MessageReceived event to command handler
        _client.MessageReceived += HandleCommandAsync;

        //finds command modules in the program and loads them
        //since a dependency injector is not being used, passes services as null
        await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), 
                                        services: null);
    }

    private async Task HandleCommandAsync(SocketMessage messageParam)
    {
        //doesn't process command if it's a system message or is empty
        var message = messageParam as SocketUserMessage;
        if (message == null || message.Content == null) return;

        //tracks where the prefix ends and where command begins
        int argPos = 0;

        //doesn't process command if prefix doesn't match, user doesn't match (has to be me), or if the user is a bot.
        if (!message.HasCharPrefix('p', ref argPos) || 
            message.Author.Id != 283810729033990147 ||
            message.Author.IsBot)
            return;

        //creates a WebSocket-based command context with the message, allowing commands access to message content, channel, sender, etc.
        var context = new SocketCommandContext(_client, message);

        //executes the command with command context
        await _commands.ExecuteAsync(
            context: context, 
            argPos: argPos,
            services: null);
    }
}