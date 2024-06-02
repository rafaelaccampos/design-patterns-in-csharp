namespace DesignPatterns.Behavioral.Interpreter
{
    public class AdditionExpression : IExpression
    {
        private IExpression _leftExpression;
        private IExpression _rightExpression;

        public AdditionExpression(IExpression leftExpression, IExpression rightExpression)
        {
            _leftExpression = leftExpression;
            _rightExpression = rightExpression;
        }

        public int Interpret()
        {
            return _leftExpression.Interpret() + _rightExpression.Interpret();
        }
    }
}
