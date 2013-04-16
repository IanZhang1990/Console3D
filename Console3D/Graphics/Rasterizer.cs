using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Console3D.Math;

namespace Console3D.Graphics
{
    class Rasterizer
    {
        /// <summary>
        /// Rasterize Line between vertex1 and vertx2
        /// </summary>
        /// <param name="frame">the display frame</param>
        /// <param name="vertex1">vertex 1</param>
        /// <param name="vertex2">vertex 2</param>
        public void RasterizeLine(Frame frame, Vector3 vertex1, Vector3 vertex2)
        {
            //   Y
            //   ^                  o  B  (vertex2)
            //   |                /            if ( B.x - A.x != 0 )
            //   |              /                     y = ax + b
            //   |            /                       a = (B.y - A.y)/(B.x - A.x)
            //   |          /                         b = B.y - a * B.x
            //   |        /                     else x = A.x,     A.y  < y < B.y
            //   |      /
            //   |   o  A (vertex1)
            //--|-----------------------------> X
            //    O

            // vertex1 is the begin point and vertex2 is the end point
            // And vertex1.x, vertex1.y, vertex2.x, vertex2.y are all integers 
            vertex1.X = (int)System.Math.Round(vertex1.X);
            vertex1.Y = (int)System.Math.Round(vertex1.Y);
            vertex2.X = (int)System.Math.Round(vertex2.X);
            vertex2.Y = (int)System.Math.Round(vertex2.Y);

            if (vertex2.X - vertex1.X != 0)
            {
                float a = (vertex2.Y - vertex1.Y) / (vertex2.X - vertex1.X);
                float b = vertex2.Y - a * vertex2.X;

                Vector3 tempPoint = new Vector3( vertex1.X, vertex1.Y, 0 );

                while (tempPoint.X != vertex2.X || tempPoint.Y != vertex2.Y)
                {
                    if (tempPoint.X != vertex2.X)
                    {
                        float nextX = (vertex2.X - vertex1.X > 0 ? tempPoint.X + 1 : tempPoint.X - 1);
                        tempPoint.X = nextX;
                    }
                    if (tempPoint.Y != vertex2.Y)
                    {
                        float nextY = a * tempPoint.X + b;
                        tempPoint.Y = (int)System.Math.Round(nextY);
                    }

                    frame.SetPixelValue((int)tempPoint.X, (int)tempPoint.Y, 2.0f);
                }

                
                // To avoid Y pixel lost
                tempPoint = new Vector3( vertex1.X, vertex1.Y, 0 );
                if ( a != 0 )
                {
                    while (tempPoint.X != vertex2.X || tempPoint.Y != vertex2.Y)
                    {
                        if (tempPoint.Y != vertex2.Y)
                        {
                            float nextY = (vertex2.Y - vertex1.Y > 0 ? tempPoint.Y + 1 : tempPoint.Y - 1);
                            tempPoint.Y = nextY;
                        }
                        if (tempPoint.X != vertex2.X)
                        {
                            float nextX = (tempPoint.Y - b) / a;
                            tempPoint.X = (int)System.Math.Round(nextX);
                        }

                        frame.SetPixelValue((int)tempPoint.X, (int)tempPoint.Y, 2.0f);
                    }
                }                
            }
            else   //vertex2.X - vertex1.X == 0
            {
                Vector3 tempPoint = new Vector3(vertex1.X, vertex1.Y, 0);
                while (tempPoint.Y != vertex2.Y)
                {
                    float nextY = vertex2.Y - vertex1.Y > 0 ? tempPoint.Y + 1 : tempPoint.Y - 1;
                    tempPoint.Y = (int)System.Math.Round(nextY);
                    frame.SetPixelValue((int)tempPoint.X, (int)tempPoint.Y, 2.0f);
                }
            }

            frame.SetPixelValue((int)vertex1.X, (int)vertex1.Y, 1.0f);
            frame.SetPixelValue((int)vertex2.X, (int)vertex2.Y, 1.0f);
        }

        public void RasterizePointis(Frame frame, Vector3[] vertices)
        {
            foreach (Vector3 vertex in vertices)
            {
                int x = (int)System.Math.Round(vertex.X);
                int y = (int)System.Math.Round(vertex.Y);
                frame.SetPixelValue(x, y, 1.0f);
            }
        }
    }
}
