using DesignPatterns.Structural.Bridge;
using FluentAssertions;

namespace DesignPatterns.Tests.Bridge
{
    public class ItemTests
    {
        [Test]
        public void ShouldBeAbleToGetACorrectPriceWhenItemIsFoodAndIsMeasureByQuantity()
        {
            var food = new Food(new Quantity())
            {
                Title = "Pizza",
                PricePerUnit = 10.00m,
                NutritionLabel = "Calorias (valor energético), 284.72 kcal"
            };

            var units = 5;
            var totalPrice = food.GetTotalPrice(units);

            totalPrice.Should().Be(50.0m);
        }

        [Test]
        public void ShouldBeAbleToGetACorrectPriceWhenItemIsFoodAndIsMeasureByKg()
        {
            var food = new Food(new Kg())
            {
                Title = "Banana",
                PricePerUnit = 10.00m,
                NutritionLabel = "Carboidratos = 31,89 g. Fibras = 2,3 g. Proteínas = 1,3 g. Potássio = 499 mg"
            };

            var units = 6;
            var totalPrice = food.GetTotalPrice(units);

            totalPrice.Should().Be(60.0m);
        }

        [Test]
        public void ShouldBeAbleToGetACorrectPriceWhenItemIsProductAndIsMeasureByQuantity()
        {
            var product = new Product(new Quantity())
            {
                Title = "Sabão",
                PricePerUnit = 10.00m,
                Category = "Limpeza"
            };

            var units = 6;
            var totalPrice = product.GetTotalPrice(units);

            totalPrice.Should().Be(60.0m);
        }

        [Test]
        public void ShouldBeAbleToGetACorrectPriceWhenItemIsProductAndIsMeasureByKg()
        {
            var product = new Product(new Kg())
            {
                Title = "Sabão em pó",
                PricePerUnit = 10.00m,
                Category = "Limpeza"
            };

            var units = 5;
            var totalPrice = product.GetTotalPrice(units);

            totalPrice.Should().Be(50.0m);
        }
    }
}
