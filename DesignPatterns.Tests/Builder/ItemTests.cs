using DesignPatterns.Creational.Builder;
using FluentAssertions;

namespace DesignPatterns.Tests.Builder
{
    public class ItemTests
    {
        [Test]
        public void ShouldBeAbleToCreateAnItem()
        {
            var item = new ItemBuilder(1, "Instrumentos Musicais", "Guitarra", 1000)
                .WithWidth(100)
                .WithHeight(50)
                .WithLenght(30)
                .WithWeight(3)
                .Generate();

            var volume = item.GetVolume();

            volume.Should().Be(0.15M);
        }
    }
}
