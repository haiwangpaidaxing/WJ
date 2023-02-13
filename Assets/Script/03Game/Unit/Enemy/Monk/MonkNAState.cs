using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMBT;
using WMEffectsSkill;
using WMState;

public class MonkNAState : MonsterComboAttack
{
    MonkDatabase monkDatabase;

    public MonkNAState(MonsterStateEnum monsterStateEnum, string animName, int combosCount = 3, int skillID = 0, string audioName = "") : base(monsterStateEnum, animName, combosCount, skillID, audioName)
    {
    }

    public override void Init(Database database)
    {
        base.Init(database);
        monkDatabase = database.GetComponent<MonkDatabase>();
    }

    protected override void Enter()
    {
        monkDatabase.attackState = RedRoleStateCheck.AttackState.NA;
        base.Enter();
    }

    protected override void AttackCheck()
    {
        switch (index)
        {
            case 0:
                monkDatabase.diaup.skillData.value = 3;
                monkDatabase.injuredData.baseEffectsSkillList = new BaseEffectsSkill[1] { monkDatabase.diaup };
                break;
            case 1:
                monkDatabase.diaup.skillData.value = 3;
                monkDatabase.injuredData.baseEffectsSkillList = new BaseEffectsSkill[1] { monkDatabase.diaup };
                break;
            case 2:
                monkDatabase.diaup.skillData.value = 5;
                monkDatabase.repel.skillData.value = 5;
                monkDatabase.injuredData.baseEffectsSkillList = new BaseEffectsSkill[2] { monkDatabase.diaup, monkDatabase.repel };
                break;
        }
        monkDatabase.injuredData.harm = database.roleAttribute.GetHarm();
        monkDatabase.injuredData.releaser = database.roleController;
        enemyFinder.enemyCB = (injured) => { injured.Injured(monkDatabase.injuredData); };
        enemyFinder.OpenFindTargetAll();
    }

    protected override BTResult Execute()
    {
        return base.Execute();
    }
 
    protected override void ComboOver()
    {
        monkDatabase.skillManager.USE(skillID);
        monkDatabase.attackState = RedRoleStateCheck.AttackState.Null;
        base.ComboOver();
    }

}
