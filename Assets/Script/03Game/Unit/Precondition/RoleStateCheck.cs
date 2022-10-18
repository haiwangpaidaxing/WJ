using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WUBT;

public class RoleStateCheck : BTPrecondition
{
    CheckType currentCheckType;
    public RoleStateCheck(CheckType checkType)
    {
        this.currentCheckType = checkType;
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
                return database.roleController.RigVelocity.y > 0;

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
        if (RayCheck.Check(database.wallSliderCheckPos, database.wallCheckDir, database.wallSlierSize, database.wallMask)
            /* && database.InputDir.x != 0 && database.InputDir.x == database.roleController.roleDir*/)
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
