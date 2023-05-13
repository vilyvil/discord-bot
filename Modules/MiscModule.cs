using Discord;
using Discord.Commands;
using Discord.WebSocket;

public class MiscModule : ModuleBase<SocketCommandContext> {
    [Command("clean")] 
    public async Task CleanAsync([Remainder] string deleteParam) {
        await Context.Message.DeleteAsync();

        IEnumerable<IMessage> messages = Context.Channel.GetMessagesAsync(250).FlattenAsync().Result;

        messages = messages.Where(x => x.Content == deleteParam);

        await (Context.Channel as ITextChannel)!.DeleteMessagesAsync(messages);
    }

    [Command("say")] 
    public async Task SendMessageAsync([Remainder] string mes) {
        await Context.Channel.SendMessageAsync(mes);
    }

    [Command("mudae")]
    public async Task mudae(string cmdString) {

    }
}