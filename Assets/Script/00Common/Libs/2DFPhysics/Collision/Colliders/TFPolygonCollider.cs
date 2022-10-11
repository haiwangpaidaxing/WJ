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
    public class TFPolygonCollider : TFCollider
    {
        public static readonly int MAX_POLY_VERTEX_COUNT = 64;

        public int VertexCount
        {
            get
            {
                return vertices.Count;
            }
        }

        [SerializeField] private List<FixVec2> vertices = new List<FixVec2>();
        [HideInInspector] public List<FixVec2> normals = new List<FixVec2>();

        protected override void Awake()
        {
            base.Awake();
            for (int i = 0; i < vertices.Count; i++)
            {
                normals.Add(FixVec2.zero);
            }
            CalculateNormals();
        }

        // Used when we only change position.
        public override void MoveAABB(FixVec2 posDiff)
        {
            boundingBox.min.x += posDiff.x;
            boundingBox.min.y += posDiff.y;
            boundingBox.max.x += posDiff.x;
            boundingBox.max.y += posDiff.y;
        }

        // Used when we rotate or vertices are added/removed.
        public override void RecalcAABB(FixVec2 pos)
        {
            boundingBox.min.x = pos.x;
            boundingBox.max.x = pos.x;
            boundingBox.min.y = pos.y;
            boundingBox.max.y = pos.y;
            for (int i = 0; i < vertices.Count; i++)
            {
                FixVec2 v = u.Transposed() * (vertices[i] * tdTransform.LocalScale);
                if (v.x + pos.x < boundingBox.min.x)
                {
                    boundingBox.min.x = v.x + pos.x;
                }
                if (v.y + pos.y < boundingBox.min.y)
                {
                    boundingBox.min.y = v.y + pos.y;
                }
                if (v.x + pos.x > boundingBox.max.x)
                {
                    boundingBox.max.x = v.x + pos.x;
                }
                if (v.y + pos.y > boundingBox.max.y)
                {
                    boundingBox.max.y = v.y + pos.y;
                }
            }
        }

        public override TFColliderType GetCType()
        {
            return TFColliderType.Polygon;
        }

        public void SetVertices(List<FixVec2> verts)
        {
            // Find the right most point on the hull
            int rightMost = 0;
            Fix highestXCoord = verts[0].x;
            for (int i = 1; i < verts.Count; ++i)
            {
                Fix x = verts[i].x;

                if (x > highestXCoord)
                {
                    highestXCoord = x;
                    rightMost = i;
                }
                // If matching x then take farthest negative y
                else if (x == highestXCoord)
                {
                    if (verts[i].y < verts[rightMost].y)
                    {
                        rightMost = i;
                    }
                }
            }

            int[] hull = new int[MAX_POLY_VERTEX_COUNT];
            int outCount = 0;
            int indexHull = rightMost;

            for (; ; )
            {
                hull[outCount] = indexHull;

                // Search for next index that wraps around the hull
                // by computing cross products to find the most counter-clockwise
                // vertex in the set, given the previos hull index
                int nextHullIndex = 0;
                for (int i = 1; i < verts.Count; ++i)
                {
                    // Skip if same coordinate as we need three unique
                    // points in the set to perform a cross product
                    if (nextHullIndex == indexHull)
                    {
                        nextHullIndex = i;
                        continue;
                    }


                    // Cross every set of three unique vertices
                    // Record each counter clockwise third vertex and add
                    // to the output hull
                    // See : http://www.oocities.org/pcgpe/math2d.html
                    FixVec2 e1 = verts[nextHullIndex] - verts[hull[outCount]];
                    FixVec2 e2 = verts[i] - verts[hull[outCount]];
                    Fix c = FixVec2.Cross(e1, e2);
                    if (c < Fix.zero)
                    {
                        nextHullIndex = i;
                    }

                    // Cross product is zero then e vectors are on same line
                    // therefore want to record vertex farthest along that line
                    if (c == Fix.zero && e2.GetMagnitudeSquared() > e1.GetMagnitudeSquared())
                    {
                        nextHullIndex = i;
                    }
                }

                ++outCount;
                indexHull = nextHullIndex;

                // Conclude algorithm upon wrap-around
                if (nextHullIndex == rightMost)
                {
                    break;
                }
            }


            // Copy vertices into shape's vertices
            for (int i = 0; i < vertices.Count; ++i)
            {
                vertices[i] = verts[hull[i]];
            }

            CalculateNormals();
        }

        public void CalculateNormals()
        {
            // Compute face normals
            for (int i = 0; i < vertices.Count; ++i)
            {
                FixVec2 face = vertices[(i + 1) % vertices.Count] - vertices[i];

                // Calculate normal with 2D cross product between vector and scalar
                normals[i] = new FixVec2(face.y, -face.x);
                normals[i] = normals[i].Normalized();
            }
        }

        public FixVec2 getSupport(FixVec2 dir)
        {
            Fix bestProjection = -Fix.MaxValue;
            FixVec2 bestVertex = new FixVec2(0, 0);


            for (int i = 0; i < vertices.Count; ++i)
            {
                FixVec2 v = vertices[i] * tdTransform.LocalScale;
                Fix projection = FixVec2.Dot(v, dir);

                if (projection > bestProjection)
                {
                    bestVertex = v;
                    bestProjection = projection;
                }
            }

            return bestVertex;
        }

        public FixVec2 GetVertex(int index)
        {
            return vertices[index] * tdTransform.LocalScale;
        }

        public override bool Raycast(out TFRaycastHit2D hit, FixVec2 pointA, FixVec2 pointB, Fix maxFraction)
        {
            hit = new TFRaycastHit2D();

            // Put the ray into the polygon's frame of reference.
            var p1 = u.Transposed() * (pointA - (FixVec2)tdTransform.Position);
            var p2 = u.Transposed() * (pointB - (FixVec2)tdTransform.Position);
            var d = p2 - p1;

            Fix lower = Fix.zero, upper = maxFraction;


            var index = -1;

            for (var i = 0; i < vertices.Count; ++i)
            {
                // p = p1 + a * d
                // dot(normal, p - v) = 0
                // dot(normal, p1 - v) + a * dot(normal, d) = 0
                var numerator = FixVec2.Dot(normals[i], GetVertex(i) - p1);
                var denominator = FixVec2.Dot(normals[i], d);

                if (denominator == Fix.zero)
                {
                    if (numerator < Fix.zero)
                    {
                        return false;
                    }
                }
                else
                {
                    // Note: we want this predicate without division:
                    // lower < numerator / denominator, where denominator < 0
                    // Since denominator < 0, we have to flip the inequality:
                    // lower < numerator / denominator <==> denominator * lower > numerator.
                    if (denominator < Fix.zero && numerator < lower * denominator)
                    {
                        // Increase lower.
                        // The segment enters this half-space.
                        lower = numerator / denominator;
                        index = i;
                    }
                    else if (denominator > Fix.zero && numerator < upper * denominator)
                    {
                        // Decrease upper.
                        // The segment exits this half-space.
                        upper = numerator / denominator;
                    }
                }

                // The use of epsilon here causes the assert on lower to trip
                // in some cases. Apparently the use of epsilon was to make edge
                // shapes work, but now those are handled separately.
                //if (upper < lower - b2_epsilon)
                if (upper < lower)
                {
                    return false;
                }
            }

            if (index >= 0)
            {
                hit.fraction = lower;
                hit.normal = tdTransform.Rotation * normals[index];
                hit.collider = this;
                return true;
            }
            return false;
        }

#if UNITY_EDITOR
        void OnDrawGizmosSelected()
        {
            if (vertices.Count == 0)
            {
                return;
            }
            FixVec2 pos = (FixVec2)tdTransform.Position;
            FixVec2 scale = (FixVec2)tdTransform.LocalScale;
            // Draw a yellow sphere at the transform's position
            UnityEditor.Handles.color = Color.green;
            for (int i = 0; i < vertices.Count - 1; i++)
            {
                Handles.DrawLine((Vector3)((pos + (tdTransform.Rotation * (vertices[i] * scale)))),
                    (Vector3)((pos + (tdTransform.Rotation * (vertices[i + 1] * scale)))));
            }
            Handles.DrawLine((Vector3)((pos + (tdTransform.Rotation * (vertices[vertices.Count - 1] * scale)))),
                (Vector3)((pos + (tdTransform.Rotation * (vertices[0] * scale)))));

            //Draw bounding box.
            UnityEditor.Handles.color = Color.red;
            Handles.DrawLine((Vector3)(boundingBox.min), (Vector3)(boundingBox.max));
        }
#endif
    }
}