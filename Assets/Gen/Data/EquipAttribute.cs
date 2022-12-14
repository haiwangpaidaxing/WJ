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

public sealed partial class EquipAttribute :  Bright.Config.BeanBase 
{
    public EquipAttribute(JSONNode _json) 
    {
        { if(!_json["Attribute_Harm"].IsNumber) { throw new SerializationException(); }  AttributeHarm = _json["Attribute_Harm"]; }
        { if(!_json["Attribute_HP"].IsNumber) { throw new SerializationException(); }  AttributeHP = _json["Attribute_HP"]; }
        { if(!_json["Attribute_CritHarm"].IsNumber) { throw new SerializationException(); }  AttributeCritHarm = _json["Attribute_CritHarm"]; }
        { if(!_json["Attribute_CriticalChance"].IsNumber) { throw new SerializationException(); }  AttributeCriticalChance = _json["Attribute_CriticalChance"]; }
        PostInit();
    }

    public EquipAttribute(float Attribute_Harm, float Attribute_HP, float Attribute_CritHarm, float Attribute_CriticalChance ) 
    {
        this.AttributeHarm = Attribute_Harm;
        this.AttributeHP = Attribute_HP;
        this.AttributeCritHarm = Attribute_CritHarm;
        this.AttributeCriticalChance = Attribute_CriticalChance;
        PostInit();
    }

    public static EquipAttribute DeserializeEquipAttribute(JSONNode _json)
    {
        return new Data.EquipAttribute(_json);
    }

    public float AttributeHarm { get; private set; }
    public float AttributeHP { get; private set; }
    public float AttributeCritHarm { get; private set; }
    public float AttributeCriticalChance { get; private set; }

    public const int __ID__ = 1579797264;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, object> _tables)
    {
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
    }

    public override string ToString()
    {
        return "{ "
        + "AttributeHarm:" + AttributeHarm + ","
        + "AttributeHP:" + AttributeHP + ","
        + "AttributeCritHarm:" + AttributeCritHarm + ","
        + "AttributeCriticalChance:" + AttributeCriticalChance + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}
