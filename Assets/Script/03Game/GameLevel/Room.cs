using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WUBT;

[System.Serializable]
public class Room
{
    //Fixed Camera Position
    public Transform fixedCamerPos;
    public Action quitCB;
    [Header("房间配置")]
    public RoomConfig roomConfig;
    public int index;
    public void Enter()
    {
        //Open Null Wall
        for (int i = 0; i < roomConfig.broundary.Count; i++)
        {
            roomConfig.broundary[i].SetActive(true);
        }
        Debug.Log("Enter" + roomConfig.RoomInfo);
        GameObject gameObject = ResourceSvc.Single.CreateMonster(1001);
        gameObject.transform.position = roomConfig.createConfig[0].startPos.position;
        gameObject.GetComponent<MonsterDatabase>().patrolPoint = roomConfig.createConfig[0].patrolPoint;
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
        //for (int i = 0; i < roomConfig.createConfig.Length; i++)
        //{

        //}
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
    [Header("房间信息")]
    public string RoomInfo;

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
    [Header("巡逻点")]
    public Transform[] patrolPoint;
}
