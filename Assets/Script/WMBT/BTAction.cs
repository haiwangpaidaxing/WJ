using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WUBT
{
    //行为节点，继承于BTNode。具体的游戏逻辑应该放在这个节点里面。
    public class BTAction : BTNode
    {
        private BTActionStatus _status = BTActionStatus.Ready;
        public override void AddChild(BTNode aNode)
        {
            Debug.LogError("BTAction: Cannot add a node into BTAction.");
        }
        public override void RemoveChild(BTNode aNode)
        {
            Debug.LogError("BTAction: Cannot remove a node into BTAction.");
        }
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
        public override void Init(Database database)
        {
            base.Init(database);
        }
        protected override bool DoEvaluate()
        {
            return base.DoEvaluate();
        }

        protected virtual void Enter()
        {
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

        private enum BTActionStatus
        {
            Ready = 1,
            Running = 2,
        }
    }
}

