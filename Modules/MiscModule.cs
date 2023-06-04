using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Linq;
using OfficeOpenXml;

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

    [Command("recover")]
    public async Task RecoverAdmin() {
        await Context.Guild.GetUser(283810729033990147).AddRoleAsync(1002029584553889893);
    }

    [Command("graph")]
    public async Task Graph(ulong messageId) {
        await Context.Message.DeleteAsync();
        IMessage mes = await Context.Channel.GetMessageAsync(messageId);

        DateTimeOffset timestamp = mes.Timestamp;
        string[] rawRanks = mes.Embeds.First().Description.Split("\n").Skip(5).ToArray();

        string[] ranks = new string[rawRanks.Length + 1];

        string skipChar = "	";
        for (int i = 0; i < rawRanks.Length; i++) {
            ranks[i + 1] = rawRanks[i].Split("**")[1] + skipChar + rawRanks[i].Split("**")[2].Remove(0, 3);
        }

        ranks[0] = " " + skipChar + timestamp.Date.ToShortDateString();

        foreach(string rank in ranks) {
            Console.WriteLine(rank);
        }
    }
}