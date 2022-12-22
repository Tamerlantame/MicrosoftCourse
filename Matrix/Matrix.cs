using System;

namespace Arithmetics.Matrix
{
    public class Matrix<T>
    {
        protected T[,] elements;
        public  int Rows { get; set; }
        public int Columns { get; set; }

        public T this[int index0, int index1]
        {
            get
            {
                if ((elements.GetLength(0) < index0) && (elements.GetLength(1) < index1))
                {
                    return elements[index0, index1];
                }
                else return default;
            }
            set => elements[index0, index1] = value;
        }
        public Matrix(int n, int m)
        {
            elements = new T[n, m];
            Rows = n;
            Columns = m;

        }
        public Matrix()
        {
            elements = new T[0, 0];
            Rows = 0;
            Columns = 0;

        }
        public Matrix(T[,] coeff)
        {
            Rows = coeff.GetLength(0);
            Columns = coeff.GetLength(1);
            elements = new T[Rows, Columns];
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    elements[i, j] = coeff[i, j];
                }
            }
        }
        public Matrix(Matrix<T> matrix)
        {
            Rows = matrix.Rows;
            Columns = matrix.Columns;
            elements = new T[matrix.Rows, matrix.Columns];
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    elements[i, j] = matrix.elements[i, j];
                }
            }
        }
       
        public Matrix(int n, int m, T[,] coeff)
        {
            Rows = n;
            Columns = m;
            elements = new T[n, m];
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    elements[i, j] = coeff[i, j];
                }
            }
        }

        public void Transpose(Matrix<T> matrix)
        {
            T temp;
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = i; j < matrix.Columns; j++)
                {
                    temp = matrix[i, j];
                    matrix[i, j] = matrix[j, i];
                    matrix[j, i] = temp;
                }
            }
        }
        public Matrix<T> GetTransposed()
        {
            Matrix<T> matrix = new Matrix<T>(Rows, Columns);
            Transpose(matrix);
            return matrix;
        }

        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    s += Convert.ToString(elements[i, j]) + " ";

                }
                s += "\n";

            }
            return s;
        }
    }
}
