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
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;

public class ResourceSvc : MonoSingle<ResourceSvc>
{
    bool isAB = true;
    public bool isTestNet;
    public Dictionary<string, AssetBundle> cacheAssetBundle;
    Queue<string> abAllNameQueue = new Queue<string>();
    public override void Init()
    {
        cacheAssetBundle = new Dictionary<string, AssetBundle>();
        cacheList = new Dictionary<string, Object>();
        tables = new Tables(LoadByteBuf);//初始化表 
#if UNITY_EDITOR
        StartCoroutine(GetUnityWebRequest("Config.txt", GetCheckConfig));
        //StartCoroutine(GetUnityWebRequest("StandaloneWindows", LoadABConfig));
#endif
        //InitAB();//编辑器模式下会加载本地文件
        Debug.Log("资源服务初始化..." + "AB加载" + isAB);
    }
    #region ABLoad
    [SerializeField]
    ResourceLoadiProgressPanel resourceLoadiProgress;

    public IEnumerator GetUnityWebRequest(string resName, System.Action<UnityWebRequest, string> successCB)
    {
        //使用Head的好处是，Head会得到要下载数据的头文件，却不会下载文件。
        long totalLength = -1;
        string fileSize = "";
        long fileSzieValue = 0;
        string uri = ResPath.GetLoadABPath(isTestNet) + resName;
        UnityWebRequest huwr = UnityWebRequest.Head(uri);
        yield return huwr.SendWebRequest();
        switch (huwr.result)
        {
            case UnityWebRequest.Result.Success:
                totalLength = long.Parse(huwr.GetResponseHeader("Content-Length"));
                //Debug.Log(totalLength);

                if ((totalLength / 1048576) == 0)
                {
                    fileSzieValue = totalLength / 1024;
                    fileSize = fileSzieValue + "kb";
                }
                else
                {
                    fileSzieValue = totalLength / 1048576;
                    fileSize = fileSzieValue + "mb";
                }
                break;
            case UnityWebRequest.Result.ConnectionError:
                Debug.LogError(huwr.error);
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError(huwr.error);
                break;
            case UnityWebRequest.Result.DataProcessingError:
                Debug.LogError(huwr.error);
                break;
        }
        using (UnityWebRequest request = UnityWebRequest.Get(uri))
        {
            //yield return request.SendWebRequest();后，是直接下载完成或者下载失败。但我们在这儿不需要等待，我们需要时刻知道下载进度
            // yield return request.SendWebRequest();
            resourceLoadiProgress.ProgressBar.value = 0;
            request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.DataProcessingError || request.result == UnityWebRequest.Result.ProtocolError || request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                resourceLoadiProgress.fileSizeText.text = "文件大小" + fileSize;
                while (!request.isDone)
                {
                    Debug.Log("正在下载" + resName + "当前进度为:" + request.downloadProgress + "%" + "文件长度为:" + fileSize);
                    float progress = request.downloadProgress * 100;
                    resourceLoadiProgress.ProgressBar.value = progress;
                    resourceLoadiProgress.LoadProgressText.text = progress.ToString("0") + "%";
                    yield return new WaitForSeconds(0.02f);
                }
            }
            if (request.isDone)
            {
                while (resourceLoadiProgress.ProgressBar.value < 1)
                {
                    resourceLoadiProgress.ProgressBar.value += 0.02f;

                    float progress = resourceLoadiProgress.ProgressBar.value * 100;

                    resourceLoadiProgress.LoadProgressText.text = progress.ToString("0") + "%";
                    yield return new WaitForSeconds(0.02f);
                }
                yield return new WaitForSeconds(0.5f);
                successCB?.Invoke(request, resName);
                yield break;
            }
            request.Dispose();
        }
    }
    public void GetCheckConfig(UnityWebRequest request, string abName)
    {
        CheckMD5DataConfig checkMD5DataConfig;
        byte[] data = request.downloadHandler.data;
        BinaryFormatter bf = new BinaryFormatter();
        WriteFile(abName, data);

        using (FileStream fsWrite = new FileStream(ResPath.GetLoadABPath() + "Config.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
        {
            checkMD5DataConfig = (CheckMD5DataConfig)bf.Deserialize(fsWrite);
        }
        foreach (var item in checkMD5DataConfig.checkDatas)
        {
            Debug.Log("Name_" + item.aBName + "_MD5_" + item.mD5);
        }
        StartCoroutine(GetUnityWebRequest("StandaloneWindows", LoadABConfig));
    }
    public void LoadABConfig(UnityWebRequest request, string abName)
    {
        //获取所有AB包的名字并却加载出来
        AssetBundle assetBundle = UnityWebRequestByAssetBundle(request);
        byte[] data = request.downloadHandler.data;
        AssetBundleManifest assetBundleManifest = assetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        string[] allABName = assetBundleManifest.GetAllAssetBundles();
        foreach (var item in allABName)
        {
            abAllNameQueue.Enqueue(item);
        }
        //将AB写入文件夹中
        WriteFile(abName, data);
        StartCoroutine(GetUnityWebRequest(abAllNameQueue.Dequeue(), LoadSingleAB));
    }
    public void LoadSingleAB(UnityWebRequest request, string abName)
    {
        AssetBundle assetBundle = UnityWebRequestByAssetBundle(request);
        byte[] data = request.downloadHandler.data;
        WriteFile(abName, data);
        cacheAssetBundle.Add(abName, assetBundle);
        if (abAllNameQueue.Count > 0)
        {
            StartCoroutine(GetUnityWebRequest(abAllNameQueue.Dequeue(), LoadSingleAB));
        }
        else
        {
            resourceLoadiProgress.LoadProgressText.text = "资源下载完毕";
            resourceLoadiProgress.fileSizeText.text = "";
        }
    }


    #region ABTOOL
    public AssetBundle UnityWebRequestByAssetBundle(UnityWebRequest request)
    {
        byte[] data = request.downloadHandler.data;
        AssetBundle assetBundle = AssetBundle.LoadFromMemory(data);
        return assetBundle;
    }
    /// <summary>
    /// AB包的写入
    /// </summary>
    public void WriteFile(string resName, byte[] data)
    {
        FileInfo fileInfo = new FileInfo(ResPath.WriteABPath + resName);
        FileStream fs = fileInfo.Create();
        fs.Write(data, 0, data.Length);
        fs.Flush();     //文件写入存储到硬盘
        fs.Close();     //关闭文件流对象
        fs.Dispose();   //销毁文件对象
    }
    #endregion
    public void InitAB()
    {
        //加载本地
        AssetBundle StandaloneWindowsAssetBundle = AssetBundle.LoadFromFile(ResPath.GetLoadABPath() + "StandaloneWindows");

        AssetBundleManifest assetBundleManifest = StandaloneWindowsAssetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        foreach (var item in assetBundleManifest.GetAllAssetBundles())
        {
            Debug.Log(item);
        }

        AssetBundle prefabsAssetBundle = AssetBundle.LoadFromFile(ResPath.GetLoadABPath() + "Prefabs");
        AssetBundle dataAssetBundle = AssetBundle.LoadFromFile(ResPath.GetLoadABPath() + "Data");
        AssetBundle soundEffectsAssetBundle = AssetBundle.LoadFromFile(ResPath.GetLoadABPath() + "SoundEffects");
        AssetBundle spritesAssetBundle = AssetBundle.LoadFromFile(ResPath.GetLoadABPath() + "Sprites");

        cacheAssetBundle.Add("Prefabs", prefabsAssetBundle);
        cacheAssetBundle.Add("Data", dataAssetBundle);
        cacheAssetBundle.Add("SoundEffects", soundEffectsAssetBundle);
        cacheAssetBundle.Add("Sprites", spritesAssetBundle);
    }

    #endregion
    #region ResourceFileLoad
    public Dictionary<string, Object> cacheList;
    LoadPanel loadPanel;
    public T Load<T>(string path) where T : Object
    {

        T t = FindCacheList<T>(path);
        if (t != null)
        {
            return t;
        }
        if (isAB)
        {
            string[] abData = GetABPath(path);
            AssetBundle ab = cacheAssetBundle[abData[0]];
            t = ab.LoadAsset<T>(abData[1]);
            if (t == null)
            {
                Debug.LogError("AB加载失败:AB包为:" + abData[0] + "资源名为：" + abData[1]);
                return null;
            }
        }
        else
        {
            t = Resources.Load<T>(path);
            if (t == null)
            {
                Debug.LogError("加载失败请检查路径" + path);
                return null;
            }
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
        if (isAB)
        {
            string[] abData = GetABPath(path);
            AssetBundle ab = cacheAssetBundle[abData[0]];
            t = ab.LoadAsset<T>(abData[1]);
            if (t == null)
            {
                Debug.LogError("AB加载失败:AB包为:" + abData[0] + "资源名为：" + abData[1]);
                return null;
            }
        }
        else
        {
            t = Resources.Load<T>(path);
            if (t == null)
            {
                Debug.LogError("加载失败请检查路径" + path);
                return null;
            }
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
    public string[] GetABPath(string path)
    {
        string[] data = new string[2];
        int abindex = path.IndexOf('/');
        string abName = path.Remove(abindex);
        data[0] = abName;
        int reIndex = path.LastIndexOf('/');
        string resName = path.Substring(reIndex + 1);
        data[1] = resName;
        //  Debug.Log("所属AB包" + abName + "资源名称" + resName);
        return data;
    }
    #endregion
    #region Data
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
    public void Save()
    {
        SaveArchive.SavePlayDataArchive(CurrentArchiveData);
    }
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
    private void OnDestroy()
    {
        AssetBundle.UnloadAllAssetBundles(true);
    }
}
