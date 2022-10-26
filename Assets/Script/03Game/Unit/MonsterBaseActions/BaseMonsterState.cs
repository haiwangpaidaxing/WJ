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
    protected bool isAnimatorOver;

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
    protected override BTResult Execute()
    {
        ISAnimatorOver();
        return base.Execute();
    }
    public virtual void ISAnimatorOver()
    {
        if (!isAnimatorOver)
        {
            AnimatorStateInfo animatorInfo;
            animatorInfo = roleController.animator.GetCurrentAnimatorStateInfo(0);  //要在update获取
            if ((animatorInfo.normalizedTime > 1.0f) && (animatorInfo.IsName(animName)))//normalizedTime：0-1在播放、0开始、1结束 MyPlay为状态机动画的名字
            {
                isAnimatorOver = true;
                AnimatorSkillOver();
            }
        }
    }

    protected virtual void AnimatorSkillOver()
    {
    }
}
