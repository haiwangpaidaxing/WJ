using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WMBT
{

    /// <summary>
    ///节点的准入条件，每一个BTNode都会有一个。具体的游戏逻辑判断可以继承于它
    /// </summary>
    public abstract class BTPrecondition : BTNode
    {
        protected abstract override bool DoEvaluate();
    }

}
