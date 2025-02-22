using System.Text;

namespace ConsoleApp1;

public class PromptBuilder
{
    private readonly string _questionPrompt;
    private readonly List<string> _restrictions;

    public PromptBuilder(string questionPrompt)
    {
        _questionPrompt = questionPrompt;
        _restrictions = new List<string>();
    }

    public PromptBuilder WithWordLimit(int wordCount)
    {
        _restrictions.Add($" Reply in {wordCount} words max.");
        return this;
    }

    public PromptBuilder WithCustomRestriction(string restrictionText)
    {
        _restrictions.Add($" {restrictionText}");
        return this;
    }

    public string Build()
    {
        var sb = new StringBuilder(_questionPrompt);

        foreach (var restriction in _restrictions)
        {
            sb.Append(restriction);
        }

        return sb.ToString();
    }
}