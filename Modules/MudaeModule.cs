using Discord;
using Discord.Commands;
using Discord.WebSocket;

[Group("mudae")]
public class MudaeModule : ModuleBase<SocketCommandContext> {
    [Command("get")]
    public async Task GetMudaeResponseAsync(string cmdParam, [Remainder] string additionalParameters = "") {
        if (!additionalParameters.Equals("")) {
            await Context.Channel.SendMessageAsync("Sorry, this command only supports informational commands! (infokl, infokeys, etc)."
                                                    + " Please use the real Mudae bot instead.");
            return;
        }

    }

    [Command("copy")]
    public async Task CopyMudae(string link) {
        await Context.Message.DeleteAsync();
        ulong channelId = Convert.ToUInt64(link.Substring(49, 19));
        ulong messageId = Convert.ToUInt64(link.Substring(69, 19));

        IMessage mes = await Context.Guild.GetTextChannel(channelId).GetMessageAsync(messageId);

        EmbedBuilder? embed = mes.Embeds.First().ToEmbedBuilder();
        string content = ".";

        await Context.Channel.SendMessageAsync(content, embed: embed.Build());
    }

    /*[Command("correct")]
    public async Task LogMudaeResponseAsync(ulong messageId) {
        await Context.Message.DeleteAsync();

        IMessage correctedMessage = await Context.Channel.GetMessageAsync(messageId);
        IReadOnlyCollection<IEmbed> embeds = toSave.Embeds;
        IEmbed? embed = (embeds.Count != 0) ? embeds.First() : null;
        
        string content = toSave.CleanContent;
        string description = (embed != null) ? embed.Description : "";
        EmbedBuilder? eb = toSave.Embeds.First().ToEmbedBuilder();
        if (eb == null) return;

        eb.Color = new Color(051, 047, 044);
        //CleanContent
        //Description

        await Context.Channel.SendMessageAsync(content + description);
    }*/

    [Command("help")]
    public async Task MudaeHelpAsync() {
        // get all saved commands and share them
        await Context.Channel.SendMessageAsync("shut up you idiot");
    }
}