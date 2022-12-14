//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using System.Collections.Generic;
using SimpleJSON;



namespace cfg.Data
{

public sealed partial class LevelConfigData :  Bright.Config.BeanBase 
{
    public LevelConfigData(JSONNode _json) 
    {
        { if(!_json["untID"].IsNumber) { throw new SerializationException(); }  UntID = _json["untID"]; }
        { if(!_json["LevelName"].IsString) { throw new SerializationException(); }  LevelName = _json["LevelName"]; }
        { if(!_json["LevelJumpSceneName"].IsString) { throw new SerializationException(); }  LevelJumpSceneName = _json["LevelJumpSceneName"]; }
        { if(!_json["LevelDatas"].IsObject) { throw new SerializationException(); }  LevelDatas = Data.LevelData.DeserializeLevelData(_json["LevelDatas"]);  }
        PostInit();
    }

    public LevelConfigData(int untID, string LevelName, string LevelJumpSceneName, Data.LevelData LevelDatas ) 
    {
        this.UntID = untID;
        this.LevelName = LevelName;
        this.LevelJumpSceneName = LevelJumpSceneName;
        this.LevelDatas = LevelDatas;
        PostInit();
    }

    public static LevelConfigData DeserializeLevelConfigData(JSONNode _json)
    {
        return new Data.LevelConfigData(_json);
    }

    /// <summary>
    /// 关卡ID
    /// </summary>
    public int UntID { get; private set; }
    /// <summary>
    /// 关卡名字
    /// </summary>
    public string LevelName { get; private set; }
    /// <summary>
    /// 跳转关卡名字
    /// </summary>
    public string LevelJumpSceneName { get; private set; }
    /// <summary>
    /// 关卡数据
    /// </summary>
    public Data.LevelData LevelDatas { get; private set; }

    public const int __ID__ = -617709972;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, object> _tables)
    {
        LevelDatas?.Resolve(_tables);
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
        LevelDatas?.TranslateText(translator);
    }

    public override string ToString()
    {
        return "{ "
        + "UntID:" + UntID + ","
        + "LevelName:" + LevelName + ","
        + "LevelJumpSceneName:" + LevelJumpSceneName + ","
        + "LevelDatas:" + LevelDatas + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}
