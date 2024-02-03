namespace DesignPatterns.Creational.Singleton
{
    public class BusinessHours
    {
        private BusinessHours(DateTime startTime, DateTime endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
        }

        private static BusinessHours _instance;

        public DateTime StartTime { get; private set; }

        public DateTime EndTime { get; private set; }

        public static BusinessHours GetInstance()
        {
            if (_instance == null)
            {
                _instance = new BusinessHours(
                    new DateTime(1, 1, 1, new Random().Next(0, 10), 0, 0),
                    new DateTime(1, 1, 1, new Random().Next(18, 24), 0, 0)
                    );
            }

            return _instance;
        }
    }
}
