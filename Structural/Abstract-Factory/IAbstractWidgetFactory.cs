namespace DesignPatterns.Structural.Abstract_Factory
{
    public interface IAbstractWidgetFactory
    {
        ILabel CreateLabel();
        IButton CreateButton();
    }
}
