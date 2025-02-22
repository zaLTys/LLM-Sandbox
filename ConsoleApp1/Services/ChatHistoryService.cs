using System.Collections.Generic;
using System.Linq;

public class ChatHistoryService
{
    private readonly List<KeyValuePair<string, string>> _chatHistory = new();

    public void AddToHistory(string userMessage, string botResponse)
    {
        _chatHistory.Add(new KeyValuePair<string, string>(userMessage, botResponse));
    }

    public string GetFormattedHistory(int lastXMessages)
    {
        var relevantHistory = _chatHistory.TakeLast(lastXMessages);
        var historyString = string.Join("\n", relevantHistory.Select(pair => $"User: {pair.Key}\nAI: {pair.Value}"));
        return string.IsNullOrWhiteSpace(historyString) ? "" : historyString + "\n\n";
    }
}