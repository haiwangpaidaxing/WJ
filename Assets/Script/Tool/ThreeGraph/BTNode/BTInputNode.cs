﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace WMTreeGraph
{
    public class BTInputNode : BTPreconditionNode
    {

        public Vector2 dir;
        public int target;
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

        protected override bool DoEvaluate()
        {
            dir.x = Input.GetAxisRaw("Horizontal");
            //	inputDir.y = Input.GetAxisRaw("Vertical");
            if (dir.x == target)
            {
                return true;
            }
            return false;
        }
    }
}