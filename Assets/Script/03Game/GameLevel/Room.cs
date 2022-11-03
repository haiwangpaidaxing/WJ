using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WUBT;

[System.Serializable]
public class Room
{
    public int currentMonsterCount;
    //Fixed Camera Position
    public Transform fixedCamerPos;
    public Action quitCB;
    [Header("房间配置")]
    public RoomConfig roomConfig;

    public void Enter()
    {
        //Open Null Wall
        GameLevelManager.Single.levelRoomPanel.Show(roomConfig.RoomInfo);
        for (int i = 0; i < roomConfig.broundary.Count; i++)
        {
            roomConfig.broundary[i].SetActive(true);
        }
        Debug.Log("Enter" + roomConfig.RoomInfo);
    }
    public void Close()
    {
        //Close Null Wall
        for (int i = 0; i < roomConfig.broundary.Count; i++)
        {
            roomConfig.broundary[i].SetActive(false);
        }

        GameLevelManager.Single.currentRoom = null;
    }
    public void Execute()
    {
        Create();
    }
    public void Create()
    {
        if (currentMonsterCount == 0)//代表制造出来的怪物全部死亡 开始准备下一波怪物
        {
            if (roomConfig.loop == 0)//循环制造怪物等于0时 代表已经通过该房间
            {
                Close();
                return;
            }
            for (int i = 0; i < roomConfig.createConfig.Length; i++)
            {

                CreateEnemyConfigData createEnemyConfigData = roomConfig.createConfig[i];
                for (int c = 0; c < createEnemyConfigData.Count.Length; c++)
                {
                    GameObject monsterObject = ResourceSvc.Single.CreateMonster(roomConfig.createConfig[i].ID);//创建
                    monsterObject.transform.SetParent(GameLevelManager.Single.EnemyList.transform, false);
                    monsterObject.transform.position = roomConfig.createConfig[i].startPos.position;//出生点
                    monsterObject.GetComponent<GobinRoleController>().dieCB = MonsterDieCB;
                    monsterObject.GetComponent<MonsterDatabase>().patrolPoint = roomConfig.createConfig[i].patrolPoint;
                    currentMonsterCount++;
                }

            }
            roomConfig.loop--;
        }

    }

    private void MonsterDieCB()
    {
        currentMonsterCount--;
    }
}

[System.Serializable]
public struct RoomConfig
{
    [Header("循环次数")]
    public int loop;
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
    public int ID;
    [Header("单次创建的数量")]
    public string Count;
    [Header("初始位置")]
    public Transform startPos;
    [Header("巡逻点")]
    public Transform[] patrolPoint;
}
