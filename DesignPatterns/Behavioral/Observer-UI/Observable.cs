namespace DesignPatterns.Behavioral.Observer_UI
{
    public abstract class Observable
    {
        public IList<IObserver> _observables;

        public Observable()
        {
            _observables = new List<IObserver>();
        }

        public void Subscribe(IObserver observer)
        {
            _observables.Add(observer);
        }

        public void NotifyAll(string name, string value)
        {
            foreach(var observer in _observables)
            {
                observer.Notify(name, value);
            }
        }
    }
}
