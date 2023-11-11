namespace DesignPatterns.Behavioral.Strategy
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

        public int GetDiffInMilli()
        {
            return _endTime.Millisecond - _startTime.Millisecond;
        }

        public int GetDiffInHours()
        {
            return (GetDiffInMilli()/(100 * 60 * 60 * 24));
        }

        public int GetDiffInDays()
        {
            return ((GetDiffInMilli()) / (100 * 60 * 60 * 24));
        }
    }
}
