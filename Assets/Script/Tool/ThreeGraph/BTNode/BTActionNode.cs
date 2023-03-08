using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMBT;
using XNode;
namespace WMTreeGraph
{
    public abstract class BTActionNode : BaseBTNode
    {
        [Header("播放动画名字")]
        public string animName;
        [Input]
        public NextNode input;
        private BTActionStatus _status = BTActionStatus.Ready;
        protected virtual BTResult Execute()
        {
            return BTResult.Running;
        }
        public override void Clear()
        {
            if (_status != BTActionStatus.Ready)
            {   // not cleared yet
                Exit();
                _status = BTActionStatus.Ready;
            }
        }
        public override bool Evaluate()
        {
            return base.Evaluate();
        }
        public override void OnInit(Database database)
        {
            base.OnInit(database);
        }
        protected override bool DoEvaluate()
        {
            return base.DoEvaluate();
        }

        protected virtual void Enter()
        {
            database.animator.Play(animName);
        }
        protected virtual void Exit()
        {
        }
        public override BTResult Update()
        {
            BTResult result = BTResult.Ended;
            if (_status == BTActionStatus.Ready)//第一次进入时
            {
                Enter();
                _status = BTActionStatus.Running;
            }
            if (_status == BTActionStatus.Running)//运行时
            {       // not using else so that the status changes reflect instantly
                result = Execute();
                if (result != BTResult.Running)//当行为结束时退出行为
                {
                    Exit();
                    _status = BTActionStatus.Ready;
                }
            }
            return result;
        }

        public override void OnCreateConnection(NodePort from, NodePort to)
        {

        }
        private enum BTActionStatus
        {
            Ready = 1,
            Running = 2,
        }
    }
}


