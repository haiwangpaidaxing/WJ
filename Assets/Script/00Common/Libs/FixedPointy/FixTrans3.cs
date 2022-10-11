/* FixedPointy - A simple fixed-point math library for C#.
 * 
 * Copyright (c) 2013 Jameson Ernst
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */
using System;

namespace FixedPointy
{
    /// <summary>
    /// Represents the position, rotation, and scale of a 3d object.
    /// </summary>
    [Serializable]
    public struct FixTrans3
    {
        public static FixTrans3 operator *(FixTrans3 lhs, FixTrans3 rhs)
        {
            FixTrans3 t = new FixTrans3();
            t.m = lhs.m * rhs.m;
            return t;
        }

        public static FixVec3 operator *(FixTrans3 lhs, FixVec3 rhs)
        {
            return new FixVec3(
                lhs.m[0, 0] * rhs.x + lhs.m[0, 1] * rhs.y + lhs.m[0, 2] * rhs.z + lhs.m[0, 3],
                lhs.m[1, 0] * rhs.x + lhs.m[1, 1] * rhs.y + lhs.m[1, 2] * rhs.z + lhs.m[1, 3],
                lhs.m[2, 0] * rhs.x + lhs.m[2, 1] * rhs.y + lhs.m[2, 2] * rhs.z + lhs.m[2, 3]
            );
        }

        public static FixTrans3 MakeRotationZ(Fix degrees)
        {
            Fix cos = FixMath.Cos(degrees);
            Fix sin = FixMath.Sin(degrees);
            return new FixTrans3(
                cos, -sin, 0, 0,
                sin, cos, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1
            );
        }

        public static FixTrans3 MakeRotationY(Fix degrees)
        {
            Fix cos = FixMath.Cos(degrees);
            Fix sin = FixMath.Sin(degrees);
            return new FixTrans3(
                cos, 0, sin, 0,
                0, 1, 0, 0,
                -sin, 0, cos, 0,
                0, 0, 0, 1
            );
        }

        public static FixTrans3 MakeRotationX(Fix degrees)
        {
            Fix cos = FixMath.Cos(degrees);
            Fix sin = FixMath.Sin(degrees);
            return new FixTrans3(
                1, 0, 0, 0,
                0, cos, -sin, 0,
                0, sin, cos, 0,
                0, 0, 0, 1
            );
        }

        public static FixTrans3 MakeRotation(FixVec3 degrees)
        {
            return MakeRotationX(degrees.x)
                .RotateY(degrees.y)
                .RotateZ(degrees.z);
        }

        public static FixTrans3 MakeScale(FixVec3 scale)
        {
            return new FixTrans3(
                scale.x, 0, 0, 0,
                0, scale.y, 0, 0,
                0, 0, scale.z, 0,
                0, 0, 0, 1
            );
        }

        public static FixTrans3 MakeTranslation(FixVec3 delta)
        {
            return new FixTrans3(
                1, 0, 0, delta.x,
                0, 1, 0, delta.y,
                0, 0, 1, delta.z,
                0, 0, 0, 1
            );
        }


        public Matrix4x4Fix m;

        public FixTrans3(
            Fix m11, Fix m12, Fix m13, Fix m14,
            Fix m21, Fix m22, Fix m23, Fix m24,
            Fix m31, Fix m32, Fix m33, Fix m34,
            Fix m41, Fix m42, Fix m43, Fix m44
        )
        {
            m = new Matrix4x4Fix();
            m.m11 = m11;
            m.m12 = m12;
            m.m13 = m13;
            m.m14 = m14;
            m.m21 = m21;
            m.m22 = m22;
            m.m23 = m23;
            m.m24 = m24;
            m.m31 = m31;
            m.m32 = m32;
            m.m33 = m33;
            m.m34 = m34;
            m.m41 = m41;
            m.m42 = m42;
            m.m43 = m43;
            m.m44 = m44;
        }

        public FixTrans3(FixVec3 position, FixVec3 rotation, FixVec3 scale)
        {
            this = MakeRotationX(rotation.x)
                .RotateY(rotation.y)
                .RotateZ(rotation.z)
                .Scale(scale)
                .Translate(position);
        }

        public FixTrans3(Matrix4x4Fix m)
        {
            this.m = m;
        }

        public FixTrans3 RotateZ(Fix degrees)
        {
            return MakeRotationZ(degrees) * this;
        }

        public FixTrans3 RotateY(Fix degrees)
        {
            return MakeRotationY(degrees) * this;
        }

        public FixTrans3 RotateX(Fix degrees)
        {
            return MakeRotationX(degrees) * this;
        }

        public FixTrans3 Rotate(FixVec3 degrees)
        {
            return MakeRotation(degrees);
        }

        public FixTrans3 Scale(FixVec3 scale)
        {
            return new FixTrans3(
                m[0, 0] * scale.x, m[0, 1] * scale.x, m[0, 2] * scale.x, m[0, 3] * scale.x,
                m[1, 0] * scale.y, m[1, 1] * scale.y, m[1, 2] * scale.y, m[1, 3] * scale.y,
                m[2, 0] * scale.z, m[2, 1] * scale.z, m[2, 2] * scale.z, m[2, 3] * scale.z,
                0, 0, 0, 1
            );
        }

        public FixTrans3 Translate(FixVec3 delta)
        {
            FixTrans3 ft = new FixTrans3(m);
            ft.m[0, 3] += delta.x;
            ft.m[1, 3] += delta.y;
            ft.m[2, 3] += delta.z;
            return ft;
        }

        public FixVec3 Apply(FixVec3 vec)
        {
            return this * vec;
        }

        public FixVec3 Position()
        {
            return new FixVec3(m[0, 3], m[1, 3], m[2, 3]);
        }

        public FixVec3 Scale()
        {
            return new FixVec3(
                new FixVec3(m[0, 0], m[1, 0], m[2, 0]).GetMagnitude(),
                new FixVec3(m[0, 1], m[1, 1], m[2, 1]).GetMagnitude(),
                new FixVec3(m[0, 2], m[1, 2], m[2, 2]).GetMagnitude());
        }

        //https://gamedev.stackexchange.com/questions/50963/how-to-extract-euler-angles-from-transformation-matrix
        public FixVec3 EulerAngle()
        {
            FixVec3 ea = new FixVec3();

            ea.x = FixMath.Atan2(-m[1, 2], m[2, 2]);

            Fix cosYangle = FixMath.Sqrt(FixMath.Pow(m[0, 0], 2) + FixMath.Pow(m[0, 1], 2));
            ea.y = FixMath.Atan2(m[0, 2], cosYangle);

            Fix sinXangle = FixMath.Sin(ea.x);
            Fix cosXangle = FixMath.Cos(ea.x);
            ea.z = FixMath.Atan2((cosXangle * m[1, 0]) + (sinXangle * m[2, 0]), (cosXangle * m[1, 1]) + (sinXangle * m[2, 1]));

            return ea;
        }

        public override string ToString()
        {
            return $"Position: [{Position().ToString()}]\n"
                + $"Rotation: [{EulerAngle().ToString()}]\n"
                + $"Scale: {Scale().ToString()}\n";
        }
    }
}
