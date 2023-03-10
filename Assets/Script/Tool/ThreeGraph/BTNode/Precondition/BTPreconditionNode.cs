using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace WMTreeGraph
{
	[NodeTint(1,0.2f,0.3f)]
	public abstract class BTPreconditionNode : BaseBTNode
	{

		[Output,Header("条件节点输出")]
		public NextNode output;
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

		public override void OnCreateConnection(NodePort from, NodePort to)
		{

		}

		public abstract override bool DoEvaluate();

	}
}