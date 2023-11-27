namespace DesignPatterns.Structural.Decorator
{
    public class LoggerUseCaseDecorator : IUseCase
    {
        private readonly IUseCase _useCase;

        public LoggerUseCaseDecorator(IUseCase useCase)
        {
            _useCase = useCase;    
        }

        public void Execute(Input input)
        {
            Console.WriteLine("Executando decorator de Log");
            _useCase.Execute(input);
        }
    }
}
