using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMTree;
using WMTreeGraph;
using XNode;

namespace WMTreeComposite
{
    public class BTParallelNode : BaseBTNode
    {

        [Input]
        public NextNode input;
        [Output]
        public NextNode output;
        [Input]
        public NextNode conditionNodeInput;
        ///当前节点
        public BaseBTNode currentBTNode;
        bool runing;
        protected override void Init()
        {
            base.Init();

        }
        public override void OnInit(TreeDatabase database)
        {
            base.OnInit(database);
        }
        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            return null; // Replace this
        }
        public override void Clear()
        {
            foreach (var item in _children)
            {
                item.Clear();
            }
            if (currentBTNode != null)
            {
                currentBTNode = null;
            }
        }   
        public override bool Evaluate()
        {
            return base.Evaluate();
        }

        public override BTResult Update()
        {
            if (!runing)
            {
                return BTResult.Ended;
            }
            foreach (BaseBTNode child in _children)
            {
                currentBTNode = child;
                BTResult bTResult = child.Update();
                if (bTResult == BTResult.Ended || bTResult == BTResult.Failed)
                {
                    return BTResult.Ended;
                }
            }
            return BTResult.Running;
        }

        public override bool DoEvaluate()
        {
            foreach (BaseBTNode child in _children)
            {
                if (!child.Evaluate())
                {
                    runing = false;
                    return false;
                }
            }
            runing = true;
            return true;
        }
    }
}