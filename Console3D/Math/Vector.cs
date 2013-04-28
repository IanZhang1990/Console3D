using System;
using System.Collections.Generic;

namespace Console3D.Math
{
    /// <summary>
    /// Vector class that implements some basic operation of a vector
    /// "Vector" here actually means Three-Dimentional Vector, which 
    /// has three values: x, y, z.
    /// </summary>
    class Vector3
    {
        /// <summary>
        /// Constructor of a Vector
        /// </summary>
        /// <param name="x">X value</param>
        /// <param name="y">Y value</param>
        /// <param name="z">Z value</param>
        public Vector3(float x, float y, float z)
        {
            this.SetValue(x, y, z);
        }

        public void SetValue(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        /// <summary>
        /// Vector Times a scalar quantity
        /// vector * scale
        /// </summary>
        /// <param name="vector">The vector</param>
        /// <param name="scale">The scalar quantity</param>
        /// <returns>A new vector as a result</returns>
        public static Vector3 Times(Vector3 vector, float scale)
        {
            return new Vector3(vector.X * scale, vector.Y * scale, vector.Z * scale);
        }

        /// <summary>
        /// One vector pluses another
        /// vector1 + vector2
        /// </summary>
        /// <param name="vector1">the first vector</param>
        /// <param name="vector2">the add vector</param>
        /// <returns>A new added vector</returns>
        public static Vector3 Plus(Vector3 vector1, Vector3 vector2)
        {
            return new Vector3(vector1.X + vector2.X, vector1.Y + vector2.Y, vector1.Z + vector2.Z);
        }

        /// <summary>
        /// One vector minuses another
        /// Vector1 - Vector2
        /// </summary>
        /// <param name="vector1">the first vector</param>
        /// <param name="vector2">the second vector</param>
        /// <returns>the result vector</returns>
        public static Vector3 Minus(Vector3 vector1, Vector3 vector2)
        {
            return new Vector3(vector1.X - vector2.X, vector1.Y - vector2.Y, vector1.Z - vector2.Z);
        }

        /// <summary>
        /// Vector dots another
        /// V1 ` V2
        /// </summary>
        /// <param name="vector1">the first vector</param>
        /// <param name="vector2">the second vector</param>
        /// <returns>the result as a float number</returns>
        public static float Dot(Vector3 vector1, Vector3 vector2)
        {
            return (vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z);
        }


        /// <summary>
        /// the length of a vector
        /// </summary>
        /// <param name="vector">the vector that we want to know its length</param>
        /// <returns>length</returns>
        public static float Mag(Vector3 vector)
        {
            return (float)( System.Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z));
        }

        /// <summary>
        /// Normalize the vector
        /// </summary>
        /// <param name="vector">the vector that will be normalized</param>
        /// <returns>the normalized vector</returns>
        public static Vector3 Norm(Vector3 vector)
        {
            float infinity = float.MaxValue;
            float mag = Vector3.Mag(vector);
            float div = mag == 0? infinity : 1.0f /mag;

            return Vector3.Times(vector, div);
        }

        /// <summary>
        /// Self normalize
        /// </summary>
        public void Norm()
        {
            float infinity = float.MaxValue;
            float mag = Vector3.Mag(this);
            float div = mag == 0? infinity : 1.0f /mag;

            Vector3 result = Vector3.Times(this, div);
            this.X = result.X;
            this.Y = result.Y;
            this.Z = result.Z;
        }


        /// <summary>
        /// Cross operation.
        /// vector1 X vector2
        /// </summary>
        /// <param name="vector1">the first vector</param>
        /// <param name="vector2">the second vector</param>
        /// <returns>the result</returns>
        public static Vector3 Cross( Vector3 vector1, Vector3 vector2 )
        {
            return new Vector3(  vector1.Y * vector2.Z - vector1.Z * vector2.Y,
                                            vector1.Z * vector2.X - vector1.X * vector2.Z,
                                            vector1.X * vector2.Y - vector1.Y * vector2.X);
        }


        public Matrix ToMatrix()
        {
            Matrix res = new Matrix(4, 1);
            res[0,0] = this.X;
            res[1, 0] = this.Y;
            res[2, 0] = this.Z;
            res[3, 0] = 1.0f;

            return res;
        }

        /// <summary>
        /// Rotate a vector around Y axis
        /// </summary>
        /// <param name="angle">rotate angle</param>
        /// <param name="vector">the vector</param>
        /// <returns>the rotated vector</returns>
        public static Vector3 YAxisRotate(float angle, Vector3 vector)
        {
            return new Vector3( vector.X * (float)System.Math.Cos(angle) + vector.Z * (float)System.Math.Sin(angle),
                                           vector.Y,
                                           -vector.X * (float)System.Math.Sin(angle) + vector.Z * (float)System.Math.Cos(angle) );
        }

        /// <summary>
        /// vector1 + vector2
        /// </summary>
        /// <param name="vector1"></param>
        /// <param name="vector2"></param>
        /// <returns></returns>
        public static Vector3 operator +(Vector3 vector1, Vector3 vector2)
        {            return Vector3.Plus(vector1, vector2);        }

        /// <summary>
        /// -vector
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Vector3 operator -(Vector3 vector)
        {            return Vector3.Times(vector, -1.0f);        }

        /// <summary>
        /// vector1 - vector2
        /// </summary>
        /// <param name="vector1"></param>
        /// <param name="vector2"></param>
        /// <returns></returns>
        public static Vector3 operator -(Vector3 vector1, Vector3 vector2)
        {
            return Vector3.Plus(vector1, Vector3.Times(vector2, -1.0f));
        }

        /// <summary>
        /// X value
        /// </summary>
        public float X{ get;set; }

        /// <summary>
        /// Y value
        /// </summary>
        public float Y{ get;set; }

        /// <summary>
        /// Z value
        /// </summary>
        public float Z{ get;set; }
    }
}
