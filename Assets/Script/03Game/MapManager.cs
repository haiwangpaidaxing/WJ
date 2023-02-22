using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    public Transform[] mapRange;
    public CreateRoomData roomData;
    public Tilemap groundTilemap;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(mapRange[0].position, mapRange[1].position);
        Gizmos.DrawLine(mapRange[0].position, mapRange[2].position);
        Gizmos.DrawLine(mapRange[1].position, mapRange[3].position);
        Gizmos.DrawLine(mapRange[2].position, mapRange[3].position);
    }
    private void Awake()
    {
        CreateRoom(roomData);
    }
    public void CreateRoom(CreateRoomData createRoomData)
    {
        Vector3Int startPos = createRoomData.startPos;
        startPos.x = -1;
        for (int y = 0; y < createRoomData.groundDepth; y++)
        {
            startPos.y += 1;
            startPos.x = -1;
            for (int x = 0; x < createRoomData.size.x; x++)
            {
                startPos.x += 1;
                if (y == createRoomData.groundDepth - 1)//�ر��
                {
                    groundTilemap.SetTile(startPos, createRoomData.groundTile[0]);
                }
                else //������
                {
                    groundTilemap.SetTile(startPos, createRoomData.soilTile[0]);
                }
               
            }
            
        }//��������
        for (int y = createRoomData.groundDepth; y < createRoomData.size.y; y++)
        {
            startPos.y += 1;
            startPos.x = -1;
            for (int x = 0; x < createRoomData.size.x; x++)
            {
                startPos.x += 1;
                if (x==0||x== createRoomData.size.x-1)
                {
                    groundTilemap.SetTile(startPos, createRoomData.groundTile[0]);
                }
                else if (y == createRoomData.size.y-1)//����
                {

                }
                Debug.Log(startPos);

            }
        }//��������
    }
}
[System.Serializable]
public class CreateRoomData
{
    public TileBase[] groundTile;
    public TileBase[] soilTile;
    public TileBase[] leftWall;
    public TileBase[] rightWall;
    [Header("������ʼ��")]
    public Vector3Int startPos;
    [Header("����������")]
    public int groundDepth;
    [Header("����߶�")]
    public int roomHeight;
    [Header("�����С")]
    public Vector2Int size;
    public RoomExit roomExit;
}
public enum RoomExit
{
    LeftRight, LeftRightUp, LeftRightDown
}