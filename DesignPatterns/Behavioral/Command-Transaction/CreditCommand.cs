namespace DesignPatterns.Behavioral.Command_Transaction
{
    public class CreditCommand : ICommand
    {
        private readonly Account _account;      
        private readonly decimal _amount;

        public CreditCommand(Account account, decimal amount)
        {
            _account = account;
            _amount = amount;
        }

        public void Execute()
        {
            _account.Credit(_amount);
        }
    }
}
