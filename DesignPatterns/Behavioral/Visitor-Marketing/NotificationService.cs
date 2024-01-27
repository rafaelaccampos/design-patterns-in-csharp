namespace DesignPatterns.Behavioral.Visitor_Marketing
{
    public class NotificationService
    {
        public IList<IMarketingMessage> Notify(IList<IMarketingMessage> messages)
        {
            var visitor = new NotificationVisitor();

            foreach (var message in messages)
            {
                message.Accept(visitor);
            }

            return messages;
        }
    }
}
