using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

public static class CreateABMD5
{
    [MenuItem("Create/CreateStreamingAssetsPathResPath")]
    public static void StreamingAssetsPathResPath()
    {
        StreamingAssetsPathResPathConfig streamingAssetsPathResPathConfig = new StreamingAssetsPathResPathConfig();
        UnityEngine.Object[] arr = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.DeepAssets);
  
        for (int i = 0; i < arr.Length; i++)
        {
            string path = AssetDatabase.GetAssetPath(arr[i]);
            path = path.Replace("Assets/StreamingAssets/", "");
            path = path.Replace("Assets/StreamingAssets", "");
            if (path!="")
            {
                streamingAssetsPathResPathConfig.resPath.Add(path);
                Debug.Log(path);
            }
        }
        string jsonData = JsonUtility.ToJson(streamingAssetsPathResPathConfig, true);
        File.WriteAllText(Application.streamingAssetsPath + "/StreamingAssetsPathResPathConfig.json", jsonData);
    }
    [MenuItem("Create/CreateABMD5")]
    public static void Create()
    {
        CheckMD5DataConfig checkMD5DataConfig = new CheckMD5DataConfig();
        string abPath = "AssetBundles/StandaloneWindows/";
        byte[] data = File.ReadAllBytes(abPath + ResPath.GetABDepend());
        AssetBundle assetBundle = AssetBundle.LoadFromMemory(data);
        AssetBundleManifest assetBundleManifest = assetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        string[] allABName = assetBundleManifest.GetAllAssetBundles();
        CheckData cdata = new CheckData(ResPath.GetABDepend(), GetFileHash((abPath + ResPath.GetABDepend())));
        checkMD5DataConfig.checkDatas.Add(cdata);
        Debug.Log(GetFileHash(abPath + ResPath.GetABDepend()));
        foreach (var item in allABName)
        {
            CheckData checkData = new CheckData(item, GetFileHash((abPath + item)));
            checkMD5DataConfig.checkDatas.Add(checkData);
            Debug.Log(GetFileHash(abPath + item));
        }
        string jsonData = JsonUtility.ToJson(checkMD5DataConfig, true);
        File.WriteAllText(abPath + "Config.json", jsonData);
        AssetBundle.UnloadAllAssetBundles(true);
        Debug.Log("WritePath" + abPath + "Config.json");

        //LuBanDataConfig luBanDataConfig = new LuBanDataConfig();
        //string lubanDataConfigPath = Application.dataPath + "/../GenerateDatas/json/";
        //DirectoryInfo directoryInfo = new DirectoryInfo(lubanDataConfigPath);
        //FileInfo[] fileInfos = directoryInfo.GetFiles();
        //foreach (var item in fileInfos)
        //{
        //    luBanDataConfig.datas.Add(item.Name);
        //}
        //string lubanData = JsonUtility.ToJson(luBanDataConfig,true);
        //File.WriteAllText(ResPath.GetLoadABPath() + "LuBanDataConfig.json", lubanData);
        //Debug.Log("WritePath" + ResPath.GetLoadABPath() + "LuBanDataConfig.json");
    }
    [MenuItem("CreateABMD5/Clear")]
    public static void Clear()
    {
        AssetBundle.UnloadAllAssetBundles(true);
    }
    public static string GetFileHash(string filePath)
    {
        try
        {
            FileStream fs = new FileStream(filePath, FileMode.Open);
            int len = (int)fs.Length;
            byte[] data = new byte[len];
            fs.Read(data, 0, len);
            fs.Close();
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(data);
            string fileMD5 = "";
            foreach (byte b in result)
            {
                fileMD5 += System.Convert.ToString(b, 16);
            }
            return fileMD5;
        }
        catch (FileNotFoundException e)
        {
            System.Console.WriteLine(e.Message);
            return "";
        }
    }
}
