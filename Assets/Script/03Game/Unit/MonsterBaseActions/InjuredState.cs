using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WMMonsterState
{
    public class InjuredState : BaseMonsterState
    {

        public InjuredState(MonsterStateEnum monsterStateEnum, string animName, string audioName = "") : base(monsterStateEnum, animName, audioName)
        {
        }
        protected override void Enter()
        {
            base.Enter();
            roleController.injuredState.Enter(mData.injuredData, () =>
            {
                mData.monsterStateEnum = MonsterStateEnum.Patrol;
            });
            mData.updateInjuredCB = UpdateInjured;
        }

        public void UpdateInjured()
        {
            roleController.injuredState.Enter(mData.injuredData, () =>
            {
                mData.monsterStateEnum = MonsterStateEnum.Patrol;
            });
        }

        public override void Clear()
        {
            base.Clear();
            mData.updateInjuredCB -= UpdateInjured;
        }

    }


}
