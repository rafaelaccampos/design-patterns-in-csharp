namespace DesignPatterns.Behavioral.Visitor_Marketing
{
    public class NotificationVisitor : INotificationVisitor
    {
        public string Visit(SmsMessage message)
        {
            return $"SMS message: From: {message.From}, To: {message.To}, Content: {message.Content}";
        }

        public string Visit(EmailMessage message)
        {
            return $"Email message: From: {message.From}, Subject: {message.Subject}, To: {message.To}, Content: {message.Content}";
        }
    }
}
