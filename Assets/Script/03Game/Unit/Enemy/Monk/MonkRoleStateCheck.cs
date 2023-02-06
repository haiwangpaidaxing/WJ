using UnityEngine;
using WMBT;
using WMState;

public class MonkRoleStateCheck : MonsterStateCheck
{
    MonkDatabase monkDatabase;
    public override void Init(Database database)
    {
        base.Init(database);
        monkDatabase = database.GetComponent<MonkDatabase>();

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
        if (monkDatabase.attackState != AttackState.Null && monkDatabase.attackState != AttackState.NA)
        {
            return false;
        }
        if (Physics2D.OverlapBox(mData.veTr + mData.attackRangeOffset, mData.attackRangeSize, 0, mData.attackMask) || monkDatabase.attackState == AttackState.NA)
        {
            monkDatabase.attackState = AttackState.NA;
            return true;
        }
        return false;
    }
}
