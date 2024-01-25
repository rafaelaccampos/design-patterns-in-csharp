namespace DesignPatterns.Behavioral.Chain_of_Responsability_Adm
{
    public class Atm
    {
        private readonly IHandler _handler;

        public Atm(IHandler handler)
        {
            _handler = handler;
        }

        public IList<Bill> Withdraw(int amount)
        {
            var bills = new List<Bill>();
            _handler.Handle(bills, amount);
            return bills;
        }
    }
}
