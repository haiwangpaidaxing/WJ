using UnityEngine;
using UnityEngine.UI;
using WMBT;

public class MonsterController : RoleController
{
    /// <summary>
    /// ���������
    /// </summary>
    protected MonsterDatabase mData;
    [HideInInspector]
    public Slider hp;
    bool updateHP;
    int targetValue;
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
        targetValue = mData.roleAttribute.GetHP();
        injuredEffects.transform.position = injuredPos.position;//������Ч��λ��
        if (mData.monsterStateEnum == WMState.MonsterStateEnum.Die)
        {
            return;
        }
        base.Injured(injuredData);
        mData.SetInjuredData(injuredData);
    }
    public override void Die()
    {
        base.Die();
        hp.gameObject.SetActive(false);
        spriteRenderer.color = Color.gray;
        mData.monsterStateEnum = WMState.MonsterStateEnum.Die;//�������˵�ö��
    }


    private void FixedUpdate()
    {
        roleDir = transform.localScale.x;
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
