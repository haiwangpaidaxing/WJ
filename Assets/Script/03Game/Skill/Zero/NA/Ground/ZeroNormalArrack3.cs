using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroNormalArrack3 : BaseNormalGroundAttack
{
    ZeroController zeroController;
    public ZeroNormalArrack3(ZeroController roleController, ref SkillData skillData) : base(roleController, ref skillData)
    {
        this.zeroController = roleController;
    }
    public override void USE(Action cb)
    {
        base.USE(cb);
    }

    protected override void AnimatorClipCB()
    {
        base.AnimatorClipCB();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }
    protected override void Damage(IInjured enemy)
    {
        base.Damage(enemy);
        base.EnhanceSenseShock();
    }
    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }
    //��ͨ������������      ����=����������=����ҡ����=�����ܽ���
    protected override void AnimatorSkillOver()
    {
        //��������
        base.AnimatorSkillOver();
    }


    public override void AttackBackswingOver()
    {
        base.AttackBackswingOver();
    }
}
