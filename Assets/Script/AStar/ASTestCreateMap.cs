using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WMAStar
{
    public class ASTestCreateMap : MonoBehaviour
    {
        public GameObject groundMap;
        public Vector2 mapSize;
        public ASNode[,] groundDatas;
        public ASManager asManager;
        private void Awake()
        {

            groundDatas = new ASNode[(int)mapSize.x, (int)mapSize.y];
            for (int x = 0; x < mapSize.x; x++)
            {
                for (int y = 0; y < mapSize.y; y++)
                {
                    GameObject ground = GameObject.Instantiate(groundMap, transform);
                    ASNode data = new ASNode { gameObject = ground, pos = new Vector2Int(x, y) };
                    groundDatas[x, y] = data;
                    ground.transform.position = new Vector3(x, y);
                    ground.SetActive(true);
                    ground.GetComponentInChildren<Text>().text = data.pos.ToString("0");
                }
            }
            asManager = new ASManager(groundDatas);
            asManager.StartingSearch(new Vector2Int(1, 1), Vector2Int.zero);
        }
    }

}
