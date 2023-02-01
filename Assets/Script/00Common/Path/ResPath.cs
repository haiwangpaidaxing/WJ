using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public static class SaveData
//{
//    public static string GetSavePath()
//    {
//        return Application.streamingAssetsPath + "/";
//    }
//}

public static class SoundEffects
{
    public const string BGM = "soundeffects/BGM/";
    public const string Button = "soundeffects/Button/";
    /// <summary>
    /// 游戏音效与UI音效保存路径
    /// </summary>
    public const string GameBGM = BGM + "GameBGM/";

    public const string CommonOnClickBg = Button + "CommonOnClickBg";
    public const string selectHero = Button + "SelectHeroItemEffectSound";
}
public static class ResPath
{
    public const string Prefabs = "prefabs/";
    public const string Audios = "prefabs/";
    public const string UI = ResPath.Prefabs + "UI/";
    public const string UIPanel = UI + "Panel/";
    public const string UIItem = UI + "Item/";
    public const string Sprites = "sprites/";
    public const string SpritesUI = Sprites + "UI/";
    public static bool ISEDITOR
    {
        get
        {
#if UNITY_EDITOR
            return true;
#endif
            return false;
        }
    }
    /// <summary>
    /// 保存文件路径
    /// </summary>
    public static string SaveFilePath
    {
        get
        {
#if UNITY_EDITOR
            return Application.streamingAssetsPath + "/";
#endif
#if UNITY_ANDROID
            return Application.persistentDataPath + "/" + "GameRes" + "/";
            Debug.Log("这是安卓平台。。。");
#endif
#if UNITY_IPHONE
        Debug.Log("这是iPhone平台。。。"); // 不好听，用IOS代替
#endif
#if UNITY_IOS
        Debug.Log("这是IOS平台。。。");
#endif
#if UNITY_STANDALONE_WIN
            return Application.streamingAssetsPath + "/";
#endif
#if UNITY_STANDALONE_OSX
    Debug.Log("这是OSX平台。。。");
#endif  
        }
    }
    public static string GetABDepend()
    {
#if UNITY_EDITOR
        return "StandaloneWindows";
#endif
#if UNITY_ANDROID
        Debug.Log("这是安卓平台。。。");
        return "StandaloneWindows";
#endif
#if UNITY_IPHONE
        Debug.Log("这是iPhone平台。。。"); // 不好听，用IOS代替
#endif
#if UNITY_IOS
        Debug.Log("这是IOS平台。。。");
#endif
#if UNITY_STANDALONE_WIN
        return "StandaloneWindows";
#endif
#if UNITY_STANDALONE_OSX
    Debug.Log("这是OSX平台。。。");
#endif

    }
    /// <summary>
    /// 获取加载AB包的路径
    /// </summary>
    /// <param name="isTestNet"></param>
    /// <returns></returns>
    public static string GetLoadABPath()
    {
#if UNITY_EDITOR
        //return @"file:///D:\GetHubProject\DreamWJ\AssetBundles\StandaloneWindows\";
        return @"http://192.168.0.117:8080/StandaloneWindows/";
#endif
#if UNITY_ANDROID
        Debug.Log("这是安卓平台。。。");
        return @"http://192.168.0.117:8080/StandaloneWindows/";
#endif
#if UNITY_IPHONE
        Debug.Log("这是iPhone平台。。。"); // 不好听，用IOS代替
#endif
#if UNITY_IOS
        Debug.Log("这是IOS平台。。。");
#endif
#if UNITY_STANDALONE_WIN
       return @"http://192.168.0.117:8080/StandaloneWindows/";
#endif
#if UNITY_STANDALONE_OSX
    Debug.Log("这是OSX平台。。。");
#endif
    }
}
public class ScenePath
{
    public const string Logic = "Main"; 
    public const string MainScene = "MainScene";
}
public static class PrefabPath
{
    public const string Unit = ResPath.Prefabs + "Unit/";
}
