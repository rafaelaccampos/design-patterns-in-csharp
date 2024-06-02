using DesignPatterns.Behavioral.Interpreter;
using FluentAssertions;

namespace DesignPatterns.Tests.Interpreter
{
    [TestFixture]
    public class InterpreterTests
    {
        private ExpressionParser _parser;

        [SetUp]
        public void Setup()
        {
            _parser = new ExpressionParser();
        }

        [Test]
        public void ShouldBeAbleToTestAddition()
        {
            var expression = _parser.Parse("3 + 5");
            var interpret = expression.Interpret();
            interpret.Should().Be(8);
        }

        [Test]
        public void ShouldBeAbleToTestMultiplication()
        {
            var expression = _parser.Parse("3 * 5");
            var interpret = expression.Interpret();
            interpret.Should().Be(15);
        }

        [Test]
        public void ShouldBeAbleToTestComplexExpression()
        {
            var expression = _parser.Parse("3 + 5 * 2");
            var interpret = expression.Interpret();
            interpret.Should().Be(13);
        }
    }
}
