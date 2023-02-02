using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMEffectsSkill;
using WMState;
using WMBT;
//½ÇÉ«µÄÆÕÍ¨¹¥»÷×´Ì¬
public class RedNAState : MonsterComboAttack
{
    RedMonsterDatabase redData;

    public RedNAState(MonsterStateEnum monsterStateEnum, string animName, string audioName = "", int combosCount = 3) : base(monsterStateEnum, animName, audioName, combosCount)
    {
    }

    public override void Init(Database database)
    {
        base.Init(database);
        redData = database.GetComponent<RedMonsterDatabase>();
    }
   
    protected override void AttackCheck()
    {
        switch (index)
        {
            case 0:
                redData.diaup.skillData.value = 3;
                redData.injuredData.baseEffectsSkillList = new BaseEffectsSkill[1] { redData.diaup };
                break;
            case 1:
                redData.diaup.skillData.value = 3;
                redData.injuredData.baseEffectsSkillList = new BaseEffectsSkill[1] { redData.diaup };
                break;
            case 2:
                redData.diaup.skillData.value = 5;
                redData.repel.skillData.value = 5;
                redData.injuredData.baseEffectsSkillList = new BaseEffectsSkill[2] { redData.diaup, redData.repel };
                break;
        }
        redData.injuredData.harm = database.roleAttribute.GetHarm();
        redData.injuredData.releaser = database.roleController;
        enemyFinder.enemyCB = (injured) => { injured.Injured(redData.injuredData); };
        enemyFinder.OpenFindTargetAll();
    }
    protected override void Enter()
    {
        redData.attackState = RedRoleStateCheck.AttackState.NA;
        if (redData.tackingRangeTarget.position.x > redData.transform.position.x)
        {
            roleController.MoveX(1, 2);
        }
        else
        {
            roleController.MoveX(-1, 2);
        }
        base.Enter();
    }

    protected override BTResult Execute()
    {
        return base.Execute();
    }

    protected override void ComboOver()
    {
        redData.attackState = RedRoleStateCheck.AttackState.Null;
        base.ComboOver();
    }

}
