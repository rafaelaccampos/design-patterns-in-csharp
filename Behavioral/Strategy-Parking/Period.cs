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

        public int GetDiffInMilliseconds()
        {
            return _endTime.Millisecond - _startTime.Millisecond;
        }

        public int GetDiffInHours()
        {
            return (GetDiffInMilliseconds()/(100 * 60 * 60 * 24));
        }

        public int GetDiffInDays()
        {
            return ((GetDiffInMilliseconds()) / (100 * 60 * 60 * 24));
        }
    }
}
