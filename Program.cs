using Discord;
using Discord.WebSocket;
using Discord.Commands;
public class Program
{
	public static Task Main(string[] args) => new Program().MainAsync();

    private DiscordSocketClient? _client;
    private CommandService? commands;
	public async Task MainAsync()
	{
        var clientconfig = new DiscordSocketConfig() {
            GatewayIntents = GatewayIntents.All
        };

        _client = new DiscordSocketClient(clientconfig);
        commands = new CommandService();
        CommandHandler cmdhandler = new CommandHandler(_client, commands);
        LoggingService logger = new LoggingService(_client, commands);
        
        string token = File.ReadAllText("token.txt");
        await cmdhandler.LoadCommandsAsync();
        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();

        // Block this task until the program is closed.
        await Task.Delay(-1);
	}
}