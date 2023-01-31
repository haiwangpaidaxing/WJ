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
       
    }
    public override void Init(Database database)
    {
        base.Init(database);
        redData = database.GetComponent<RedMonsterDatabase>();
    }
    protected override void Enter()
    {
        redData.attackState = RedRoleStateCheck.AttackState.TA;
        base.Enter();
        if (redData.tackingRangeTarget.position.x > redData.transform.position.x)
        {
            roleController.MoveX(1,5);
        }
        else
        {
            roleController.MoveX(-1,5);
        }
    }
    protected override BTResult Execute()
    {

        if (Vector2.Distance(roleController.transform.position, redData.tackingRangeTarget.position)<=0.5f)
        {
            roleController.MoveX(0, 0);
        }
        return base.Execute();
        
        //if ()
        //{

        //}
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
