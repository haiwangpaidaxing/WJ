using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMState;
using WUBT;


public class RoleStateCheck : BTPrecondition
{
    HeroDatabase heroDatabase;
    HeroController heroController;
    CheckType currentCheckType;
    public RoleStateCheck(CheckType checkType)
    {
        this.currentCheckType = checkType;
    }

    public override void Init(Database database)
    {
        base.Init(database);
        heroDatabase = (HeroDatabase)database;
        heroController = (HeroController)database.roleController;
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
            //Collider2D[] collider2D = Physics2D.OverlapBoxAll(database.GroundCheckPos, database.GroundSize, 0);
            case CheckType.Injured:
                return heroDatabase.roleState == RoleState.Injured;
            case CheckType.Die:
                return heroDatabase.roleState == RoleState.Die;
            default:
                return false;
        }
    }
    //public LayerMask groundWall = 1 << 0;
    public bool Wall()
    {
        RaycastHit2D[] raycastHit2D = Physics2D.RaycastAll(heroDatabase.wallSliderCheckPos + heroDatabase.wallOffset, Vector2.right, heroDatabase.wallSlierSize);
        Debug.DrawRay(heroDatabase.wallSliderCheckPos, Vector3.right * heroDatabase.wallSlierSize, Color.blue);
        //进入爬墙检测时
        foreach (var item in raycastHit2D)
        {
            //   Debug.Log(heroDatabase.wallMask.value);
            //if (LayerMask.LayerToName(item.collider.gameObject.layer) == heroDatabase.wallMask)
            //{

            //}
            LayerMask layerMask = 1 << item.collider.gameObject.layer;
            if (layerMask == heroDatabase.wallMask || item.collider.tag == "Ground")
            {
                if (!Physics2D.Raycast(database.transform.position, Vector2.down, heroDatabase.detectionHighly, LayerMask.GetMask("Ground")))
                {
                    return true;
                }
            }
        }
        //需要与墙壁到达一定高度才能激活 否则会来回不断切换别的状态
        //     Debug.Log("需要与墙壁到达一定高度才能激活 否则会来回不断切换别的状态");
        return false;
    }
    public enum CheckType
    {
        Air,
        Ground,
        Idle,
        Run,
        GroundJump,
        Fall,
        Attack,
        AirJump,
        Hutr,
        WallSlider,
        Injured,
        Die
    }
}

public class MonsterStateCheck : BTPrecondition
{
    protected MonsterDatabase mData;
    protected MonsterStateEnum currentCheckType;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="checkType">初始状态</param>
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
                return Patrol();
            case MonsterStateEnum.Tracking:
                return Tracking();
            case MonsterStateEnum.Attack:
                return Attack();
            case MonsterStateEnum.Attack1:
                return Attack1();
            case MonsterStateEnum.Attack2:
                return Attack2();
            case MonsterStateEnum.Attack3:
                return Attack3();
            case MonsterStateEnum.Idle:
                return Idle();
            case MonsterStateEnum.Injured:
                return Injured();
            case MonsterStateEnum.Die:
                return Die();
        }
        return false;
    }
    public virtual bool Attack()
    {
        if (Physics2D.OverlapBox(mData.veTr + mData.attackRangeOffset, mData.attackRangeSize, 0, mData.attackMask) || mData.monsterStateEnum == MonsterStateEnum.Attack)
        {
            return true;
        }
        return false;
    }

    public virtual bool Attack1()
    {
        return false;
    }
    public virtual bool Attack2()
    {
        return false;
    }
    public virtual bool Attack3()
    {
        return false;
    }

    public virtual bool Patrol()//巡逻
    {
        return true;
    }

    public virtual bool Tracking()//追踪敌人
    {
        Collider2D collider2D = Physics2D.OverlapBox(mData.veTr + mData.tackingRangeOffset, mData.tackingRangeSize, 0, mData.tackingMask);
        if (collider2D == null)
        {
            mData.tackingRangeTarget = null;
            return false;
        }
        mData.tackingRangeTarget = collider2D.transform;
        return true;
    }

    public virtual bool Idle()
    {
        if (mData.monsterStateEnum == MonsterStateEnum.Idle)
        {
            return true;
        }
        return false;
    }

    public virtual bool Injured()
    {

        if (mData.monsterStateEnum == MonsterStateEnum.Injured)
        {
            return true;
        }
        return false;
    }

    public virtual bool Die()
    {
        if (mData.monsterStateEnum == MonsterStateEnum.Die)
        {
            return true;
        }
        return false;
    }
}
