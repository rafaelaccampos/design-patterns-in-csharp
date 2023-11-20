using DesignPatterns.Behavioral.Observer_UI;
using FluentAssertions;
using FluentAssertions.Execution;

namespace DesignPatterns.Tests.Observer
{
    public class ComponentTests
    {
        [Test]
        public void ShouldBeAbleToWriteInTheInputAndShowUpdatedDataInTheLabel()
        {
            var inputText = new InputText("country"); //observable
            var firstCountryLabel = new Label($"País: {inputText.Name}"); //observer
            var secondCountryLabel = new Label($"Country: {inputText.Name}"); //observer

            inputText.Subscribe(firstCountryLabel);
            inputText.Subscribe(secondCountryLabel);
            inputText.SetValue("Brasil");

            using(new AssertionScope())
            {
                firstCountryLabel.Value.Should().Be("País: Brasil");
                secondCountryLabel.Value.Should().Be("Country: Brasil");
            }

            inputText.SetValue("França");

            using (new AssertionScope())
            {
                firstCountryLabel.Value.Should().Be("País: França");
                secondCountryLabel.Value.Should().Be("Country: França");
            }
        }
    }
}
