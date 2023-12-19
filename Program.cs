using Discord;
using Discord.WebSocket;
using Discord.Commands;

public class Program
{
	public static Task Main(string[] args) => new Program().MainAsync();

    private DiscordSocketClient? _client;
    private LoggingService? _logger;
    private CommandService? _commands;
    private CommandHandler? _cmdhandler;

	public async Task MainAsync()
	{
        var clientconfig = new DiscordSocketConfig() {
            GatewayIntents = GatewayIntents.All
        };

        _client = new DiscordSocketClient(clientconfig);

        _commands = new CommandService();
        _cmdhandler = new CommandHandler(_client, _commands);

        _logger = new LoggingService(_client, _commands);
        
        string token = File.ReadAllText("token.txt");
        await _cmdhandler.LoadCommandsAsync();
        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();

        // Block this task until the program is closed.
        await Task.Delay(-1);
	}
}