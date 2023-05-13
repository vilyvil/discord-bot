using Discord;
using Discord.Commands;
using Discord.WebSocket;

[Group("mudae")]
public class MudaeModule : ModuleBase<SocketCommandContext> {
    [Command]
    public async Task getMudaeResponse([Remainder] string cmdParam) {
        
    }

    [Command("add")]
    public async Task addMudaeResponse() {
        
    }
}