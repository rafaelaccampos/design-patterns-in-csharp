namespace DesignPatterns.Behavioral.Strategy_Parking
{
    public class Period
    {
        private readonly DateTime _startTime;
        private readonly DateTime _endTime;

        public Period(DateTime startDate, DateTime endTime)
        {
            _startTime = startDate;
            _endTime = endTime;
        }

        public long GetDiffInMilliseconds()
        {
            var difference = _endTime - _startTime;
            return Convert.ToInt64(difference.TotalMilliseconds);
        }

        public long GetDiffInHours()
        {
            return GetDiffInMilliseconds()/(1000 * 60 * 60);
        }

        public long GetDiffInDays()
        {
            return ((GetDiffInMilliseconds()) / (1000 * 60 * 60 * 24));
        }
    }
}
