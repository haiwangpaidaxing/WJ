using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WUBT;

namespace WMMonsterState
{
    public class InjuredState : BaseMonsterState
    {   

        public InjuredState(MonsterStateEnum monsterStateEnum, string animName, string audioName = "") : base(monsterStateEnum, animName, audioName)
        {
        }
        public bool isInjured;
        protected override void Enter()
        {
            base.Enter();
            EnterInjured();
            mData.updateInjuredCB = UpdateInjured;
        }

        public void UpdateInjured()
        {
            EnterInjured();
        }

        public void EnterInjured()
        {
            isInjured = false;
            roleController.injuredState.Enter(mData.injuredData, () =>
            {
                isInjured = true;
            });
        }

        protected override BTResult Execute()
        {

            bool isGround = BoxCheck.Check(database.GroundCheckPos, database.GroundSize, database.GroundMask);
            if (isInjured)
            {
                if (isGround)
                {
                    mData.monsterStateEnum = MonsterStateEnum.Patrol;
                }
            }
            return base.Execute();
        }

        public override void Clear()
        {
            base.Clear();
            mData.updateInjuredCB -= UpdateInjured;
        }

    }


}
