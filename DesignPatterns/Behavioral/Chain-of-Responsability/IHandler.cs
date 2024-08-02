namespace DesignPatterns.Behavioral.Chain_of_Responsability_Adm
{
    public interface IHandler
    {
        void Handle(IList<Bill> bills, int amount);
    }
}
