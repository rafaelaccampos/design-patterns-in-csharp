namespace DesignPatterns.Behavioral.Command_Transaction
{
    public class DebitCommand : ICommand
    {
        private readonly Account _account;
        private readonly decimal _amount;

        public DebitCommand(Account account, decimal amount)
        {
            _account = account;
            _amount = amount;
        }

        public void Execute()
        {
            _account.Debit(_amount);
        }
    }
}
