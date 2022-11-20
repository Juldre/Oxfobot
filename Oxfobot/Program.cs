using Discord.WebSocket;
using System.Net.Http.Headers;

namespace Oxfobot
{
    internal class Program
    {
        private DiscordSocketClient _client;

        public static Task Main(string[] args) => new Program().MainAsync();

        public async Task MainAsync()
        {
            if (!File.Exists("key.txt"))
            {
                Console.WriteLine("Keyfile does not exists, creating one...");
                File.WriteAllText("key.txt", "");
                return;
            }

            _client = new DiscordSocketClient();
            _client.Log += _client_Log;
            _client.MessageReceived += _client_MessageReceived;
            await _client.LoginAsync(Discord.TokenType.Bot, File.ReadAllText("key.txt"));
            await _client.StartAsync();
            await Task.Delay(-1);
        }

        private async Task _client_MessageReceived(SocketMessage arg)
        {
            Console.WriteLine("MessageRecieved | " +(arg.Source) +" | " + (!arg.Author.IsBot ? "Member" : (arg.Author.IsWebhook?"Webhook":"Bot")) +$" | {arg.Author.Username}#{arg.Author.Discriminator}: {arg.Content}");
        }

        private Task _client_Log(Discord.LogMessage arg)
        {
            Console.WriteLine($"{arg.Source} | {arg.Message}");
            return Task.CompletedTask;
        }
    }
}