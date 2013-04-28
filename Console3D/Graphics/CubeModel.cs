using System;
using System.Collections.Generic;
using System.Text;
using Console3D.Math;

namespace Console3D.Graphics
{
    /// <summary>
    ///                1                   2
    ///                o-------------o
    ///              /|            4   /|
    ///         3 o--|---------- o  |
    ///            |   o --------- |- o  6
    ///            | /  5             |/
    ///            o------------- o
    ///            7                   8
    /// </summary>
    class CubeModel
    {
        public Vector3[] Vertices = new Vector3[8];
        public Vector3[] tempVertices = new Vector3[8];
        public Vector3[] wvpVertices = new Vector3[8];

        public CubeModel()
        {
            Vertices[0] = new Vector3(14, 20, 0);
            Vertices[1] = new Vector3(34, 20, 0);
            Vertices[2] = new Vector3(14, 20, 20);
            Vertices[3] = new Vector3(34, 20, 20);
            Vertices[4] = new Vector3(10-4, 0, 0);
            Vertices[5] = new Vector3(30-4, 0, 0);
            Vertices[6] = new Vector3(10-4, 0, 20);
            Vertices[7] = new Vector3(30-4, 0, 20);

            wvpVertices[0] = new Vector3(0, 0, 0);
            wvpVertices[1] = new Vector3(0, 0, 0);
            wvpVertices[2] = new Vector3(0, 0, 0);
            wvpVertices[3] = new Vector3(0, 0, 0);
            wvpVertices[4] = new Vector3(0, 0, 0);
            wvpVertices[5] = new Vector3(0, 0, 0);
            wvpVertices[6] = new Vector3(0, 0, 0);
            wvpVertices[7] = new Vector3(0, 0, 0);


            for (int i = 0; i < Vertices.Length; i++)
            {
                tempVertices[i] = new Vector3(Vertices[i].X, Vertices[i].Y,Vertices[i].Z);
            }
        }


        public void RotateYAxis(float degree)
        {
            for( int i = 0; i < Vertices.Length; i++)
            {
                Vector3 transVert = new Vector3(Vertices[i].X - 20, Vertices[i].Y, Vertices[i].Z-10);
                tempVertices[i] = Vector3.YAxisRotate(degree, transVert);                
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="world"></param>
        /// <param name="view"></param>
        /// <param name="projection"></param>
        public void ApplyTransformation(Matrix world, Matrix view, Matrix projection)
        {
            this.ApplyTransformation(projection * view * world);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="WVP"></param>
        public void ApplyTransformation(Matrix WVP)
        {
            for (int i = 0; i < this.Vertices.Length; i++ )
            {
                Matrix tempMat = tempVertices[i].ToMatrix();
                tempMat = WVP * tempMat;
                wvpVertices[i].X = tempMat[0, 0]; wvpVertices[i].Y = tempMat[1, 0];

                // Translate the vertex
                wvpVertices[i].X += 24;
                wvpVertices[i].Y += 5;

                tempVertices[i] = new Vector3(Vertices[i].X, Vertices[i].Y, Vertices[i].Z);
            }
        }

        public void Rasterize(Frame frame)
        {
            Rasterizer rster = new Rasterizer();

            rster.RasterizePointis(frame, wvpVertices);

            
            rster.RasterizeLine(frame, wvpVertices[0], wvpVertices[1]);        // line 1->2
            rster.RasterizeLine(frame, wvpVertices[0], wvpVertices[2]);        // line 1->3
            rster.RasterizeLine(frame, wvpVertices[0], wvpVertices[4]);        // line 1->5

            rster.RasterizeLine(frame, wvpVertices[1], wvpVertices[3]);        // line 2->4
            rster.RasterizeLine(frame, wvpVertices[1], wvpVertices[5]);        // line 2->6

            rster.RasterizeLine(frame, wvpVertices[2], wvpVertices[3]);        // line 3->4
            rster.RasterizeLine(frame, wvpVertices[2], wvpVertices[6]);        // line 3->4

            rster.RasterizeLine(frame, wvpVertices[3], wvpVertices[7]);        // line 4->8

            rster.RasterizeLine(frame, wvpVertices[4], wvpVertices[6]);        // line 5->7
            rster.RasterizeLine(frame, wvpVertices[4], wvpVertices[5]);        // line 5->6

            rster.RasterizeLine(frame, wvpVertices[5], wvpVertices[7]);        // line 6->8

            rster.RasterizeLine(frame, wvpVertices[6], wvpVertices[7]);        // line 7->8
            
        }


    }
}
