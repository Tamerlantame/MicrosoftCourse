using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Arithmetics.Matrix
{
    public class IntegerSquareMatrix : IntegerMatrix
    {
        public IntegerSquareMatrix() : base()
        {
        }
        public IntegerSquareMatrix(int n) : base(n, n)
        {
        }
        public IntegerSquareMatrix(int n, int[,] coeff) : base(n, n, coeff)
        {
        }

        public IntegerSquareMatrix(IntegerSquareMatrix matrix) : base(matrix)
        {

        }

        /// <summary>
        /// Returns an Identity <paramref name="n"/> × <paramref name="n"/> matrix.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static IntegerSquareMatrix GetE(int n)
        {
            IntegerSquareMatrix E = new IntegerSquareMatrix(n);
            for (int k = 0; k < n; k++)
                E[k, k] = 1;
            return E;
        }

        //TODO Если из файла, то надо проверять, что в файле, квадратная ли?
        
        public IntegerSquareMatrix(string path) : base(path)
        {

        }
        
        public override bool IsSymmetric()
        {
            return base.IsSymmetric();
        }

        public new IntegerSquareMatrix GetTransposed()
        {
            IntegerSquareMatrix matrix = new IntegerSquareMatrix(Rows);
            Transpose(matrix);
            return matrix;
        }
        public static IntegerSquareMatrix operator *(IntegerSquareMatrix factor1, IntegerSquareMatrix factor2)
        {
            IntegerSquareMatrix composition = new IntegerSquareMatrix(factor1.Rows);

            for (int i = 0; i < factor1.Rows; i++)
            {
                for (int j = 0; j < factor2.Columns; j++)
                {
                    for (int k = 0; k < factor2.Rows; k++)
                        composition[i, j] += factor1[i, k] * factor2[k, j];

                }
            }
            return composition;
        }

        public static IntegerSquareMatrix operator %(IntegerSquareMatrix divinded, int mod) => divinded % mod;

        /// <summary>
        /// Repeated Squaring algorithm for <paramref name="deg"/> power of the matrix modulo <paramref name="mod"/>
        /// </summary>
        /// <param name="deg"></param>
        /// <param name="mod"></param>
        /// <returns></returns>
        public IntegerSquareMatrix ModPow(long deg, int mod)
        {
            IntegerSquareMatrix Result = GetE(Columns);
            IntegerSquareMatrix Squared = new IntegerSquareMatrix(this);
            while (deg > 0)
            {
                if (deg % 2 == 1)
                {
                    Result = (Result * Squared) % mod;
                }
                Squared = (Squared * Squared) % mod;
                deg /= 2;
            }
            return Result;
        }
        public IntegerSquareMatrix SubtractionOfLines(int reducedLine, int deductibleline)
        {
            IntegerSquareMatrix newMatrix = new IntegerSquareMatrix(Rows, elements);
            for (int i = 0; i < Rows; i++)
            {
                newMatrix[reducedLine, i] -= newMatrix[deductibleline, i];
            }
            return newMatrix;
        }
        public IntegerSquareMatrix AdditionOfLines(int summline, int termline)
        {
            IntegerSquareMatrix newMatrix = new IntegerSquareMatrix(Rows, elements);
            for (int i = 0; i < Rows; i++)
            {
                newMatrix[summline, i] += newMatrix[termline, i];
            }
            return newMatrix;
        }
        public IntegerSquareMatrix SwapLine(int line1, int line2)
        {
            IntegerSquareMatrix newMatrix = new IntegerSquareMatrix(Rows, elements);
            int temp;
            for (int i = 0; i < Rows; i++)
            {
                temp = newMatrix[line1, i];
                newMatrix[line1, i] = newMatrix[line2, i];
                newMatrix[line2, i] = temp;
            }
            return newMatrix;

        }
        public IntegerSquareMatrix ToTopTriangleMatrixModPrime(int prime_number)
        {
            IntegerSquareMatrix triangleMatrix = new IntegerSquareMatrix(Rows, elements);
            int sign_of_det = 0;
            for (int i = 0; i < triangleMatrix.Rows; i++)
            {
                for (int j = i; j < triangleMatrix.Columns; j++)
                {
                    if (triangleMatrix[j, i] != 0)
                    {
                        int reverselement = NumberFunctions.revers_element_mod_prime(triangleMatrix[j, i], prime_number);
                        for (int k = j; k < triangleMatrix.Columns; k++)
                        {
                            triangleMatrix[j, k] *= reverselement;
                            triangleMatrix[j, k] %= prime_number;
                        }
                        if (i != j)
                        {
                            triangleMatrix = triangleMatrix.SwapLine(j, i);
                            sign_of_det++;
                            sign_of_det %= 2;                                            //ОПРЕДЕЛЯЮ ЗНАК det
                        }
                        for (int k = j + 1; k < triangleMatrix.Columns; k++)
                        {
                            while (triangleMatrix[k, j] != 0)
                            {
                                if (triangleMatrix[k, j] > 0)
                                    triangleMatrix = triangleMatrix.SubtractionOfLines(k, j);
                                else
                                    triangleMatrix = triangleMatrix.AdditionOfLines(k, j);
                            }
                        }
                    }


                }
            }
            return triangleMatrix;
        }
        public int DetMod(int prime_number)
        {
            IntegerSquareMatrix triangleMatrix = new IntegerSquareMatrix(Rows, elements);
            int signOfDet = 0;
            for (int i = 0; i < triangleMatrix.Rows; i++)
            {
                for (int j = i; j < triangleMatrix.Columns; j++)
                {
                    if (triangleMatrix[j, i] != 0)
                    {
                        int reverselement = NumberFunctions.revers_element_mod_prime(triangleMatrix[j, i], prime_number);
                        for (int k = j; k < triangleMatrix.Columns; k++)
                        {
                            triangleMatrix[j, k] *= reverselement;
                            triangleMatrix[j, k] %= prime_number;
                        }
                        if (i != j)
                        {
                            triangleMatrix = triangleMatrix.SwapLine(j, i);
                            signOfDet++;
                            signOfDet %= 2;                                            //ОПРЕДЕЛЯЮ ЗНАК det
                        }
                        for (int k = j + 1; k < triangleMatrix.Columns; k++)
                        {
                            while (triangleMatrix[k, j] != 0)
                            {
                                if (triangleMatrix[k, j] > 0)
                                    triangleMatrix = triangleMatrix.SubtractionOfLines(k, j);
                                else
                                    triangleMatrix = triangleMatrix.AdditionOfLines(k, j);
                            }
                        }
                    }


                }
            }
            int det = 1;
            for (int i = 0; i < triangleMatrix.Columns; i++)
            {
                det *= triangleMatrix[i, i];
            }
            if (signOfDet == 0)
                return det;
            else
                return -1 * det;

        }

        public IntegerSquareMatrix ToTopTriangleMatrixModPrime(int prime_number1, int degree)
        {
            IntegerSquareMatrix triangleMatrix = new IntegerSquareMatrix(Rows, elements);
            int sign_of_det = 0;
            int prime_number = NumberFunctions.FastPow(prime_number1, degree);
            for (int i = 0; i < triangleMatrix.Rows; i++)
            {
                for (int j = i; j < triangleMatrix.Columns; j++)
                {
                    if (triangleMatrix[j, i] != 0)
                    {
                        int reverselement = NumberFunctions.revers_element_mod_prime(triangleMatrix[j, i], prime_number);
                        for (int k = j; k < triangleMatrix.Columns; k++)
                        {
                            triangleMatrix[j, k] *= reverselement;
                            triangleMatrix[j, k] %= prime_number;
                        }
                        if (i != j)
                        {
                            triangleMatrix = triangleMatrix.SwapLine(j, i);
                            sign_of_det++;
                            sign_of_det %= 2;                                            //ОПРЕДЕЛЯЮ ЗНАК det
                        }
                        for (int k = j + 1; k < triangleMatrix.Columns; k++)
                        {
                            while (triangleMatrix[k, j] != 0)
                            {
                                if (triangleMatrix[k, j] > 0)
                                    triangleMatrix = triangleMatrix.SubtractionOfLines(k, j);
                                else
                                    triangleMatrix = triangleMatrix.AdditionOfLines(k, j);
                            }
                        }
                    }


                }
            }
            return triangleMatrix;
        }

        public IntegerSquareMatrix AddColomnsAndRows(int[] new_colomns, int[] new_rows)
        {
            int[,] newElements = new int[Rows + 1, Columns + 1];
            for (int i = 0; i < Rows + 1; i++)
            {
                for (int j = 0; j < Columns + 1; j++)
                {
                    if ((j != Rows) && (i != Columns))
                        newElements[i, j] = elements[i, j];
                    else
                    {
                        if ((j == Rows) && (i != Columns))
                            newElements[i, j] = new_colomns[i];
                        else
                        {
                            if ((j != Rows) && (i == Columns))
                                newElements[i, j] = new_rows[j];
                            else
                                newElements[i, j] = new_colomns[i] + new_rows[j];
                        }
                    }

                }
            }
            IntegerSquareMatrix matrix = new IntegerSquareMatrix(Rows + 1, newElements);

            return matrix;
        }

    }
}


