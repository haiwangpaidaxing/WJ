using cfg.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelManager : MonoBehaviour
{
    [SerializeField]
    Room[] room;
    [SerializeField]
    int currentIndex;
    Room currentRoom;

    RoleController roleController;
  

    public void Start()
    {
        currentIndex = 0;
        currentRoom = room[0];
        currentRoom.quitCB = NextRoom;
    }
    void NextRoom()
    {
        currentIndex++;
        if (currentIndex == room.Length)
        {
            //GO
        }
        currentRoom = room[currentIndex];
        CameraControl.Single.SetTarget(roleController.transform);
    }
    private void Update()
    {
        if (CameraControl.Single.TrackingTarget != currentRoom.fixedCamerPos)
        {
            if (Vector2.Distance(roleController.transform.position, currentRoom.fixedCamerPos.position) < 1f)
            {
                CameraControl.Single.SetTarget(currentRoom.fixedCamerPos);
            }
        }
    }
}