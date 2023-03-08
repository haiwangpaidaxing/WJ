using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using WMBT;

namespace WMTreeGraph
{
    [System.Serializable]
    public class NextNode
    { }
    public class BaseBTNode : Node
    {
        [HideInInspector]
        public string nodeName;
        //子节点数组
        [SerializeField]
        protected List<BaseBTNode> _children;
        public List<BaseBTNode> Children { get { return _children; } }


        [SerializeField, Header("条件节点")]
        protected BTPreconditionNode precond;

        // 黑板 
        [HideInInspector, Header("数据面板")]
        public Database database;
        // Cooldown function.冷却时间函数
        [HideInInspector]
        public float interval = 0;
        private float _lastTimeEvaluated = 0;

        public BaseBTNode()
        {

        }

        /// <summary>
        /// 初始化各个节点
        /// </summary>
        /// <param name="database"></param>
        public virtual void OnInit(Database database)
        {
            this.database = database;
            if (precond != null)
            {
                precond.OnInit(database);
            }
            if (_children == null)
            {
                _children = new List<BaseBTNode>();
            }
            for (int i = 0; i < _children.Count; i++)
            {
                _children[i].OnInit(database);
            }
        }


        /// <summary>
        /// 判断是否执行
        /// </summary>
        /// <returns></returns>
        public virtual bool Evaluate()
        {
            if (CheckInterval() && (precond == null || precond.DoEvaluate()) && DoEvaluate())
            {
                return true;
            }
            return false;
        }
        protected virtual bool DoEvaluate() { return true; }

        public bool CheckInterval()
        {
            //当前时间 -最后时间 
            return true;
        }

        /// <summary>
        /// 节点执行
        /// </summary>
        /// <returns></returns>
        public virtual BTResult Update()
        {
            return BTResult.Ended;
        }

        #region 节点增删
        /// <summary>
        /// 添加一个节点
        /// </summary>
        /// <param name="aNode"></param>
        public virtual void AddChild(BaseBTNode aNode)
        {
            if (_children == null)
            {
                _children = new List<BaseBTNode>();
            }
            if (aNode != null)
            {
                if (!_children.Contains(aNode))
                {
                    _children.Add(aNode);
                }
            }
        }
        /// <summary>
        /// 删除一个节点
        /// </summary>
        /// <param name="aNode"></param>
        public virtual void RemoveChild(BaseBTNode aNode)
        {
            if (_children != null && aNode != null)
            {
                _children.Remove(aNode);
            }
        }


        public virtual void Clear() { }
        #endregion


        public override void OnCreateConnection(NodePort from, NodePort to)
        {
            if (from.node == this)
            {
                foreach (var item in from.GetConnections())
                {
                    if (item.node == this)
                    {
                        continue;
                    }
                    AddChild(item.node as BaseBTNode);
                }
            }

            if (to.fieldName == "conditionNodeInput")
            {
                foreach (var item in to.GetConnections())
                {
                    precond = item.node as BTPreconditionNode;
                }
            }
        }

        public override void OnRemoveConnection(NodePort port)
        {
            if (_children != null)
            {
                _children.Clear();
            }
            foreach (var item in port.GetConnections())
            {
                BaseBTNode baseBTNode = item.node as BaseBTNode;
                AddChild(baseBTNode);
            }
        }


    }
    public enum BTResult
    {
        /// <summary>
        /// 结束
        /// </summary>
        Ended = 1,
        /// <summary>
        /// 运行
        /// </summary>
        Running = 2,
        /// <summary>
        /// 失败
        /// </summary>
        Failed = 3,
    }

}
