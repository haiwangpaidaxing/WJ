using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WUBT;

namespace WMState
{
    public class MosterInjuredState : BaseMonsterState
    {

        public MosterInjuredState(MonsterStateEnum monsterStateEnum, string animName, string audioName = "") : base(monsterStateEnum, animName, audioName)
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
            if (isAnimatorOver)
            {
                if (isInjured)
                {
                    if (isGround)
                    {
                        mData.monsterStateEnum = MonsterStateEnum.Patrol;
                    }
                }
            }
            return base.Execute();
        }

        public override void Clear()
        {
            base.Clear();
            mData.injuredData = null;
            mData.updateInjuredCB -= UpdateInjured;
        }

    }


}
