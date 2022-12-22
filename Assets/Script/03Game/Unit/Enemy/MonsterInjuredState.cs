using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// </summary>
public class MonsterInjuredState
{
    Action injuredOver;
    RoleController roleController;
    int injuredID;
    public MonsterInjuredState(RoleController roleController)
    {
        this.roleController = roleController;
    }
    public void Enter(InjuredData injuredData, Action injuredOver = null)
    {
        if (injuredID!=0)
        {
            TimerSvc.instance.ReoveTask(injuredID);
        }
        roleController.animator.Play("Injured");   
        this.injuredOver = injuredOver;
        if (injuredData.baseEffectsSkillList!=null)
        {
            for (int i = 0; i < injuredData.baseEffectsSkillList.Length; i++)
            {
                if (injuredData.baseEffectsSkillList[i] == null)
                {
                    continue;
                }
                injuredData.baseEffectsSkillList[i].USE(roleController, injuredData.releaser);
            }
        }
        injuredID = TimerSvc.instance.AddTask(1 * 1000, () =>
         {
             injuredID = 0;
             this.injuredOver?.Invoke();
         });
    }

}
