using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WUBT;

public class MonsterController : RoleController
{
    protected MonsterDatabase mData;
    public Action dieCB;
    public Slider hp;
    public bool updateHP;
    public int targetValue;
    public override void Init()
    {
        base.Init();
        mData = GetComponent<MonsterDatabase>();
        hp = GetComponentInChildren<Slider>();
        hp.maxValue = mData.roleAttribute.GetHP();
        hp.value = mData.roleAttribute.GetHP();
    }
    public override void Injured(InjuredData injuredData)
    {
        updateHP = true;
        roleAttribute.SetHp((int)injuredData.harm);
        GameObject injuredEffects = ResourceSvc.Single.LoadOrCreate<GameObject>("Prefabs/Effects/InjuredEffects"); injuredEffects.transform.position = injuredPos.position;
        if (mData.monsterStateEnum == WMState.MonsterStateEnum.Die)
        {
            return;
        }
        targetValue = mData.roleAttribute.GetHP();
        base.Injured(injuredData);
        mData.SetInjuredData(injuredData);
    }
    public override void Die()
    {
        base.Die();
        mData.monsterStateEnum = WMState.MonsterStateEnum.Die;
        dieCB?.Invoke();
    }
    private void FixedUpdate()
    {
        if (updateHP)
        {
            if (hp.value == targetValue)
            {
                return;
            }
            hp.value = Mathf.Lerp(hp.value, targetValue, Time.deltaTime * 2);
        }
    }
}
