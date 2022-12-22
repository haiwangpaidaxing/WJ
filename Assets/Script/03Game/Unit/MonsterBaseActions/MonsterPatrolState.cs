using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMState;
using WUBT;

public class MonsterPatrolState : BaseMonsterState
{

    //巡逻的下标
    protected int index;
    //当前的巡逻点
    protected Transform currentPatrol;
    public MonsterPatrolState(MonsterStateEnum monsterStateEnum, string animName, string audioName = "") : base(monsterStateEnum, animName, audioName)
    {
    }

    protected override void Enter()
    {
        index = 0;
        GetPartolPonit();
        base.Enter();
    }

    protected Transform GetPartolPonit()
    {
        if (mData.patrolPoint.Length==0)
        {
            return null;
        }
        index++;
        index %= mData.patrolPoint.Length;
        currentPatrol = mData.patrolPoint[index];
        return currentPatrol;
    }

    protected override BTResult Execute()
    {
        if (currentPatrol==null)
        {
            return BTResult.Ended;
        }
        Vector2 tr = mData.transform.position;
        Vector2 dir = Vector2.zero;
        if (currentPatrol.position.x > tr.x)
        {
            dir = Vector2.right;
            //right
        }
        else if (currentPatrol.position.x < tr.x)
        {
            dir = Vector2.left;
            //left

        }
        database.roleController.MoveX(dir.x, database.roleAttribute.GetMoveSpeed() / 2);
        if (Mathf.Abs(Vector2.Distance(tr, currentPatrol.position)) < 1)
        {
            GetPartolPonit();
        }
        return base.Execute();
    }

}
