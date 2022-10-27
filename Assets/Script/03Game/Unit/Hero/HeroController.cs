using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WUBT;

public class HeroController : RoleController
{
    public RoleTree roleTree;
    public HeroDatabase heroDatabase;
    public override void Init()
    {
        base.Init();
        roleTree = GetComponent<RoleTree>();
        heroDatabase = GetComponent<HeroDatabase>();
        InputEvene();
    }
    public override void Injured(InjuredData injuredData)
    {
        if (heroDatabase.roleState == RoleState.Def)
        {
            injuredCB?.Invoke(injuredData);
            return;
        }
        roleAttribute.SetHp((int)injuredData.harm);
        heroDatabase.Injured(injuredData);
        //base.Injured(injuredData);
    }
    public override void Die()
    {
        base.Die();
    }
    public virtual void InputEvene()
    {
        InputController.Single.operaterCB = USESkill;
    }
    public void USESkill(int skillID)
    {
        if (heroDatabase.roleState==RoleState.Injured)
        {
            return;
        }
        if (currentSkill != null)
        {
            return;
        }
        BaseSkill baseSkill = skillManager.USESkill(skillID);
        if (baseSkill != null)
        {
            roleTree.isRuning = false;
            if (currentSkill != null)
            {
                currentSkill.Quit();
            }
            currentSkill = baseSkill;
            currentSkill.USE(() =>
            {
                roleTree.isRuning = true;
                currentSkill = null;
            });
        }
    }
}
public enum RoleState
{

    Null, Def, Injured, Die
}
