namespace DesignPatterns.Creational.Factory_Method
{
    public class PaymentServiceFactory : IPaymentServiceFactory
    {
        private static IDictionary<PaymentMethod, IPaymentService> paymentsServices = new Dictionary<PaymentMethod, IPaymentService>()
        {
            {
                PaymentMethod.CreditCard, new CreditCardService()
            },
            {
                PaymentMethod.PaymentSlip, new PaymentSlipService()
            }
        };

        public IPaymentService GetService(PaymentMethod paymentMethod)
        {
            if(!paymentsServices.TryGetValue(paymentMethod, out var service))
            {
                throw new InvalidOperationException("Payment Method not found!");
            }

            return service;
        }
    }
}
