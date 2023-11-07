namespace DesignPatterns.Structural.Abstract_Factory
{
    public class LightThemeFactory : IAbstractWidgetFactory
    {
        public ILabel CreateLabel()
        {
            return new LightLabel();
        }

        public IButton CreateButton()
        {
            return new LightButton();
        }
    }
}
