using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WUBT;

namespace WMMonsterState
{
    public class AttackState : BaseMonsterState
    {
        public AttackState(MonsterStateEnum monsterStateEnum, string animName, string audioName = "") : base(monsterStateEnum, animName, audioName)
        {
        }

        protected override BTResult Execute()
        {
            if (isAnimatorOver)
            {
                mData.monsterStateEnum = MonsterStateEnum.Idle;
                return BTResult.Ended;
            }
            else
            {
                return base.Execute();
            }
        }
    }
}

