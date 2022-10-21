using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WUBT
{
    public class BTTree<T> : MonoBehaviour where T : Database
    {
        public T database;
        public BTNode root;
        public bool isRuning = true;
        public virtual void Init()
        {
            database = GetComponent<T>();
        }
        private void FixedUpdate()
        {
            if (!isRuning) return;
            if (root.Evaluate())
            {
                root.Update();
            }

        }
    }
}
