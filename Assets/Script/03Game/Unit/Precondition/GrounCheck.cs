using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WUBT;

public class StateCheck : BTPrecondition
{
    public CheckType groundCheck;
    public StateCheck(CheckType groundCheck)
    {
        this.groundCheck = groundCheck;

    }
    protected override bool DoEvaluate()
    {
        switch (groundCheck)
        {
            case CheckType.Air:
                return !BoxCheck.Check(database.GroundCheckPos, database.GroundSize, database.GroundMask);
            case CheckType.Ground:
                return BoxCheck.Check(database.GroundCheckPos, database.GroundSize, database.GroundMask);
            case CheckType.WallSlider:
                return RayCheck.Check(database.wallSliderCheckPos, database.wallCheckDir, database.wallSlierSize, database.wallMask);
        }
        return false;
    }
    public enum CheckType
    {
        Air, Ground, WallSlider
    }
}
