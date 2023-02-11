using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMBT;
using WMEffectsSkill;

public class MonkDatabase : MonsterDatabase
{
    public EffectsSkillData efData = new EffectsSkillData();
    public Diaup diaup;
    public Repel repel;
    public RedRoleStateCheck.AttackState attackState;
    [Header("�ػ���Χ����")]
    public MonsterattackDetectionData taData;
    [Header("���ܷ�Χ����")]
    public MonsterattackDetectionData skillData;
    public override void Init()
    {
        base.Init();
        diaup = new Diaup(efData);
        repel = new Repel(efData);
        tackingRangeTarget = GameObject.FindGameObjectWithTag("Hero").transform;
    }

    public override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Vector2 tr = transform.position;
        Gizmos.color = taData.attackDrawColor;
        Gizmos.DrawWireCube(tr + taData.attackRangeOffset, taData.attackRangeSize);

        Gizmos.color = skillData.attackDrawColor;
        Gizmos.DrawWireCube(tr + skillData.attackRangeOffset, skillData.attackRangeSize);
    }
}

[System.Serializable]
public class MonsterattackDetectionData
{
    [Header("������Χ")]
    public Vector2 attackRangeSize;
    [Header("������Χƫ��ֵ")]
    public Vector2 attackRangeOffset;
    [Header("��Чͼ��")]
    public LayerMask attackMask;
    [Header("�滭��ɫ")]
    public Color attackDrawColor;
}