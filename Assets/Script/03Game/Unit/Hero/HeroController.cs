using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WUBT;

public class HeroController : RoleController
{
    public RoleTree roleTree;
    public HeroDatabase heroDatabase;
    RoleInfoPane infoPane;
    bool isUpdateHPUI;//更新UI
    bool isUpdateMPUI;//更新UI
    int currentRoleHP;
    int currentRoleMP;
    public override void Init()
    {
        base.Init();
        infoPane = GetComponentInChildren<RoleInfoPane>(true);
        infoPane.SetHP(this.roleAttribute.GetHP(), this.roleAttribute.MaxHP);
        roleAttribute.hpValueCB += (value) =>
        {
            currentRoleHP = value < 0 ? 0 : value;
            isUpdateHPUI = true;
        };
        roleAttribute.mpValueCB += (value) =>
        {
            currentRoleMP = value < 0 ? 0 : value;
            isUpdateMPUI = true;
        };
        roleTree = GetComponent<RoleTree>();
        heroDatabase = GetComponent<HeroDatabase>();
        InputEvene();
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (isUpdateHPUI)
        {

            if (infoPane.HP.fillAmount != (currentRoleHP / roleAttribute.MaxHP))
            {
                float value = infoPane.HP.fillAmount * roleAttribute.MaxHP;
                value = Mathf.Lerp(value, (currentRoleHP), Time.deltaTime * 1);
                Debug.Log(value + "_" + currentRoleHP);
                infoPane.SetHP(value, this.roleAttribute.MaxHP);
            }
            else
            {
                isUpdateHPUI = false;
            }

        }
        if (isUpdateMPUI)
        {
            if (infoPane.MP.fillAmount != (currentRoleMP / roleAttribute.MaxMP))
            {
                float value = infoPane.MP.fillAmount * roleAttribute.MaxMP;
                value = Mathf.Lerp(value, (currentRoleMP), Time.deltaTime * 2);
                Debug.Log(value + "_" + currentRoleMP);
                infoPane.SetMP(value, this.roleAttribute.MaxMP);
            }
            else
            {
                isUpdateMPUI = false;
            }
        }
       
    }
    protected override void Update()
    {
        base.Update();    
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
        if (heroDatabase.roleState == RoleState.Injured)
        {
            return;
        }
        if (currentSkill != null)
        {
            return;
        }
        BaseSkill baseSkill = skillManager.USESkill(skillID, ref roleAttribute);
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
