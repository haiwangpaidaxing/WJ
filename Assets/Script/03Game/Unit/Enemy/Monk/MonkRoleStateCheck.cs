using UnityEngine;
using WMBT;
using WMState;

public class MonkRoleStateCheck : MonsterStateCheck
{
    MonkDatabase monk_Data;
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
        if (monk_Data.attackState != AttackState.Null && monk_Data.attackState != AttackState.NA)
        {
            return false;
        }
        if (Physics2D.OverlapBox(mData.veTr + monk_Data.taData.attackRangeOffset, monk_Data.taData.attackRangeSize, 0,monk_Data.taData.attackMask) || monk_Data.attackState == AttackState.NA)
        {
            monk_Data.attackState = AttackState.NA;
            return true;
        }
        return false;
    }

    public override bool Attack3()
    {
        if (monk_Data.attackState != AttackState.Null && monk_Data.attackState != AttackState.NA)
        {
            return false;
        }
        if (Physics2D.OverlapBox(mData.veTr + monk_Data.skillData.attackRangeOffset, monk_Data.skillData.attackRangeSize, 0, monk_Data.skillData.attackMask) || monk_Data.attackState == AttackState.NA)
        {
            monk_Data.attackState = AttackState.NA;
            return true;
        }
        return false;
    }
}
