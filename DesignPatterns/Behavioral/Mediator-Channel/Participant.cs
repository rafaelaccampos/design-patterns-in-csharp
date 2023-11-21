namespace DesignPatterns.Behavioral.Mediator_Channel
{
    public class Participant
    {
        private readonly string _name;

        public Participant(string name)
        {
            _name = name;
        }

        public void Send(Participant to, string message)
        {
            to.Receive(this, message);
        }

        public void Receive(Participant from, string message)
        {
            Console.WriteLine($"O participante {from._name} mandou para {this._name}: ${message}");
        }
    }
}
