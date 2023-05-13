using Discord;
using Discord.Commands;
using Discord.WebSocket;

public class CountingModule : ModuleBase<SocketCommandContext> {
    [Command("verifycount")]
    public async Task TestAsync() {
        SocketUserMessage message = Context.Message;
        DiscordSocketClient client = Context.Client;
        ISocketMessageChannel commandCh = Context.Channel;

        await message.DeleteAsync();

        IMessageChannel? countChannel = client.GetChannelAsync(1098950016795680799).Result as IMessageChannel;
        if (countChannel == null) {
            Console.WriteLine("Error: Given channel could not be found");
            return;
        }

        // TODO: save ulong in file and get from file
        ulong last_verified_message = 1098950314461245512;

        int prevNum;
        bool parse = Int32.TryParse(countChannel.GetMessageAsync(last_verified_message).Result.Content, out prevNum);

        if (parse) {
            //while (countChannel.GetMessagesAsync(1) != countChannel.GetMessageAsync(last_verified_message)) {
            IEnumerable<IMessage> messages = countChannel.GetMessagesAsync(last_verified_message, Direction.Before, 500, CacheMode.AllowDownload).FlattenAsync().Result.Reverse();
            
            int curNum;

            foreach (IMessage mes in messages) {
                parse = Int32.TryParse(mes.Content, out curNum);

                Console.WriteLine(mes.Content);

                if (!parse) {
                    await commandCh.SendMessageAsync(mes.Content + " cannot parse");
                } else if (prevNum != curNum + 1) {
                    await commandCh.SendMessageAsync(mes.Content + " wrong order");
                } else {
                    prevNum = curNum;
                }
            }
            //}

            //last_verified_message = 
        } else {
            await message.Channel.SendMessageAsync("error: message " + last_verified_message + " could not be converted to int");
        }
    }
}