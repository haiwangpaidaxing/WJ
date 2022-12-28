using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMEffectsSkill;
using WUBT;

public class RedMonsterDatabase : MonsterDatabase
{
    [Header("Զ�̹�������")]
    public float RangeAttackDis;
    [Header("������λ��")]
    public Transform createArrowPos;
    [Header("��ǰ��������")]
    public int currentArrow;
    [Header("�����������")]
    public int arrowMax;
    public float arrowRecoverTime;
    float currentArrowRecoverTime;
    public RedRoleStateCheck.AttackState attackState;
    public InjuredData injuredData = new InjuredData();
    public EffectsSkillData efData = new EffectsSkillData();
    public Diaup diaup;
    public Repel repel;
    public Vector2 thumpRangeSize;
   
    public override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Vector2 tr = transform.position;
        Gizmos.DrawRay(transform.position, transform.right * RangeAttackDis);
    }
    public override void Init()
    {
        base.Init();
        diaup = new Diaup(efData);
        repel = new Repel(efData);
        currentArrow = arrowMax;
        tackingRangeTarget = GameObject.FindGameObjectWithTag("Hero").transform;
    }

    private void Update()
    {
        if (currentArrow==0)
        {
            currentArrowRecoverTime += Time.deltaTime;
            if (currentArrowRecoverTime>=arrowRecoverTime)
            {
                currentArrow = arrowMax;
                currentArrowRecoverTime = 0;
            }
        }
    }
}
