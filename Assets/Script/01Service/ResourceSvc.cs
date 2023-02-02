using cfg;
using cfg.Data;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using WMEffectsSkill;
using WMBT;


public class ResourceSvc : MonoSingle<ResourceSvc>
{
    bool isAB = true;
    public bool isEDITOR;
    public Dictionary<string, AssetBundle> cacheAssetBundle;
    Queue<string> abDownloadQueue = new Queue<string>();

    StreamingAssetsPathResPathConfig streamingAssetsPathResPathConfig;
    public override void Init()
    {
        cacheAssetBundle = new Dictionary<string, AssetBundle>();
        cacheList = new Dictionary<string, Object>();
        //首次进入游戏是 因为没有AB包的原因 又需要一个加载面板资源 所以必须内资一个基础的加载面板      显示下载进度等
        Transform uiRoot = GameObject.Find("UIRoot").transform;
        GameObject loadPanel = Resources.Load<GameObject>("ResourceLoadiProgressPanel");
        resourceLoadiProgress = GameObject.Instantiate(loadPanel).GetComponent<ResourceLoadiProgressPanel>();
        resourceLoadiProgress.transform.SetParent(uiRoot, false);
        bool checkFile = File.Exists(ResPath.SaveFilePath + "Config.json");
        if (isEDITOR)
        {
            InitAB();//编辑器模式下会加载本地文件

            //if (!Directory.Exists(ResPath.SaveFilePath))//第一次进入
            //{
            //    CopyDirIntoDestDirectory(Application.streamingAssetsPath, ResPath.SaveFilePath, true);
            //    EnterGameCheck(checkFile);
            //}
            //else
            //{
            //    EnterGameCheck(checkFile);
            //}
        }
        else
        {
#if UNITY_ANDROID
            if (!Directory.Exists(ResPath.SaveFilePath))//第一次进入
            {
                Directory.CreateDirectory(ResPath.SaveFilePath);
                StartCoroutine(Get(Application.streamingAssetsPath + "/StreamingAssetsPathResPathConfig.json", (data) =>
                {
                    streamingAssetsPathResPathConfig = JsonUtility.FromJson<StreamingAssetsPathResPathConfig>(data);
                    AndroidCopyStreamingAssetsPathRes();
                }));
                bool resConfig = File.Exists(ResPath.SaveFilePath + "Config.json");
                EnterGameCheck(resConfig);
                resourceLoadiProgress.Debug("首次进入" + "路径" + ResPath.SaveFilePath + "\n路径是否存在" + Directory.Exists(ResPath.SaveFilePath).ToString());
            }
            else
            {
                EnterGameCheck(checkFile);
            }
            Debug.Log("这是安卓平台。。。");
#endif
#if UNITY_STANDALONE_WIN
            EnterGameCheck(checkFile);
#endif
        }

        //检测服务器是否开始    //一种是服务器未开启  一种是本机未开启网络
        //if (!File.Exists(ResPath.SaveFilePath + "Config.json"))//获取校验文件
        //{
        //    Debug.Log("首次启动游戏");
        //    StartCoroutine(GetUnityWebRequest("StandaloneWindows", LoadABConfig));
        //}
        //else
        //{
        //    StartCoroutine(GetUnityWebRequest("Config.json", GetCheckConfig));
        //}
        Debug.Log("资源服务初始化..." + "AB加载" + isAB);
    }
    #region ABLoad

    [System.Obsolete]
    IEnumerator Get(string resPath, System.Action<byte[]> done)
    {

        UnityWebRequest webRequest = UnityWebRequest.Get(resPath);
        yield return webRequest.SendWebRequest();
        switch (webRequest.result)
        {
            case UnityWebRequest.Result.Success:
                //异常处理，很多博文用了error!=null这是错误的，请看下文其他属性部分
                // Debug.Log(webRequest.downloadHandler.text);
                byte[] bytes = webRequest.downloadHandler.data;
                done?.Invoke(bytes);
                // resourceLoadiProgress.Debug(webRequest.downloadHandler.text);
                //创建文件
                // CreatFile(ResPath.SaveFilePath + "Test", bytes);
                yield break;
        }

    }

    IEnumerator Get(string resPath, System.Action<string> done)
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(resPath);
        yield return webRequest.SendWebRequest();
        switch (webRequest.result)
        {
            case UnityWebRequest.Result.Success:
                //异常处理，很多博文用了error!=null这是错误的，请看下文其他属性部分
                // Debug.Log(webRequest.downloadHandler.text);
                done?.Invoke(webRequest.downloadHandler.text);
                // resourceLoadiProgress.Debug(webRequest.downloadHandler.text);
                //创建文件
                // CreatFile(ResPath.SaveFilePath + "Test", bytes);
                yield break;
        }
    }
    /// <summary>
    /// 安卓端
    /// </summary>
    public void AndroidCopyStreamingAssetsPathRes()
    {
        foreach (var item in streamingAssetsPathResPathConfig.resPath)
        {
            StartCoroutine(Get(Application.streamingAssetsPath + "/" + item, (data) =>
                 {
                     CreatFile(ResPath.SaveFilePath + item, data);
                 }));
        }

    }
    public static void CreatFile(string filePath, byte[] bytes)
    {
        FileInfo file = new FileInfo(filePath);
        Stream stream = file.Create();
        stream.Write(bytes, 0, bytes.Length);
        stream.Close();
        stream.Dispose();
    }


    ResourceLoadiProgressPanel resourceLoadiProgress;
    /// <summary>
    /// 检测服务器
    /// </summary>
    /// <param name="checkCB"></param>
    /// <returns></returns>
    public IEnumerator CheckServer(System.Action<bool> checkCB)
    {
        string uri = ResPath.GetLoadABPath() + "Config.json";
        Debug.Log("HTTP_Path" + ResPath.GetLoadABPath());
        UnityWebRequest huwr = UnityWebRequest.Head(uri);
        yield return huwr.SendWebRequest();
        switch (huwr.result)
        {
            case UnityWebRequest.Result.Success:
                checkCB(true);
                yield break;
            case UnityWebRequest.Result.ConnectionError:
                checkCB(false);
                UISvc.Single.AddTips("服务器未连接网络");
                Debug.Log("未连接网络" + huwr.error);
                yield break;
            case UnityWebRequest.Result.ProtocolError:
                checkCB(false);
                UISvc.Single.AddTips("错误路径" + ResPath.GetLoadABPath());
                Debug.Log("错误路径" + ResPath.GetLoadABPath());
                yield break;
            case UnityWebRequest.Result.DataProcessingError:
                checkCB(false);
                UISvc.Single.AddTips("错误路径" + ResPath.GetLoadABPath());
                Debug.Log("错误路径" + ResPath.GetLoadABPath());
                yield break;
        }
    }
    public IEnumerator GetUnityWebRequest(string resName, System.Action<UnityWebRequest, string> successCB)
    {
        // UISvc.Single.AddTips("");
        UISvc.Single.AddTips("开始下载资源" + resName);
        Debug.Log("开始下载资源" + resName);
        //使用Head的好处是，Head会得到要下载数据的头文件，却不会下载文件。
        long totalLength = -1;
        string fileSize = "";
        long fileSizeValue = 0;
        string uri = ResPath.GetLoadABPath() + resName;
        UnityWebRequest huwr = UnityWebRequest.Head(uri);
        yield return huwr.SendWebRequest();
        switch (huwr.result)
        {
            case UnityWebRequest.Result.Success:
                totalLength = long.Parse(huwr.GetResponseHeader("Content-Length"));
                //Debug.Log(totalLength);

                if ((totalLength / 1048576) == 0)
                {
                    fileSizeValue = totalLength / 1024;
                    fileSizeValue = fileSizeValue > 0 ? fileSizeValue : 1;
                    fileSize = fileSizeValue + "kb";
                }
                else
                {
                    fileSizeValue = totalLength / 1048576;
                    fileSize = fileSizeValue + "MB";
                }
                break;
            case UnityWebRequest.Result.ConnectionError:
                UISvc.Single.AddTips("未连接网络" + huwr.error);
                Debug.LogError("未连接网络" + huwr.error);
                yield break;
            case UnityWebRequest.Result.ProtocolError:
                UISvc.Single.AddTips("错误路径" + ResPath.GetLoadABPath() + resName + "_" + huwr.error);
                Debug.LogError("错误路径" + ResPath.GetLoadABPath() + resName + "_" + huwr.error);
                yield break;
            case UnityWebRequest.Result.DataProcessingError:
                UISvc.Single.AddTips("错误路径" + ResPath.GetLoadABPath() + resName + "_" + huwr.error);
                Debug.LogError("错误路径" + ResPath.GetLoadABPath() + resName + "_" + huwr.error);
                yield break;
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
                yield break;
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
    public void EnterGameCheck(bool value)
    {
        NormalNetEnterGame(value);
        //if (!PingNetAddress())//网络状态异常时候
        //{
        //    NetworkExceptionEnteredGame(value);
        //}
        //else
        //{

        //}
    }
    /// <summary>
    /// 网络异常时候进入游戏
    /// </summary>
    public void NetworkExceptionEnteredGame(bool value)
    {
        //未连接网络 并且首次启动游戏时
        if (!value)//因为校验文件是最后下载的如果 没有检验文件代表下载过程中，中途断网等  
        {
            UISvc.Single.AddTips("第一次打开游戏需要连接网络");
            Debug.Log("TODO提示第一次打开游戏需要连接网络，强制退出");
        }
        else
        {
            //  UISvc.Single.AddTips("跳过校验直接开始游戏");
            Debug.Log("跳过校验直接开始游戏");
            GameRoot.Single.abLoadDone();
        }
    }
    /// <summary>
    /// 网络正常进入游戏
    /// </summary>
    public void NormalNetEnterGame(bool value)
    {
        StartCoroutine(CheckServer((isOpen) =>
        {
            if (isOpen)//当服务器开启时候
            {
                if (!value)
                {
                    UISvc.Single.AddTips("首次启动游戏");
                    Debug.Log("首次启动游戏");
                    StartCoroutine(GetUnityWebRequest("StandaloneWindows", LoadABConfig));
                }
                else
                {
                    //服务器开启 有网络状态  并却已经存在文件时  校验检测 
                    //  UISvc.Single.AddTips("服务器开启 有网络状态  并却已经存在文件时  校验检测 ");
                    Debug.Log("服务器开启 有网络状态  并却已经存在文件时  校验检测 ");
                    StartCoroutine(GetUnityWebRequest("Config.json", GetCheckConfig));//检测更新
                }
            }
            else
            {
                if (!value)
                {
                    //    UISvc.Single.AddTips("在有网络状态下，检测到服务器未开启，未有文件，强制退出");
                    Debug.Log("在有网络状态下，检测到服务器未开启，未有文件，强制退出");
                }
                else
                {
                    //    UISvc.Single.AddTips("在有网络状态下，检测到服务器未开启，已有文件，跳过校验直接开始游戏");
                    Debug.Log("在有网络状态下，检测到服务器未开启，已有文件，跳过校验直接开始游戏");
                    GameRoot.Single.abLoadDone();
                }
            }
        }));
    }
    /// <summary>
    /// 网络状态检测
    /// </summary>
    /// <returns></returns>
    private bool PingNetAddress()
    {
        try
        {
            System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
            System.Net.NetworkInformation.PingReply pr = ping.Send("www.baidu.com", 3000);
            if (pr.Status == System.Net.NetworkInformation.IPStatus.Success)
            {
                Debug.Log("网络连接信号正常");
                return true;
            }
            else
            {
                Debug.Log("网络连接无信号");
                return false;
            }
        }
        catch (System.Exception e)
        {
            Debug.Log("网络连接信号异常" + e.Message);
            return false;

        }
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
            abDownloadQueue.Enqueue(item);
        }
        //将AB写入文件夹中
        WriteFile(abName, data);
        StartCoroutine(GetUnityWebRequest(abDownloadQueue.Dequeue(), LoadSingleAB));
    }

    public void LoadSingleAB(UnityWebRequest request, string abName)
    {
        AssetBundle assetBundle = UnityWebRequestByAssetBundle(request);
        byte[] data = request.downloadHandler.data;
        WriteFile(abName, data);
        string abKey = abName;
        //char head = abKey[0];
        //abKey = abKey.Remove(0, 1);
        //head = char.ToUpper(head);
        //abKey = abKey.Insert(0, head.ToString());
        cacheAssetBundle.Add(abKey, assetBundle);
        //  Debug.Log(abKey);
        if (abDownloadQueue.Count > 0)
        {
            StartCoroutine(GetUnityWebRequest(abDownloadQueue.Dequeue(), LoadSingleAB));
        }
        else
        {
            StartCoroutine(GetUnityWebRequest("Config.json", DownloadCheckConfig));
        }
    }



    public void DownloadDone()
    {
        tables = new Tables(LoadByteBuf);//初始化表
        Destroy(resourceLoadiProgress.gameObject);
    }

    public void LoadGameData(UnityWebRequest request, string resName)
    {
        AssetBundle assetBundle = UnityWebRequestByAssetBundle(request);
    }
    /// <summary>
    /// 下载校验配置
    /// </summary>
    /// <param name="request"></param>
    /// <param name="resName"></param>
    public void DownloadCheckConfig(UnityWebRequest request, string resName)
    {
        string data = request.downloadHandler.text;
        WriteFile(resName, data);
        resourceLoadiProgress.LoadProgressText.text = "资源下载完毕";
        resourceLoadiProgress.fileSizeText.text = "";
        GameRoot.Single.abLoadDone?.Invoke();
    }
    //public void InitCacheAssetBundle()
    //{
    //    AssetBundle assetBundle = AssetBundle.LoadFromFile(ResPath.SaveFilePath + ResPath.GetABDepend());
    //    AssetBundleManifest assetBundleManifest = assetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
    //    string[] allABName = assetBundleManifest.GetAllAssetBundles();
    //    foreach (var item in allABName)
    //    {
    //        AssetBundle ab = AssetBundle.LoadFromFile(ResPath.SaveFilePath + item);
    //        cacheList.Add(item, ab);
    //    }
    //}
    #region ABTOOL
    /// <summary>
    /// 检测更新
    /// </summary>
    /// <param name="request"></param>
    /// <param name="abName"></param>
    public void GetCheckConfig(UnityWebRequest request, string abName)
    {
        resourceLoadiProgress.LoadProgressText.text = "资源检测更新中";
        resourceLoadiProgress.fileSizeText.text = "";
        string configName = "Config.json";
        string loadABPath = ResPath.GetLoadABPath();
        string localData = File.ReadAllText(ResPath.SaveFilePath + configName);
        string data = request.downloadHandler.text;
        CheckMD5DataConfig newCheckMD5DataConfig;
        CheckMD5DataConfig localCheckMD5DataConfig;

        localCheckMD5DataConfig = JsonUtility.FromJson<CheckMD5DataConfig>(localData);
        newCheckMD5DataConfig = JsonUtility.FromJson<CheckMD5DataConfig>(data);

        if (localCheckMD5DataConfig.checkDatas.Count != newCheckMD5DataConfig.checkDatas.Count)
        {
            Debug.Log("资源增减TODO....");
        }
        else
        {
            for (int l = 0; l < localCheckMD5DataConfig.checkDatas.Count; l++)
            {
                if (localCheckMD5DataConfig.checkDatas[l].mD5 != newCheckMD5DataConfig.checkDatas[l].mD5)
                {
                    abDownloadQueue.Enqueue(localCheckMD5DataConfig.checkDatas[l].aBName);
                }
                else
                {

                    Debug.Log("正在校验资源" + localCheckMD5DataConfig.checkDatas[l].aBName);
                }
            }
        }
        if (abDownloadQueue.Count > 0)
        {

            UISvc.Single.AddTips("检测到资源需要更新");
            Debug.Log("检测到资源需要更新");
            resourceLoadiProgress.LoadProgressText.text = "...检测到资源需要更新...";
            TimerSvc.instance.AddTask(0.5F * 1000, () => { StartCoroutine(GetUnityWebRequest(abDownloadQueue.Dequeue(), LoadSingleAB)); });
            return;
        }
        else
        {
            resourceLoadiProgress.LoadProgressText.text = "资源无需更新";
            Debug.Log("资源无需更新");
            //   UISvc.Single.AddTips("资源无需更新");
            TimerSvc.instance.AddTask(0.5F * 1000, () => { GameRoot.Single.abLoadDone?.Invoke(); });
        }
    }
    public AssetBundle UnityWebRequestByAssetBundle(UnityWebRequest request)
    {
        byte[] data = request.downloadHandler.data;
        AssetBundle assetBundle = AssetBundle.LoadFromMemory(data);
        return assetBundle;
    }
    /// <summary>
    /// 文件写入
    /// </summary>
    public void WriteFile(string resName, byte[] data)
    {
        FileInfo fileInfo = new FileInfo(ResPath.SaveFilePath + resName);
        FileStream fs = fileInfo.Create();
        fs.Write(data, 0, data.Length);
        fs.Flush();     //文件写入存储到硬盘
        fs.Close();     //关闭文件流对象
        fs.Dispose();   //销毁文件对象
    }
    /// <summary>
    /// 文件写入
    /// </summary>
    public void WriteFile(string resName, string data)
    {
        File.WriteAllText(ResPath.SaveFilePath + resName, data);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sourceFileName"></param>
    /// <param name="destFileName">复制到的文件夹</param>
    /// <param name="overwrite">覆盖</param>
    public void CopyDirIntoDestDirectory(string sourceFileName, string destFileName, bool overwrite)
    {
        if (!Directory.Exists(destFileName))
        {
            Directory.CreateDirectory(destFileName);
        }
        //resourceLoadiProgress.Debug("流文件路径" + sourceFileName+"\n");
        try
        {
            foreach (var file in Directory.GetFiles(sourceFileName))
            {
                resourceLoadiProgress.Debug("FileName:" + file + "------");
                File.Copy(file, Path.Combine(destFileName, Path.GetFileName(file)), overwrite);
            }
        }
        catch (System.Exception a)
        {
            resourceLoadiProgress.Debug(a.ToString());
        }


        //foreach (var d in Directory.GetDirectories(sourceFileName))
        //{
        //    CopyDirIntoDestDirectory(d, Path.Combine(destFileName, Path.GetFileName(d)), overwrite);
        //}
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

        AssetBundle prefabsAssetBundle = AssetBundle.LoadFromFile(ResPath.GetLoadABPath() + "prefabs");
        AssetBundle dataAssetBundle = AssetBundle.LoadFromFile(ResPath.GetLoadABPath() + "Data");
        AssetBundle soundEffectsAssetBundle = AssetBundle.LoadFromFile(ResPath.GetLoadABPath() + "soundeffects");
        AssetBundle spritesAssetBundle = AssetBundle.LoadFromFile(ResPath.GetLoadABPath() + "sprites");

        cacheAssetBundle.Add("prefabs", prefabsAssetBundle);
        cacheAssetBundle.Add("data", dataAssetBundle);
        cacheAssetBundle.Add("soundeffects", soundEffectsAssetBundle);
        cacheAssetBundle.Add("sprites", spritesAssetBundle);
        TimerSvc.instance.AddTask(1 * 1000, () => { GameRoot.Single.abLoadDone(); });
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
        //UISvc.Single.AddTips("LoadOrCreate");
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
                Debug.LogError("AB加载失败:AB包为:" + abData[0] + "资源名为:" + abData[1]);
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

        if (!cacheAssetBundle.ContainsKey(data[0]))
        {//soundeffects
            //data[0] = data[0].ToLower();
            Debug.Log("未加载该AB:" + data[0]);
            AssetBundle ab = AssetBundle.LoadFromFile(ResPath.SaveFilePath + data[0]);
            cacheAssetBundle.Add(data[0], ab);
        }
        return data;
    }
    #endregion
    #region Data
    Tables tables;
    EffectsSkillDataConfig effectsSkillDataConfig;
    public ArchiveData CurrentArchiveData;
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
                Debug.Log("技能效果配置" + File.Exists(ResPath.SaveFilePath + "EffectsSkillDataConfig.Json"));
                StreamReader reader = new StreamReader(ResPath.SaveFilePath + "EffectsSkillDataConfig.Json");
                string jsonData = reader.ReadToEnd();
                reader.Close();
                reader.Dispose();
                //string aDataText = File.ReadAllText(ResPath.SaveFilePath + "EffectsSkillDataConfig.json");
                effectsSkillDataConfig = JsonUtility.FromJson<EffectsSkillDataConfig>(jsonData);
            }
            return effectsSkillDataConfig;
        }
    }
    private JSONNode LoadByteBuf(string fileName)
    {
        //"D:\GetHubProject\Dream\GenerateDatas\json\item_tbitem.json"
        //return JSON.Parse(File.ReadAllText(ResPath.SaveFilePath + "GenerateDatas/json/" + fileName + ".json", System.Text.Encoding.UTF8)); 
        // AssetBundle ab = AssetBundle.LoadFromFile(ResPath.SaveFilePath + "");
        string abName = "generatedatas";
        if (!cacheAssetBundle.ContainsKey(abName))
        {
            AssetBundle ab = AssetBundle.LoadFromFile(ResPath.SaveFilePath + abName);
            cacheAssetBundle.Add("generatedatas", ab);
        }
        TextAsset textAsset = cacheAssetBundle[abName].LoadAsset<TextAsset>(fileName);
        return JSON.Parse(textAsset.text);
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
    public void JumpScene(string sName, System.Action jumpDone = null)
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
    /// <summary>
    /// 保存存档
    /// </summary>
    public void Save()
    {
        SaveArchive.SavePlayDataArchive(CurrentArchiveData);
    }
    /// <summary>
    /// 根据ID获取存档
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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
    /// <summary>
    /// 设置已经存在的存档
    /// </summary>
    /// <param name="newData"></param>
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
    /// <summary>
    /// 存档配置
    /// </summary>
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
    /// <summary>
    /// 存档配置
    /// </summary>
    /// <returns></returns>
    ArchiveDataConfig GetArchiveConfig()
    {
        Debug.Log("获取存档配置路径为" + ResPath.SaveFilePath + "ArchiveDataConfig.json" + "——存档是否存在" + File.Exists(ResPath.SaveFilePath + "ArchiveDataConfig.json"));
        if (!File.Exists(ResPath.SaveFilePath + "ArchiveDataConfig.json"))
        {
            archiveDataConfig = new ArchiveDataConfig();
            string pathJson = JsonUtility.ToJson(archiveDataConfig, true);
            using (StreamWriter streamWriter = File.CreateText(ResPath.SaveFilePath + "ArchiveDataConfig.json"))
            {
                //表示生成C#代码
                streamWriter.Write(pathJson);
                streamWriter.Close();
            }
        }
        string aDataText = File.ReadAllText(ResPath.SaveFilePath + "ArchiveDataConfig.json");
        archiveDataConfig = JsonUtility.FromJson<ArchiveDataConfig>(aDataText);
        // UISvc.Single.AddTips("存档数量" + archiveDataConfig.dataList.Count);
        return archiveDataConfig;
    }
    /// <summary>
    /// 设置存档数据
    /// </summary>
    /// <param name="id"></param>
    public void SetArchiveData(int id)
    {
        CurrentArchiveData = GetArchiveByID(id);
        CurrentArchiveData.playerData.roleData = GetRoleDataByID(CurrentArchiveData.playerData.selectRoleId);
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
        BTTree bTTree = monster.GetComponent<BTTree>();
        if (bTTree != null)
        {
            bTTree.Init();
        }
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
[System.Serializable]
public class CheckMD5DataConfig
{
    public List<CheckData> checkDatas = new List<CheckData>();
}
[System.Serializable]
public class CheckData
{
    public string aBName;
    public string mD5;

    public CheckData(string abName, string md5)
    {
        this.aBName = abName;
        this.mD5 = md5;
    }
}
[System.Serializable]
public class StreamingAssetsPathResPathConfig
{
    public List<string> resPath = new List<string>();
}
