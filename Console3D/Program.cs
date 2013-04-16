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

            Matrix WorldMatrix = Matrix.IdentityMatrix(4, 4);

            float rotate = 0.0f;

            while ( true )
            {
                // ========================================================================
                //                                         Vertex Processing..... 
                // ========================================================================
                // Calculate the position for each vertex. 
                // Considering the object matrix, world matrix, view matrix and projection matrix.
                // Rotation and Translation happen here.
                rotate += .06f;
                Cube.RotateYAxis(rotate);
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
                // Determine the color of each pixel in each fragment
                // Here we do not implement the stage of processing, because I still don't know how 
                // to make a console app colorful.


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
