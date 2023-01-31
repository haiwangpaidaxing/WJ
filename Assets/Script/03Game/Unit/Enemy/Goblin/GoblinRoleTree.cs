using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMState;

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


            BTPrioritySelector injuredNode = new BTPrioritySelector(new MonsterStateCheck(MonsterStateEnum.Injured), "Injured");
            MosterInjuredState injuredState = new MosterInjuredState(MonsterStateEnum.Injured, "Injured");

            BTPrioritySelector patoplNode = new BTPrioritySelector(new MonsterStateCheck(MonsterStateEnum.Patrol), "MonsterPatopl");
            MonsterPatrolState patrolState = new MonsterPatrolState(MonsterStateEnum.Patrol, "Run");

            BTPrioritySelector tackingNode = new BTPrioritySelector(new MonsterStateCheck(MonsterStateEnum.Tracking), "MonsterTacking");
            TackingState tackingState = new TackingState(MonsterStateEnum.Tracking, "Run");

            BTPrioritySelector attackNode = new BTPrioritySelector(new MonsterStateCheck(MonsterStateEnum.Attack), "MonsterAttack");

            MonsterAttackState attackState = new MonsterAttackState(MonsterStateEnum.Attack, "Attack");

            BTPrioritySelector idleNode = new BTPrioritySelector(new MonsterStateCheck(MonsterStateEnum.Idle), "Idle");
            MonsterIdleState idleState = new MonsterIdleState(MonsterStateEnum.Idle, "Idle");

            BTPrioritySelector dieNode = new BTPrioritySelector(new MonsterStateCheck(MonsterStateEnum.Die),"Die");
            MonsterDieState dieState = new MonsterDieState(MonsterStateEnum.Die, "Die");

            dieNode.AddChild(dieState);
            injuredNode.AddChild(injuredState);
            idleNode.AddChild(idleState);
            attackNode.AddChild(attackState);
            tackingNode.AddChild(tackingState);
            patoplNode.AddChild(patrolState);

            //BTPrioritySelector trckingNode = new BTPrioritySelector(new MonsterStateCheck(MonsterStateCheck.CheckType.Tracking), "Monster Trcking");

            //BTPrioritySelector attackNode = new BTPrioritySelector(new MonsterStateCheck(MonsterStateCheck.CheckType.Attack), "MonsterAttack");
            //  ResourceSvc

            root.AddChild(dieNode);
            root.AddChild(injuredNode);
            root.AddChild(idleNode);
            root.AddChild(attackNode);
            root.AddChild(tackingNode);
            root.AddChild(patoplNode);
            root.Init(database);
        }
    }

}
