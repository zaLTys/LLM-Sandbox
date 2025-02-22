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

        var prompt = new PromptBuilder("What is your purpose?")
            .WithWordLimit(50)
            .WithCustomRestriction("Please short answer")
            .Build();
        
        var response = await chatClient.GetResponseAsync(prompt);
        
        Console.WriteLine(response.Message.Text);
    }
}