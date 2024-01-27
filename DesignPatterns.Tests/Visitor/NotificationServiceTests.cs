using DesignPatterns.Behavioral.Visitor_Marketing;
using FluentAssertions.Execution;
using NSubstitute;

namespace DesignPatterns.Tests.Visitor
{
    public class NotificationServiceTests
    {
        [Test]
        public void ShouldBeAbleToCreateEmailMessageNotification()
        {
            var emailMessages = new List<IMarketingMessage>
            {
                new EmailMessage("Rafa", "T", "BFF", "We need to go out sometime!"),
            };

            var notificationVisitor = Substitute.For<INotificationVisitor>();
            var notificationService = new NotificationService(notificationVisitor);
            notificationService.Notify(emailMessages);

            var expectedEmailMessage = new EmailMessage("Rafa", "T", "BFF", "We need to go out sometime!");
            using (new AssertionScope())
            {
                notificationVisitor.Received(1).Visit(Arg.Is<EmailMessage>(n =>
                    n.From == expectedEmailMessage.From &&
                    n.To == expectedEmailMessage.To &&
                    n.Subject == expectedEmailMessage.Subject &&
                    n.Content == expectedEmailMessage.Content));
            }
        }

        [Test]
        public void ShouldBeAbleToCreateSMSMessageNotification()
        {
            var smsMessages = new List<IMarketingMessage>
            {
                new SmsMessage("T", "Rafa", "Definitely, we need to schedule!"),
            };

            var notificationVisitor = Substitute.For<INotificationVisitor>();
            var notificationService = new NotificationService(notificationVisitor);
            notificationService.Notify(smsMessages);

            var expectedSmsMessage = new SmsMessage("T", "Rafa", "Definitely, we need to schedule!");
            using (new AssertionScope())
            {
                notificationVisitor.Received(1).Visit(Arg.Is<SmsMessage>(n =>
                    n.From == expectedSmsMessage.From &&
                    n.To == expectedSmsMessage.To &&
                    n.Content == expectedSmsMessage.Content));
            }
        }

        [Test]
        public void ShouldBeAbleToCreateEmailAndSmsMessagesNotifications()
        {
            var emailMessage = new EmailMessage("Rafa", "T", "BFF", "We need to go out sometime!");
            var smsMessage = new SmsMessage("T", "Rafa", "Definitely, we need to schedule!");

            var messages = new List<IMarketingMessage>
            {
                emailMessage,
                smsMessage
            };

            var notificationVisitor = Substitute.For<INotificationVisitor>();
            var notificationService = new NotificationService(notificationVisitor);
            notificationService.Notify(messages);

            using (new AssertionScope())
            {
                notificationVisitor.Received(1).Visit(Arg.Is<EmailMessage>(n =>
                    n.From == emailMessage.From &&
                    n.To == emailMessage.To &&
                    n.Subject == emailMessage.Subject &&
                    n.Content == emailMessage.Content));

                notificationVisitor.Received(1).Visit(Arg.Is<SmsMessage>(n =>
                    n.From == smsMessage.From &&
                    n.To == smsMessage.To &&
                    n.Content == smsMessage.Content));
            }
        }
    }
}
