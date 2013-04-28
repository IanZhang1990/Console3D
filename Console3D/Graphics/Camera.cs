using System;
using System.Collections.Generic;
using Console3D.Math;

//////////////////////////////////////////////////////////////////////////
// Coordinate System:
//                                   Y
//                                   |
//                                   | 
//                                   |
//                                   | 
//                                   |-------------------     X
//                                 / O
//                               /
//                             /
//                           /
//                         Z 
//////////////////////////////////////////////////////////////////////////


namespace Console3D.Graphics
{
    class Camera
    {
        /// <summary>
        /// Constructor of the class
        /// After the construction, we can get a View Matrix.
        /// The view matrix transforms the world coordinates system into the coordinate system of the camera.
        /// </summary>
        /// <param name="eyePos">the position of the camera</param>
        /// <param name="lookat">the target position, which is the point the camera is looking at.</param>
        /// <param name="up">up vector, which defines how we see the target.</param>
        public Camera(Vector3 eyePos, Vector3 lookat, Vector3 up)
        {
            this.ViewMatrix = ConstructViewMatrix(eyePos, lookat, up);
            this.PerspectiveProjectionMatrix = ConstructPerspectiveProjectionMatrix(70, 50.0f/40.0f, 1.0f, 300.0f);
        }

        /// <summary>
        /// After the construction, we can get a View Matrix.
        /// The view matrix transforms the world coordinates system into the coordinate system of the camera.
        /// </summary>
        /// <param name="eyePos">the position of the camera</param>
        /// <param name="lookat">the target position, which is the point the camera is looking at.</param>
        /// <param name="up">up vector, which defines how we see the target.</param>
        /// <returns></returns>
        private Matrix ConstructViewMatrix(Vector3 eyePos, Vector3 lookat, Vector3 up)
        {
            // Reference: http://3dgep.com/?p=1700
            // Titled: Understanding the view matrix

            Vector3 ZAxis = Vector3.Norm(eyePos - lookat);                                          // The "look-at" vector.
            Vector3 XAxis = Vector3.Norm(Vector3.Cross(up, ZAxis));                           // The "right" vector.
            Vector3 YAxis = Vector3.Norm(Vector3.Cross(ZAxis, XAxis));                      // The "up" vector.

            // Create a 4x4 orientation matrix from the right, up, and at vectors
            Matrix orientation = new Matrix(4, 4);
            orientation[0, 0] = XAxis.X; orientation[0, 1] = YAxis.X; orientation[0, 2] = ZAxis.X; orientation[0, 3] = 0.0f;
            orientation[1, 0] = XAxis.Y;  orientation[1, 1] = YAxis.Y; orientation[1, 2] = ZAxis.Y;  orientation[1, 3] = 0.0f;
            orientation[2, 0] = XAxis.Z; orientation[2, 1] = YAxis.Z; orientation[2, 2] = ZAxis.Z; orientation[2, 3] = 0.0f;
            orientation[3, 0] = 0;           orientation[3, 1] = 0;           orientation[3, 2] = 0            ; orientation[3, 3] = 1.0f;

            // Create a 4x4 translation matrix by negating the eye position.
            Matrix translation = new Matrix(4, 4);
            translation[0, 0] = 1.0f;         translation[0, 1] = 0.0f;         translation[0, 2] = 0.0f;        translation[0, 3] = 0.0f;
            translation[1, 0] = 0.0f;         translation[1, 1] = 1.0f;          translation[1, 2] = 0.0f;         translation[1, 3] = 0.0f;
            translation[2, 0] = 0.0f;         translation[2, 1] = 0.0f;         translation[2, 2] = 1.0f;         translation[2, 3] = 0.0f;
            translation[3, 0] = -eyePos.X; translation[3, 1] = -eyePos.Y; translation[3, 2] = -eyePos.Z; translation[3, 3] = 1.0f;

            // Combine the orientation and translation to compute the view matrix
            return (translation * orientation);
        }

        /// <summary>
        /// Construct the Perspective Projection Matrix.
        /// The matrix will transform a 3D point from the space into the near clip plane.
        /// Which means a vector is translated from 3D to 2D.
        ///                                  /   |
        ///                          /           |
        ///                  /   |               |
        ///   eye  /           |               |
        ///       o    ) fov   |               |
        ///          \           |               |
        ///                  \   |               |
        ///                   near \           |
        ///                                  \   |
        ///                                     far
        /// </summary>
        /// <param name="fov"> the angle of the frustrum. ( in degree ) </param>
        /// <param name="aspect"> the ratio of Width / Height </param>
        /// <param name="znear">the near plane</param>
        /// <param name="zfar">the far plane</param>
        /// <returns>Perspective Projection Matrix</returns>
        private Matrix ConstructPerspectiveProjectionMatrix( float fov, float aspect, float znear, float zfar )
        {
            // Reference 1: http://www.songho.ca/opengl/gl_projectionmatrix.html
            // Titled: OpenGL Projection Matrix
            // Reference 2: http://www.geeks3d.com/20090729/howto-perspective-projection-matrix-in-opengl/
            // Titled: Perspective Projection Matrix in OpenGL

            float PI = 3.1415926535897932384626433832795f;
            float PI_OVER_180 = 0.017453292519943295769236907684886f;
            float PI_OVER_360 = 0.0087266462599716478846184538424431f;

            float xymax = znear * (float)(System.Math.Tan( fov * PI_OVER_360 ));
            float ymin = -xymax;
            float xmin = -xymax;

            float width = xymax - xmin;
            float height = xymax - ymin;

            float depth = zfar - znear;
            float q = -(zfar + znear) / depth;
            float qn = -2 * (zfar * znear) / depth;

            float w = 2 * znear / width;
            w = w / aspect;
            float h = 2 * znear / height;

            Matrix mat = new Matrix(4, 4);

            mat[0, 0] = w;    mat[0, 1] = 0;    mat[0, 2] = 0;    mat[0, 3] = 0;
            mat[1,0] = 0;      mat[1,1] = h;      mat[1,2] = 0;      mat[1,3] = 0;
            mat[2,0] = 0;      mat[2,1] = 0;     mat[2,2] = q;     mat[2,3] = -1;
            mat[3,0] = 0;      mat[3,1] = 0;     mat[3,2] = qn;   mat[3,3] = 0;

            return mat;
        }



        /// <summary>
        /// Construct the Orthor Projection Matrix
        /// Please DONOT USE it right now.
        /// It is not implemented
        /// </summary>
        /// <returns></returns>
        private Matrix ConstructOrthorProjectionMatrix()
        {
            throw new NotImplementedException("Please DONOT USE it right now. I haven't really write the code.");
        }

        /// <summary>
        /// I assume that determining projection matrix is duty of camera.
        /// So I write it here. 
        /// </summary>
        public Matrix PerspectiveProjectionMatrix = new Matrix(4, 4);

        /// <summary>
        /// Orthor Projection Matrix.
        /// Please DONOT USE it right now.
        /// </summary>
        public Matrix OrthorProjectionMatrix = new Matrix(4, 4);

        /// <summary>
        /// View Matrix which transforms the world coordinates system into the coordinate system of the camera.
        /// </summary>
        public Matrix ViewMatrix = new Matrix(4, 4);
    }
}
