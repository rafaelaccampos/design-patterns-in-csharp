namespace DesignPatterns.Behavioral.Interpreter
{
    public class MultiplicationExpression : IExpression
    {
        private IExpression _leftExpression;
        private IExpression _rightExpression;

        public MultiplicationExpression(IExpression leftExpression, IExpression rightExpression)
        {
            _leftExpression = leftExpression;
            _rightExpression = rightExpression;
        }

        public int Interpret()
        {
            return _leftExpression.Interpret() * _rightExpression.Interpret();
        }
    }
}
