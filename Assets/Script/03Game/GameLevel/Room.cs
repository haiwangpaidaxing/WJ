using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Room
{
    //Fixed Camera Position
    public Transform fixedCamerPos;
    public Action quitCB;
    [Header("房间配置")]
    public RoomConfig roomConfig;
    public void Enter()
    {
        //Open Null Wall
        for (int i = 0; i < roomConfig.broundary.Count; i++)
        {
            roomConfig.broundary[i].SetActive(true);
        }

    }
    public void Close()
    {
        //Close Null Wall
        for (int i = 0; i < roomConfig.broundary.Count; i++)
        {
            roomConfig.broundary[i].SetActive(false);
        }
    }
    public void Execute()
    {

    }
    public void Create()
    {
        for (int i = 0; i < roomConfig.createConfig.Length; i++)
        {
            //
        }
    }
}

[System.Serializable]
public struct RoomConfig
{
    [Header("循环次数")]
    public int cycles;
    [Header("房间大小")]
    public Vector2 RoomSize;
    [Header("触发房间的范围")]
    public Transform RoomPos;
    [Header("房间边界位置")]
    public List<GameObject> broundary;//边界位置
    [Header("创建怪物配置")]
    public CreateEnemyConfigData[] createConfig;
}

[System.Serializable]
public struct CreateEnemyConfigData
{
    [Header("创建ID")]
    public string ID;
    [Header("单次创建的数量")]
    public string Count;
    [Header("初始位置")]
    public Transform startPos;
}
