using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Arithmetics.Parsers
{
    //C# realization of Shunting-yard algorithm
    //used this git link
    //https://en.wikipedia.org/wiki/Abstract_syntax_tree
    //https://en.wikipedia.org/wiki/Shunting-yard_algorithm
    //https://web.archive.org/web/20110718214204/http://en.literateprograms.org/Shunting_yard_algorithm_(C)
    //https://gist.github.com/istupakov/c49ef290c3bc3dbe329bf68f67971470
    //https://www.codeproject.com/Tips/351042/Shunting-Yard-algorithm-in-Csharp
    enum TokenType { Number, Variable, Parenthesis, Operator, WhiteSpace };

    struct Token
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
    }
    class Operator : IComparable<Operator>
    {
        public string Name { get; set; }
        public int Precedence { get; set; }
        public bool RightAssociative { get; set; }

        public int CompareTo(Operator other)
        {
            return this.Precedence - other.Precedence;
        }
    }
    class PolynomialExpressionParser
    {
        private IDictionary<string, Operator> operators = new Dictionary<string, Operator>
        {
            ["+"] = new Operator { Name = "+", Precedence = 1 },
            ["-"] = new Operator { Name = "-", Precedence = 1 },
            ["*"] = new Operator { Name = "*", Precedence = 2 },
            ["/"] = new Operator { Name = "/", Precedence = 2 },
            ["^"] = new Operator { Name = "^", Precedence = 3, RightAssociative = true }

        };
        private bool CompareOperators(Operator op1, Operator op2)
        {
            return op1.RightAssociative ? op1.Precedence < op2.Precedence : op1.Precedence <= op2.Precedence;
        }
        private bool CompareOperators(string op1, string op2) => CompareOperators(operators[op1], operators[op2]);
        private TokenType DetermineType(char ch)
        {
            if (char.IsLetter(ch))
                return TokenType.Variable;
            if (char.IsDigit(ch))
                return TokenType.Number;
            if (char.IsWhiteSpace(ch))
                return TokenType.WhiteSpace;
            if (ch == '(' || ch == ')')
                return TokenType.Parenthesis;
            if (operators.ContainsKey(Convert.ToString(ch)))
                return TokenType.Operator;
            throw new Exception("Wrong character");
        }
        public IEnumerable<Token> Tokenize(string formula)//TextReader reader)
        {
            //formula = formula.Replace(" ", "");
            var token = new StringBuilder();

            //int curr;
            //while (
                foreach(char curr in formula)//(curr = reader.Read()) != -1)
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


        //private SortedList<string, Polynomial> polynomialList;
        //public PolynomialExpressionParser(SortedList<string,Polynomial> polynomials)
        //{
        //    polynomialList = new SortedList<string, Polynomial>(polynomials);
        //}

        //private Polynomial Culc(string str)
        //{

        //    int countleft = 0, countright = 0;
        //    for (int i = 0; i < str.Length; i++)
        //    {
        //        if (str[i] == '(')
        //            countleft++;
        //        if (str[i] == ')')
        //            countright++;
        //    }
        //    if (
        //        (countleft != countright)
        //        ||
        //        ((str.Contains("(")) && (!str.Contains(")")))
        //        ||
        //        ((str.Contains("(")) && (!str.Contains(")")))
        //        )
        //    {
        //        Console.WriteLine("ошибка со скобками"); // Exception для скобок
        //        return null;
        //    }
        //    if (countleft > 0)//часть кода для реализации скобок
        //    {
        //        int i = 0;
        //        while (str[i] != '(')
        //            i++;
        //        int k = str.Length;
        //        while (str[k] != ')')
        //            k++;
        //    }
        //    return Computation(str);

        //}
        //private Polynomial Computation(string str)
        //{
        //    string path = str;
        //    for (int i = 0; i < str.Length; i++)//цикл для выполнения оперaций первого приоритета
        //    {
        //        if (str[i] == '*')
        //        {
        //            int j = i;
        //            while ((str[j] != '+') || (str[j] != '-'))        ///  выделяю операнды при операции "*"
        //                j--;                                     ///  name1 и name2 это строки с
        //                                                         ///  названиями переменных слева и 
        //            string name1 = str.Substring(j, i - j);     ///  справа от "*" соответственно
        //            int k = i;
        //            while ((str[k] != '+') || (str[k] != '-'))
        //                k++;

        //            string name2 = str.Substring(i, i + k);
        //            string path1 = str.Substring(0, i - j);
        //            string path2 = str.Substring(i + k);
        //            Polynomial result;
        //            try
        //            {
        //                result = (polynomialList[name1] * polynomialList[name2]);
        //                if (!polynomialList.ContainsKey(result.ToString()))
        //                {
        //                    polynomialList.Add(result.ToString(), result);
        //                }
        //            }
        //            catch (KeyNotFoundException t)
        //            {
        //                throw t;
        //            }
        //            catch (Exception e)
        //            {
        //                throw e;
        //            }

        //        }
        //    }
        //    return null;
        //}
    }

}
