using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroNormalAttack1 : BaseNormalGroundAttack
{
    ZeroController zeroController;
    public ZeroNormalAttack1(ZeroController roleController, ref SkillData skillData) : base(roleController, ref skillData)
    {
        this.zeroController = roleController;
    }
    public override void USE(Action cb)
    {
        //�ܲ�״̬�ǽ�����ͨ������һ�� ǰ���й���ʱ�����һ������Ч��
        if (Math.Abs(roleController.RigVelocity.x) >= database.roleAttribute.GetMoveSpeed())
        {

        }
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
