using cfg.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelManager : MonoBehaviour
{
    [SerializeField]
    Room[] rooms;

    Room currentRoom;
    RoleController roleController;

    private void OnDrawGizmos()
    {
        if (rooms != null && rooms.Length > 0)
        {
            foreach (var item in rooms)
            {
                Gizmos.DrawWireCube(item.roomConfig.RoomPos.position, item.roomConfig.RoomSize);
            }
        }
    }

    public void FixedUpdate()
    {
        if (rooms != null && rooms.Length > 0)
        {

            foreach (var item in rooms)
            {
                if (Physics2D.OverlapBox(item.roomConfig.RoomPos.position, item.roomConfig.RoomSize, 0, LayerMask.GetMask("Hero")))
                {
                    Debug.Log("玩家进入房间" + item.roomConfig.RoomPos.name);
                }
            }
        }
    }
}