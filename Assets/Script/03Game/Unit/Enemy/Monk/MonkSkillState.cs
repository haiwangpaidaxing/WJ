using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMBT;
using WMEffectsSkill;
using WMState;

public class MonkSkillState : MonsterAttackState
{
    InjuredData injuredData;
    MonkDatabase monkDatabase;
    int index;
    public MonkSkillState(MonsterStateEnum monsterStateEnum, string animName, int skillID = 0, string audioName = "") : base(monsterStateEnum, animName, skillID, audioName)
    {
    }
    public override void Init(Database database)
    {
        base.Init(database);
        monkDatabase = database.GetComponent<MonkDatabase>();
        injuredData = new InjuredData();
    }
    protected override void Enter()
    {
        base.Enter();
        monkDatabase.attackState = RedRoleStateCheck.AttackState.Skill;

    }
    protected override void AttackCheck()
    {
        enemyFinder.Close();
        switch (index)
        {
            case 0:
                monkDatabase.diaup.skillData.value = 1;
                injuredData.baseEffectsSkillList = new BaseEffectsSkill[1] { monkDatabase.diaup };
                break;
            case 1:
                monkDatabase.diaup.skillData.value = 3;
                injuredData.baseEffectsSkillList = new BaseEffectsSkill[1] { monkDatabase.diaup };
                break;
            case 2:
                monkDatabase.diaup.skillData.value = 6;
                injuredData.baseEffectsSkillList = new BaseEffectsSkill[1] { monkDatabase.diaup };
                break;
            case 3:
                monkDatabase.repel.skillData.value = 5;
                monkDatabase.diaup.skillData.value = 5;
                injuredData.baseEffectsSkillList = new BaseEffectsSkill[2] { monkDatabase.diaup, monkDatabase.repel };
                break;

        }
        Debug.Log(index);
        injuredData.harm = database.roleAttribute.GetHarm();
        injuredData.releaser = database.roleController;
        enemyFinder.enemyCB = (injured) => { injured.Injured(injuredData); };
        enemyFinder.OpenFindTargetAll();
        index++;
    }

    protected override void AnimatorSkillOver()
    {
        index = 0;
        monkDatabase.skillManager.USE(skillID);
        monkDatabase.attackState = MonsterStateCheck.AttackState.Null;

    }
}
