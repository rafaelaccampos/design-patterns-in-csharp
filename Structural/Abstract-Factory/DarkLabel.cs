namespace DesignPatterns.Structural.Abstract_Factory
{
    public class DarkLabel : ILabel
    {
        public DarkLabel()
        {
            Color = "white";
        }

        public string Color { get; private set; }
    }
}
