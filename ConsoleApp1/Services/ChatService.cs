using System;
using System.Threading.Tasks;
using Microsoft.Extensions.AI;

public class ChatService
{
    private readonly IChatClient _chatClient;
    private readonly ChatHistoryService _chatHistoryService;
    private readonly PromptService _promptService;

    public ChatService(IChatClient chatClient, ChatHistoryService chatHistoryService, PromptService promptService)
    {
        _chatClient = chatClient;
        _chatHistoryService = chatHistoryService;
        _promptService = promptService;
    }

    public async Task<string> SendMessageStreamingAsync(string userMessage)
    {
        string finalPrompt = _promptService.BuildPrompt(userMessage);
        Console.Write("AI: "); // Ensure streaming looks clean

        string chatResponse = "";

        await foreach (var item in _chatClient.GetStreamingResponseAsync(finalPrompt))
        {
            Console.Write(item.Text); // Stream each part
            chatResponse += item.Text;
        }

        Console.WriteLine(); // Move to a new line after streaming is done

        _chatHistoryService.AddToHistory(userMessage, chatResponse);

        return chatResponse;
    }
}