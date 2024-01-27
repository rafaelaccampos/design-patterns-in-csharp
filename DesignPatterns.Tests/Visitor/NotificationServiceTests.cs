using DesignPatterns.Behavioral.Visitor_Marketing;
using FluentAssertions;
using FluentAssertions.Execution;

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

            var notificationVisitor = new NotificationVisitor();
            var notificationService = new NotificationService(notificationVisitor);
            var notifications = notificationService.Notify(emailMessages).ToList();

            var expectedEmailMessage = "Email message: From: Rafa, Subject: BFF, To: T, Content: We need to go out sometime!";
            using (new AssertionScope())
            {
                notifications.Should().BeEquivalentTo(expectedEmailMessage);
            }
        }

        [Test]
        public void ShouldBeAbleToCreateSMSMessageNotification()
        {
            var smsMessages = new List<IMarketingMessage>
            {
                new SmsMessage("T", "Rafa", "Definitely, we need to schedule!"),
            };

            var notificationVisitor = new NotificationVisitor();
            var notificationService = new NotificationService(notificationVisitor);
            var notifications = notificationService.Notify(smsMessages).ToList();

            var expectedSmsMessage = "SMS message: From: T, To: Rafa, Content: Definitely, we need to schedule!";
            using (new AssertionScope())
            {
                notifications.Should().BeEquivalentTo(expectedSmsMessage);
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

            var notificationVisitor = new NotificationVisitor();
            var notificationService = new NotificationService(notificationVisitor);
            var notifications = notificationService.Notify(messages).ToList();

            var expectedMessages = new List<string>
            {
                { "Email message: From: Rafa, Subject: BFF, To: T, Content: We need to go out sometime!" },
                { "SMS message: From: T, To: Rafa, Content: Definitely, we need to schedule!" },
            };

            using (new AssertionScope())
            {
                notifications.Should().Contain(expectedMessages);
            }
        }
    }
}
