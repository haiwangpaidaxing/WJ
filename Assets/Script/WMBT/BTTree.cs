using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WMBT
{
    public class BTTree : MonoBehaviour
    {
        [HideInInspector]
        public Database database;
        public BTNode root;
        public bool isRuning = true;

      
        public virtual void Init()
        {
            database = GetComponent<Database>();
            InitBehavior();
        }
        private void FixedUpdate()
        {
            if (!isRuning) return;
            if (root.Evaluate())
            {
                root.Update();
            }
        }

        protected virtual void InitBehavior()
        {
        }
    }
}
