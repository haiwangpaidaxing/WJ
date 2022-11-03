using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttack : BaseSkill
{
    /// <summary>
    /// 敌人搜索器
    /// </summary>
    protected EnemyFinder enemyFinder;
    /// <summary>
    /// 是否在顿帧的状态
    /// </summary>
    protected bool isPauseFrame;

    public BaseAttack(RoleController roleController, ref SkillData skillData) : base(roleController, ref skillData)
    {
        enemyFinder = roleController.GetComponent<EnemyFinder>();
    }
    /// <summary>
    /// 增强打击感
    /// 镜头模糊 摄像头抖动 顿帧
    /// </summary>
    protected virtual void EnhanceSenseShock()
    {
        LensBlurEffects();
        CameraShake();
        PauseFrame();
    }
    /// <summary>
    /// 镜头模糊
    /// </summary>
    protected virtual void LensBlurEffects()
    {
        //镜头模糊
        GameObject lensBlurEffects = ResourceSvc.Single.LoadOrCreate<GameObject>(EffectPath.LensBlur);
        lensBlurEffects.transform.position = roleController.transform.position;
        GameObject.Destroy(lensBlurEffects, 0.2F);
    }
    /// <summary>
    /// 摄像机抖动
    /// </summary>
    protected virtual void CameraShake()
    {
        ///相机
        CameraControl.Single.StartShake();
    }
    /// <summary>
    /// 顿帧
    /// </summary>
    protected virtual void PauseFrame()
    {
        ///卡肉
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
