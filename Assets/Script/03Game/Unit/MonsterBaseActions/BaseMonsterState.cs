using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMState;
using WUBT;

public class BaseMonsterState : BaseRoleState
{
    protected MonsterDatabase mData;
    protected MonsterStateEnum stateEnum;
 

    public BaseMonsterState(MonsterStateEnum monsterStateEnum, string animName, string audioName = "") : base(animName, audioName)
    {
        this.stateEnum = monsterStateEnum;
    }

    protected override void Enter()
    {
        isAnimatorOver = false;
        mData.monsterStateEnum = stateEnum;
        roleController.MoveX(0,0);
        base.Enter();
    }

    public override void Init(Database database)
    {
        mData = database as MonsterDatabase;
        base.Init(database);
    }
   
}
