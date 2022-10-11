using WMEffectsSkill;


/// <summary>
/// 技能类型，可叠加
/// </summary>
public enum DamageType
{
    Bullet = 4,             //特效粒子碰撞伤害
    None = 8,               //无伤害，未使用，为none可以不选
    Buff = 32,              //buff技能
}
/// <summary>
/// 使用技能时的对自身的效果
/// </summary>
public enum SelfSkillEffect
{
    /// <summary>
    /// 无
    /// </summary>
    Not,
    /// <summary>
    /// 位移效果
    /// </summary>
    DisplacementEffect,
    /// <summary>
    /// Levitation
    /// </summary>
    Fly
}
/// <summary>
/// 对敌人技能效果
/// </summary>
public enum EffectsEnemyAbilities
{
    /// <summary>
    /// 无
    /// </summary>
    Not,

    Repel,

    Diaup
}
[System.Serializable]
public class SkillData
{
    public int id;//技能ID
    public int nexSkillID;//下一个技能ID
    public string skillName;
    public string skillDescribe;
    public string animName;//动画名称
    public string soundEffectsPath;//技能音效
    public cfg.Data.Attribute attribute;//技能属性
    public BaseEffectsSkill[] baseEffectsSkills;
    //public EffectsSkillData[] baseEffectsSkillDatas;
    //public EffectsEnemyData[] effectsEnemyAbilities;//技能对敌效果
    //public SelfSkillEffectData[] selfSkillEffects;//技能对自身效果
    [UnityEngine.Header("技能CD")]
    public float cd;//技能CD
    public float skillTime;//技能CD
    [UnityEngine.Header("技能当前CD时间")]
    public float currentCD;//技能当前CD时间
    [UnityEngine.Header("技能的后摇")]
    public float attackBackswing = 0.1f;//技能的后摇 
    /// <summary>
    /// 连招有效时间 影响是否能链接下一步连招
    /// </summary>
    [UnityEngine.Header("连招有效时间 影响是否能链接下一步连招")]
    public float comboValidTime;
    [UnityEngine.Header("当前连招有效时间")]  /// <summary>
                                      /// 当前连招有效时间
                                      /// </summary>
    public float comboCurrentValidTime;//lian 
    [UnityEngine.Header("技能效果最大数量")]
    public int skillEffectsNuumber = 2;
}

/// <summary>
/// 技能对敌人技能效果数据
/// </summary>
public class EffectsEnemyData
{
    public EffectsEnemyAbilities effectsEnemyAbilities;
    public int value;
    public UnityEngine.Vector2 dir;
}
