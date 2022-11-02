using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftCourse.Parser
{
    public class Operator : IComparable<Operator>
    {
        private delegate double BinaryFunc(double leftOp, double rightOp);


        private BinaryFunc BiOperator;

        
        public string? Name { get; private set; }
        public int Precedence { get; private set; }
        public bool RightAssociative { get; private set; }

        /// <summary>
        /// return IDictionary containing operators
        /// </summary>
        /// <returns></returns>
        public static IDictionary<string, Operator> GetOperators()
        {
            IDictionary<string, Operator> operators = new Dictionary<string, Operator>
            {
                ["+"] = new Operator { Name = "+", Precedence = 1, BiOperator = ((double x, double y) => x + y) },
                ["-"] = new Operator { Name = "-", Precedence = 1, BiOperator = ((double x, double y) => x - y) },
                ["*"] = new Operator { Name = "*", Precedence = 2, BiOperator = ((double x, double y) => x * y) },
                ["/"] = new Operator { Name = "/", Precedence = 2, BiOperator = ((double x, double y) => x / y) },
                //["^"] = new Operator { Name = "^", Precedence = 3, RightAssociative = true, function = ((double x, double y) => Math.Pow(x, y))}
            };
            return operators;
        }
        public int CompareTo(Operator other)
        {
            return this.Precedence - other.Precedence;
        }
    }
}
