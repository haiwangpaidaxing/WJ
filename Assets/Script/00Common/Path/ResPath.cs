using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveData
{
    public static string GetSavePath()
    {
        return Application.streamingAssetsPath+"/";
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
}
public class ScenePath
{
    public const string MainScene = "MainScene";
}
public static class PrefabPath
{
    public const string Unit = ResPath.Prefabs + "Unit/";
}
