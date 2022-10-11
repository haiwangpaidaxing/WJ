using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseComboController : BaseSkill
{
    /// <summary>
    /// ��ǰ�����±�
    /// </summary>
    protected int currentSkillInedx;
    protected int comboCurrentValidTask = -1;
    protected BaseSkill currentSkill;
    protected BaseSkill[] comboSkillList;
    /// <summary>
    ///��ǰ��ҡ���ܵ�����ID 
    /// </summary>
    protected int currentBackswingOverTaskID;
    public BaseComboController(RoleController roleController, ref SkillData skillData, ref BaseSkill[] comboSkillList)
    {
        this.roleController = roleController;
        this.skillData = skillData;
        this.comboSkillList = comboSkillList;
    }

    public override void USE(Action cb)
    {
        if (skillEndCB == null)//
        {
            base.USE(cb);
            if (comboCurrentValidTask != -1)
            {
                TimerSvc.instance.ReoveTask(comboCurrentValidTask);
                comboCurrentValidTask = -1;
            }
            currentSkill = comboSkillList[currentSkillInedx];
            currentSkill.USE(CurrentSkillOverCB);
        }
    }

    /// <summary>
    /// ��ǰ���ܽ����Ļص�
    /// </summary>
    protected void CurrentSkillOverCB()
    {
        currentSkillInedx++;
        currentSkillInedx %= comboSkillList.Length;
        skillEndCB?.Invoke();
        skillEndCB = null;
        currentSkill.skillData.comboCurrentValidTime = currentSkill.skillData.comboValidTime;
        comboCurrentValidTask = TimerSvc.instance.AddTask(currentSkill.skillData.comboCurrentValidTime * 1000, () =>
        {
            comboCurrentValidTask = -1;
            currentSkill.skillData.comboCurrentValidTime = 0;
            currentSkillInedx = 0;
        }, "��ͨ����1��һ��������Чʱ��");
    }
    public override void OnUpdate()
    {
        currentSkill.OnUpdate();
    }
    public override void OnFixedUpdate()
    {
        currentSkill.OnFixedUpdate();
    }
    protected override void PlayAnimator()
    {
    }
}
