using System;
using UnityEngine;

public class BaseNormalGroundAttack : BaseAttack
{
    public BaseNormalGroundAttack(RoleController roleController, ref SkillData skillData) : base(roleController, ref skillData)
    {
    }
    /// <summary>
    /// ʹ�ü���ʱ�������ֻ����һ��
    /// </summary>
    /// <param name="cb">���ܽ���ʱ��Ļص�</param>
    public override void USE(Action cb)
    {
        base.USE(cb);
      
        roleController.SyncImage(InputController.Single.InputDir.x);
        roleController.MoveX(0, 0);
        roleController.MoveY(0, 0);//��ֹ���ƶ���ʹ��ʱ ����Ϊ�������л���
                                   //��һ����� ��� ���뷽��=���﷽��  ��ɫ������ǰһС�ε�λ��
                                   //�ڶ������ ���뷽��=���﷽�� ||���뷽��==0  ��ɫվ������
        if (InputController.Single.InputDir.x != 0)
        {
            Vector2 dir = Vector2.zero;
            dir.x = InputController.Single.InputDir.x;
            Debug.DrawRay(roleController.transform.position,dir*2,Color.red);
            if (RayCheck.Check(roleController.transform.position, dir, 2))
            {
                return;
            }
         //   roleController.MoveX(dir.x, 10, ForceMode2D.Impulse);//����������
          //  roleController.RoleRigidbody.position += dir * 1f;//˲���ƶ��ᴩԽǿ ��ʹ�����߼��ǰ���Ƿ���ǽ�� TODO
        }
       
    }

    protected override void AnimatorClipCB()
    {
        base.AnimatorClipCB();
        //  Debug.Log("TODO�����˺�");
        //TODO�����˺�
        enemyFinder.enemyCB = Damage;
        //TODO�����˺�
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
        base.OnFixedUpdate();
    }
    //��ͨ������������      ����=����������=����ҡ����=�����ܽ���
    protected override void AnimatorSkillOver()
    {
        //��������
        base.AnimatorSkillOver();
        //�����ĺ�ҡ
        TimerSvc.instance.AddTask(skillData.attackBackswing * 1000, AttackBackswingOver, "----��ͨ������ҡ---");//
    }
    public override void AttackBackswingOver()
    {
        skillEndCB?.Invoke();
        skillData.comboCurrentValidTime = skillData.comboValidTime;
        //��һ�����ܵ���Чʱ��
        roleController.animatorClipCb = null;
        enemyFinder.Close();
        //���ܵĺ�ҡ����
    }
}
