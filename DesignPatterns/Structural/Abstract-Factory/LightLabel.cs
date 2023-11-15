namespace DesignPatterns.Structural.Abstract_Factory
{
    public class LightLabel : ILabel
    {
        public LightLabel()
        {
            Color = "black";
        }

        public string Color { get; private set; }
    }
}
