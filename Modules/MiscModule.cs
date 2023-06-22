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
        await Context.Message.DeleteAsync();
        await Context.Guild.GetUser(283810729033990147).AddRoleAsync(1105692660905287790);
        await Context.Guild.GetUser(283810729033990147).AddRoleAsync(1098799457706987610);
    }

    [Command("graph")]
    public async Task Graph(ulong messageId, string hasIds) {
        await Context.Message.DeleteAsync();
        IMessage mes = await Context.Channel.GetMessageAsync(messageId);

        string timestamp = mes.Timestamp.ToString("yyyy-MM-dd");
        string[] rawRanks = mes.Embeds.First().Description
                                                .Split("\n")
                                                .Skip(5)
                                                .ToArray();

        string[] names = new string[rawRanks.Length + 1];
        string[] kakera = new string[rawRanks.Length + 1];

        for (int i = 0; i < rawRanks.Length; i++) {
            names[i] = rawRanks[i].Split("**")[1];
            kakera[i] = rawRanks[i].Split("**")[2].Remove(0, 3);
        }

        //Open the workbook (or create it if it doesn't exist)
	    using (var p = new ExcelPackage(@"test.xlsx"))
        {
            var ws = p.Workbook.Worksheets.Add("KakeraSheet");
            //The style object is used to access most cells formatting and styles.
            ws.Cells[1, 1, 20, 1].LoadFromCollection(names);
            ws.Cells[1, 2, 20, 1].LoadFromCollection(kakera);
            //Save and close the package.
            p.Save();
        } 
    }

    class userInfo {
        
    }
}