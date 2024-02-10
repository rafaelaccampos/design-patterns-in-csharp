using DesignPatterns.Structural.Composite;
using FluentAssertions;
using FluentAssertions.Execution;

namespace DesignPatterns.Tests.Composite
{
    public class CompositeTests
    {
        [Test]
        public void ShouldBeAbleToGetExpensesFromEmployees()
        {
            var composite = new ManagerComposite("John Doe", "Director", 100000);
            composite.Add(new Employee("Janice", "Programmer", 300));
            composite.Add(new Employee("London", "Programmer", 300));

            var secondComposite = new ManagerComposite("Nathaniel", "Manager", 15000);
            composite.Add(secondComposite);
            secondComposite.Add(new Employee("David", "Architect", 300));
            secondComposite.Add(new Employee("Jake", "Architect", 300));

            using (new AssertionScope())
            {
                composite.GetExpenses().Should().Be(116200);
                secondComposite.GetExpenses().Should().Be(15600);
            }
        }
    }
}
