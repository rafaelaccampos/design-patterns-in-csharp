namespace DesignPatterns.Behavioral.Observer_UI
{
    public class InputText : Observable
    {
        public InputText(string name)
            : base()
        {
            Value = "";
            Name = name;
        }

        public string Value { get; private set; }
        public string Name { get; private set; }

        public void SetValue(string value)
        {
            Value = value;
            NotifyAll(Name, Value);
        }
    }
}
