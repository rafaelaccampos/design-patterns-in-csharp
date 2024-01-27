using DesignPatterns.Behavioral.Visitor_Marketing;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;

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

            var notificationService = new NotificationService();
            var notifications = notificationService.Notify(emailMessages);

            var expectedEmailMessage = new EmailMessage("Rafa", "T", "BFF", "We need to go out sometime!");

            using (new AssertionScope())
            {
                notifications.OfType<EmailMessage>().Should().ContainEquivalentOf(expectedEmailMessage);
            }
        }

        [Test]
        public void ShouldBeAbleToCreateSMSMessageNotification()
        {
            var smsMessages = new List<IMarketingMessage>
            {
                new SmsMessage("T", "Rafa", "Definitely, we need to schedule!"),
            };

            var notificationService = new NotificationService();
            var notifications = notificationService.Notify(smsMessages);

            var expectedSmsMessage = new SmsMessage("T", "Rafa", "Definitely, we need to schedule!");

            using (new AssertionScope())
            {
                notifications.OfType<SmsMessage>().Should().ContainEquivalentOf(expectedSmsMessage);
            }
        }

        [Test]
        public void ShouldBeAbleToCreateEmailAndSmsMessagesNotifications()
        {
            var messages = new List<IMarketingMessage>
            {
                new EmailMessage("Rafa", "T", "BFF", "We need to go out sometime!"),
                new SmsMessage("T", "Rafa", "Definitely, we need to schedule!"),
            };

            var notificationService = new NotificationService();
            var notifications = notificationService.Notify(messages);

            using (new AssertionScope())
            {
                notifications.OfType<EmailMessage>().Should().ContainSingle();
                notifications.OfType<SmsMessage>().Should().ContainSingle();
                notifications.Should().ContainEquivalentOf(new EmailMessage("Rafa", "T", "BFF", "We need to go out sometime!"));
                notifications.Should().ContainEquivalentOf(new SmsMessage("T", "Rafa", "Definitely, we need to schedule!"));
            }
        }
    }
}
