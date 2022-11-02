using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftCourse.Parser
{
    public class Parser
    {
        private static IDictionary<string, Operator> operators;

        public static IDictionary<string, Operator> Operators { get => operators; }

        public Parser()
        {
             operators = Operator.GetOperators();

        }

        private bool CompareOperators(Operator op1, Operator op2)=> op1.RightAssociative ? op1.Precedence < op2.Precedence : op1.Precedence <= op2.Precedence;
        
        private bool CompareOperators(string op1, string op2) => CompareOperators(Operators[op1], Operators[op2]);

        public IEnumerable<Token> ShuntingYard(IEnumerable<Token> tokens)
        {
            var stack = new Stack<Token>();
            foreach (var tok in tokens)
            {
                switch (tok.Type)
                {
                    case TokenType.Number:
                    case TokenType.Variable:
                        yield return tok;
                        break;
                    case TokenType.Function:
                        stack.Push(tok);
                        break;
                    case TokenType.Comma:
                        while (stack.Peek().Value != "(")
                            yield return stack.Pop();
                        break;
                    case TokenType.Operator:
                        while (stack.Any() && stack.Peek().Type == TokenType.Operator && CompareOperators(tok.Value, stack.Peek().Value))
                            yield return stack.Pop();
                        stack.Push(tok);
                        break;
                    case TokenType.Parenthesis:
                        if (tok.Value == "(")
                            stack.Push(tok);
                        else
                        {
                            while (stack.Peek().Value != "(")
                                yield return stack.Pop();
                            stack.Pop();
                            if (stack.Peek().Type == TokenType.Function)
                                yield return stack.Pop();
                        }
                        break;
                    default:
                        throw new Exception("Wrong token");
                }
            }
            while (stack.Any())
            {
                var tok = stack.Pop();
                if (tok.Type == TokenType.Parenthesis)
                    throw new Exception("Mismatched parentheses");
                yield return tok;
            }
        }

        /// <summary>
        /// Возвращает expression в обратной польской записи
        /// </summary>
        /// <param name="tokens">
        /// Коллекция токенов
        /// </param>
        /// <returns></returns>
        private string ExpressionToRPN(string expression, out List<Token> tokens)
        {
            var text = expression;
            var reader = new StringReader(text);
            tokens = Tokenizer.Tokenize(reader).ToList();
            var rpn = ShuntingYard(tokens);
            var rpnStr = string.Join(" ", rpn.Select(t => t.Value));
            tokens = rpn.ToList();
            return rpnStr;
        }
    }
}
