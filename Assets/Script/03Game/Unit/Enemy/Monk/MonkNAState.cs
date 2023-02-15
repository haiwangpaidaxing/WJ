using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMBT;
using WMEffectsSkill;
using WMState;

public class MonkNAState : MonsterComboAttack
{
    MonkDatabase monkDatabase;
    InjuredData injuredData;
    public MonkNAState(MonsterStateEnum monsterStateEnum, string animName, int combosCount = 3, int skillID = 0, string audioName = "") : base(monsterStateEnum, animName, combosCount, skillID, audioName)
    {
    }

    public override void Init(Database database)
    {
        base.Init(database);
        monkDatabase = database.GetComponent<MonkDatabase>();
        injuredData = new InjuredData();
        if (index ==0)
        {
            LookTarget(monkDatabase.tackingRangeTarget);
        }
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
                injuredData.baseEffectsSkillList = new BaseEffectsSkill[1] { monkDatabase.diaup };
                break;
            case 1:
                monkDatabase.diaup.skillData.value = 3;
                injuredData.baseEffectsSkillList = new BaseEffectsSkill[1] { monkDatabase.diaup };
                break;
            case 2:
              
                monkDatabase.diaup.skillData.value = 5;
                monkDatabase.repel.skillData.value = 5;
                injuredData.baseEffectsSkillList = new BaseEffectsSkill[2] { monkDatabase.diaup, monkDatabase.repel };
                break;
        }
        injuredData.harm = database.roleAttribute.GetHarm();
        injuredData.releaser = database.roleController;
        enemyFinder.enemyCB = (injured) => {
            CameraControl.Single.StartShake();
            injured.Injured(injuredData); };
        enemyFinder.OpenFindTargetAll();
    }

    protected override BTResult Execute()
    {
        return base.Execute();
    }
    public override void Clear()
    {
        if (monkDatabase.skillManager.Find(skillID))
        {
            monkDatabase.skillManager.USE(skillID);
        }
        base.Clear();
    }
    protected override void ComboOver()
    {
        monkDatabase.skillManager.USE(skillID);
        monkDatabase.attackState = RedRoleStateCheck.AttackState.Null;
        base.ComboOver();
    }

}
