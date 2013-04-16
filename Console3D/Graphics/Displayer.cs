using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Console3D.Graphics
{
    class Displayer
    {
        public static void DisplayFrame(Frame frame)
        {
            string displayString = String.Empty;

            for (int i = 0; i < frame.Rows; i++ )
            {
                for (int j = 0; j < frame.Columns; j++ )
                {
                    displayString += frame[i, j].StringValue;
                }
                displayString += "\n";
            }

            System.Console.Write(displayString);
        }
    }
}
