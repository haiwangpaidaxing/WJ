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
        //CreateRoom(roomData);
        CreateGround(roomData.startPos, roomData.size.y, roomData.size.x, ref roomData.groundList, ref roomData.soilList);
        GenertaTile(roomData.soilList, roomData.soilTile);
        GenertaTile(roomData.groundList, roomData.groundTile);
        Vector3Int leftPos = roomData.groundList[0];
        Vector3Int rigPos = roomData.groundList[roomData.groundList.Count - 1];
        for (int i = 0; i < roomData.size.y; i++)
        {
            groundTilemap.SetTile(leftPos, roomData.groundTile[0]);
            groundTilemap.SetTile(rigPos, roomData.groundTile[0]);
            leftPos.y--;
            rigPos.y--;
        }//修饰墙角
        GenerateArea(roomData.groundList);
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
    public void CreateGround(Vector3Int startPos, int groundY, int groundX, ref List<Vector3Int> ground, ref List<Vector3Int> soil)
    {
        for (int y = 0; y < groundY; y++)
        {
            startPos.y += 1;
            startPos.x = -1;
            for (int x = 0; x < groundX; x++)
            {
                startPos.x += 1;
                if (y == groundY - 1)//地表层
                {

                    ground.Add(startPos);
                }
                else //泥土层
                {
                    soil.Add(startPos);
                }

            }
        }//基础地面
    }
    /// <summary>
    /// 修饰墙壁
    /// </summary>
    /// <param name="wallList"></param>
    /// <param name="wallStartPoint"></param>
    /// <param name="wallTile"></param>
    /// <param name="dir"></param>
    public void EmbellishWall(List<Vector3Int> wallList, TileBase[] wallStartPoint, TileBase wallTile, int dir)
    {
        int height = Random.Range(3, wallList.Count);
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
    public void GenerateArea(List<Vector3Int> ground)
    {
        for (int i = 0; i < 1; i++)
        {
            int sizeY = Random.Range(-10, 10);
            int sizeX = Random.Range(8, 14);
            int dir = 1;
            if (ground.Count < 6)
            {
                return;
            }
            int startPoint = Random.Range(0, ground.Count);//区域开始点
            if (sizeY < 0)
            {
                dir = -1;
            }
            List<Vector3Int> areaList = Square(ref ground, ground[startPoint], new Vector2Int(sizeX, Mathf.Abs(sizeY)), dir);
            Debug.Log("区域深度：" + sizeY + "长度" + sizeX);
        }
    }
    public void SquareEmbellishUp(List<Vector3Int> square, int sizeX, int dir)
    {
        int a = 0;
        for (int i = 0; i < square.Count; i++)
        {
            if (i == a)
            {
                a += sizeX - 1;
                groundTilemap.SetTile(square[i], roomData.groundTile[0]);
                int x = square[i].x;
            }
        }
    }
    public void SquareEmbellishDown(List<Vector3Int> square)
    {

    }
    public List<Vector3Int> Square(ref List<Vector3Int> ground, Vector3Int startPoint, Vector2Int size, int dir = 1)
    {
        List<Vector3Int> square = new List<Vector3Int>();
        List<Vector3Int> groundboundary = new List<Vector3Int>();
        List<Vector3Int> wallboundary = new List<Vector3Int>();
        //for (int i = 0; i < size.x; i++)
        //{
        //    int a = 0;
        //    startPoint.x++;
        //    if (ground.Contains(startPoint))
        //    {
               
        //    }
        //    if (a==size.x)
        //    {
        //        continue;
        //    }
        //}
        for (int y = 0; y < size.y; y++)
        {
            for (int x = 0; x < size.x; x++)
            {
                if (ground.Contains(startPoint))
                {
                    ground.Remove(startPoint);
                }
                if (y == size.y - 1)
                {
                    groundboundary.Add(startPoint);
                }
                if (x == 0)
                {
                    wallboundary.Add(startPoint);
                }
                if (x == size.x - 1)
                {
                    wallboundary.Add(startPoint);
                }
                square.Add(startPoint);
                startPoint.x++;
            }
            startPoint.y += dir;
            startPoint.x -= size.x;
        }

        if (dir < 0)
        {
            GenertaTile(square, new TileBase[1] { null });
        }
        else
        {
            GenertaTile(square, roomData.soilTile);
        }
        GenertaTile(wallboundary, roomData.groundTile);
        GenertaTile(groundboundary, roomData.groundTile);
        return square;
    }

    public void GenertaTile(List<Vector3Int> vector3Ints, TileBase[] tileBases)
    {
        foreach (var item in vector3Ints)
        {
            groundTilemap.SetTile(item, tileBases[Random.Range(0, tileBases.Length)]);
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
    [Header("地面大小")]
    public Vector2Int size;
    public RoomExit roomExit;
    public Area area;
    public List<Vector3Int> groundList = new List<Vector3Int>();
    public List<Vector3Int> soilList = new List<Vector3Int>();
    public List<Vector3Int> roofList = new List<Vector3Int>();
    public List<Vector3Int> rightWallList = new List<Vector3Int>();
    public List<Vector3Int> leftWallList = new List<Vector3Int>();
}
public enum RoomExit
{
    LeftRight, LeftRightUp, LeftRightDown
}
public enum Area
{
    //路径
    Path,
    //怪物
    Monster,
    //陷阱
    Trap
}
[System.Serializable]
public class AreaData
{
    public Area area;
    [Header("当前房间区域数量")]
    public int count;//区域数量
}

