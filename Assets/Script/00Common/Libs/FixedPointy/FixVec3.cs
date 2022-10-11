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
using UnityEngine;

namespace FixedPointy
{
    [Serializable]
    public struct FixVec3
    {
        public static readonly FixVec3 zero = new FixVec3();
        public static readonly FixVec3 one = new FixVec3(1, 1, 1);
        public static readonly FixVec3 right = new FixVec3(1, 0, 0);

        public static readonly FixVec3 up = new FixVec3(0, 1, 0);
        public static readonly FixVec3 forward = new FixVec3(0, 0, 1);

        public static implicit operator FixVec3(FixVec2 value)
        {
            return new FixVec3(value.x, value.y, 0);
        }

        public static FixVec3 operator +(FixVec3 rhs)
        {
            return rhs;
        }
        public static FixVec3 operator -(FixVec3 rhs)
        {
            return new FixVec3(-rhs.x, -rhs.y, -rhs.z);
        }

        public static FixVec3 operator +(FixVec3 lhs, FixVec3 rhs)
        {
            return new FixVec3(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);
        }
        public static FixVec3 operator -(FixVec3 lhs, FixVec3 rhs)
        {
            return new FixVec3(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);
        }

        public static FixVec3 operator +(FixVec3 lhs, Fix rhs)
        {
            return lhs.ScalarAdd(rhs);
        }
        public static FixVec3 operator +(Fix lhs, FixVec3 rhs)
        {
            return rhs.ScalarAdd(lhs);
        }
        public static FixVec3 operator -(FixVec3 lhs, Fix rhs)
        {
            return new FixVec3(lhs.x - rhs, lhs.y - rhs, lhs.z - rhs);
        }
        public static FixVec3 operator *(FixVec3 lhs, Fix rhs)
        {
            return lhs.ScalarMultiply(rhs);
        }
        public static FixVec3 operator *(Fix lhs, FixVec3 rhs)
        {
            return rhs.ScalarMultiply(lhs);
        }
        public static FixVec3 operator /(FixVec3 lhs, Fix rhs)
        {
            return new FixVec3(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs);
        }

        public static explicit operator Vector3(FixVec3 v)
        {
            return new Vector3((float)v.x, (float)v.y, (float)v.z);
        }

        public Fix x, y, z;

        public FixVec3(Fix x, Fix y, Fix z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static Fix Dot(FixVec3 lhs, FixVec3 rhs)
        {
            return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
        }

        public static FixVec3 Cross(FixVec3 lhs, FixVec3 rhs)
        {
            return new FixVec3(
                lhs.y * rhs.z - lhs.z * rhs.y,
                lhs.z * rhs.x - lhs.x * rhs.z,
                lhs.x * rhs.y - lhs.y * rhs.x
            );
        }

        public Fix Dot(FixVec3 rhs)
        {
            return x * rhs.x + y * rhs.y + z * rhs.z;
        }

        public FixVec3 Cross(FixVec3 rhs)
        {
            return new FixVec3(
                y * rhs.z - z * rhs.y,
                z * rhs.x - x * rhs.z,
                x * rhs.y - y * rhs.x
            );
        }

        FixVec3 ScalarAdd(Fix value)
        {
            return new FixVec3(x + value, y + value, z + value);
        }
        FixVec3 ScalarMultiply(Fix value)
        {
            return new FixVec3(x * value, y * value, z * value);
        }

        public Fix GetMagnitude()
        {
            ulong N = (ulong)((long)x.raw * (long)x.raw + (long)y.raw * (long)y.raw + (long)z.raw * (long)z.raw);

            return new Fix((int)(FixMath.SqrtULong(N << 2) + 1) >> 1);
        }

        public FixVec3 Normalize()
        {
            if (x == 0 && y == 0 && z == 0)
                return this;

            var m = GetMagnitude();
            x /= m;
            y /= m;
            z /= m;
            return this;
        }

        public FixVec3 Normalized()
        {
            if (x == 0 && y == 0 && z == 0)
                return FixVec3.zero;

            var m = GetMagnitude();
            return new FixVec3(x / m, y / m, z / m);
        }

        public override string ToString()
        {
            return string.Format("({0}, {1}, {2})", x, y, z);
        }

        public override bool Equals(System.Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                FixVec3 fv = (FixVec3)obj;
                return x == fv.x.raw && y.raw == fv.y.raw && z.raw == fv.z.raw;
            }
        }
    }
}
