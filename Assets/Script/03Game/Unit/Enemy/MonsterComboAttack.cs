using WMBT;
using WMState;

public class MonsterComboAttack : MonsterAttackState
{
    protected int index;//攻击次数下标
    protected int combosCount;
    protected string oldAnimName;

    public MonsterComboAttack(MonsterStateEnum monsterStateEnum, string animName, string audioName = "", int combosCount = 3) : base(monsterStateEnum, animName, audioName)
    {
        oldAnimName = animName;
        this.combosCount = combosCount;
    }

    public MonsterComboAttack(MonsterStateEnum monsterStateEnum, string animName, int combosCount, int skillID, string audioName = "") : base(monsterStateEnum, animName, skillID, audioName)
    {
        oldAnimName = animName;
        this.combosCount = combosCount;
    }

    protected override void Enter()
    {
        animName = oldAnimName + index;
        //   UnityEngine.Debug.Log(animName);
        base.Enter();
    }

    protected override BTResult Execute()
    {
        if (isAnimatorOver && index == combosCount)
        {
            ComboOver();
            return BTResult.Ended;
        }
        else
        {
            return base.Execute();
        }
    }

    protected virtual void ComboOver()
    {
        index = 0;
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
