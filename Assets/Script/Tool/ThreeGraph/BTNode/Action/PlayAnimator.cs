using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMTreeGraph;
using XNode;

namespace WMTreeAction
{
    public class PlayAnimator : BTActionNode
    {
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}
		protected override void Enter()
		{
			base.Enter();
		}
		protected override BTResult Execute()
		{
			return base.Execute();
		}
		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
			return null; // Replace this
		}
	}
}