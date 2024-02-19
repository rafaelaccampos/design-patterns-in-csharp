namespace DesignPatterns.Structural.Facade
{
    public interface IAntiFraudFacade
    {
        AntiFraudOutput Check(AntiFraudInput input);
    }
}
