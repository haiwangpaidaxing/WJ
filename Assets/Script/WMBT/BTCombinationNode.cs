using System.Collections.Generic;
using UnityEngine;

namespace WMBT
{
    /// <summary>
    /// 选择组合节点
    /// </summary>
    public class BTPrioritySelector : BTNode
    {
        //A>B>C 任意一节点成功返回true

        ///Priority Selector逻辑节点，
        ///继承于BTNode。每次执行，
        ///先有序地遍历子节点，
        ///然后执行符合准入条件的第一个子结点。
        ///可以看作是根据条件来选择一个子结点的选择器。

        ///当前节点
        public BTNode currentBTNode;

        public BTPrioritySelector(BTPrecondition bTPrecondition, string name = "") : base(bTPrecondition, name)
        {
        }

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

        public override void Init(Database database)
        {
            base.Init(database);
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

        protected override bool DoEvaluate()
        {
            //A>B>C   
            foreach (BTNode child in children)
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
    }
    /// <summary>
    /// 顺序组合节点
    /// </summary>
    public class BTSequence : BTNode
    {
        ///该节点会从左到右的依次执行其子节点，只要子节点返回“成功”
        ///就继续执行后面的节点，直到有一个节点返回“运行中”或“失败”时，会停止后续节点的运行，
        ///并且向父节点返回“运行中”或“失败”，如果所有子节点都返回“成功”则向父节点返回“成功”。
        ///Sequence逻辑节点，继承于BTNode。每次执行，有序地执行各个子结点，当一个子结点结束后才执行下一个。
        ///严格按照节点A、B、C的顺序执行，当最后的行为C结束后，BTSequence结束。 <summary>
        /// 该节点会从左到右的依次执行其子节点，只要子节点返回“成功”
        BTNode currenNode;
        int currenIndex;
        public override void Clear()
        {
            if (currenNode != null)
            {
                currenNode = null;
                currenIndex = -1;
            }
            foreach (BTNode child in children)
            {
                child.Clear();
            }
        }
        public override void Init(Database database)
        {
            base.Init(database);
        }

        public override BTResult Update()
        {
            BTResult result = currenNode.Update();
            if (result == BTResult.Ended)
            {
                currenIndex++;
                if (currenIndex >= children.Count)
                {
                    //一轮结束
                    currenNode.Clear();
                    currenIndex = -1;
                    currenNode = null;
                }
                else
                {
                    currenNode.Clear();
                    currenNode=children[currenIndex];
                    return BTResult.Running;
                }
            }
            return result;
        }

        protected override bool DoEvaluate()
        {
            //第一次进入时
            if (currenNode == null)
            {
                currenIndex = 0;
                currenNode = children[currenIndex];
                return currenNode.Evaluate();
            }
            else//第二次进入时
            {
                bool result = currenNode.Evaluate();
                if (!result)
                {
                    currenNode.Clear();
                    currenIndex = -1;
                    currenNode = null;
                }
                return false;
            }

        }
    }

    /// <summary>
    /// 并行组合节点
    /// </summary>
    public class BTParallel : BTNode
    {
        protected List<BTResult> _results;
        protected ParallelFunction _func;

        public BTParallel(BTPrecondition bTPrecondition, ParallelFunction _func, string name = "") : base(bTPrecondition, name)
        {
            this._func = _func;
        }

        public override void Clear()
        {
            ResetResults();

            foreach (BTNode child in children)
            {
                child.Clear();
            }
        }


        public override void Init(Database database)
        {
            base.Init(database);
            if (_results==null)
            {
                _results = new List<BTResult>();
            }
        }

        public override BTResult Update()
        {
            int endingResultCount = 0;
            for (int i = 0; i < children.Count; i++)
            {

                if (_func == ParallelFunction.And)
                {
                    if (_results[i] == BTResult.Running)
                    {
                        _results[i] = children[i].Update();
                    }
                    if (_results[i] != BTResult.Running)
                    {
                        endingResultCount++;
                    }
                }
                else
                {
                    if (_results[i] == BTResult.Running)
                    {
                        _results[i] = children[i].Update();
                    }
                    if (_results[i] != BTResult.Running)
                    {
                        ResetResults();
                        return BTResult.Ended;
                    }
                }
            }
            if (endingResultCount == children.Count)
            {   // only apply to AND func
                ResetResults();
                return BTResult.Ended;
            }
            return BTResult.Running;
        }

        private void ResetResults()
        {
            for (int i = 0; i < _results.Count; i++)
            {
                _results[i] = BTResult.Running;
            }
        }
        public override void AddChild(BTNode aNode)
        {
            if (_results == null)
            {
                _results = new List<BTResult>();
            }
            base.AddChild(aNode);
            _results.Add(BTResult.Running);
        }

        public override void RemoveChild(BTNode aNode)
        {
            if (_results == null)
            {
                _results = new List<BTResult>();
            }
            int index = _children.IndexOf(aNode);
            _results.RemoveAt(index);
            base.RemoveChild(aNode);
          
        }

        protected override bool DoEvaluate()
        {
            foreach (BTNode child in children)
            {
                if (!child.Evaluate())
                {
                    return false;
                }
            }
            return true;
        }


        public enum ParallelFunction
        {
            And = 1,    // returns Ended when all results are not running
            Or = 2,     // returns Ended when any result is not running
        }
    }
}

