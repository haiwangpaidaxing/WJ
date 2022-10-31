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
        BinaryFormatter bf = new BinaryFormatter();
        using (FileStream fsWrite = new FileStream(ResPath.GetLoadABPath() + "Config.txt", FileMode.OpenOrCreate, FileAccess.Write))
        {

            bf.Serialize(fsWrite, checkMD5DataConfig);
        }
     
        AssetBundle.UnloadAllAssetBundles(true);
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

