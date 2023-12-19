using Discord;
using Discord.Commands;
using Discord.WebSocket;

// Commands for a fun discord channel where community counts as high as possible starting from 1
// NOT FINISHED LOL
public class CountingModule : ModuleBase<SocketCommandContext> {
    // TODO: FIGURE OUT WHAT TO DO WITH THIS
    [Command("count")]
    public async Task CountAsync(ulong channelId) {

        SocketUserMessage message = Context.Message;
        DiscordSocketClient client = Context.Client;
        ISocketMessageChannel cmdChnl = Context.Channel;

        await message.DeleteAsync();

        IMessageChannel? countChannel = client.GetChannelAsync(channelId).Result as IMessageChannel;
        if (countChannel == null) {
            await message.Channel.SendMessageAsync("error: given channel could not be found");
            return;
        }

        // TODO: save ulong in file and get from file
        ulong last_verified_message = 1098950314461245512;

        int prevNum;
        bool parse = Int32.TryParse(countChannel.GetMessageAsync(last_verified_message).Result.Content, out prevNum);

        if (!parse) {
            await message.Channel.SendMessageAsync("error: message with id " + last_verified_message + " could not be converted to int");
            return;
        }

        //while (countChannel.GetMessagesAsync(1) != countChannel.GetMessageAsync(last_verified_message)) {
        IEnumerable<IMessage> messages = countChannel.GetMessagesAsync(last_verified_message, Direction.Before, 500, CacheMode.AllowDownload)
                                                    .FlattenAsync()
                                                    .Result
                                                    .Reverse();
        
        int curNum;

        foreach (IMessage mes in messages) {
            parse = Int32.TryParse(mes.Content, out curNum);

            Console.WriteLine(mes.Content);

            if (!parse) {
                await cmdChnl.SendMessageAsync(mes.Content + " cannot parse");
            } else if (prevNum != curNum + 1) {
                await cmdChnl.SendMessageAsync(mes.Content + " wrong order");
            } else {
                prevNum = curNum;
            }
        }
        //}

        //last_verified_message = 
    }

    public ulong getVerifiedMessageId() {
        return 0;
    }

    public void setVerifiedMessageId(ulong msgId) {

    }
}