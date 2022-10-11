using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WUBT
{
    public class BTTree : MonoBehaviour
    {
        public Database database;
        public BTNode root;
        public bool isRuning = true;
        public void Init()
        {
            database = GetComponent<Database>();
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
