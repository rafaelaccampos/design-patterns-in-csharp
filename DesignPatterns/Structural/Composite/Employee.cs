namespace DesignPatterns.Structural.Composite
{
    public class Employee : EmployeeComponent
    {
        public Employee(string name, string role, decimal expenses)
            : base(name, role, expenses)
        {
        }

        public override decimal GetExpenses()
            => Expenses;
    }
}
