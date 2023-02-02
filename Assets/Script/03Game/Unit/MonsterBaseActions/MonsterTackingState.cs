using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMBT;

namespace WMState
{
    public class MonsterTackingState : BaseMonsterState
    {
        public MonsterTackingState(MonsterStateEnum monsterStateEnum, string animName, string audioName = "") : base(monsterStateEnum, animName, audioName)
        {
        }

        protected override BTResult Execute()
        {
            Vector2 tr = mData.transform.position;
            Vector2 dir = Vector2.zero;
            Vector2 target = mData.tackingRangeTarget.position;
            if (target.x > tr.x)
            {
                dir = Vector2.right;
                //right
            }
            else if (target.x < tr.x)
            {
                dir = Vector2.left;
                //left
            }
            database.roleController.MoveX(dir.x, database.roleAttribute.GetMoveSpeed()); 
            return base.Execute();
        }
        public override void Clear()
        {
            database.roleController.MoveX(0, 0);
            base.Clear();
        }
    }
}