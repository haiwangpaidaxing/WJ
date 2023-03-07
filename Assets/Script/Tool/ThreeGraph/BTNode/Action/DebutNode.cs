using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace WMTreeGraph
{

	public class DebutNode : BTActionNode
	{

		public string debugInfo;
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


		protected override BTResult Execute()
		{
			Debug.Log(debugInfo);
			return base.Execute();
		}


	}
}