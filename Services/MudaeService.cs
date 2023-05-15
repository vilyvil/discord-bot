using Discord;
using Discord.WebSocket;
using Discord.Commands;

public class MudaeService
{
    public async Task LogResponseAsync(SocketCommandContext context) {
        string command = context.Message.Content.ToLower();
        IMessage? mudaeMessage = GetMudaeMessage(context);
        if (mudaeMessage == null) return;
        string response = mudaeMessage.Content.ToLower();

        
    }

    public IMessage? GetMudaeMessage(SocketCommandContext context) {
        IMessage[] messages = context.Channel.GetMessagesAsync(fromMessage: context.Message, dir: Direction.After, limit: 2)
                                                        .FlattenAsync()
                                                        .Result
                                                        .ToArray();

        foreach(IMessage message in messages) {
            if (message.Author.Id == 432610292342587392)
                return message;
        }
        return null;
    }

    public async Task getResponseAsync(string command) {
        
    }
}