namespace DesignPatterns.Structural.Abstract_Factory
{
    public class DarkThemeFactory : IAbstractWidgetFactory
    {
        public ILabel CreateLabel()
        {
            return new DarkLabel();
        }

        public IButton CreateButton()
        {
            return new DarkButton();
        }
    }
}
