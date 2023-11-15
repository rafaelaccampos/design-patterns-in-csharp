using DesignPatterns.Structural.Abstract_Factory;
using FluentAssertions;
using FluentAssertions.Execution;

namespace DesignPatterns.Tests.AbstractFactory
{
    public class ViewTests
    {
        [Test]
        public void ShouldBeAbleToCreateAViewWithLightTheme()
        {
            var view = new View(new LightThemeFactory());

            using(new AssertionScope())
            {
                view.Label.Color.Should().Be("black");
                view.Button.Color.Should().Be("white");
                view.Button.BackgroundColor.Should().Be("blue");
            }
        }

        [Test]
        public void ShouldBeAbleToCreateAViewWithDarkTheme()
        {
            var view = new View(new DarkThemeFactory());

            using(new AssertionScope())
            {
                view.Label.Color.Should().Be("white");
                view.Button.Color.Should().Be("white");
                view.Button.BackgroundColor.Should().Be("black");
            }
        }
    }
}
