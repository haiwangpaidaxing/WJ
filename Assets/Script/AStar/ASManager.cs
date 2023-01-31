using System;
using System.Collections.Generic;
using UnityEngine;

namespace WMAStar
{
    [System.Serializable]
    public class ASManager
    {
        public List<ASNode> openList = new List<ASNode>();
        public List<ASNode> closeList = new List<ASNode>();
        public ASNode[,] mapInfo;
        Vector2 mapSize;
        public ASManager()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapInfo">地图信息</param>
        /// <param name="mapSize">地图边界</param>
        public ASManager(ASNode[,] mapInfo, Vector2 mapSize)
        {
            this.mapSize = mapSize;
            this.mapInfo = mapInfo;
        }
        public void StartingSearch(Vector2Int startPos, Vector2Int endPos)
        {
            if (!CheckBoundaryX(startPos.x) || !CheckBoundaryY(startPos.y) || !CheckBoundaryX(endPos.x) || !CheckBoundaryY(endPos.y))
            {
                return;
            }
            openList.Clear();
            closeList.Clear();
            ASNode startNode = new ASNode();
            startNode.pos = startPos;
            closeList.Add(startNode);
            int count = 0;
            while (true)
            {
                ASNode aSNode = closeList[closeList.Count - 1];
                if (aSNode.pos == endPos)//最新格子是否是终点
                {
                    return;
                }
                FindNode(startPos, endPos, aSNode.pos);
                if (openList.Count == 0)
                {
                    Debug.Log("死路");
                    return;
                }
                closeList.Add(openList[0]);
                openList.Clear();
                count++;
                if (count >= 11111)
                {
                    return;
                }
            }
        }

        public void FindNode(Vector2 startPos, Vector2 endPos, Vector2Int vector2)
        {
            //防止越界 
            int right = vector2.x + 1;
            if (CheckBoundaryX(right) && mapInfo[right, vector2.y] != null)//右边
            {
                ASNode node = mapInfo[right, vector2.y];
                if (!closeList.Contains(node))
                {
                    ComputeValue(node, startPos, endPos);
                    OpenListAdd(node);
                }

            }

            int left = vector2.x - 1;
            if (CheckBoundaryX(left) && mapInfo[left, vector2.y] != null)//左边
            {
                ASNode node = mapInfo[left, vector2.y];
                if (!closeList.Contains(node))
                {
                    ComputeValue(node, startPos, endPos);
                    OpenListAdd(node);
                }
            }

            int up = vector2.y + 1;
            if (CheckBoundaryY(up) && mapInfo[vector2.x, up] != null)//上边
            {
                ASNode node = mapInfo[vector2.x, up];
                if (!closeList.Contains(node))
                {
                    ComputeValue(node, startPos, endPos);
                    OpenListAdd(mapInfo[vector2.x, up]);
                }

            }

            int down = vector2.y - 1;
            if (CheckBoundaryY(down) && mapInfo[vector2.x, down] != null)//下边
            {
                ASNode node = mapInfo[vector2.x, down];
                if (!closeList.Contains(node))
                {
                    ComputeValue(node, startPos, endPos);
                    OpenListAdd(mapInfo[vector2.x, down]);
                }

            }

            Vector2Int right_Up = new Vector2Int(vector2.x + 1, vector2.y + 1);
            if (CheckBoundaryX(right_Up.x) && CheckBoundaryY(right_Up.y) && mapInfo[right_Up.x, right_Up.y] != null)//右上
            {
                ASNode node = mapInfo[right_Up.x, right_Up.y];
                if (!closeList.Contains(node))
                {
                    ComputeValue(node, startPos, endPos, 1.4f);
                    OpenListAdd(mapInfo[right_Up.x, right_Up.y]);
                }

            }

            Vector2Int left_Up = new Vector2Int(vector2.x - 1, vector2.y + 1);
            if (CheckBoundaryX(left_Up.x) && CheckBoundaryY(left_Up.y) && mapInfo[left_Up.x, left_Up.y] != null)//左上
            {
                ASNode node = mapInfo[left_Up.x, left_Up.y];
                if (!closeList.Contains(node))
                {
                    ComputeValue(node, startPos, endPos, 1.4f);
                    OpenListAdd(mapInfo[left_Up.x, left_Up.y]);
                }

            }

            Vector2Int right_Down = new Vector2Int(vector2.x + 1, vector2.y - 1);
            if (CheckBoundaryX(right_Down.x) && CheckBoundaryY(right_Down.y) && mapInfo[right_Down.x, right_Down.y] != null)//左上
            {
                ASNode node = mapInfo[right_Down.x, right_Down.y];
                if (!closeList.Contains(node))
                {
                    ComputeValue(node, startPos, endPos, 1.4f);
                    OpenListAdd(mapInfo[right_Down.x, right_Down.y]);
                }

            }

            Vector2Int left_Down = new Vector2Int(vector2.x - 1, vector2.y - 1);
            if (CheckBoundaryX(left_Down.x) && CheckBoundaryY(left_Down.y) && mapInfo[left_Down.x, left_Down.y] != null)//左下
            {
                ASNode node = mapInfo[left_Down.x, left_Down.y];
                if (!closeList.Contains(node))
                {
                    ComputeValue(node, startPos, endPos, 1.4f);
                    OpenListAdd(mapInfo[left_Down.x, left_Down.y]);
                }
            }

        }

        public void OpenListAdd(ASNode node)
        {
            if (openList.Count >= 1)
            {
                ASNode minNode = openList[0];
                if (minNode.F < node.F)
                {
                    openList.Add(node);
                }
                else if (minNode.F > node.F)
                {
                    openList.Insert(0, node);
                }
                else if (openList[0].F == node.F)
                {
                    if (minNode.H < node.H || minNode.H == node.H)
                    {
                        openList.Add(node);
                    }
                    else if (minNode.H > node.H)
                    {
                        openList.Insert(0, node);
                    }

                }
            }
            else
            {
                openList.Add(node);
            }
        }
        public bool CheckBoundaryX(int x)
        {
            if (x >= 0 && x < mapSize.x)
            {
                return true;
            }
            return false;
        }
        public bool CheckBoundaryY(int y)
        {
            if (y >= 0 && y < mapSize.y)
            {
                return true;
            }
            return false;
        }
        public void ComputeValue(ASNode node, Vector2 startPos, Vector2 endPos, float value = 1)
        {
            float g = Mathf.Abs(startPos.x - node.pos.x) + Mathf.Abs(startPos.y - node.pos.y);
            float h = Mathf.Abs(endPos.x - node.pos.x) + Mathf.Abs(endPos.y - node.pos.y);
            node.SetG(g);
            node.SetH(h);
        }
        public void OnRest()
        {
            openList.Clear();
            closeList.Clear();
        }
    }

}
