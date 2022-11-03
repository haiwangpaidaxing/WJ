using cfg.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelManager : MonoSingle<GameLevelManager>
{
    [SerializeField]
    List<Room> rooms;
    public Transform roleStartPos;
    public LevelRoomPanel levelRoomPanel;
    public GameObject EnemyList;
    private void Awake()
    {
        EnemyList = new GameObject("EnemyList");
        levelRoomPanel = UISvc.Single.GetPanel<LevelRoomPanel>(UIPath.GameLevelRoomPanel, UISvc.StateType.Show);
        GameObject role = GameRoot.Single.CreateRole(MainSceneSys.Single.playerData.roleData);
        role.transform.position = roleStartPos.position;
        CameraControl.Single.SetTarget(role.transform);
        Init();
    }
    public void Init()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject roomObject = transform.GetChild(i).gameObject;
            for (int a = 0; a < roomObject.transform.childCount; a++)
            {
                GameObject wall = roomObject.transform.GetChild(a).gameObject;
                rooms[i].roomConfig.broundary.Add(wall);
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (rooms != null && rooms.Count > 0)
        {
            foreach (var item in rooms)
            {
                Gizmos.DrawWireCube(item.roomConfig.RoomPos.position, item.roomConfig.RoomSize);
            }
        }
    }
    public Room currentRoom;
    public void FixedUpdate()
    {
        if (rooms != null && rooms.Count > 0)
        {
            for (int i = rooms.Count - 1; i >= 0; i--)
            {
                if (Physics2D.OverlapBox(rooms[i].roomConfig.RoomPos.position, rooms[i].roomConfig.RoomSize, 0, LayerMask.GetMask("Hero")))
                {
                    rooms[i].Enter();
                    currentRoom = rooms[i];
                    rooms.RemoveAt(i);

                }
            }
        }
        if (currentRoom != null)
        {
            currentRoom.Execute();
        }
    }
}