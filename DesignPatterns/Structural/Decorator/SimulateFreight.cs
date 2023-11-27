namespace DesignPatterns.Structural.Decorator
{
    public class SimulateFreight : IUseCase
    {
        public void Execute(Input input)
        {
            Console.WriteLine($"Executando o simulate freight: {input}");
        }
    }
}
