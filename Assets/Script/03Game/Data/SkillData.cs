using WMEffectsSkill;


/// <summary>
/// �������ͣ��ɵ���
/// </summary>
public enum DamageType
{
    Bullet = 4,             //��Ч������ײ�˺�
    None = 8,               //���˺���δʹ�ã�Ϊnone���Բ�ѡ
    Buff = 32,              //buff����
}
/// <summary>
/// ʹ�ü���ʱ�Ķ������Ч��
/// </summary>
public enum SelfSkillEffect
{
    /// <summary>
    /// ��
    /// </summary>
    Not,
    /// <summary>
    /// λ��Ч��
    /// </summary>
    DisplacementEffect,
    /// <summary>
    /// Levitation
    /// </summary>
    Fly
}
/// <summary>
/// �Ե��˼���Ч��
/// </summary>
public enum EffectsEnemyAbilities
{
    /// <summary>
    /// ��
    /// </summary>
    Not,

    Repel,

    Diaup
}
[System.Serializable]
public class SkillData
{
    public int id;//����ID
    public int nexSkillID;//��һ������ID
    public string skillName;
    public string skillDescribe;
    public string animName;//��������
    public string soundEffectsPath;//������Ч
    public cfg.Data.Attribute attribute;//��������
    public BaseEffectsSkill[] baseEffectsSkills;
    //public EffectsSkillData[] baseEffectsSkillDatas;
    //public EffectsEnemyData[] effectsEnemyAbilities;//���ܶԵ�Ч��
    //public SelfSkillEffectData[] selfSkillEffects;//���ܶ�����Ч��
    [UnityEngine.Header("����CD")]
    public float cd;//����CD
    public float skillTime;//����CD
    [UnityEngine.Header("���ܵ�ǰCDʱ��")]
    public float currentCD;//���ܵ�ǰCDʱ��
    [UnityEngine.Header("���ܵĺ�ҡ")]
    public float attackBackswing = 0.1f;//���ܵĺ�ҡ 
    /// <summary>
    /// ������Чʱ�� Ӱ���Ƿ���������һ������
    /// </summary>
    [UnityEngine.Header("������Чʱ�� Ӱ���Ƿ���������һ������")]
    public float comboValidTime;
    [UnityEngine.Header("��ǰ������Чʱ��")]  /// <summary>
                                      /// ��ǰ������Чʱ��
                                      /// </summary>
    public float comboCurrentValidTime;//lian 
    [UnityEngine.Header("����Ч���������")]
    public int skillEffectsNuumber = 2;
}

/// <summary>
/// ���ܶԵ��˼���Ч������
/// </summary>
public class EffectsEnemyData
{
    public EffectsEnemyAbilities effectsEnemyAbilities;
    public int value;
    public UnityEngine.Vector2 dir;
}
