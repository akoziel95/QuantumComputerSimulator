using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumComputer
{
    public static class Matrix
    {

        ///    1    [ 1  1 ]
        /// sqrt(2) [ 1 -1 ]
        public static double[][] HadamardBasic =>
            new double[][]
            {
                new double[] {1, 1},
                new double[] {1, -1}
            };
        public static double[][] IdentityTwo =>
               new double[][]
            {
                new double[] {1, 0},
                new double[] {0, 1}
            };

        public static double[][] TensorWithIdentity(double [][] toBeMultiplied,int length)
        {
            if (length == 0)
                return toBeMultiplied;
            return TensorWithIdentity(TensorProduct(toBeMultiplied, IdentityTwo), --length);
        }

        public static double[][] TensorProduct(double[][] matrix1, double[][] matrix2)
        {
            var rows = matrix1.Length * matrix2.Length;
            var res = InitalizeMatrix(rows, rows);

            for (int i = 0; i < matrix2.Length; i++)
            {
                for (int j = 0; j < matrix2[i].Length; j++)
                {
                    var prod = MultiplyByScalar(matrix1, matrix2[i][j]);
                    var topLeftCornerColumn = j * matrix1.Length;
                    var topLeftCornerRow = i * matrix1.Length;
                    for (int k = 0; k < matrix1.Length ; k++)
                    {
                        for (int m = 0; m < matrix1.Length; m++)
                        {
                            res[topLeftCornerRow + k][topLeftCornerColumn + m] = prod[k][m];
                        }
                    }

                }
            }
            return res;
        }
        public static double[][] MultiplyMatrices(double[][] matrix1, double[][] matrix2)
        {
            if (matrix1[0].Length != matrix2.Length)
                throw new ArgumentOutOfRangeException("Wrong matrix size, cannot tensor");
            var rows = matrix1.Length;
            var columns = matrix2[0].Length;
            var result = InitalizeMatrix(rows, columns);
            
            for (int i = 0; i <rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    for (int k = 0; k < matrix2.Length; k++)
                    {
                        var temp = matrix1[i][k] * matrix2[k][j];
                        result[i][j] += temp;
                    }
                }
            }
            return result;
        }
        public static double[] MultiplyVectorByMatrix(double[] vector, double[][] matrix)
        {
            return new double[1];
        }
        public static double[][] InitalizeMatrix(int rows, int columns)
        {
            var result = new double[rows][];
            for (int i = 0; i < rows; i++)
            {
                result[i] = new double[columns];
            }
            return result;
        }
        
        public static double[][] MultiplyByScalar(double [][]matrix, double scalar)
        {
            var res = InitalizeMatrix(matrix.Length, matrix[0].Length);
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    res[i][j] =  matrix[i][j] * scalar;
                }
            }
            return res;
        }

        
    }
}
