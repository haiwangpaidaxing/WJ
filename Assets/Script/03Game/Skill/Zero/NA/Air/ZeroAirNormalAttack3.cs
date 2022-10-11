using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroAirNormalAttack3 : BaseAirNormalAttack
{
    bool isGround;
    ZeroController zeroController;
    public ZeroAirNormalAttack3(ZeroController roleController, ref SkillData skillData) : base(roleController, ref skillData)
    {
        this.zeroController = roleController;
    }

    public override void SetGravity(float scale)
    {

    }




    public override void OnFixedUpdate()
    {

        isGround = BoxCheck.Check(database.GroundCheckPos, database.GroundSize, database.GroundMask);
        if (isGround)
        {
            animator.Play("AirA4");//
        }
        else
        {
            roleController.MoveY(-5, 10);
        }
    }

    public override void ISAnimatorOver()
    {
        if (!isAnimatorOver)
        {
            AnimatorStateInfo animatorInfo;
            animatorInfo = animator.GetCurrentAnimatorStateInfo(0);  //要在update获取
            if ((animatorInfo.normalizedTime > 1.0f) && (animatorInfo.IsName("AirA4")))//normalizedTime：0-1在播放、0开始、1结束 MyPlay为状态机动画的名字
            {
                isAnimatorOver = true;
                AnimatorSkillOver();
            }
        }
    }
    public override void ISPlayAnimator()
    {
    }

    protected override void AnimatorSkillOver()
    {
        base.AnimatorSkillOver();
    }

    public override void AttackBackswingOver()
    {
        skillEndCB?.Invoke();
        skillData.comboCurrentValidTime = skillData.comboValidTime;
    }
    protected override void Damage(IInjured enemy)
    {
        base.Damage(enemy);
        base.EnhanceSenseShock();

    }
    protected override void AnimatorClipCB()
    {
        base.AnimatorClipCB();
    }
}
