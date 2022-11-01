using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

public static class CreateABMD5
{
    [MenuItem("CreateABMD5/Create")]
    public static void Create()
    {

        CheckMD5DataConfig checkMD5DataConfig = new CheckMD5DataConfig();
        byte[] data = File.ReadAllBytes(ResPath.GetLoadABPath() + ResPath.GetABDepend());
        AssetBundle assetBundle = AssetBundle.LoadFromMemory(data);
        AssetBundleManifest assetBundleManifest = assetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        string[] allABName = assetBundleManifest.GetAllAssetBundles();
        CheckData cdata = new CheckData(ResPath.GetABDepend(), GetFileHash((ResPath.GetLoadABPath() + ResPath.GetABDepend())));
        checkMD5DataConfig.checkDatas.Add(cdata);
        Debug.Log(GetFileHash(ResPath.GetLoadABPath() + ResPath.GetABDepend()));
        foreach (var item in allABName)
        {
            CheckData checkData = new CheckData(item, GetFileHash((ResPath.GetLoadABPath() + item)));
            checkMD5DataConfig.checkDatas.Add(checkData);
            Debug.Log(GetFileHash(ResPath.GetLoadABPath() + item));
        }
        string jsonData = JsonUtility.ToJson(checkMD5DataConfig, true);
        File.WriteAllText(ResPath.GetLoadABPath() + "Config.json", jsonData);
        AssetBundle.UnloadAllAssetBundles(true);
        Debug.Log("WritePath" + ResPath.GetLoadABPath() + "Config.json");

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
