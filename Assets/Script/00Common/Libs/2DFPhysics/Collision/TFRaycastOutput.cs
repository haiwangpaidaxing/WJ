using FixedPointy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TF
{
    public struct TFRaycastOutput
    {
        public int nodeID;
        public TFRaycastHit2D hit;

        public TFRaycastOutput(int nodeID, TFRaycastHit2D hit)
        {
            this.nodeID = nodeID;
            this.hit = hit;
        }
    }
}