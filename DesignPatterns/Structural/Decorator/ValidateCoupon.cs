namespace DesignPatterns.Structural.Decorator
{
    public class ValidateCoupon : IUseCase
    {
        public void Execute(Input input)
        {
            Console.WriteLine($"Executando o validate coupon {input}"); 
        }
    }
}
