using UnityEngine;
using WMBT;
using WMState;

public class MonkRoleStateCheck : MonsterStateCheck
{
    MonkDatabase monk_Data;
    private int skillID;
    public override void Init(Database database)
    {
        base.Init(database);
        monk_Data = database.GetComponent<MonkDatabase>();

    }
    public MonkRoleStateCheck(MonsterStateEnum checkType) : base(checkType)
    {
    }

    public override bool Attack()
    {
        return true;
    }

    public override bool Attack1()
    {
        if (monk_Data.skillManager.Find((int)MonsterStateEnum.Attack1))
        {
            return false;
        }
        if (monk_Data.attackState != AttackState.Null && monk_Data.attackState != AttackState.NA)
        {
            return false;
        }

        if (Physics2D.OverlapBox(mData.veTr + mData.attackRangeOffset, mData.attackRangeSize, 0, mData.attackMask) || monk_Data.attackState == AttackState.NA)
        {
            monk_Data.attackState = AttackState.NA;
            return true;
        }

        return false;
    }

    public override bool Attack2()
    {
        if (monk_Data.skillManager.Find((int)MonsterStateEnum.Attack2))
        {
            return false;
        }

        if (monk_Data.attackState != AttackState.Null && monk_Data.attackState != AttackState.TA)
        {
            return false;
        }
        if (Physics2D.OverlapBox(mData.veTr + monk_Data.taData.attackRangeOffset, monk_Data.taData.attackRangeSize, 0, monk_Data.taData.attackMask) || monk_Data.attackState == AttackState.TA)
        {
            monk_Data.attackState = AttackState.TA;
            return true;
        }
        return false;
    }

    public override bool Attack3()
    {
        if (monk_Data.skillManager.Find((int)MonsterStateEnum.Attack3))
        {
            return false;
        }
        if (monk_Data.attackState != AttackState.Null && monk_Data.attackState != AttackState.Skill)
        {
            return false;
        }
        if (Physics2D.OverlapBox(mData.veTr + monk_Data.skillData.attackRangeOffset, monk_Data.skillData.attackRangeSize, 0, monk_Data.skillData.attackMask) || monk_Data.attackState == AttackState.Skill)
        {
            monk_Data.attackState = AttackState.Skill;
            return true;
        }
        return false;
    }
}
