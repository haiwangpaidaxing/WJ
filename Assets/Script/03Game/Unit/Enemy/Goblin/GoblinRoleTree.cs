using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMMonsterState;

namespace WUBT
{
    public class GoblinRoleTree : BTTree<MonsterDatabase>
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
            InjuredState injuredState = new InjuredState(MonsterStateEnum.Injured, "Injured");

            BTPrioritySelector patoplNode = new BTPrioritySelector(new MonsterStateCheck(MonsterStateEnum.Patrol), "MonsterPatopl");
            PatrolState patrolState = new PatrolState(MonsterStateEnum.Patrol, "Run");

            BTPrioritySelector tackingNode = new BTPrioritySelector(new MonsterStateCheck(MonsterStateEnum.Tracking), "MonsterTacking");
            TackingState tackingState = new TackingState(MonsterStateEnum.Tracking, "Run");

            BTPrioritySelector attackNode = new BTPrioritySelector(new MonsterStateCheck(MonsterStateEnum.Attack), "MonsterAttack");

            AttackState attackState = new AttackState(MonsterStateEnum.Attack, "Attack");

            BTPrioritySelector idleNode = new BTPrioritySelector(new MonsterStateCheck(MonsterStateEnum.Idle), "Idle");
            IdleState idleState = new IdleState(MonsterStateEnum.Idle, "Idle");

            injuredNode.AddChild(injuredState);
            idleNode.AddChild(idleState);
            attackNode.AddChild(attackState);
            tackingNode.AddChild(tackingState);
            patoplNode.AddChild(patrolState);

            //BTPrioritySelector trckingNode = new BTPrioritySelector(new MonsterStateCheck(MonsterStateCheck.CheckType.Tracking), "Monster Trcking");

            //BTPrioritySelector attackNode = new BTPrioritySelector(new MonsterStateCheck(MonsterStateCheck.CheckType.Attack), "MonsterAttack");
            //  ResourceSvc

            root.AddChild(injuredNode);
            root.AddChild(idleNode);
            root.AddChild(attackNode);
            root.AddChild(tackingNode);
            root.AddChild(patoplNode);
            root.Init(database);
        }
    }

}
