using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using WMTreeGraph;
namespace WMTreeAction
{
	public class JumpNode : BTActionNode
	{
		public float ForceJump;
		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}
		protected override void Enter()
		{
			base.Enter();
			database.unitController.MoveY(1, ForceJump);
		}
        protected override BTResult Execute()
		{
			database.unitController.MoveX(database.InputDir.x,5);
			return base.Execute();
		}
		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
			return null; // Replace this
		}
     
    }
}
