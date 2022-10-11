using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSvc :MonoSingle<MessageSvc>
{
    public override void Init()
    {
        Debug.Log("消息服务初始化...");
    }
}
