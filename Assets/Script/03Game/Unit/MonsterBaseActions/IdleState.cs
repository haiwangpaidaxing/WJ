using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WUBT;

namespace WMMonsterState
{
    public class IdleState : BaseMonsterState
    {
        int idleTime = 1;
        bool isIdle;
        public IdleState(MonsterStateEnum monsterStateEnum, string animName, string audioName = "") : base(monsterStateEnum, animName, audioName)
        {
        }
        int task;

        protected override void Enter()
        {
            base.Enter();
            isIdle = false;
            task = TimerSvc.instance.AddTask(idleTime * 1000, () =>
              {
                  Debug.Log("Monster Idle Change Patrol");
                  isIdle = true;
                  mData.monsterStateEnum = MonsterStateEnum.Patrol;
              }, "Monster Idle Change Patrol");
        }

        protected override BTResult Execute()
        {
            if (isIdle)
            {
                return BTResult.Ended;
            }
            else
            {
                return base.Execute();
            }
        }

        public override void Clear()
        {
            TimerSvc.instance.ReoveTask(task);
            base.Clear();
        }

    }

}
