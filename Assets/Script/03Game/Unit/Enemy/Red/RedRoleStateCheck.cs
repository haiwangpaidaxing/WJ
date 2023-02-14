using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMState;
using WMBT;


public class RedRoleStateCheck : MonsterStateCheck
{
   
  //  public AttackState attackState;
    RedMonsterDatabase redData;
    public float targetDistance;
    public RedRoleStateCheck(MonsterStateEnum checkType) : base(checkType)
    {
    }

    public override bool Attack()
    {
        //是否可以攻击
        //if (mData.executionState)
        //{
        //    return false;
        //}
        return true;

    }
    /// <summary>
    /// 普通攻击
    /// </summary>
    /// <returns></returns>
    public override bool Attack1()
    {
        if (redData.attackState != AttackState.Null&& redData.attackState != AttackState.NA)
        {
            return false;
        }
        if (Physics2D.OverlapBox(mData.veTr + mData.attackRangeOffset, mData.attackRangeSize, 0, mData.attackMask) || redData.attackState == AttackState.NA)
        {
            redData. attackState = AttackState.NA;
            return true;
        }
        return false;
    }

    /// <summary>
    /// 重击
    /// </summary>
    /// <returns></returns>
    public override bool Attack2()
    {
        if (redData.attackState != AttackState.Null && redData. attackState != AttackState.TA)
        {
            return false;
        }
        if (Physics2D.OverlapBox(mData.veTr + mData.attackRangeOffset,redData.thumpRangeSize, 0, mData.attackMask) || redData.attackState == AttackState.TA)
        {
            if (redData.attackState != AttackState.TA)
            {
                if (Random.Range(0, 2) == 0)//暂时定义百分之五十概率 使用重击
                {
                    redData.attackState = AttackState.TA;
                    return true;
                }
            }
            else
            {
                return true;
            }
          
        }
        return false;
    }
    /// <summary>
    /// 远程攻击
    /// </summary>
    /// <returns></returns>
    public override bool Attack3()
    {
        if (redData.attackState != AttackState.Null && redData. attackState != AttackState.RA)
        {
            return false;
        }
        targetDistance = Vector2.Distance(mData.transform.position, mData.tackingRangeTarget.position);
        //远程攻击判断
        if (targetDistance >= redData.RangeAttackDis && redData.currentArrow > 0)
        {
            redData.attackState = AttackState.RA;
            return true;
        }
        return false;
    }

    public override bool Die()
    {
        return base.Die();
    }

    public override bool Idle()
    {
        return base.Idle();
    }

    public override void Init(Database database)
    {
        base.Init(database);
        mData = database.GetComponent<MonsterDatabase>(); 
        redData = database.GetComponent<RedMonsterDatabase>();
    }

    public override bool Injured()
    {
        
           return base.Injured();
      
    }

    public override bool Patrol()
    {
        return base.Patrol();
    }

    public override bool Tracking()
    {
        return base.Tracking();
    }
}
