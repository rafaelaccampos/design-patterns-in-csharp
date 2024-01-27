namespace DesignPatterns.Behavioral.Visitor_Marketing
{
    public class NotificationService
    {
        private readonly INotificationVisitor _notificationVisitor;

        public NotificationService(INotificationVisitor notificationVisitor)
        {
            _notificationVisitor = notificationVisitor;
        }

        public IEnumerable<string> Notify(IList<IMarketingMessage> messages)
        {
            var visitor = _notificationVisitor;

            foreach (var message in messages)
            {
                yield return message.Accept(visitor);
            }
        }
    }
}
