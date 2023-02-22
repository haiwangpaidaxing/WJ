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
                if (y == createRoomData.groundDepth - 1)//地表层
                {
                    groundTilemap.SetTile(startPos, createRoomData.groundTile[0]);
                    createRoomData.groundList.Add(startPos);
                }
                else //泥土层
                {
                    groundTilemap.SetTile(startPos, createRoomData.soilTile[0]);
                }

            }

        }//基础地面
        for (int y = createRoomData.groundDepth; y < createRoomData.size.y; y++)
        {
            startPos.y += 1;
            startPos.x = -1;
            for (int x = 0; x < createRoomData.size.x; x++)
            {
                startPos.x += 1;
                if (y == createRoomData.size.y - 1)
                {
                    groundTilemap.SetTile(startPos, createRoomData.roofWallTile[0]);
                    createRoomData.roofList.Add(startPos);
                }
                else if (x == 0)
                {
                    groundTilemap.SetTile(startPos, createRoomData.leftWallTile[0]);
                    createRoomData.leftWallList.Add(startPos);
                }
                else if (x == createRoomData.size.x - 1)
                {
                    groundTilemap.SetTile(startPos, createRoomData.rightWallTile[0]);
                    createRoomData.rightWallList.Add(startPos);
                }

            }
        }//基础地面
        switch (createRoomData.roomExit)
        {
            case RoomExit.LeftRight:
                TileBase[] startWallTile = new TileBase[2] {
                    createRoomData.wallLeftSatrtTile[0] ,createRoomData.wallRightSatrtTile[0]};
                EmbellishWall(createRoomData.leftWallList, startWallTile, createRoomData.rightWallTile[0], 1);
                TileBase[] startWallTile1 = new TileBase[2] {
                createRoomData.wallRightSatrtTile[0]  ,  createRoomData.wallLeftSatrtTile[0] };
                EmbellishWall(createRoomData.rightWallList, startWallTile1, createRoomData.leftWallTile[0], -1);
                break;
            case RoomExit.LeftRightUp:
                break;
            case RoomExit.LeftRightDown:
                break;
        }
    }

    public void EmbellishWall(List<Vector3Int> wallList, TileBase[] wallStartPoint, TileBase wallTile, int dir)
    {
        int height = Random.Range(3, wallList.Count );
        for (int i = 0; i < height; i++)
        {
            groundTilemap.SetTile(wallList[i], null);
        }
        Vector3Int start1 = wallList[height];
        Vector3Int start2 = wallList[height];
        start2.x += dir;
        groundTilemap.SetTile(start1, wallStartPoint[0]);
        groundTilemap.SetTile(start2, wallStartPoint[1]);
        for (int i = height + 1; i < wallList.Count; i++)
        {
            Vector3Int pos = wallList[i];
            pos.x += dir;
            groundTilemap.SetTile(pos, wallTile);
        }
    }

}
[System.Serializable]
public class CreateRoomData
{
    public TileBase[] groundTile;
    public TileBase[] soilTile;
    public TileBase[] leftWallTile;
    public TileBase[] rightWallTile;
    public TileBase[] roofWallTile;
    public TileBase[] wallLeftSatrtTile, wallRightSatrtTile;
    [Header("房间起始点")]
    public Vector3Int startPos;
    [Header("房间地面深度")]
    public int groundDepth;
    [Header("房间高度")]
    public int roomHeight;
    [Header("房间大小")]
    public Vector2Int size;
    public RoomExit roomExit;

    public List<Vector3Int> groundList = new List<Vector3Int>();
    public List<Vector3Int> rightWallList = new List<Vector3Int>();
    public List<Vector3Int> leftWallList = new List<Vector3Int>();
    public List<Vector3Int> roofList = new List<Vector3Int>();
}
public enum RoomExit
{
    LeftRight, LeftRightUp, LeftRightDown
}