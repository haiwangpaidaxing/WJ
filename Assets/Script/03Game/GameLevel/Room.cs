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
    [Header("��������")]
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
        if (currentMonsterCount == 0)//������������Ĺ���ȫ������ ��ʼ׼����һ������
        {
            if (roomConfig.loop == 0)//ѭ������������0ʱ �����Ѿ�ͨ���÷���
            {
                Close();
                return;
            }
            for (int i = 0; i < roomConfig.createConfig.Length; i++)
            {

                CreateEnemyConfigData createEnemyConfigData = roomConfig.createConfig[i];
                for (int c = 0; c < createEnemyConfigData.Count.Length; c++)
                {
                    GameObject monsterObject = ResourceSvc.Single.CreateMonster(roomConfig.createConfig[i].ID);//����
                    monsterObject.transform.SetParent(GameLevelManager.Single.EnemyList.transform, false);
                    monsterObject.transform.position = roomConfig.createConfig[i].startPos.position;//������
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
    [Header("ѭ������")]
    public int loop;
    [Header("�����С")]
    public Vector2 RoomSize;
    [Header("��������ķ�Χ")]
    public Transform RoomPos;
    [Header("����߽�λ��")]
    public List<GameObject> broundary;//�߽�λ��
    [Header("������������")]
    public CreateEnemyConfigData[] createConfig;
    [Header("������Ϣ")]
    public string RoomInfo;

}

[System.Serializable]
public struct CreateEnemyConfigData
{
    [Header("����ID")]
    public int ID;
    [Header("���δ���������")]
    public string Count;
    [Header("��ʼλ��")]
    public Transform startPos;
    [Header("Ѳ�ߵ�")]
    public Transform[] patrolPoint;
}
