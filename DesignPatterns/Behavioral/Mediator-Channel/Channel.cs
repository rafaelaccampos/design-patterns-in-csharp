namespace DesignPatterns.Behavioral.Mediator_Channel
{
    public class Channel
    {
        private readonly IList<Participant> _participants;

        public Channel()
        {
            _participants = new List<Participant>();
        }

        public void Register(Participant participant)
        {
            _participants.Add(participant);
        }

        public void SendAll(Participant from, string message)
        {
            foreach(var to in _participants)
            {
                if (from != to)
                {
                    to.Receive(from, message);
                }
            }
        }
    }
}
