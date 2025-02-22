using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConsoleApp1;

class Program
{
    static async Task Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder();
        builder.Services.AddChatClient(new OllamaChatClient(new Uri("http://localhost:11434"), "llama3"));
        
        var app = builder.Build();
        var chatClient = app.Services.GetRequiredService<IChatClient>();

        var chatHistoryService = new ChatHistoryService();
        var promptService = new PromptService(chatHistoryService, historyLimit: 3);
        var chatService = new ChatService(chatClient, chatHistoryService, promptService);

        while (true)
        {
            Console.Write("You: ");
            string userInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(userInput)) break;

            await chatService.SendMessageStreamingAsync(userInput);
        }
    }
}