using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMEffectsSkill;
using WMState;
using WUBT;

public class RedRoleTAState : MonsterAttackState
{
    //Red µÄÖØ»÷×´Ì¬
    RedMonsterDatabase redData;
    public RedRoleTAState(MonsterStateEnum monsterStateEnum, string animName, string audioName = "") : base(monsterStateEnum, animName, audioName)
    {
        redData = mData as RedMonsterDatabase;
    }
    public override void Init(Database database)
    {
        base.Init(database);
        redData = mData as RedMonsterDatabase;
    }
    protected override void Enter()
    {
        redData.attackState = RedRoleStateCheck.AttackState.TA;
        base.Enter();
    }
    protected override void AttackCheck()
    {
        redData.diaup.skillData.value = 5;
        redData.repel.skillData.value = 5;
        redData.injuredData.baseEffectsSkillList = new BaseEffectsSkill[2] { redData.diaup, redData.repel };
        //InjuredData injuredData = new InjuredData();
        //EffectsSkillData efData = new EffectsSkillData();
        //Diaup diaup;
        //Repel repel;
        //diaup = new Diaup(efData);
        //repel = new Repel(efData);
        //diaup.skillData.value = 10;
        //repel.skillData.value = 5;
        //injuredData.baseEffectsSkillList = new BaseEffectsSkill[1] { diaup };
        redData.injuredData.harm = database.roleAttribute.GetHarm();
        redData.injuredData.releaser = database.roleController;
        enemyFinder.enemyCB = (injured) =>
        {
            injured.Injured(redData.injuredData);
        };
        enemyFinder.OpenFindTargetAll();
    }
    protected override void AnimatorSkillOver()
    {
        redData.attackState = RedRoleStateCheck.AttackState.Null;
        base.AnimatorSkillOver();
    }
}
