namespace DesignPatterns.Structural.Decorator
{
    public class PlaceOrder : IUseCase
    {
        public void Execute(Input input)
        {
            Console.WriteLine($"Executando o place order {input}");
        }
    }
}
