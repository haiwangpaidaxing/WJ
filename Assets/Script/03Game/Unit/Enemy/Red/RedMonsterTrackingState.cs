using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMState;
using WMBT;

public class RedMonsterTrackingState : MonsterTackingState
{
    public RedMonsterTrackingState(MonsterStateEnum monsterStateEnum, string animName, string audioName = "") : base(monsterStateEnum, animName, audioName)
    {
    }

    protected override void Enter()
    {
        base.Enter();
        //Collider2D collider2D = Physics2D.OverlapBox(mData.veTr + mData.tackingRangeOffset, mData.tackingRangeSize, 0, mData.tackingMask);
        //if (collider2D == null)
        //{
        //    mData.tackingRangeTarget = null;
        //}
        //mData.tackingRangeTarget = collider2D.transform;
    }

    protected override BTResult Execute()
    {
        return base.Execute();
    }
}
