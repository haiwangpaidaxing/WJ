using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FixedPointy;
using TF.Core;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TF.Colliders
{
    public class TFCircleCollider : TFCollider
    {
        public Fix radius;

        // Used when we only change position.
        public override void MoveAABB(FixVec2 posDiff)
        {
            boundingBox.min.x += posDiff.x;
            boundingBox.min.y += posDiff.y;
            boundingBox.max.x += posDiff.x;
            boundingBox.max.y += posDiff.y;
        }

        // Used when we rotate or radius is changed.
        public override void RecalcAABB(FixVec2 pos)
        {
            boundingBox.min.x = -radius + pos.x;
            boundingBox.min.y = -radius + pos.y;
            boundingBox.max.x = radius + pos.x;
            boundingBox.max.y = radius + pos.y;
        }

        public override TFColliderType GetCType()
        {
            return TFColliderType.Circle;
        }

        public override bool Raycast(out TFRaycastHit2D hit, FixVec2 pointA, FixVec2 pointB, Fix maxFraction)
        {
            hit = default;
            FixVec2 center = (FixVec2)tdTransform.Position;
            FixVec2 s = pointA - center;
            Fix b = FixVec2.Dot(s, s) - radius * radius;

            // Solve quadratic equation.
            FixVec2 r = pointB - pointA;
            Fix c = FixVec2.Dot(s, r);
            Fix rr = FixVec2.Dot(r, r);
            Fix sigma = c * c - rr * b;

            // Check for negative discriminant and short segment.
            if (sigma < Fix.zero || rr < Fix.Epsilon)
            {
                return false;
            }

            // Find the point of intersection on the line with the circle.
            Fix a = -(c + FixMath.Sqrt(sigma));

            // Is the intersection point on the segment?
            if (Fix.zero <= a && a <= maxFraction * rr)
            {
                a /= rr;
                hit.fraction = a;
                hit.normal = s + a * r;
                hit.normal.Normalize();
                hit.collider = this;
                return true;
            }
            return false;
        }

#if UNITY_EDITOR
        void OnDrawGizmosSelected()
        {
            // Draw a yellow sphere at the transform's position
            UnityEditor.Handles.color = Color.green;
            Handles.DrawWireDisc(transform.position + (new Vector3((float)offset.x, (float)offset.y, 0)), Vector3.forward, ((float)radius));
        }
#endif
    }
}