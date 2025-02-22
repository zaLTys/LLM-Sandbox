using System.Collections.Generic;
using System.Linq;
using ConsoleApp1;

public class PromptService
{
    private readonly ChatHistoryService _chatHistoryService;
    private readonly int _historyLimit;

    public PromptService(ChatHistoryService chatHistoryService, int historyLimit = 3)
    {
        _chatHistoryService = chatHistoryService;
        _historyLimit = historyLimit;
    }

    public string BuildPrompt(string userMessage)
    {
        string chatHistory = _chatHistoryService.GetFormattedHistory(_historyLimit);

        var promptBuilder = new PromptBuilder(userMessage)
            .WithWordLimit(50)
            .WithCustomRestriction("Please provide a concise response.")
            .WithHistory(3, _chatHistoryService);
        

        return promptBuilder.Build();
    }
}