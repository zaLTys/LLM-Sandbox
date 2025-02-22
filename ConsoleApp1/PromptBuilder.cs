using System.Text;

public class PromptBuilder
{
    private readonly string _questionPrompt;
    private readonly List<string> _restrictions;
    private string _chatHistory = "";

    public PromptBuilder(string questionPrompt)
    {
        _questionPrompt = questionPrompt;
        _restrictions = new List<string>();
    }

    public PromptBuilder WithWordLimit(int wordCount)
    {
        _restrictions.Add($"Reply in {wordCount} words max.");
        return this;
    }

    public PromptBuilder WithCustomRestriction(string restrictionText)
    {
        _restrictions.Add(restrictionText);
        return this;
    }

    public PromptBuilder WithHistory(int lastXMessages, ChatHistoryService historyService)
    {
        _chatHistory = historyService.GetFormattedHistory(lastXMessages);
        return this;
    }

    public string Build()
    {
        var sb = new StringBuilder();

        if (!string.IsNullOrWhiteSpace(_chatHistory))
        {
            sb.Append("Having previous conversation context:\n")
                .Append(_chatHistory)
                .Append("\n\n");
        }

        sb.Append("Answer me this next question:\n")
            .Append(_questionPrompt)
            .Append("\n\n");

        if (_restrictions.Count > 0)
        {
            sb.Append("Apply restrictions:\n")
                .Append(string.Join(" ", _restrictions))
                .Append("\n");
        }

        return sb.ToString();
    }
}