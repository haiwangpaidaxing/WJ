using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMState;
using WUBT;

public class RoleWallSlider : BaseRoleState
{

    HeroDatabase heroDatabase;
    public RoleWallSlider(string animName, string audioName = "") : base(animName, audioName)
    {
    }

    public override void Init(Database database)
    {
        heroDatabase = database as HeroDatabase;
        base.Init(database);
    }

    public int enterDir;
    protected override void Enter()
    {
        enterDir = (int)roleController.roleDir;
        //Collider2D collider2D = Physics2D.OverlapBox(heroDatabase.wallSliderCheckPos, Vector2.right, heroDatabase.wallSlierSize, heroDatabase.wallMask);
        //Debug.Log(collider2D.transform.position);
        //if (RayCheck.Check(heroDatabase.wallSliderCheckPos, Vector2.right, heroDatabase.wallSlierSize, heroDatabase.wallMask) && !Physics2D.Raycast(database.transform.position, Vector2.down, heroDatabase.detectionHighly, LayerMask.GetMask("Ground"))
        //     /*&& database.InputDir.x != 0 && database.InputDir.x == database.roleController.roleDir*/)
        //{
        //}
        base.Enter();
    }

    protected override BTResult Execute()
    {
        if (database.InputDir.x != 0 && enterDir != database.InputDir.x)
        {
            roleController.MoveX(database.InputDir.x, 4);
            roleController.MoveY(1, 7);

        }
        else
        {
            roleController.MoveY(1, (-1 + -Time.deltaTime));
        }
        return base.Execute();
    }
    protected override void Exit()
    {
        base.Exit();
    }
}
