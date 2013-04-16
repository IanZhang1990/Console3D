using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Console3D.Graphics
{
    /// <summary>
    /// Frame 
    /// A frame is actually a string that contains 40 lines and each line has 50 characters with a space before each character.
    /// A character with a space before it is called a PIXEL:
    ///            [ #]
    /// I will use a # symbol to display a point.
    /// It works fine in my computer.
    /// </summary>
    class Frame
    {
        /// <summary>
        /// The coordinate system is like:
        ///   Y
        ///  8|
        ///  7|
        ///  6|
        ///  5|
        ///  4|
        ///  3|
        ///  2|
        ///  1 |
        ///0 -|----------------------------- X
        ///     0 1 2 3 4 5 6 7 8 9 10
        /// </summary>
        public Pixel[,] PixelMatrix = new Pixel[40, 50];


        public Frame()
        {
            for (int i = 0; i < this.Rows; i++ )
            {
                for (int j = 0; j < this.Columns; j++ )
                {
                    PixelMatrix[i, j] = new Pixel();
                }
            }
        }

        /// <summary>
        /// Set the pixel value
        /// </summary>
        /// <param name="x">the X position</param>
        /// <param name="y">the Y position</param>
        public void SetPixelValue(int x, int y, float value)
        {
            if ( x >= Columns || x < 0 || y >= Rows || y < 0   )
            {
                return;
            }


            PixelMatrix[ Rows-1-y , x ].NumericValue = value;
        }

        public Pixel this[int irow, int icol]
        {
            get 
            {
                if (irow >= 0 && irow < this.Rows && icol >= 0 && icol < this.Columns)
                    return this.PixelMatrix[irow, icol];
                else
                    return null;
            }
            set { this.PixelMatrix[irow, icol] = value; }
        }

        public int Rows = 40;
        public int Columns = 50;
    }
}
