using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingle<GameManager>
{
    public ArchiveData ArchiveData { get { return archiveData; } }
    public ArchiveData archiveData;
    private void Awake()
    {
        CameraControl.Single.SetTarget(GameRoot.Single.CreateRole(MainSceneSys.Single.playerData.roleData).transform);
    }
    public void Init(ArchiveData archiveData)
    {
        this.archiveData = archiveData;
    }
}
