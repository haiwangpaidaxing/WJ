using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetSvc : MonoSingle<NetSvc>
{
    public override void Init()
    {
        Debug.Log("网络服务初始化...");
    }
}
