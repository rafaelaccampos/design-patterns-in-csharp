namespace DesignPatterns.Structural.Abstract_Factory
{
    public class View
    {
        public View(IAbstractWidgetFactory abstractWidgetFactory)
        {
            Label = abstractWidgetFactory.CreateLabel();
            Button = abstractWidgetFactory.CreateButton();
        }

        public ILabel Label { get; private set; }
        public IButton Button { get; private set; }
    }
}
