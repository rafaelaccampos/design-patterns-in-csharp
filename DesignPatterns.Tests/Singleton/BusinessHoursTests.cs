using DesignPatterns.Creational.Singleton;
using FluentAssertions;

namespace DesignPatterns.Tests.Singleton
{
    public class BusinessHoursTests
    {
        [Test]
        public void ShouldBeAbleToGetAnInstance()
        {
            var instance = BusinessHours.GetInstance();
            
            instance.Should().NotBeNull();
        }

        [Test]
        public void ShouldBeAbleToCheckIfInstancesAreTheSame()
        {
            var firstIntance = BusinessHours.GetInstance();
            var secondIntance = BusinessHours.GetInstance();

            firstIntance.Should().Be(secondIntance);
        }
    }
}
