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
    RedMonsterDatabase redData;

    public override void Init(Database database)
    {
        base.Init(database);
        redData = database.GetComponent<RedMonsterDatabase>();
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
        animName = oldAnimName + index;
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
        if (isAnimatorOver && index == combosCount)
        {
            redData.attackState = RedRoleStateCheck.AttackState.Null;
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
