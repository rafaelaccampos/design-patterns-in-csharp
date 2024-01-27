namespace DesignPatterns.Behavioral.Visitor_Marketing
{
    public interface IMarketingMessage
    {
        string From { get; }

        string To { get; }

        string Content { get; }

        string Accept(INotificationVisitor visitor);
    }
}
