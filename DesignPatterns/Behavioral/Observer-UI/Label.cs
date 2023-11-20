namespace DesignPatterns.Behavioral.Observer_UI
{
    public class Label : IObserver
    {
        private readonly string _expression;

        public Label(string expression)
        {
            _expression = expression;
            Value = "";
        }

        public string Value { get; private set; }

        public void Notify(string name, string value)
        {
            Value = _expression.Replace($"{name}", value);
        }
    }
}
