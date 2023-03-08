using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMTreeGraph;
using XNode;

namespace WMTreeAction
{
    public class MoveNode : BTActionNode
    {
        [Header("移动的方向")]
        public float dir;
        [Header("移动速度")]
        public float speed;
        // Use this for initialization
        protected override void Init()
        {
            base.Init();

        }
        protected override void Enter()
        {
            Debug.Log("Init");
            base.Enter();
        }
        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            return null; // Replace this
        }

        protected override BTResult Execute()
        {
            dir = Input.GetAxisRaw("Horizontal");
            database.rig.velocity = Vector2.right * dir * speed;
            Debug.Log("MoveNode");
            return base.Execute();
        }


    }
}
