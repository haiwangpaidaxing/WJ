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

        protected override void Enter()
        {
            base.Enter();
            isIdle = false;
            TimerSvc.instance.AddTask(idleTime * 1000, () =>
            {
                isIdle = true;
                mData.monsterStateEnum = MonsterStateEnum.Patrol;
            }, "");
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

    }

}
