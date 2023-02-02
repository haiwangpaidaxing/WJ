using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMState;
using WMBT;

namespace WMState
{
    public class MonsterDieState : BaseMonsterState
    {
        public MonsterDieState(MonsterStateEnum monsterStateEnum, string animName, string audioName = "") : base(monsterStateEnum, animName, audioName)
        {
        }
        protected override BTResult Execute()
        {
            if (isAnimatorOver)
            {
                GameObject.Destroy(database.gameObject);
            }
            return base.Execute();
        }
    }

}
