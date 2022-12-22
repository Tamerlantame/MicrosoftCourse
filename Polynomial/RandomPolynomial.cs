using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arithmetics.Polynomial1
{
    class RandomPolynomial : ISequence<Polynomial>
    {
        public int deg;
        protected int minimumdeg = 0, maximumdeg, minimumcoeff = 0, maximumcoeff;
        public SortedList<int, double> coeff = new SortedList<int, double>();
        public RandomPolynomial()
        {
            maximumdeg = 10;
            maximumcoeff = 10;
            Random rnd = new Random();
            deg = rnd.Next(maximumdeg);
            Random rndcoeff = new Random();
            for (int i = 0; i < deg; i++)
            {
                coeff.Add(i, rndcoeff.Next(maximumcoeff));
            }
        }
        public RandomPolynomial(int maxdeg, int maxcoeff)
        {
            maximumdeg = maxdeg;
            maximumcoeff = maxcoeff;
            Random rnd = new Random();
            deg = rnd.Next(maximumdeg);
            Random rndcoeff = new Random();
            for (int i = 0; i < deg; i++)
            {
                coeff.Add(i, rndcoeff.Next(maxcoeff));
            }

        }
        public RandomPolynomial(int mindeg, int maxdeg, int mincoeff, int maxcoeff)
        {
            maximumdeg = maxdeg;
            maximumcoeff = maxcoeff;
            minimumdeg = mindeg;
            minimumcoeff = mincoeff;
            Random rnd = new Random();
            deg = rnd.Next(minimumdeg, maximumdeg);
            Random rndcoeff = new Random();
            for (int i = minimumdeg; i < maximumdeg; i++)
            {
                if (!coeff.ContainsKey(i))
                    coeff.Add(i, rndcoeff.Next(minimumcoeff, maximumcoeff));
                else
                    coeff[i] = rndcoeff.Next(minimumcoeff, maximumcoeff);
            }
        }
        public Polynomial Next()
        {
            RandomPolynomial randomPolynomial = new RandomPolynomial(minimumdeg, maximumdeg, minimumcoeff, maximumcoeff);
            Polynomial polynomial = new Polynomial(randomPolynomial.coeff);

            return polynomial;
        }
    }
}
