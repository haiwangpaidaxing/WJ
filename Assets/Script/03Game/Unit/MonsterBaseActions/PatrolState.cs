using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WUBT;

public class PatrolState : BaseRoleState
{

    MonsterDatabase mData;
    int index;
    Vector2 oldPos;
    protected Transform currentPatrol;
    public PatrolState(string animName, string audioName = "") : base(animName, audioName)
    {
    }
    public override void Init(Database database)
    {
        base.Init(database);
        mData = (MonsterDatabase)database;
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
