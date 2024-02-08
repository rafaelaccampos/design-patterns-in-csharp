namespace DesignPatterns.Creational.Flyweight
{
    public class PaymentsMethodFactory
    {
        private Dictionary<PaymentMethod, PaymentMethodDetails> _paymentMethods;

        public PaymentsMethodFactory()
        {
            _paymentMethods = new Dictionary<PaymentMethod, PaymentMethodDetails>
            {
                { PaymentMethod.CreditCard, new PaymentMethodDetails("Sobre o Cartão de Crédito!", 1, null) },
                { PaymentMethod.PaymentSlip, new PaymentMethodDetails("Sobre o Boleto!", 10, 1000) },
                { PaymentMethod.CreditCard, new PaymentMethodDetails("Sobre o PayPal!", 16.5m, null) },
            };
        }

        public PaymentMethodDetails GetPaymentMethod(PaymentMethod paymentMethod)
        {
            return _paymentMethods[paymentMethod];
        }
    }
}
