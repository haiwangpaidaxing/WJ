using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFinder : MonoBehaviour
{
    public Transform boxPos;
    public Vector2 boxSize;
    public string[] enemyTag;
    Vector2 tr;
    RoleController roleController;
    public Action<IInjured> enemyCB;
    [SerializeField]
    List<IInjured> injuredList = new List<IInjured>();
    private void Awake()
    {
        roleController = GetComponent<RoleController>();
    }
    [SerializeField]
    public Color debugColor;
    public void OnDrawGizmos()
    {
        Gizmos.color = debugColor;
        Gizmos.DrawWireCube(boxPos.position, boxSize);

    }
    //TODO 寻找最近
    /// <summary>
    /// 查找单个
    /// </summary>
    public void FindSingle()
    {
        //if (Physics2D.OverlapBox(boxPos, boxSize, 0))
        //{

        //}
    }
    struct CloseRangelData
    {
        public float distance;
        public IInjured injured;
        public string name;
        public GameObject self;
        public CloseRangelData(float distance, IInjured injured, GameObject self, string name = "")
        {
            this.distance = distance;
            this.injured = injured;
            this.name = name;
            this.self = self;
        }
    }
    /// <summary>
    /// 查找最近的目标
    /// </summary>
    public IInjured FindCloseRange(ref GameObject enemy)
    {
        List<CloseRangelData> closeRangelDatas = new List<CloseRangelData>();
        Collider2D[] collider = Physics2D.OverlapBoxAll(boxPos.position, boxSize, 0);
        for (int i = 0; i < collider.Length; i++)
        {
            for (int tagIndex = 0; tagIndex < enemyTag.Length; tagIndex++)
            {
                if (collider[i].tag == enemyTag[tagIndex])
                {
                    IInjured injured = collider[i].GetComponent<IInjured>();
                    if (injured != null && !injuredList.Contains(injured))
                    {
                        float dis = Vector2.Distance(transform.position, collider[i].transform.position);
                        closeRangelDatas.Add(new CloseRangelData(dis, injured, collider[i].gameObject, collider[i].name));
                    }
                }
            }
        }

        for (int w = 0; w < closeRangelDatas.Count; w++)
        {
            for (int n = 0; n < closeRangelDatas.Count - 1; n++)
            {
                if (closeRangelDatas[w].distance > closeRangelDatas[n + 1].distance)
                {
                    CloseRangelData crd = closeRangelDatas[w];
                    closeRangelDatas[w] = closeRangelDatas[n + 1];
                    closeRangelDatas[n + 1] = crd;
                }
            }
        }
        if (closeRangelDatas.Count == 0)
        {
            return null;
        }
        enemy = closeRangelDatas[0].self;
        return closeRangelDatas[0].injured;
    }
    public IInjured FindCloseRange()
    {
        List<CloseRangelData> closeRangelDatas = new List<CloseRangelData>();
        Collider2D[] collider = Physics2D.OverlapBoxAll(boxPos.position, boxSize, 0);
        for (int i = 0; i < collider.Length; i++)
        {
            for (int tagIndex = 0; tagIndex < enemyTag.Length; tagIndex++)
            {
                if (collider[i].tag == enemyTag[tagIndex])
                {
                    IInjured injured = collider[i].GetComponent<IInjured>();
                    if (injured != null && !injuredList.Contains(injured))
                    {
                        float dis = Vector2.Distance(transform.position, collider[i].transform.position);
                        closeRangelDatas.Add(new CloseRangelData(dis, injured, collider[i].gameObject, collider[i].name));
                    }
                }
            }
        }

        for (int w = 0; w < closeRangelDatas.Count; w++)
        {
            for (int n = 0; n < closeRangelDatas.Count - 1; n++)
            {
                if (closeRangelDatas[w].distance > closeRangelDatas[n + 1].distance)
                {
                    CloseRangelData crd = closeRangelDatas[w];
                    closeRangelDatas[w] = closeRangelDatas[n + 1];
                    closeRangelDatas[n + 1] = crd;
                }
            }
        }
        if (closeRangelDatas.Count == 0)
        {
            return null;
        }
        return closeRangelDatas[0].injured;
    }
    ///在指定的时间内找到敌人 并且只找到一次
    /// <summary>
    /// 查找所有
    /// </summary>
    public void OpenFindTargetAll()
    {
        Collider2D[] collider = Physics2D.OverlapBoxAll(boxPos.position, boxSize, 0);
        for (int i = 0; i < collider.Length; i++)
        {
            for (int tagIndex = 0; tagIndex < enemyTag.Length; tagIndex++)
            {
                if (collider[i].tag == enemyTag[tagIndex])
                {
                    IInjured injured = collider[i].GetComponent<IInjured>();
                    if (injured != null && !injuredList.Contains(injured))
                    {
                        injuredList.Add(injured);
                        enemyCB?.Invoke(injured);
                    }
                }
            }
        }
    }
    public void OpenFindTargetAll(string[] targetTag)
    {
        Collider2D[] collider = Physics2D.OverlapBoxAll(boxPos.position, boxSize, 0);
        for (int i = 0; i < collider.Length; i++)
        {
            for (int tagIndex = 0; tagIndex < targetTag.Length; tagIndex++)
            {
                if (collider[i].tag == targetTag[tagIndex])
                {
                    IInjured injured = collider[i].GetComponent<IInjured>();
                    if (injured != null && !injuredList.Contains(injured))
                    {
                        injuredList.Add(injured);
                        enemyCB?.Invoke(injured);
                    }
                }
            }
        }
    }
    public void Close()
    {
        injuredList.Clear();
        enemyCB = null;
    }

}
