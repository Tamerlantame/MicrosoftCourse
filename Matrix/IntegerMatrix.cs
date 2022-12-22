using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;

namespace Arithmetics.Matrix
{
    public class IntegerMatrix : Matrix<int>
    {

        public IntegerMatrix(int n, int m) : base(n, m)
        {
        }

        public IntegerMatrix(Matrix<int> matrix) : base(matrix)
        {
        }

        public IntegerMatrix()
        {
        }

        public IntegerMatrix(int n, int m, int[,] coeff) : base(n, m, coeff)
        {
        }

        ///<summary>  Создание матрицы по ссылке на текстовый файл с матрицей; полагается, что матрица написана правильно
        ///
        /// 
        /// 
        ///TODO: Избавиться от этого конструктора. Не ясно, что должно быть в файле, в каком виде. Какие происходят ошибки???
        ///Это должно быть не здесь.
        ///

        [ObsoleteAttribute("Избавиться от этого конструктора.")]
        public IntegerMatrix(string path)
        {
            {
                GetFromFile(path);
            }
            
        }



        public void GetFromFile(string path)
        {

            try
            {
                string[] text = File.ReadAllLines(path);
                string[] RowElemnts;
                List<string[]> AllElements = new List<string[]>();
                RowElemnts = text[0].Split(' ');
                AllElements.Add(RowElemnts);
                int RowLength = RowElemnts.Length;
                for (int i = 1; i < text.Length; i++)
                {
                    RowElemnts = text[i].Split(' ');
                    if (RowElemnts.Length != RowLength)
                    {
                        throw new Exception();
                    }
                    AllElements.Add(RowElemnts);
                }
                //проверяем, чтобы все строки содержали одинаковое количество элементов, создаем список массивов строк, каждая строка в массиве-элемент матрицы
                elements = new int[text.Length, RowLength];
                foreach (string[] item in AllElements)
                {

                    for (int j = 0; j < RowElemnts.Length; j++)
                    {
                        
                            elements[AllElements.IndexOf(item), j] = Convert.ToInt32(item[j]);

                    }

                }
                Columns = RowLength;
                Rows = text.Length;
                
                 
                
            }
            catch
            {
                throw new Exception("Вы ввели что-то не то");
            }
        
    }
        public virtual bool IsSymmetric()
        {
            if (Rows != Columns)


            {
                return false;
            }
            else
            {
                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Columns; j++)
                    {
                        if (elements[i, j] != elements[j, i])
                        {
                            return false;
                        }
                    }
                }
                return true;
            }

        }

        public new IntegerMatrix GetTransposed()
        {
            IntegerMatrix matrix = new IntegerMatrix(Rows, Columns);
            Transpose(matrix);
            return matrix;
        }

        public static IntegerMatrix operator *(IntegerMatrix factor1, IntegerMatrix factor2)
        {

            if (factor1.Rows != factor2.Columns)//factor1.GetLength(1) число столбцов в 1 матрице 
            {
                return null;     //factor2.GetLength(0) число строк в 2 матрице
            }

            IntegerMatrix composition = new IntegerMatrix(factor1.Rows, factor2.Columns);

            for (int i = 0; i < factor1.Rows; i++)
            {
                for (int j = 0; j < factor2.Columns; j++)
                {
                    for (int k = 0; k < factor2.Rows; k++)
                    {
                        composition[i, j] += factor1[i, k] * factor2[k, j];
                    }
                }
            }
            return composition;
        }

        public static IntegerMatrix operator %(IntegerMatrix divinded, int mod)
        {
            IntegerMatrix result = new IntegerMatrix(divinded);
            for (int i = 0; i < divinded.Rows; i++)
            {
                for (int j = 0; j < divinded.Columns; j++)
                {
                    result[i, j] = divinded[i, j] % mod;
                }
            }

            return result;
        }
    }
}
