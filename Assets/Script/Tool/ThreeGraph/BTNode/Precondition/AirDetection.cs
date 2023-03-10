using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using WMTreeGraph;
namespace WMTreePrecondition
{
    public class AirDetection : BTPreconditionNode
    {
        public override bool DoEvaluate()
        {
            if (database.rig.velocity.y>0)
            {
                return true;
            }
            return false;
        }
    }
}
