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
            animatorInfo = animator.GetCurrentAnimatorStateInfo(0);  //Ҫ��update��ȡ
            if ((animatorInfo.normalizedTime > 1.0f) && (animatorInfo.IsName("AirA4")))//normalizedTime��0-1�ڲ��š�0��ʼ��1���� MyPlayΪ״̬������������
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
