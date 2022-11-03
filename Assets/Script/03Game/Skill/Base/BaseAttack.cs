using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttack : BaseSkill
{
    /// <summary>
    /// ����������
    /// </summary>
    protected EnemyFinder enemyFinder;
    /// <summary>
    /// �Ƿ��ڶ�֡��״̬
    /// </summary>
    protected bool isPauseFrame;

    public BaseAttack(RoleController roleController, ref SkillData skillData) : base(roleController, ref skillData)
    {
        enemyFinder = roleController.GetComponent<EnemyFinder>();
    }
    /// <summary>
    /// ��ǿ�����
    /// ��ͷģ�� ����ͷ���� ��֡
    /// </summary>
    protected virtual void EnhanceSenseShock()
    {
        LensBlurEffects();
        CameraShake();
        PauseFrame();
    }
    /// <summary>
    /// ��ͷģ��
    /// </summary>
    protected virtual void LensBlurEffects()
    {
        //��ͷģ��
        GameObject lensBlurEffects = ResourceSvc.Single.LoadOrCreate<GameObject>(EffectPath.LensBlur);
        lensBlurEffects.transform.position = roleController.transform.position;
        GameObject.Destroy(lensBlurEffects, 0.2F);
    }
    /// <summary>
    /// ���������
    /// </summary>
    protected virtual void CameraShake()
    {
        ///���
        CameraControl.Single.StartShake();
    }
    /// <summary>
    /// ��֡
    /// </summary>
    protected virtual void PauseFrame()
    {
        ///����
        if (isPauseFrame) return;
        isPauseFrame = true;
        animator.speed = 0.2F;
        Time.timeScale = 0.2f;
        TimerSvc.instance.AddTask(0.2f * 1000, () =>
        {
            Time.timeScale = 1;
            animator.speed = 1;
            isPauseFrame = false;
        });
        isPauseFrame = true;
    }
    public override void Quit()
    {
        enemyFinder.Close();
    }
    protected virtual void Damage(IInjured enemy)
    {

    }
}
