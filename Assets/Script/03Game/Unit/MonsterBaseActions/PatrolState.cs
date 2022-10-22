using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMMonsterState;
using WUBT;

public class PatrolState : BaseMonsterState
{

    int index;
    protected Transform currentPatrol;
    public PatrolState(MonsterStateEnum monsterStateEnum, string animName, string audioName = "") : base(monsterStateEnum, animName, audioName)
    {
    }

    protected override void Enter()
    {
        index = 0;
        GetPartolPonit();
        base.Enter();
    }

    public Transform GetPartolPonit()
    {
        index++;
        index %= mData.patrolPoint.Length;
        currentPatrol = mData.patrolPoint[index];
        return currentPatrol;
    }

    protected override BTResult Execute()
    {
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
        database.roleController.MoveX(dir.x, database.roleAttribute.GetMoveSpeed());
        if (Mathf.Abs(Vector2.Distance(tr, currentPatrol.position)) < 1)
        {
            GetPartolPonit();
        }
        return base.Execute();
    }

}
