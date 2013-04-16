using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Console3D.Math;
using Console3D.Graphics;

namespace Console3D
{
    class Program
    {
        static void Main(string[] args)
        {
            // ========================================================================
            //                                         Preprocessing..... 
            // ========================================================================
            CubeModel Cube = new CubeModel();
            Camera Camera = new Camera(new Vector3(0 , 10, 50),
                                                        new Vector3(0,0,0),
                                                        new Vector3(0, 1, 0));

            //Matrix WorldMatrix = Matrix.IdentityMatrix(4,4);
            Matrix WorldMatrix = Matrix.IdentityMatrix(4, 4);


            float rotateDegree = 0.0f;

            /*
            System.Console.WriteLine("1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0");
            System.Console.WriteLine("2 # # # # # # # # # 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0");
            System.Console.WriteLine("3");
            System.Console.WriteLine("4");
            System.Console.WriteLine("5");
            System.Console.WriteLine("6");
            System.Console.WriteLine("7");
            System.Console.WriteLine("8");
            System.Console.WriteLine("9");
            System.Console.WriteLine("10");
            System.Console.WriteLine("11");
            System.Console.WriteLine("12");
            System.Console.WriteLine("13");
            System.Console.WriteLine("14");
            System.Console.WriteLine("15");
            System.Console.WriteLine("16");
            System.Console.WriteLine("17");
            System.Console.WriteLine("18");
            System.Console.WriteLine("19");
            System.Console.WriteLine("20");
            System.Console.WriteLine("21");
            System.Console.WriteLine("22");
            System.Console.WriteLine("23");
            System.Console.WriteLine("24");
            System.Console.WriteLine("25");
            System.Console.WriteLine("26");
            System.Console.WriteLine("27");
            System.Console.WriteLine("28");
            System.Console.WriteLine("29");
            System.Console.WriteLine("30");
            System.Console.WriteLine("31");
            System.Console.WriteLine("32");
            System.Console.WriteLine("33");
            System.Console.WriteLine("34");
            System.Console.WriteLine("35");
            System.Console.WriteLine("36");
            System.Console.WriteLine("37");
            System.Console.WriteLine("38");
            System.Console.WriteLine("39");
            System.Console.WriteLine("40");


            System.Console.WriteLine("\n\n{0}", System.Math.Round(1.5f));
            System.Console.WriteLine( System.Math.Round(1.44444f));
            System.Console.WriteLine( System.Math.Round(1.56666f));
            System.Console.WriteLine(System.Math.Round(1.66666f));
            */


            while ( true )
            {
                // ========================================================================
                //                                         Vertex Processing..... 
                // ========================================================================
                // Calculate the position for each vertex. 
                // Considering the object matrix, world matrix, view matrix and projection matrix.
                // Rotation and Translation happen here.
                rotateDegree += .06f;
                Cube.RotateYAxis(rotateDegree);
                Cube.ApplyTransformation(WorldMatrix, Camera.ViewMatrix, Camera.PerspectiveProjectionMatrix);


                // ========================================================================
                //                                          Rasterize....
                // ========================================================================
                // Interpolate each vertices, get the line between every vertices pair.
                // Then calculate each pixel's value
                Frame newFrame = new Frame();
                Cube.Rasterize(newFrame);


                // ========================================================================
                //                                     Fragment Processing....
                // ========================================================================
                // Determine the color of each pixel


                // ========================================================================
                //                                            Output....
                // ========================================================================
                // Show the 3D model in the screen
                Displayer Displayer = new Displayer();
                Displayer.DisplayFrame(newFrame);

                System.Threading.Thread.Sleep(80);
                System.Console.Clear();
            }
        }
    }
}
