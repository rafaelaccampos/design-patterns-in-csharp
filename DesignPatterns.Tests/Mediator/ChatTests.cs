using DesignPatterns.Behavioral.Mediator_Channel;

namespace DesignPatterns.Tests.Mediator
{
    public class ChatTests
    {
        [Test]
        public void ShouldBeAbleToExchangeMessagesBetweenTheParticipants()
        {
            var firstParticipant = new Participant("FirstParticipant");
            var secondParticipant = new Participant("SecondParticipant");
            var thirdParcipant = new Participant("ThirdParticipant");

            firstParticipant.Send(secondParticipant, "Hello!");
            firstParticipant.Send(thirdParcipant, "Hello!");
            secondParticipant.Send(firstParticipant, "Hello, how are you?");
            thirdParcipant.Send(firstParticipant, "Hello, how are you?");
        }

        [Test]
        public void ShouldBeAbleToExchangeMessagesBetweenTheChannel()
        {
            var firstParticipant = new Participant("FirstParticipant");
            var secondParticipant = new Participant("SecondParticipant");
            var thirdParcipant = new Participant("ThirdParticipant");

            var channel = new Channel();
            channel.Register(firstParticipant);
            channel.Register(secondParticipant);
            channel.Register(thirdParcipant);

            channel.SendAll(firstParticipant, "Hello!");
        }
    }
}
