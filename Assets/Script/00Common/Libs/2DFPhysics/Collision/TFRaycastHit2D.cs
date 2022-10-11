using FixedPointy;
using System.Collections;
using System.Collections.Generic;
using TF.Colliders;
using UnityEngine;

namespace TF{
    public struct TFRaycastHit2D
    {
        public FixVec2 normal;
        public TFCollider collider;
        // Distance from the ray origin to impact point.
        public Fix distance;
        public Fix fraction;
        // The point in world space where the ray hit the collider's surface.
        public FixVec2 point;

        public static implicit operator bool(TFRaycastHit2D hit)
        {
            return hit.collider != null;
        }
    }
}
