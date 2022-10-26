using System.Collections;
using System.Collections.Generic;
using System.IO;
using Bright.Serialization;
using UnityEngine;
using SimpleJSON;
using cfg;
using cfg.Data;
using UnityEngine.SceneManagement;
using WMEffectsSkill;
using WUBT;

public class ResourceSvc : MonoSingle<ResourceSvc>
{
    public Dictionary<string, Object> cacheList;
    public AssetBundle ab;
    LoadPanel loadPanel;
    Tables tables;
    EffectsSkillDataConfig effectsSkillDataConfig;
    public ArchiveData CurrentArchiveData { get; set; }
    public cfg.Data.EquipData[] EquipConfig { get { return tables.TBEquipConfig.DataList.ToArray(); } }
    public cfg.Data.RoleData[] RoleDataConfig { get { return tables.TBRoleData.DataList.ToArray(); } }
    public LevelConfigData[] LevelConfigDatas
    {
        get
        {
            return tables.TBLevelConfig.DataList.ToArray();
        }
    }

    public MonsterData[] MonsterDatas { get { return tables.TBMonsterData.DataList.ToArray(); } }

    public override void Init()
    {
        cacheList = new Dictionary<string, Object>();
        tables = new Tables(LoadByteBuf);//初始化表

        Debug.Log("资源服务初始化...");
    }
    public void Save()
    {
        SaveArchive.SavePlayDataArchive(CurrentArchiveData);
    }

    public cfg.Data.RoleData GetRoleDataByID(int id)
    {
        return tables.TBRoleData.Get(id);
    }
    public LevelConfigData GetLevelDataByID(int id)
    {
        return tables.TBLevelConfig.Get(id);
    }
    public cfg.Data.EquipData GetEquipDataByID(int id)
    {
        return tables.TBEquipConfig.Get(id);
    }
    public EffectsSkillDataConfig EffectsSkillDataConfig
    {
        get
        {
            if (effectsSkillDataConfig == null)
            {
                string aDataText = File.ReadAllText(SaveData.GetSavePath() + "EffectsSkillDataConfig.json");
                effectsSkillDataConfig = JsonUtility.FromJson<EffectsSkillDataConfig>(aDataText);
            }
            return effectsSkillDataConfig;
        }
    }
    private JSONNode LoadByteBuf(string fileName)
    {
        //"D:\GetHubProject\Dream\GenerateDatas\json\item_tbitem.json"
        return JSON.Parse(File.ReadAllText(Application.dataPath + "/../GenerateDatas/json/" + fileName + ".json", System.Text.Encoding.UTF8));
    }
    public WMData.EquipData CfgDataEquidToWMDataEquip(cfg.Data.EquipData cfgDataEquip)
    {
        WMData.EquipData wmDataEquip;
        wmDataEquip.Id = cfgDataEquip.UntID;
        wmDataEquip.EquipName = cfgDataEquip.UnitName;
        wmDataEquip.ResName = cfgDataEquip.ResName;
        wmDataEquip.Describe = cfgDataEquip.Describe;
        wmDataEquip.EquipType = cfgDataEquip.EquipType;
        wmDataEquip.EntryNumber = cfgDataEquip.EntryNumber;
        wmDataEquip.PriceWeapons = cfgDataEquip.PriceWeapons;
        wmDataEquip.EquipQualityType = cfgDataEquip.EquipQualityType;
        wmDataEquip.ISOpen = cfgDataEquip.ISOpen;

        wmDataEquip.entryKey = new List<WMData.EntryKey>();

        wmDataEquip.Attribute_CritHarm = cfgDataEquip.EquipAttribute.AttributeCritHarm;
        wmDataEquip.Attribute_CriticalChance = cfgDataEquip.EquipAttribute.AttributeCriticalChance;
        wmDataEquip.Attribute_Harm = cfgDataEquip.EquipAttribute.AttributeHarm;
        wmDataEquip.Attribute_HP = cfgDataEquip.EquipAttribute.AttributeHP;
        wmDataEquip.bagIndex = 0;


        return wmDataEquip;
    }

    #region 资源加载
    public T Load<T>(string path) where T : Object
    {
        T t = FindCacheList<T>(path);
        if (t != null)
        {
            return t;
        }
        t = Resources.Load<T>(path);
        if (t == null)
        {
            Debug.LogError("加载失败请检查路径" + path);
            return null;
        }
        cacheList.Add(path, t);
        return t;
    }
    /// <summary>
    /// 尝试创建
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    public T LoadOrCreate<T>(string path) where T : Object
    {
        T t = FindCacheList<T>(path);
        if (t != null)
        {
            if (t as GameObject)
            {
                T ob = GameObject.Instantiate(t);
                return ob;
            }
        }
        t = Resources.Load<T>(path);
        if (t == null)
        {
            Debug.LogError("加载失败请检查路径" + path);
            return null;
        }
        cacheList.Add(path, t);
        if (t as GameObject)
        {
            T ob = GameObject.Instantiate(t);
            return ob;
        }
        return t;
    }
    private T FindCacheList<T>(string path) where T : Object
    {
        Object ob;
        T t;
        if (cacheList.TryGetValue(path, out ob) && ob != null)
        {
            return t = ob as T;
        }
        return null;
    }

    //public GameObject CreateMonster(inr id)
    //{

    //}
    #endregion
    #region 跳转场景
    bool isLoadScene;
    /// <summary>
    /// 跳转场景
    /// </summary>
    public void JumpSceme(string sName, System.Action jumpDone = null)
    {
        //TODO 
        if (isLoadScene) return;
        if (loadPanel == null)
        {
            loadPanel = UISvc.Single.GetPanel<LoadPanel>(UIPath.LoadPanel);
        }
        AsyncOperation async = SceneManager.LoadSceneAsync(sName, LoadSceneMode.Single);
        coroutineLoadScene = StartCoroutine(LoadScene(async, jumpDone));
    }
    Coroutine coroutineLoadScene;
    public IEnumerator LoadScene(AsyncOperation async, System.Action jumpDone = null)
    {
        isLoadScene = true;
        loadPanel.UpdatProgress(0);
        UISvc.Single.SetSinglePanel(loadPanel, UISvc.StateType.Show);
        async.allowSceneActivation = false;
        while (true)
        {
            if (async.progress == 0.9f)
            {
                loadPanel.UpdatProgress(100);
                if (Input.anyKey)
                {
                    jumpDone?.Invoke();
                    UISvc.Single.SetPanelState(loadPanel);
                    async.allowSceneActivation = true;
                    isLoadScene = false;
                    StopCoroutine(coroutineLoadScene);
                }
            }
            else
            {
                loadPanel.UpdatProgress(async.progress * 100);
            }
            yield return new WaitForSeconds(0.02f);
        }

    }
    #endregion
    #region 存档
    public ArchiveData GetArchiveByID(int id)
    {
        foreach (var item in archiveDataConfig.dataList)
        {
            if (item.archiveID == id)
            {
                return item;
            }
        }
        return new ArchiveData();
    }
    public void SetArchiveByID(ArchiveData newData)
    {
        int id = newData.archiveID;
        for (int i = 0; i < archiveDataConfig.dataList.Count; i++)
        {
            if (archiveDataConfig.dataList[i].archiveID == newData.archiveID)
            {
                archiveDataConfig.dataList[i] = newData;
                return;
            }
        }
    }
    public ArchiveDataConfig ArchiveDataConfig
    {
        get
        {
            if (archiveDataConfig != null)
            {
                return archiveDataConfig;
            }
            archiveDataConfig = GetArchiveConfig();
            return archiveDataConfig;
        }
    }
    ArchiveDataConfig archiveDataConfig;
    ArchiveDataConfig GetArchiveConfig()
    {
        if (!File.Exists(SaveData.GetSavePath() + "ArchiveDataConfig.json"))
        {
            archiveDataConfig = new ArchiveDataConfig();
            string pathJson = JsonUtility.ToJson(archiveDataConfig, true);
            using (StreamWriter streamWriter = File.CreateText(SaveData.GetSavePath() + "ArchiveDataConfig.json"))
            {
                //表示生成C#代码
                streamWriter.Write(pathJson);
                streamWriter.Close();
            }
        }
        string aDataText = File.ReadAllText(SaveData.GetSavePath() + "ArchiveDataConfig.json");
        archiveDataConfig = JsonUtility.FromJson<ArchiveDataConfig>(aDataText);
        return archiveDataConfig;
    }

    public void SetArchiveData(int id)
    {
        CurrentArchiveData = GetArchiveByID(id);
        PlayerData playerData = CurrentArchiveData.playerData;
        for (int i = 0; i < playerData.skillDatas.Count; i++)
        {
            for (int a = 0; a < playerData.skillDatas[i].baseEffectsSkills.Length; a++)
            {
                GameRoot.Single.SetSkillEffect(playerData.skillDatas[i].baseEffectsSkills[a].skillData);
            }
        }//再次反射 
    }
    #endregion

    public GameObject CreateMonster(int id)
    {
        MonsterData monsterData = tables.TBMonsterData.Get(id);
        GameObject monster = LoadOrCreate<GameObject>(EnemyPath.Enemy + "/" + monsterData.ResName);
        monster.AddComponent<RoleAttribute>().Init(monsterData.RoleAttribute);//必须首位添加
        monster.GetComponent<Database>().Init();
        monster.GetComponent<BTTree<MonsterDatabase>>().Init();
        monster.GetComponent<MonsterController>().Init(); 
        return monster;
    }
    public void Clear()
    {
        cacheList.Clear();
    }
}
