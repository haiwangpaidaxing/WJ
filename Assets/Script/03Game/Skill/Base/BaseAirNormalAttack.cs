using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAirNormalAttack : BaseAttack
{
    public BaseAirNormalAttack(RoleController roleController, ref SkillData skillData) : base(roleController, ref skillData)
    {
    }
    public override void USE(Action cb)
    {
        base.USE(cb);
        roleController.SyncImage(InputController.Single.InputDir.x);
        roleController.MoveX(0, 0);//��ֹ���ƶ���ʹ��ʱ ����Ϊ�������л���
        roleController.MoveY(0, 0);//��ֹ���ƶ���ʹ��ʱ ����Ϊ�������л���
                                   //��һ����� ��� ���뷽��=���﷽��  ��ɫ������ǰһС�ε�λ��
                                   //�ڶ������ ���뷽��=���﷽�� ||���뷽��==0  ��ɫվ������
        if (InputController.Single.InputDir.x != 0)
        {
            //Vector3 dir = Vector3.zero;
            //dir.x = InputController.Single.InputDir.x;
            //roleController.transform.position += dir * 0.5f;
        }
        SetGravity(0);//�������� �Ϳ�
        Debug.Log("����ʹ����ͨ����");
    }

    public virtual void SetGravity(float scale)
    {
        roleController.RoleRigidbody.gravityScale = scale;
    }


    protected override void AnimatorClipCB()
    {
        //TODO�����˺�
        base.AnimatorClipCB();
        enemyFinder.enemyCB = Damage;
        enemyFinder.OpenFindTargetAll();
    }
    protected override void Damage(IInjured enemy)
    {
        base.Damage(enemy);
        InjuredData injuredData = new InjuredData();
        injuredData.baseEffectsSkillList = skillData.baseEffectsSkills;
        injuredData.harm = roleController.roleAttribute.GetHarm();
        injuredData.releaser = roleController;
        enemy.Injured(injuredData);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnFixedUpdate()
    {
      //  roleController.MoveY(0, 0);
        base.OnFixedUpdate();
    }
    //��ͨ������������      ����=����������=����ҡ����=�����ܽ���
    protected override void AnimatorSkillOver()
    {
        //��������
        base.AnimatorSkillOver();
        enemyFinder.Close();
        //�����ĺ�ҡ
        TimerSvc.instance.AddTask(skillData.attackBackswing * 1000, AttackBackswingOver, "----��ͨ������ҡ---");//
    }
    public override void AttackBackswingOver()
    {
        skillEndCB?.Invoke();
        skillData.comboCurrentValidTime = skillData.comboValidTime;
        roleController.animatorClipCb = null;
        //��һ�����ܵ���Чʱ��
        SetGravity(1);
        //���ܵĺ�ҡ����
    }

}
