namespace DesignPatterns.Creational.Flyweight
{
    public class PaymentMethodDetails
    {
        public PaymentMethodDetails(
            string guidanceText, 
            decimal? minimumValue, 
            decimal? maximumValue)
        {
            GuidanceText = guidanceText;
            MinimumValue = minimumValue;
            MaximumValue = maximumValue;
        }

        public string GuidanceText { get; set; }

        public decimal? MinimumValue { get; set; }

        public decimal? MaximumValue { get; set; }
    }
}
