namespace DesignPatterns.Behavioral.Visitor_Marketing
{
    public class NotificationVisitor : INotificationVisitor
    {
        public string Visit(SmsMessage message)
        {
            return $"SMS message: {message}";
        }

        public string Visit(EmailMessage message)
        {
            return $"Email message: {message}";
        }
    }
}
