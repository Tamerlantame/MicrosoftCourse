using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftCourse.Parser
{
    public enum TokenType { Number, Variable, Function, Parenthesis, Operator, Comma, WhiteSpace };
    public enum FunctionType { Unary, Binary };

    public struct Token
    {
        public TokenType Type { get; }
        public string Value { get; }
        public override string ToString()
        {
            return $"{Type}: {Value}";
        }
        public Token(TokenType type, string value)
        {
            Type = type;
            Value = value;
        }
        public override bool Equals( object? obj)
        {
            var type = obj.ToString().Split(": ")[0];
            var value = obj.ToString().Split(": ")[1];
            obj.ToString();
            if (this.Type.ToString() != type)
                return false;
            if (this.Value.ToString() != value)
                return false;
            return true;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }



    public class Tokenizer
    {
        private static IDictionary<string, Operator> operators = Operator.GetOperators();
        private static TokenType DetermineType(char ch)
        {
            if (char.IsLetter(ch) && ch != 'x')
                return TokenType.Variable;
            if (char.IsDigit(ch) || ch == '^' || ch == 'x')
                return TokenType.Number;
            if (char.IsWhiteSpace(ch))
                return TokenType.WhiteSpace;
            if (ch == ',')
                return TokenType.Comma;
            if (ch == '(' || ch == ')')
                return TokenType.Parenthesis;
            if (operators.ContainsKey(Convert.ToString(ch)))
                return TokenType.Operator;

            throw new Exception("Wrong character");
        }

        public static IEnumerable<Token> Tokenize(TextReader reader)
        {
            var token = new StringBuilder();

            int curr;
            while ((curr = reader.Read()) != -1)
            {
                var ch = (char)curr;
                var currType = DetermineType(ch);
                if (currType == TokenType.WhiteSpace)
                    continue;

                token.Append(ch);

                var next = reader.Peek();
                var nextType = next != -1 ? DetermineType((char)next) : TokenType.WhiteSpace;
                if (currType != nextType)
                {
                    if (next == '(')
                        yield return new Token(TokenType.Function, token.ToString());
                    else
                        yield return new Token(currType, token.ToString());
                    token.Clear();
                }
            }

        }
    }
}
