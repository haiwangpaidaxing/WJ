using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMState;

public class MonkSkillState : MonsterAttackState
{
    public MonkSkillState(MonsterStateEnum monsterStateEnum, string animName, int skillID = 0, string audioName = "") : base(monsterStateEnum, animName, skillID, audioName)
    {
    }
    protected override void Enter()
    {
        base.Enter();
    }
    protected override void AttackCheck()
    {
        enemyFinder.Close();
        base.AttackCheck();
    }
}
