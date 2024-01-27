namespace DesignPatterns.Behavioral.Visitor_Marketing
{
    public interface INotificationVisitor
    {
        string Visit(SmsMessage message);

        string Visit(EmailMessage message);
    }
}
