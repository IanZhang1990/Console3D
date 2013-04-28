using System;
using System.Collections.Generic;
using System.Text;

namespace Console3D.Math
{
    class Matrix
    {
        public int Rows;
        public int Columns;
        private float[,] matrix;

        public Matrix(int rows, int cols)
        {
            this.Rows = rows;
            this.Columns = cols;
            matrix = new float[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    matrix[i, j] = 0;
        }

        public float this[int irow, int icol]
        {
            get { return matrix[irow, icol]; }
            set { matrix[irow, icol] = value; }
        }

        /// <summary>
        /// Get a Zero Matrix
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        /// <returns></returns>
        public static Matrix ZeroMatrix(int rows, int cols)
        {
            Matrix mat = new Matrix(rows, cols);

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    mat[i, j] = 0;

            return mat;
        }


        /// <summary>
        /// Generates an identity matrix
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        /// <returns></returns>
        public static Matrix IdentityMatrix(int rows, int cols)
        {
            Matrix mat = ZeroMatrix(rows, cols);

            for (int i = 0; i < System.Math.Min(rows, cols); i++)
                mat[i, i] = 1;

            return mat;
        }

        /// <summary>
        /// Generates a random matrix
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        /// <param name="disp">dispersion. Random number range: [- disp, + disp]</param>
        /// <returns></returns>
        public static Matrix RandomMatrix(int rows, int cols, int disp)
        {
            Random random = new Random();
            Matrix mat = new Matrix(rows, cols);

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    mat[i, j] = random.Next(-disp, disp);

            return mat;
        }

        /// <summary>
        /// A stupid matrix multiplication
        /// </summary>
        /// <param name="m1"></param>
        /// <param name="m2"></param>
        /// <returns></returns>
        public static Matrix Multiply(Matrix m1, Matrix m2)
        {
            if (m1.Columns != m2.Rows)
                throw new Exception("Wrong dimensions of matrix!");

            Matrix result = ZeroMatrix(m1.Rows, m2.Columns);

            for (int i = 0; i < result.Rows; i++)
                for (int j = 0; j < result.Columns; j++)
                    for (int k = 0; k < m1.Columns; k++)
                        result[i, j] += m1[i, k] * m2[k, j];
            return result;
        }

        /// <summary>
        /// Times a constant n
        /// </summary>
        /// <param name="mat"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static Matrix Times(Matrix mat, float n)
        {
            Matrix r = new Matrix(mat.Rows, mat.Columns);

            for (int i = 0; i < mat.Rows; i++)
                for (int j = 0; j < mat.Columns; j++)
                    r[i, j] = mat[i, j] * n;

            return r;
        }

        /// <summary>
        /// Mat1 + Mat2
        /// </summary>
        /// <param name="mat1"></param>
        /// <param name="mat2"></param>
        /// <returns></returns>
        public static Matrix Add(Matrix mat1, Matrix mat2)
        {
            if (mat1.Rows != mat2.Rows || mat1.Columns != mat2.Columns)
                throw new Exception("Matrices must have the same dimensions!");

            Matrix r = new Matrix(mat1.Rows, mat1.Columns);

            for (int i = 0; i < r.Rows; i++)
                for (int j = 0; j < r.Columns; j++)
                    r[i, j] = mat1[i, j] + mat2[i, j];
            return r;
        }

        public static Matrix operator -(Matrix mat)
        { return Matrix.Times(mat, -1.0f); }

        public static Matrix operator +(Matrix mat1, Matrix mat2)
        { return Matrix.Add(mat1, mat2); }

        public static Matrix operator -(Matrix mat1, Matrix mat2)
        { return Matrix.Add(mat1, Matrix.Times(mat2, -1.0f)); }

        public static Matrix operator *(Matrix mat1, Matrix mat2)
        { return Matrix.Multiply(mat1, mat2); }

        public static Matrix operator *(float n, Matrix mat)
        { return Matrix.Times(mat, n); }
     }

}
