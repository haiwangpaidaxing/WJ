using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMBT;
using WMEffectsSkill;
using WMState;

public class MonkTAState : MonsterAttackState
{
    InjuredData injuredData;
    MonkDatabase monkDatabase;
    public MonkTAState(MonsterStateEnum monsterStateEnum, string animName, int skillID = 0, string audioName = "") : base(monsterStateEnum, animName, skillID, audioName)
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
        monkDatabase.attackState = RedRoleStateCheck.AttackState.TA;
        LookTarget(monkDatabase.tackingRangeTarget);

    }

    protected override void AttackCheck()
    {
        CameraControl.Single.StartShake();
        monkDatabase.repel.skillData.value = 5;
        monkDatabase.diaup.skillData.value = 5;
        injuredData.baseEffectsSkillList = new BaseEffectsSkill[2] { monkDatabase.diaup, monkDatabase.repel };
        injuredData.harm = database.roleAttribute.GetHarm();
        injuredData.releaser = database.roleController;
        enemyFinder.enemyCB = (injured) => {
            CameraControl.Single.StartShake();
            injured.Injured(injuredData); };
        enemyFinder.OpenFindTargetAll();
    }

    public override void Clear()
    {
        if (monkDatabase.skillManager.Find(skillID))
        {
            monkDatabase.skillManager.USE(skillID);
        }
        base.Clear();
    }
    protected override void AnimatorSkillOver()
    {
        monkDatabase.skillManager.USE(skillID);
        monkDatabase.attackState = MonsterStateCheck.AttackState.Null;

    }

}
