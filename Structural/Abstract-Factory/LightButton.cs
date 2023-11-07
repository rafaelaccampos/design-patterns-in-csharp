namespace DesignPatterns.Structural.Abstract_Factory
{
    public class LightButton : IButton
    {
        public LightButton()
        {
            Color = "white";
            BackgroundColor = "blue";
        }

        public string Color { get; private set; }

        public string BackgroundColor { get; private set; }
    }
}
