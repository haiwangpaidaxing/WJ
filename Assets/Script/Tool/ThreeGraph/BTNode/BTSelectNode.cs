using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMTree;
using WMTreeGraph;
using XNode;

namespace WMTreeComposite
{
    public class BTSelectNode : BaseBTNode
    {

        [Input]
        public NextNode input;
        [Output]
        public NextNode output;
        [Input]
        public NextNode conditionNodeInput;

        //A>B>C 任意一节点成功返回true
        ///Priority Selector逻辑节点，
        ///继承于BTNode。每次执行，
        ///先有序地遍历子节点，
        ///然后执行符合准入条件的第一个子结点。
        ///可以看作是根据条件来选择一个子结点的选择器。

        ///当前节点
        public BaseBTNode currentBTNode;

        //public BTSelectNode(BTPreconditionNode bTPrecondition, string name = "") : base(bTPrecondition, name)
        //{

        //}

        public override void Clear()
        {
            if (currentBTNode != null)
            {
                currentBTNode.Clear();
                currentBTNode = null;
            }
        }

        public override bool Evaluate()
        {
            return base.Evaluate();
        }

        public override void OnInit(TreeDatabase database)
        {
            base.OnInit(database);
        }

        public override BTResult Update()
        {
            if (currentBTNode == null)
            {
                return BTResult.Ended;
            }
            BTResult bTResult = currentBTNode.Update();//执行当前节点
            if (bTResult != BTResult.Running)
            {
                currentBTNode.Clear();//退出当前节点
                currentBTNode = null;
            }

            return bTResult;
        }

       public override bool DoEvaluate()
        {
            //A>B>C   
            foreach (BaseBTNode child in Children)
            {
                if (child.Evaluate())//判断当前节点是否能执行  
                {
                    if (currentBTNode != null && currentBTNode != child)
                    {
                        currentBTNode.Clear();
                    }
                    currentBTNode = child;
                    return true;
                }
            }//循环完一遍后发现没有能执行的子节点
            if (currentBTNode != null)
            {
                currentBTNode.Clear();
                currentBTNode = null;
            }
            return false;
        }
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

    }
}