using Discord;
using Discord.WebSocket;
using Discord.Commands;
using OfficeOpenXml;

public class Program
{
	public static Task Main(string[] args) => new Program().MainAsync();

    private DiscordSocketClient? _client;
    private LoggingService? _logger;
    private CommandService? _commands;
    private CommandHandler? _cmdhandler;
    private MudaeCommandHandler? _mudaecmdhandler;
    private MudaeService? _mudae;

	public async Task MainAsync()
	{
        var clientconfig = new DiscordSocketConfig() {
            GatewayIntents = GatewayIntents.All
        };

        _client = new DiscordSocketClient(clientconfig);

        _commands = new CommandService();
        _cmdhandler = new CommandHandler(_client, _commands);

        _mudae = new MudaeService();
        _mudaecmdhandler = new MudaeCommandHandler(_client, _mudae);

        _logger = new LoggingService(_client, _commands);
        
        string token = File.ReadAllText("token.txt");
        await _cmdhandler.LoadCommandsAsync();
        await _mudaecmdhandler.LoadLogger();
        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        // Block this task until the program is closed.
        await Task.Delay(-1);
	}
}