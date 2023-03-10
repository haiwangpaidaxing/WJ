using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMTreeGraph;
using XNode;

namespace WMTreePrecondition
{
    public class BTIfNode : BTPreconditionNode
    {
        [Input,Header("条件节点输入")]
        public NextNode conditionNodeInput;
        public enum Condition
        {
            OR, And
        }
        public Condition condition;
        public override bool DoEvaluate()
        {
            if (condition == Condition.And)
            {
                foreach (var item in precondList)
                {
                    if (!item.DoEvaluate())
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                foreach (var item in precondList)
                {
                    if (item.DoEvaluate())
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        public override void OnCreateConnection(NodePort from, NodePort to)
        {
            if (to.fieldName == "conditionNodeInput")
            {
                if (precondList == null)
                {
                    precondList = new List<BTPreconditionNode>();
                }
                foreach (var item in to.GetConnections())
                {
                    if (item.node==this)
                    {
                        continue;
                    }
                    BTPreconditionNode precond = item.node as BTPreconditionNode;
                    if (precond != null && !precondList.Contains(precond))
                    {
                        precondList.Add(precond);
                    }
                }
            }
        }

        public override void OnRemoveConnection(NodePort port)
        {
            if (port.fieldName == "conditionNodeInput")
            {
                if (precondList != null)
                {
                    precondList.Clear();
                    foreach (var item in port.GetConnections())
                    {
                        BTPreconditionNode precond = item.node as BTPreconditionNode;
                        precondList.Add(precond);
                    }
                }
            }
        }

    }
}