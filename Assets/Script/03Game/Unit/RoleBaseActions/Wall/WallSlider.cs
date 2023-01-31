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
        heroDatabase = database.GetComponent<HeroDatabase>();
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
        wallSiderSpeed = -1;//重置下滑速度
        base.Enter();
    }
    /// <summary>
    /// 下滑速度
    /// </summary>
    float wallSiderSpeed;
    protected override BTResult Execute()
    {
        if (database.InputDir.y == -1)
        {
            wallSiderSpeed = -5;//加快下滑速度
        }
        if (database.InputDir.x != 0 && enterDir != database.InputDir.x)
        {
            roleController.MoveX(database.InputDir.x, 4);
            roleController.MoveY(1, 7);

        }
        else
        {
            wallSiderSpeed += (-Time.deltaTime);
            roleController.MoveY(1, wallSiderSpeed);
        }
        return base.Execute();
    }
    protected override void Exit()
    {
        base.Exit();
    }
}
