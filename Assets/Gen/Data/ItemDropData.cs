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

public sealed partial class ItemDropData :  Bright.Config.BeanBase 
{
    public ItemDropData(JSONNode _json) 
    {
        { if(!_json["Max"].IsNumber) { throw new SerializationException(); }  Max = _json["Max"]; }
        { if(!_json["Min"].IsNumber) { throw new SerializationException(); }  Min = _json["Min"]; }
        { if(!_json["ID"].IsNumber) { throw new SerializationException(); }  ID = _json["ID"]; }
        PostInit();
    }

    public ItemDropData(int Max, int Min, int ID ) 
    {
        this.Max = Max;
        this.Min = Min;
        this.ID = ID;
        PostInit();
    }

    public static ItemDropData DeserializeItemDropData(JSONNode _json)
    {
        return new Data.ItemDropData(_json);
    }

    public int Max { get; private set; }
    public int Min { get; private set; }
    public int ID { get; private set; }

    public const int __ID__ = 584430384;
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
        + "Max:" + Max + ","
        + "Min:" + Min + ","
        + "ID:" + ID + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}