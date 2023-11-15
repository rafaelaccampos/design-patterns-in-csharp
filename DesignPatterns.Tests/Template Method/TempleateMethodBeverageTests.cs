using DesignPatterns.Behavioral.TemplateMethod_Beverage;
using FluentAssertions;

namespace DesignPatterns.Tests.Template_Method
{
    public class TempleateMethodBeverageTests
    {
        [Test]
        public void ShouldBeAbleToCalculateTaxesOfInvoice()
        {
            var invoice = new Invoice();
            invoice.Add(new Beer("Heineken", 20));
            invoice.Add(new Whisky("Jack Daniels", 100));
            invoice.Add(new Water("Crystal", 5));

            var taxes = invoice.GetTaxes();

            taxes.Should().Be(22);
        }
    }
}
