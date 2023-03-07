using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMBT;
using XNode;
using XNodeEditor;
namespace WMTreeGraph
{

    public class PlayAnimator : BTActionNode
    {
        public string animName;
        public Animator animator;
        public override void OnInit(Database database)
        {
          
            animator = database.GetComponent<Animator>();
            base.OnInit(database);
        }
        protected override void Enter()
        {
            base.Enter();
            animator.Play(animName);
        }

        
    }

}