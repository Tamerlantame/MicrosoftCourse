// verified by ususucsus. gadost' detected and destructed

using System;
using System.Collections;
using System.Collections.Generic;
using Arithmetics.Parsers;

namespace Arithmetics.Polynomial1
{
    public class Polynomial : IComparable<Polynomial>, IEnumerable<int>
    {
        private readonly SortedList<int, double> coeff;

        public int Deg { get; }

        public double this[int index]
        {
            get
            {
                if (coeff.ContainsKey(index))
                {
                    return coeff[index];
                }
                else return 0;
            }
        }

        /// <summary>
        /// If <paramref name="poly"/> is a correct representaion of a polynomial (i.e. a sum of monomials of the form either a, ax or ax^b for some integers a and b.), 
        /// constructs the corresponding polynomial. Otherwise the polynomial is initialized as 0. 
        /// </summary>
        /// <param name="poly">string representation of a polynomial</param>
        [Obsolete("Use Polynomial(SortedList<int, double> coeff) instead")]
        public Polynomial(string poly)
        {
            try
            {
                coeff = PolynomialParser.Parse(poly);
            }
            catch (InvalidPolynomialStringException)
            {
                coeff.Add(0, 0);
            }
            Deg = coeff.Keys[coeff.Keys.Count - 1];
        }

        public Polynomial()
        {
            Deg = 0;
            coeff = new SortedList<int, double>();
        }

        public Polynomial(int Deg)
        {
            this.Deg = Deg;
            coeff = new SortedList<int, double>();
            for (int i = 0; i < this.Deg + 1; i++)
            {
                coeff.Add(i, 0);
            }
        }

        public Polynomial(SortedList<int, double> coeff)
        {

            Deg = coeff.Keys[coeff.Count - 1];
            foreach (int deg in coeff.Keys)
            {
                this.coeff.Add(deg, coeff[deg]);
            }

        }

        public Polynomial(Polynomial a)
        {
            Deg = a.Deg;
            coeff = new SortedList<int, double>();
            foreach (int deg in a.coeff.Keys)
            {
                this.coeff.Add(deg, a.coeff[deg]);
            }

        }

        /// <summary>
        /// Пока что совсем не аккуратно.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string s = "";

            for (int i = Deg; i >= 0; i--)
            {
                if (this.coeff.ContainsKey(i))
                {
                    if (coeff[i] >= 0)
                    {
                        switch (i)
                        {
                            case 0:
                                if (coeff[i] != 0)
                                    s += "+" + coeff[i];
                                break;
                            case 1:
                                if (coeff[i] != 0)
                                {
                                    if (Deg >= 2)
                                    {
                                        if (coeff[i] == 1)
                                            s += "+x";
                                        else
                                            s += "+" + coeff[i] + "x";
                                    }
                                    else
                                    {
                                        if (coeff[i] == 1)
                                            s += "x";
                                        else
                                            s += coeff[i] + "x";
                                    }
                                }
                                break;
                            default:
                                if (coeff[i] != 0)
                                {
                                    if (i != this.coeff.Keys[this.coeff.Keys.Count - 1])
                                    {
                                        if (coeff[i] == 1)
                                            s += "+x^" + i;
                                        else
                                            s += "+" + coeff[i] + "x^" + i;
                                    }
                                    else
                                    {
                                        if (coeff[i] == 1)
                                            s += "x^" + i;
                                        else
                                            s += coeff[i] + "x^" + i;
                                    }
                                }
                                break;
                        }
                    }
                    else
                    {
                        switch (i)
                        {
                            case 0:
                                s += coeff[i];
                                break;
                            case 1:
                                if (coeff[i] == -1)
                                    s += "-x";
                                else
                                    s += coeff[i] + "x";

                                break;
                            default:
                                if (coeff[i] == -1)
                                    s += "-x^" + i;
                                else
                                    s += +coeff[i] + "x^" + i;

                                break;
                        }
                    }
                }
            }
            if (s == "")
                return "0";
            else
                return s;
        }

        public static Polynomial operator +(Polynomial p1, Polynomial p2)
        {
            SortedList<int, double> coeff = new SortedList<int, double>();
            foreach (int deg in p1.coeff.Keys)
            {
                coeff.Add(deg, p1.coeff[deg]);
            }
            foreach (int deg in p2.coeff.Keys)
            {
                if (coeff.ContainsKey(deg))
                    coeff[deg] += p2.coeff[deg];
                else
                    coeff.Add(deg, p2.coeff[deg]);
            }
            return new Polynomial(coeff);

        }

        public static Polynomial operator -(Polynomial p1, Polynomial p2)
        {
            SortedList<int, double> coeff = new SortedList<int, double>();
            foreach (int deg in p1.coeff.Keys)
            {
                coeff.Add(deg, p1.coeff[deg]);
            }
            foreach (int deg in p2.coeff.Keys)
            {
                if (coeff.ContainsKey(deg))
                    coeff[deg] -= p2.coeff[deg];
                else
                    coeff.Add(deg, p2.coeff[deg]);
            }
            return new Polynomial(coeff);
        }

        public static Polynomial operator *(Polynomial p1, Polynomial p2)
        {
            SortedList<int, double> coeff = new SortedList<int, double>();
            foreach (int deg1 in p1)
            {
                foreach (int deg2 in p2)
                {
                    if (!coeff.ContainsKey(deg1 + deg2))
                    {
                        coeff.Add(deg1 + deg2, p1[deg1] * p2[deg2]);
                    }
                    else
                    {
                        coeff[deg1 + deg2] += p1[deg1] * p2[deg2];
                    }
                }
            }
            return new Polynomial(coeff);
        }

        public static Polynomial operator *(Polynomial p1, int number)
        {
            SortedList<int, double> coeff = new SortedList<int, double>();
            foreach(int deg in p1.coeff.Keys)
            {
                coeff.Add(deg, p1.coeff[deg] * number);
            }
            return new Polynomial(coeff);
        }

        public static Polynomial operator *(int number, Polynomial p1)
        {
            return p1 * number;
        }
        public static Polynomial operator /(Polynomial p1, int number)
        {
            SortedList<int, double> coeff = new SortedList<int, double>();
            foreach (int deg in p1.coeff.Keys)
            {
                coeff.Add(deg, p1.coeff[deg] / number);
            }
            return new Polynomial(coeff);
        }
        public static bool operator ==(Polynomial p1, Polynomial p2)
        {
            foreach (int deg in p1.coeff.Keys)
            {
                if (p1[deg] != p2[deg])
                    return false;
            }
            foreach (int deg in p2.coeff.Keys)
            {
                if (p2[deg] != p1[deg])
                    return false;
            }
            return true;
        }

        public static bool operator !=(Polynomial p1, Polynomial p2)
        {
            return !(p1==p2);
        }

        public override bool Equals(object obj)
        {
            return obj is Polynomial polynomial &&
                   EqualityComparer<SortedList<int, double>>.Default.Equals(coeff, polynomial.coeff) &&
                   Deg == polynomial.Deg;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        //TODO подумать как реализовать CompareTo более детально
        public int CompareTo(Polynomial a)
        {
            if (this.Deg != a.Deg)
                return this.Deg.CompareTo(a.Deg);
            else
                return this.coeff[this.Deg].CompareTo(a.coeff[a.Deg]);

        }

        public IEnumerator<int> GetEnumerator()
        {

            return coeff.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return coeff.Keys.GetEnumerator();
        }
    }
}