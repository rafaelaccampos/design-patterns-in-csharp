namespace DesignPatterns.Structural.Abstract_Factory
{
    public class DarkButton : IButton
    {
        public DarkButton()
        {
            Color = "white";
            BackgroundColor = "black";
        }

        public string Color { get; private set; }

        public string BackgroundColor { get; private set; }
    }
}
