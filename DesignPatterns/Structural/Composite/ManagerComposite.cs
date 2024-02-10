namespace DesignPatterns.Structural.Composite
{
    public class ManagerComposite : EmployeeComponent
    {
        private readonly IList<EmployeeComponent> _children;

        public ManagerComposite(string name, string role, decimal expenses) 
            : base(name, role, expenses)
        {
            _children = new List<EmployeeComponent>();
        }

        public override decimal GetExpenses()
        {
            return _children.Sum(c => c.GetExpenses()) + Expenses;
        }

        public void Add(EmployeeComponent component)
        {
            _children.Add(component);
        }

        public void Remove(EmployeeComponent component)
        {
            _children.Remove(component);
        }
    }
}
