using System.Diagnostics;

namespace DesignPatterns.Structural.Decorator
{
    public class PerformanceUseCaseDecorator : IUseCase
    {
        private readonly IUseCase _useCase;

        public PerformanceUseCaseDecorator(IUseCase useCase)
        {
            _useCase = useCase;
        }

        public void Execute(Input input)
        {
            var stopWatch = new Stopwatch();

            stopWatch.Start();
            _useCase.Execute(input);
            stopWatch.Stop();

            Console.WriteLine($"Performance: {stopWatch.Elapsed}");
        }
    }
}
