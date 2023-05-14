using Discord;
using Discord.Commands;
using Discord.WebSocket;

[Group("mudae")]
public class MudaeModule : ModuleBase<SocketCommandContext> {
    [Command]
    public async Task getMudaeResponse(string cmdParam, [Remainder] string additionalParameters) {
        if (!additionalParameters.Equals("")) {
            await Context.Channel.SendMessageAsync("Sorry, this command only supports informational commands! (infokl, infokeys, etc)."
                                                    + " Please use the real Mudae bot instead.");
            return;
        }

        
    }

    // TODO FIGURE OUT WHAT TO DO WITH THIS
    [Command("add")]
    public async Task addMudaeResponse() {
        
    }
}