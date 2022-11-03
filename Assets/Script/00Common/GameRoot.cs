using System.Collections;
using UnityEngine;
using System.CodeDom;
using System.IO;
using System.CodeDom.Compiler;
using UnityEditor;
using WMEffectsSkill;
using System;

public interface IInit
{
    void Init();
}
public class GameRoot : MonoSingle<GameRoot>
{
    [HideInInspector]
    public AudioSvc audioSvc;
    [HideInInspector]
    public ResourceSvc resourceSvc;
    [HideInInspector]
    public NetSvc netSvc;
    [HideInInspector]
    public MessageSvc messageSvc;
    [HideInInspector]
    public TimerSvc globalTimerSvc;
    public UISvc uISvc;
    public LogicSys logicSys;

    private void Awake()
    {
        LayerMask l0 = 1 << 0;//1
        LayerMask l1 = 1 << 1;//2
        LayerMask l2 = 1 << 2;//4
        LayerMask l3 = 1 << 3;//8
        LayerMask l4 = 1 << 4;//16
        LayerMask l5 = 1 << 5;//32

        int c = 0;
        for (int i = 0; i <11 ; i++)
        {
            LayerMask l = 1 << i;//1
            Debug.Log(l.value);
        }
        Debug.Log("GameRootInit...");
        DontDestroyOnLoad(gameObject);
        //初始化服务
        GetSvc(ref globalTimerSvc);
        GetSvc(ref uISvc);
        GetSvc(ref resourceSvc);
        GetSvc(ref audioSvc);
        GetSvc(ref messageSvc);

        GetSvc(ref netSvc);

        resourceSvc.abLoadDone = () =>
        {
            logicSys = GetComponent<LogicSys>();
            logicSys.Init();
            resourceSvc.abLoadDone = null;
            resourceSvc.DownloadDone();
        };
        //初始化系统

        //WMData.EquipData equipData = new WMData.EquipData();
        //equipData.EquipQualityType = cfg.Data.EquipQualityType.Rare;
        //equipData.entryKey = new System.Collections.Generic.List<WMData.EntryKey>();
        //equipData.OpenEquip();
    }

    void LoadABDone()
    {

    }
    public void GetSvc<T>(ref T t) where T : MonoSingle<T>
    {

        try
        {
            t = GetComponent<T>();
            if (t == null)
            {
                t = gameObject.AddComponent<T>();
            }
            t.Init();
        }
        catch (Exception a)
        {
            Debug.Log(a);
        }
    }
    private void Update()
    {
        globalTimerSvc.UpdateTask();
    }
    public GameObject CreateRole(cfg.Data.RoleData roleData)
    {
        GameObject role = resourceSvc.LoadOrCreate<GameObject>(HeroPath.Hero + "/" + roleData.ResName);
        role.AddComponent<RoleAttribute>().Init(roleData.RoleAttribute, resourceSvc.CurrentArchiveData.equipSoltDatas);

        role.GetComponent<RoleController>().Init();
        role.GetComponent<WUBT.HeroDatabase>().Init();
        role.GetComponent<RoleTree>().Init();
        return role;
    }


    /// <summary>
    /// 设置技能效果
    /// </summary>
    /// <param name="effectsSkillData"></param>
    public void SetSkillEffect(EffectsSkillData effectsSkillData)
    {
        SkillData skillData = null;
        foreach (var skill in resourceSvc.CurrentArchiveData.playerData.skillDatas)
        {
            if (effectsSkillData.skillID == skill.id)
            {
                skillData = skill;
                break;
            }
        }
        if (skillData == null)
        {
            return;
        }
        Type type = Type.GetType("WMEffectsSkill." + effectsSkillData.className);
        Type[] types = new Type[1];
        types[0] = typeof(EffectsSkillData);
        object obj = Activator.CreateInstance(type, new object[1] { effectsSkillData });
        skillData.baseEffectsSkills[effectsSkillData.skillEffectsIndex] = (BaseEffectsSkill)obj;
        resourceSvc.Save();
    }


    /// <summary>
    /// 顿帧
    /// </summary>
    public void PauseFrame(Animator[] animators)
    {
        ///卡肉

        for (int i = 0; i < animators.Length; i++)
        {
            animators[i].speed = 0.2F;
        }
        Time.timeScale = 0.2f;
        TimerSvc.instance.AddTask(0.2f * 1000, () =>
        {
            Time.timeScale = 1;
            for (int i = 0; i < animators.Length; i++)
            {
                animators[i].speed = 1;
            }
        });
    }

    /// <summary>
    /// 镜头模糊
    /// </summary>
    public void LensBlurEffects(Transform pos)
    {
        //镜头模糊
        GameObject lensBlurEffects = ResourceSvc.Single.LoadOrCreate<GameObject>(EffectPath.LensBlur);
        lensBlurEffects.transform.position = pos.position;
        GameObject.Destroy(lensBlurEffects, 0.2F);
    }
    public Coroutine skillCDCoroutine;
}
