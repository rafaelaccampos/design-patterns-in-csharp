namespace DesignPatterns.Behavioral.Interpreter
{
    public class ExpressionParser
    {
        public IExpression Parse(string expression)
        {
            var rpn = ConvertToRPN(expression);
            return EvaluateRPN(rpn);
        }

        private Queue<string> ConvertToRPN(string expression)
        {
            var outputQueue = new Queue<string>();
            var operatorStack = new Stack<string>();
            var tokens = expression.Split(' ');

            var precedence = new Dictionary<string, int>
            {
                { "+", 1 },
                { "*", 2 }
            };

            foreach (var token in tokens)
            {
                if (int.TryParse(token, out _))
                {
                    outputQueue.Enqueue(token);
                }
                else if (token == "+" || token == "*")
                {
                    while (operatorStack.Any() && precedence[operatorStack.Peek()] >= precedence[token])
                    {
                        outputQueue.Enqueue(operatorStack.Pop());
                    }
                    operatorStack.Push(token);
                }
                else
                {
                    throw new InvalidOperationException($"Unexpected token: {token}");
                }
            }

            while (operatorStack.Any())
            {
                outputQueue.Enqueue(operatorStack.Pop());
            }

            return outputQueue;
        }

        private IExpression EvaluateRPN(Queue<string> rpn)
        {
            var stack = new Stack<IExpression>();

            while (rpn.Any())
            {
                var token = rpn.Dequeue();

                if (int.TryParse(token, out int number))
                {
                    stack.Push(new NumberExpression(number));
                }
                else if (token == "+")
                {
                    var right = stack.Pop();
                    var left = stack.Pop();
                    stack.Push(new AdditionExpression(left, right));
                }
                else if (token == "*")
                {
                    var right = stack.Pop();
                    var left = stack.Pop();
                    stack.Push(new MultiplicationExpression(left, right));
                }
                else
                {
                    throw new InvalidOperationException($"Unexpected token: {token}");
                }
            }

            return stack.Pop();
        }
    }
}
