using Discord;
using Discord.WebSocket;
using Discord.Commands;

public class LoggingService 
{
    public LoggingService(DiscordSocketClient _client, CommandService _commands) {
        //hooks socketclient logging to LogError
        _client.Log += LogError;
        //hooks commandservice logging to LogError
        _commands.Log += LogError;
    }
    private Task LogError(LogMessage message) {
        if (message.Exception is CommandException cmdException) {
			Console.WriteLine($"[CommandException]: {cmdException.Command.Aliases.First()}, Severity: {message.Severity}"
				+ $" failed to execute in {cmdException.Context.Channel}.");
			Console.WriteLine(cmdException);
		}
		else {
            Console.WriteLine($"[LogMessage]: {message}, Severity: {message.Severity}");
        }

        return Task.CompletedTask;
    }
}