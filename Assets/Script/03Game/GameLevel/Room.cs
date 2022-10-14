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
    [Header("��������")]
    public RoomConfig roomConfig;
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
    [Header("ѭ������")]
    public int cycles;
    [Header("�����С")]
    public Vector2 RoomSize;
    [Header("����λ��")]
    public Transform RoomPos;
    [Header("����߽�λ��")]
    public Transform[] broundaryPos;//�߽�λ��
    [Header("������������")]
    public CreateEnemyConfigData[] createConfig;
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
}
