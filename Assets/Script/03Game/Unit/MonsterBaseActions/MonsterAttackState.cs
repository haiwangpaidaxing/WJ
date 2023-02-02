using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMBT;

namespace WMState
{
    public class MonsterAttackState : BaseMonsterState
    {
        protected EnemyFinder enemyFinder;
        public override void Init(Database database)
        {
            base.Init(database);
            enemyFinder = database.GetComponent<EnemyFinder>();
        }
        public MonsterAttackState(MonsterStateEnum monsterStateEnum, string animName, string audioName = "") : base(monsterStateEnum, animName, audioName)
        {

        }

        protected override void Enter()
        {
            enemyFinder.Close();
            base.Enter();
            Transform target = mData.tackingRangeTarget;
            if (target == null)
            {
                mData.monsterStateEnum = MonsterStateEnum.Idle;
                return;
            }
            Vector2 tr = mData.transform.position;
            if (target.position.x > tr.x)
            {
                roleController.SyncImage(1);
            }
            else if (target.position.x < tr.x)
            {

                roleController.SyncImage(-1);
            }//矫正需要面向玩家英雄
            roleController.animatorClipCb = AttackCheck;
        }

        protected virtual void AttackCheck()
        {
            InjuredData injuredData = new InjuredData();
             injuredData.harm = database.roleAttribute.GetHarm();
              injuredData.releaser = database.roleController;
             enemyFinder.enemyCB = (injured) => { injured.Injured(injuredData); };
            enemyFinder.OpenFindTargetAll();
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

        public override void Clear()
        {
            enemyFinder.Close();
            base.Clear();
        }
    }

}

