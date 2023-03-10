using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using WMTreeGraph;
namespace WMTreePrecondition
{
    public class GroundDetection : BTPreconditionNode
    {
       
        public enum State
        {
            Ground, Air
        }
        [Header("判断条件")]
        public State state;
        // Use this for initialization
        protected override void Init()
        {
            base.Init();

        }
        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            return null; // Replace this
        }

       public override bool DoEvaluate()
        {
            
            if (state == State.Ground && database.ISGround)
            {
                return true;
            }
            else if (state == State.Air && !database.ISGround)
            {
                return true;
            }
            return false;
        }
    }
}
