using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WUBT;

public class MonsterController : RoleController
{
    /// <summary>
    /// ���������
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
    /// ���˵�����
    /// </summary>
    /// <param name="injuredData"></param>
    public override void Injured(InjuredData injuredData)
    {
        updateHP = true;//������µ�ǰ��HP
        roleAttribute.SetHp((int)injuredData.harm);//���ý�ɫ��Ѫ��
        GameObject injuredEffects = ResourceSvc.Single.LoadOrCreate<GameObject>("prefabs/Effects/InjuredEffects");//������Ч
        injuredEffects.transform.position = injuredPos.position;//������Ч��λ��
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
        mData.monsterStateEnum = WMState.MonsterStateEnum.Die;//�������˵�ö��
        dieCB?.Invoke();//�����Ļص�
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
