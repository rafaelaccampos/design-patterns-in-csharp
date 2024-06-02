using System.Net.WebSockets;

namespace DesignPatterns.Behavioral.Interpreter
{
    public class ExpressionParser
    {
        public IExpression Parse(string expression)
        {
            Stack<IExpression> stack = new Stack<IExpression>();
            string[] tokens = expression.Split(' ');

            foreach(var token in tokens) 
            {
                if(int.TryParse(token, out var value))
                {
                    stack.Push(new NumberExpression(value));
                }
                else if(token == "+")
                {
                    var right = stack.Pop();
                    var left = stack.Pop();
                    stack.Push(new AdditionExpression(left, right));
                }
                else if(token == "*")
                {
                    var right = stack.Pop();
                    var left = stack.Pop();
                    stack.Push(new MultiplicationExpression(left, right));
                }
            }

            return stack.Pop();
        }
    }
}
