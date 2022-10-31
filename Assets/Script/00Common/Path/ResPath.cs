using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveData
{
    public static string GetSavePath()
    {
        return Application.streamingAssetsPath + "/";
    }
}

public static class SoundEffects
{
    public const string BGM = "SoundEffects/BGM/";
    public const string Button = "SoundEffects/Button/";
    /// <summary>
    /// 游戏音效与UI音效保存路径
    /// </summary>
    public const string GameBGM = BGM + "GameBGM/";

    public const string CommonOnClickBg = Button + "CommonOnClickBg";
    public const string selectHero = Button + "SelectHeroItemEffectSound";
}
public static class ResPath
{
    public const string Prefabs = "Prefabs/";
    public const string Audios = "Prefabs/";
    public const string UI = ResPath.Prefabs + "UI/";
    public const string UIPanel = UI + "Panel/";
    public const string UIItem = UI + "Item/";
    public const string Sprites = "Sprites/";
    public const string SpritesUI = Sprites + "UI/";

    public static string WriteABPath
    {
        get
        {
#if UNITY_EDITOR
            return Application.streamingAssetsPath + "/";
#endif
#if UNITY_ANDROID
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
    public static string GetABPath(bool isTestNet = false)
    {
#if UNITY_EDITOR
        if (isTestNet)
        {
            //return @"file:///D:\GetHubProject\DreamWJ\AssetBundles\StandaloneWindows\";
            return @"http://localhost/AssetBundles/StandaloneWindows/";
        }
        return "AssetBundles/StandaloneWindows/";
#endif
#if UNITY_ANDROID
        Debug.Log("这是安卓平台。。。");
#endif
#if UNITY_IPHONE
        Debug.Log("这是iPhone平台。。。"); // 不好听，用IOS代替
#endif
#if UNITY_IOS
        Debug.Log("这是IOS平台。。。");
#endif
#if UNITY_STANDALONE_WIN
        return @"http://localhost/AssetBundles/StandaloneWindows/";
#endif
#if UNITY_STANDALONE_OSX
    Debug.Log("这是OSX平台。。。");
#endif
    }
}
public class ScenePath
{
    public const string MainScene = "MainScene";
}
public static class PrefabPath
{
    public const string Unit = ResPath.Prefabs + "Unit/";
}
