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
    [Header("��������")]
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
    [Header("ѭ������")]
    public int cycles;
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
    public string ID;
    [Header("���δ���������")]
    public string Count;
    [Header("��ʼλ��")]
    public Transform startPos;
    [Header("Ѳ�ߵ�")]
    public Transform[] patrolPoint;
}
