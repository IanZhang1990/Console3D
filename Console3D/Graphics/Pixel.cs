using System;
using System.Collections.Generic;
using System.Text;

namespace Console3D.Graphics
{
    class Pixel
    {
        public float NumericValue = 0.0f;

        public String StringValue
        {
            get{    return NumericValue == 0.0f ? "  " : (NumericValue == 1.0f? " #": " o");}
        }
    }
}
