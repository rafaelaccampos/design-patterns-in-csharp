namespace DesignPatterns.Behavioral.Visitor_Marketing
{
    public class NotificationService
    {
        private readonly INotificationVisitor _notificationVisitor;

        public NotificationService(INotificationVisitor notificationVisitor)
        {
            _notificationVisitor = notificationVisitor;
        }

        public void Notify(IList<IMarketingMessage> messages)
        {
            var visitor = _notificationVisitor;

            foreach (var message in messages)
            {
                message.Accept(visitor);
            }
        }
    }
}
