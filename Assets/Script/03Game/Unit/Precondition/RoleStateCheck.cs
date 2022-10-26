using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMState;
using WUBT;


public class RoleStateCheck : BTPrecondition
{
    HeroDatabase heroDatabase;
    CheckType currentCheckType;
    public RoleStateCheck(CheckType checkType)
    {
        this.currentCheckType = checkType;
    }

    public override void Init(Database database)
    {
        base.Init(database);
        heroDatabase = (HeroDatabase)database;
    }
    protected override bool DoEvaluate()
    {
        switch (currentCheckType)
        {
            case CheckType.Idle:
                return true;
            case CheckType.Run:
                return Mathf.Abs(database.InputDir.x) > 0 && database.InputDir.y == 0;
            case CheckType.GroundJump:
                return database.InputDir.y == 1 && database.GetComponent<RoleController>().currentSkill == null;
            case CheckType.Fall:
                return database.roleController.RigVelocity.y < 0;

            case CheckType.AirJump:
                return database.roleController.RigVelocity.y > 0 && !BoxCheck.Check(database.GroundCheckPos, database.GroundSize, database.GroundMask);

            case CheckType.WallSlider:
                return Wall();
            case CheckType.Air:
                return !BoxCheck.Check(database.GroundCheckPos, database.GroundSize, database.GroundMask);

            case CheckType.Ground:
                return BoxCheck.Check(database.GroundCheckPos, database.GroundSize, database.GroundMask);

            default:
                return false;
        }
    }

    public bool Wall()
    {
        //需要与墙壁到达一定高度才能激活 否则会来回不断切换别的状态
        Debug.Log("需要与墙壁到达一定高度才能激活 否则会来回不断切换别的状态");
        if (RayCheck.Check(heroDatabase.wallSliderCheckPos, Vector2.right, heroDatabase.wallSlierSize, heroDatabase.wallMask) && !Physics2D.Raycast(database.transform.position, Vector2.down, heroDatabase.detectionHighly, LayerMask.GetMask("Ground"))
             /*&& database.InputDir.x != 0 && database.InputDir.x == database.roleController.roleDir*/)
        {
            return true;
        }
        return false;
    }
    public enum CheckType
    {
        Air, Ground, Idle, Run, GroundJump, Fall, Attack, AirJump, Hutr, WallSlider
    }
}

public class MonsterStateCheck : BTPrecondition
{
    MonsterDatabase mData;
    MonsterStateEnum currentCheckType;
    public MonsterStateCheck(MonsterStateEnum checkType)
    {
        this.currentCheckType = checkType;
    }
    public override void Init(Database database)
    {
        base.Init(database);
        mData = (MonsterDatabase)database;
    }
    protected override bool DoEvaluate()
    {
        switch (currentCheckType)
        {
            case MonsterStateEnum.Patrol:
                return true;
            case MonsterStateEnum.Tracking:
                Collider2D collider2D = Physics2D.OverlapBox(mData.veTr + mData.tackingRangeOffset, mData.tackingRangeSize, 0, mData.tackingMask);
                if (collider2D == null)
                {
                    mData.tackingRangeTarget = null;
                    return false;
                }
                mData.tackingRangeTarget = collider2D.transform;
                return true;
            case MonsterStateEnum.Attack:
                return Attack();
            case MonsterStateEnum.Idle:
                if (mData.monsterStateEnum == MonsterStateEnum.Idle)
                {
                    return true;
                }
                return false;
            case MonsterStateEnum.Injured:
                if (mData.monsterStateEnum == MonsterStateEnum.Injured)
                {
                    return true;
                }
                return false;
            case MonsterStateEnum.Die:
                if (mData.monsterStateEnum == MonsterStateEnum.Die)
                {
                    return true;
                }
                return false;
        }
        return false;
    }
    public bool Attack()
    {
        if (Physics2D.OverlapBox(mData.veTr + mData.attackRangeOffset, mData.attackRangeSize, 0, mData.attackMask) || mData.monsterStateEnum == MonsterStateEnum.Attack)
        {
            return true;
        }
        return false;
    }

}
