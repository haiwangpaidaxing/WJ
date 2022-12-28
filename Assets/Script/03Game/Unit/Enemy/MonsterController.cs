using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WUBT;

public class MonsterController : RoleController
{
    /// <summary>
    /// 怪物的数据
    /// </summary>
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
    /// <summary>
    /// 受伤的数据
    /// </summary>
    /// <param name="injuredData"></param>
    public override void Injured(InjuredData injuredData)
    {
        updateHP = true;//代表更新当前的HP
        roleAttribute.SetHp((int)injuredData.harm);//设置角色的血量
        GameObject injuredEffects = ResourceSvc.Single.LoadOrCreate<GameObject>("prefabs/Effects/InjuredEffects");//受伤特效
        injuredEffects.transform.position = injuredPos.position;//受伤特效的位置
        if ( mData.monsterStateEnum == WMState.MonsterStateEnum.Die)
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
        mData.monsterStateEnum = WMState.MonsterStateEnum.Die;//更改受伤的枚举
        dieCB?.Invoke();//死亡的回调
    }
    private void FixedUpdate()
    {
        roleDir= transform.localScale.x ;
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
