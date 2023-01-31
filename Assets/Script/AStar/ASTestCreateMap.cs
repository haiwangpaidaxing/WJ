using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace WMAStar
{
    public class ASTestCreateMap : MonoBehaviour
    {
        public GameObject groundMap;
        public Vector2 mapSize;
        public ASNode[,] groundDatas;
        public ASTestGround[,] grounds;
        public ASManager asManager;

        public Button resetButton;
        public Button startButton;

        public Vector2Int startPos;
        public Vector2Int endPos;
        private void Awake()
        {
            TimerSvc.Single.Init();
            resetButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("AStar");
            });
            startButton.onClick.AddListener(() =>
            {
                asManager.StartingSearch(startPos, endPos);
                grounds[endPos.x, endPos.y].spriteRenderer.color = Color.blue;
            });
            grounds = new ASTestGround[(int)mapSize.x, (int)mapSize.y];
            groundDatas = new ASNode[(int)mapSize.x, (int)mapSize.y];
            for (int x = 0; x < mapSize.x; x++)
            {
                for (int y = 0; y < mapSize.y; y++)
                {
                    GameObject ground = GameObject.Instantiate(groundMap, transform);
                    ASNode data = new ASNode { gameObject = ground, pos = new Vector2Int(x, y) };
                    grounds[x, y] = ground.GetComponent<ASTestGround>();
                    groundDatas[x, y] = data;
                    ground.transform.position = new Vector3(x, y);
                    ground.SetActive(true);
                    ground.GetComponentInChildren<Text>().text = data.pos.ToString("0");
                }
            }
            int r0 = Random.Range(0, (int)mapSize.x);
            int r1 = Random.Range(0, (int)mapSize.y);
            groundDatas[r0, r1] = null;
            GameObject gameObject = grounds[r0, r1].gameObject;
            grounds[r0, r1] = null;
            Destroy(gameObject);
            asManager = new ASManager(groundDatas, mapSize);
        }

        private void Update()
        {
            TimerSvc.Single.UpdateTask();
            if (Input.GetKeyDown(KeyCode.R))
            {
                asManager.OnRest();
                foreach (var item in groundDatas)
                {
                    ASNode aSNode = asManager.mapInfo[item.pos.x, item.pos.y];
                    grounds[item.pos.x, item.pos.y].spriteRenderer.color = Color.white;
                    grounds[item.pos.x, item.pos.y].text.text = "" + aSNode.pos;
                }
                foreach (var item in grounds)
                {
                    item.spriteRenderer.color = Color.white;
                }
            }

            foreach (var item in asManager.closeList)
            {
                grounds[item.pos.x, item.pos.y].spriteRenderer.color = Color.green;
            }

        }
    }

}
