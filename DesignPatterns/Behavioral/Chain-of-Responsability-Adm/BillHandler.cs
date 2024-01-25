namespace DesignPatterns.Behavioral.Chain_of_Responsability_Adm
{
    public class BillHandler : IHandler
    {
        private readonly IHandler? _nextHandler;
        private readonly int _type;
       
        public BillHandler(IHandler? nextHandler, int type)
        {
            _nextHandler = nextHandler;
            _type = type;
        }

        public void Handle(IList<Bill> bills, int amount)
        {
            decimal notes = amount / _type;
            var count = (int)Math.Floor(notes);
            var bill = new Bill
            {
                Count = count,
                Type = _type,
            };

            bills.Add(bill);
            var remaining = amount % _type;

            if (_nextHandler != null)
            {
                _nextHandler.Handle(bills, remaining);
            }

            if(remaining > 0)
            {
                throw new Exception("Without available notes");
            }
        }
    }
}
