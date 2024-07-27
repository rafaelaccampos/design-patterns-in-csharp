using DesignPatterns.Creational.Prototype;
using FluentAssertions;
using FluentAssertions.Execution;

namespace DesignPatterns.Tests.Prototype
{
    public class CarTests
    {
        [Test]
        public void ShouldBeAbleToCloneAnExactlyCopyOfTheCar()
        {
            var car = new Car("Toyota Camry", "Red", 5);

            var clonedCar = car.Clone() as Car;

            using (new AssertionScope())
            {
                car.Model.Should().Be(clonedCar!.Model);
                car.Color.Should().Be(clonedCar.Color);
                car.Seats.Should().Be(clonedCar.Seats);
            }
        }

        [Test]
        public void ShouldBeAbleToCloneAndModifyWithoutAffectingTheOriginalCar()
        {
            var car = new Car("Toyota Camry", "Red", 5);

            var clonedCar = car.Clone() as Car;
            clonedCar!.Color = "Blue";

            using (new AssertionScope())
            {
                car.Color.Should().Be("Red");
                clonedCar.Color.Should().Be("Blue");
            }
        }
    }
}
