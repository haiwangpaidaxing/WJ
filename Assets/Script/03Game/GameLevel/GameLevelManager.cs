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
        if (rooms!=null&&rooms.Length>0)
        {
            foreach (var item in rooms)
            {
               // Gizmos.DrawWireCube();
            }
        }
    }
}