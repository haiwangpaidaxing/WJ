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

public sealed partial class EquipData :  Bright.Config.BeanBase 
{
    public EquipData(JSONNode _json) 
    {
        { if(!_json["untID"].IsNumber) { throw new SerializationException(); }  UntID = _json["untID"]; }
        { if(!_json["unitName"].IsString) { throw new SerializationException(); }  UnitName = _json["unitName"]; }
        { if(!_json["ResName"].IsString) { throw new SerializationException(); }  ResName = _json["ResName"]; }
        { if(!_json["Describe"].IsString) { throw new SerializationException(); }  Describe = _json["Describe"]; }
        { if(!_json["EquipType"].IsNumber) { throw new SerializationException(); }  EquipType = (Data.EquipType)_json["EquipType"].AsInt; }
        { if(!_json["EntryNumber"].IsNumber) { throw new SerializationException(); }  EntryNumber = _json["EntryNumber"]; }
        { if(!_json["PriceWeapons"].IsNumber) { throw new SerializationException(); }  PriceWeapons = _json["PriceWeapons"]; }
        { if(!_json["EquipQualityType"].IsNumber) { throw new SerializationException(); }  EquipQualityType = (Data.EquipQualityType)_json["EquipQualityType"].AsInt; }
        { if(!_json["EquipAttribute"].IsObject) { throw new SerializationException(); }  EquipAttribute = Data.EquipAttribute.DeserializeEquipAttribute(_json["EquipAttribute"]);  }
        { if(!_json["ISOpen"].IsBoolean) { throw new SerializationException(); }  ISOpen = _json["ISOpen"]; }
        PostInit();
    }

    public EquipData(int untID, string unitName, string ResName, string Describe, Data.EquipType EquipType, int EntryNumber, int PriceWeapons, Data.EquipQualityType EquipQualityType, Data.EquipAttribute EquipAttribute, bool ISOpen ) 
    {
        this.UntID = untID;
        this.UnitName = unitName;
        this.ResName = ResName;
        this.Describe = Describe;
        this.EquipType = EquipType;
        this.EntryNumber = EntryNumber;
        this.PriceWeapons = PriceWeapons;
        this.EquipQualityType = EquipQualityType;
        this.EquipAttribute = EquipAttribute;
        this.ISOpen = ISOpen;
        PostInit();
    }

    public static EquipData DeserializeEquipData(JSONNode _json)
    {
        return new Data.EquipData(_json);
    }

    /// <summary>
    /// ??????ID
    /// </summary>
    public int UntID { get; private set; }
    /// <summary>
    /// ????????????
    /// </summary>
    public string UnitName { get; private set; }
    /// <summary>
    /// ????????????
    /// </summary>
    public string ResName { get; private set; }
    /// <summary>
    /// ????????????
    /// </summary>
    public string Describe { get; private set; }
    /// <summary>
    /// ????????????
    /// </summary>
    public Data.EquipType EquipType { get; private set; }
    /// <summary>
    /// ????????????
    /// </summary>
    public int EntryNumber { get; private set; }
    /// <summary>
    /// ????????????
    /// </summary>
    public int PriceWeapons { get; private set; }
    /// <summary>
    /// ????????????
    /// </summary>
    public Data.EquipQualityType EquipQualityType { get; private set; }
    /// <summary>
    /// ???????????????
    /// </summary>
    public Data.EquipAttribute EquipAttribute { get; private set; }
    /// <summary>
    /// ??????????????????
    /// </summary>
    public bool ISOpen { get; private set; }

    public const int __ID__ = -496019786;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, object> _tables)
    {
        EquipAttribute?.Resolve(_tables);
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
        EquipAttribute?.TranslateText(translator);
    }

    public override string ToString()
    {
        return "{ "
        + "UntID:" + UntID + ","
        + "UnitName:" + UnitName + ","
        + "ResName:" + ResName + ","
        + "Describe:" + Describe + ","
        + "EquipType:" + EquipType + ","
        + "EntryNumber:" + EntryNumber + ","
        + "PriceWeapons:" + PriceWeapons + ","
        + "EquipQualityType:" + EquipQualityType + ","
        + "EquipAttribute:" + EquipAttribute + ","
        + "ISOpen:" + ISOpen + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}
