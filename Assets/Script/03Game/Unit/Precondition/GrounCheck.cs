using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WUBT;

public class StateCheck : BTPrecondition
{
    public CheckType groundCheck;
    HeroDatabase heroDatabase;
    public override void Init(Database database)
    {
        heroDatabase = (HeroDatabase)database;
        base.Init(database);
    }
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
                return RayCheck.Check(heroDatabase.wallSliderCheckPos, Vector2.right, heroDatabase.wallSlierSize, heroDatabase.wallMask);
        }
        return false;
    }
    public enum CheckType
    {
        Air, Ground, WallSlider
    }
}
