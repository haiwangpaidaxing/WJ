using System.Collections.Generic;
using UnityEngine;

namespace WMBT
{
    ///所有节点的base class 定义了一些节点的基本功能，并提供一些可继承的函数。
    public class BTNode
    {
        public string nodeName;
        //子节点数组
        protected List<BTNode> _children;
        public List<BTNode> children { get { return _children; } }
        /// <summary>
        /// 准入判断
        /// </summary>
        public BTPrecondition precond;
        // 黑板 
        public Database database;
        // Cooldown function.冷却时间函数
        public float interval = 0;
        private float _lastTimeEvaluated = 0;

        public BTNode(BTPrecondition bTPrecondition, string name = "")
        {
            this.nodeName = name;
            this.precond = bTPrecondition;
        }

        public BTNode()
        {
          
        }

        /// <summary>
        /// 初始化各个节点
        /// </summary>
        /// <param name="database"></param>
        public virtual void Init(Database database)
        {
            this.database = database;
            if (precond!=null)
            {
                precond.Init(database);
            }
            if (_children == null)
            {
                _children = new List<BTNode>();
            }
            for (int i = 0; i < _children.Count; i++)
            {
                _children[i].Init(database);
            }
        }


        /// <summary>
        /// 判断是否执行
        /// </summary>
        /// <returns></returns>
        public virtual bool Evaluate()
        {
            if (CheckInterval() && (precond == null|| precond.DoEvaluate()) && DoEvaluate())
            {
                return true;
            }
            return false;
        }
        protected virtual bool DoEvaluate() { return true; }

        public bool CheckInterval()
        {
            //当前时间 -最后时间 
            if (Time.time - _lastTimeEvaluated >= interval)
            {
                _lastTimeEvaluated = Time.time;
                return true;
            }
            return false;
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
        public virtual void AddChild(BTNode aNode)
        {
            if (_children == null)
            {
                _children = new List<BTNode>();
            }
            if (aNode != null)
            {
                _children.Add(aNode);
            }
        }
        /// <summary>
        /// 删除一个节点
        /// </summary>
        /// <param name="aNode"></param>
        public virtual void RemoveChild(BTNode aNode)
        {
            if (_children != null && aNode != null)
            {
                _children.Remove(aNode);
            }
        }
        public virtual void Clear() { }
        #endregion
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
