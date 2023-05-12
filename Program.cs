using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System.Text.Json;
public class Program
{
	public static Task Main(string[] args) => new Program().MainAsync();

    private DiscordSocketClient _client;
	public async Task MainAsync()
	{
        var config = new DiscordSocketConfig()
        {
            // Other config options can be presented here.
            GatewayIntents = GatewayIntents.All
        };

        _client = new DiscordSocketClient(config);
        _client.MessageReceived += CommandHandler;
        
        string token = File.ReadAllText("token.txt");
        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();

        // Block this task until the program is closed.
        await Task.Delay(-1);
	}

    private Task CommandHandler(SocketMessage message)
    {
        IMessageChannel commandChannel = message.Channel;
        String command = "";
        int commandLength;

        if (message.Content.Length == 0 || !message.Content.StartsWith("p") || message.Author.Id != 283810729033990147)
            return Task.CompletedTask;

        if (message.Author.IsBot || message.Author.IsWebhook)
            return Task.CompletedTask;

        if (message.Content.Contains(' '))
            commandLength = message.Content.IndexOf(' ');
        else 
            commandLength = message.Content.Length;

        command = message.Content.Substring(1, commandLength - 1);

        if (command.Equals("count")) {
            message.DeleteAsync();

            IMessageChannel? countChannel = _client.GetChannelAsync(1098950016795680799).Result as IMessageChannel;
            if (countChannel == null) {
                Console.WriteLine("Error: Given channel could not be found");
                return Task.CompletedTask;
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
                        commandChannel.SendMessageAsync(mes.Content + " cannot parse");
                    } else if (prevNum != curNum + 1) {
                        commandChannel.SendMessageAsync(mes.Content + " wrong order");
                    } else {
                        prevNum = curNum;
                    }
                }
                //}

                //last_verified_message = 
            } else {
                message.Channel.SendMessageAsync("error: message " + last_verified_message + " could not be converted to int");
            }
        }

        if (command.Equals("f")) {
            message.DeleteAsync();
            commandChannel.SendMessageAsync("$tsk");
        }

        if (command.Equals("d")) {
            message.DeleteAsync();
            message.Channel.GetMessagesAsync();
        }

        if (command.Equals("clean")) {
            message.DeleteAsync();

            IEnumerable<IMessage> messages = commandChannel.GetMessagesAsync(250).FlattenAsync().Result;

            messages = messages.Where(x => x.Content == "<@283810729033990147>");

            (commandChannel as ITextChannel)!.DeleteMessagesAsync(messages);
        }

        return Task.CompletedTask;
    }

    public async Task MyRatelimitCallback(IRateLimitInfo info)
    {
        Console.WriteLine($"{info.IsGlobal} {info.Limit} {info.Remaining} {info.ResetAfter}");
    }
}