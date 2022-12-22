using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMEffectsSkill;
using WMState;
using WUBT;
//角色的普通攻击状态
public class RedNAState : MonsterAttackState
{
    private int index;//攻击次数下标
    private int combosCount;
    public string oldAnimName;
    RedMonsterDatabase redMonsterDatabase;

    public override void Init(Database database)
    {
        base.Init(database);
        redMonsterDatabase = mData as RedMonsterDatabase;
    }
    public RedNAState(MonsterStateEnum monsterStateEnum, string animName, string audioName = "", int combosCount = 3) : base(monsterStateEnum, animName, audioName)
    {
        oldAnimName = animName;
        this.combosCount = combosCount;

    }
    protected override void AttackCheck()
    {
        switch (index)
        {
            case 0:
                redMonsterDatabase.diaup.skillData.value = 3;
                redMonsterDatabase.injuredData.baseEffectsSkillList = new BaseEffectsSkill[1] { redMonsterDatabase.diaup };
                break;
            case 1:
                redMonsterDatabase.diaup.skillData.value = 3;
                redMonsterDatabase.injuredData.baseEffectsSkillList = new BaseEffectsSkill[1] { redMonsterDatabase.diaup };
                break;
            case 2:
                redMonsterDatabase.diaup.skillData.value = 5;
                redMonsterDatabase.repel.skillData.value = 5;
                redMonsterDatabase.injuredData.baseEffectsSkillList = new BaseEffectsSkill[2] { redMonsterDatabase.diaup, redMonsterDatabase.repel };
                break;
        }
        redMonsterDatabase.injuredData.harm = database.roleAttribute.GetHarm();
        redMonsterDatabase.injuredData.releaser = database.roleController;
        enemyFinder.enemyCB = (injured) => { injured.Injured(redMonsterDatabase.injuredData); };
        enemyFinder.OpenFindTargetAll();
    }
    protected override void Enter()
    {
        redMonsterDatabase.attackState = RedRoleStateCheck.AttackState.NA;
        animName = oldAnimName + index;
        base.Enter();
    }

    protected override BTResult Execute()
    {
        if (isAnimatorOver && index == combosCount)
        {
            redMonsterDatabase.attackState = RedRoleStateCheck.AttackState.Null;
            index = 0;
            return BTResult.Ended;
        }
        else
        {
            return base.Execute();
        }
    }

    protected override void AnimatorSkillOver()
    {
        index++;
        if (index != combosCount)
        {
            animName = oldAnimName + index;
            animator.Play(animName);

        }
        base.AnimatorSkillOver();
    }

}
