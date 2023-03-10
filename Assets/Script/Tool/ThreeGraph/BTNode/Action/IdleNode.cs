using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMTreeGraph;
using XNode;

namespace WMTreeAction
{
	public class IdleNode : BTActionNode
	{

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

        protected override void Exit()
        {
            base.Exit();
        }
    }
}
