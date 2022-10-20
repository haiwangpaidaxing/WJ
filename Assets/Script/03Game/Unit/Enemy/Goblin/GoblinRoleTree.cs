using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WUBT
{
    public class GoblinRoleTree : BTTree
    {
        public override void Init()
        {
            base.Init();
            InitBehavior();
        }

        private void InitBehavior()
        {
            root = new BTPrioritySelector(null, "Root");

            //选择节点地面与空中
            BTPrioritySelector patoplNode = new BTPrioritySelector(new MonsterStateCheck(MonsterStateCheck.CheckType.Patrol), "MonsterPatopl");

            BTPrioritySelector trckingNode = new BTPrioritySelector(new MonsterStateCheck(MonsterStateCheck.CheckType.Tracking), "Monster Trcking");

            BTPrioritySelector attackNode = new BTPrioritySelector(new MonsterStateCheck(MonsterStateCheck.CheckType.Attack), "MonsterAttack");

        }
    }
}
