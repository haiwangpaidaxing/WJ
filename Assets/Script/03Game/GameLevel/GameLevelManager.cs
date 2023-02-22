using System;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelManager : MonoSingle<GameLevelManager>
{
    [SerializeField]
    List<Room> rooms;//所有房间数据
    [Header("角色初始位置设定")]
    public Transform roleStartPos;
    public LevelRoomPanel levelRoomPanel;
    public GameObject EnemyList;
    public RoleSkillPanel skillPanel;
    public GameOverPanel gameOverPanel;
    public HeroController heroController { get; set; }
    [SerializeField]
    PlayerLevelData playerLevelData;
    public Action OnUpdatekillCount;
    [SerializeField]
    float gameOverCountDown = 3;
    bool isCountDown;
    private void Awake()
    {
        playerLevelData = new PlayerLevelData();
        EnemyList = new GameObject("EnemyList");
        levelRoomPanel = UISvc.Single.GetPanel<LevelRoomPanel>(UIPath.GameLevelRoomPanel, UISvc.StateType.Show);
        gameOverPanel = UISvc.Single.GetPanel<GameOverPanel>(UIPath.GameOverPanel, UISvc.StateType.Close);
        GameObject role = ResourceSvc.Single.CreateRole(ResourceSvc.Single.CurrentArchiveData.playerData.roleData);
        role.transform.position = roleStartPos.position;
        CameraControl.Single.SetTarget(role.transform);
        heroController = role.GetComponent<HeroController>();
        heroController.DieCB += RoleDie;
        Init();
        gameOverPanel.ReturnBtn.onClick.AddListener(EnterMainScene);
        OnUpdatekillCount = UpdateKillCount;
        gameOverPanel.ReturnBtn.onClick.AddListener(Settlement);
    }

    private void Settlement()
    {
        ResourceSvc.Single.CurrentArchiveData.playerData.gold += playerLevelData.goldCount;
        ResourceSvc.Single.Save();
    }

    private void UpdateKillCount()
    {
        playerLevelData.killCount++;
        playerLevelData.goldCount = playerLevelData.killCount * 10;
    }

    public void EnterMainScene()
    {
        ResourceSvc.Single.JumpScene(ScenePath.MainScene, () => { MainSceneSys.Single.Init(); });
    }
    public void RoleDie()
    {
        InputController.Single.Close();
        levelRoomPanel.Show("游戏失败");
        TimerSvc.instance.AddTask(0.5F * 1000, () =>
          {
              if (currentRoom != null)
              {
                  currentRoom.End();
              }
              gameOverPanel.KillMonsterCountText.text = playerLevelData.killCount.ToString();
              gameOverPanel.GoldCountText.text = playerLevelData.goldCount.ToString();
              GameRoot.Single.PauseFrame(new Animator[] { heroController.animator });
              UISvc.Single.SetPanelState(gameOverPanel, UISvc.StateType.Show);
          });

    }
    public void GameWinner()
    {
        InputController.Single.Close();
        isCountDown = true;
    }

    private void Update()
    {
        if (isCountDown)
        {
            gameOverCountDown -= Time.fixedTime;
            levelRoomPanel.Show(gameOverCountDown.ToString("0"));
            if (gameOverCountDown <= 0)
            {
                isCountDown = false;
                levelRoomPanel.Show("游戏胜利");
                TimerSvc.instance.AddTask(0.5F * 1000, () =>
                {
                    if (currentRoom != null)
                    {
                        currentRoom.End();
                    }
                    gameOverPanel.KillMonsterCountText.text = playerLevelData.killCount.ToString();
                    gameOverPanel.GoldCountText.text = playerLevelData.goldCount.ToString();
                    UISvc.Single.SetPanelState(gameOverPanel, UISvc.StateType.Show);
                });
            }
        }
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
[System.Serializable]
public struct PlayerLevelData
{
    public float time;
    public int killCount;
    public int goldCount;
}