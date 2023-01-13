using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WMAStar
{
    [System.Serializable]
    public class ASManager
    {
        public List<ASNode> openList=new List<ASNode>();
        public List<ASNode> closeList=new List<ASNode>();
        public ASNode[,] mapInfo;
        public ASManager()
        {

        }
        public ASManager(ASNode[,] mapInfo)
        {
            this.mapInfo = mapInfo;
        }
        public void StartingSearch(Vector2Int startPos, Vector2Int enePos)
        {
            closeList.Clear();
            ASNode startNode = new ASNode();
            startNode.pos = startPos;
            closeList.Add(startNode);
            while (closeList.Count > 0)
            {
                ASNode aSNode = closeList[closeList.Count - 1];
                FindNode(aSNode.pos);

                return;
            }
        }

        public void FindNode(Vector2Int vector2)
        {
            //防止越界 
            if (mapInfo[vector2.x + 1, vector2.y] != null)//右边
            {
                openList.Add(mapInfo[vector2.x + 1, vector2.y] );
            }
            if (mapInfo[vector2.x - 1, vector2.y] != null)//左边
            {
                  openList.Add(mapInfo[vector2.x - 1, vector2.y]);
            }
            if (mapInfo[vector2.x, vector2.y + 1] != null)//上边
            {
                openList.Add(mapInfo[vector2.x, vector2.y + 1]);
            }
            if (mapInfo[vector2.x, vector2.y - 1] != null)//下边
            {
                openList.Add(mapInfo[vector2.x , vector2.y - 1]);
            }

            if (mapInfo[vector2.x + 1, vector2.y + 1] != null)//右上
            {
                openList.Add(mapInfo[vector2.x + 1, vector2.y + 1]);
            }
            if (mapInfo[vector2.x - 1, vector2.y + 1] != null)//左上
            {
                openList.Add(mapInfo[vector2.x - 1, vector2.y + 1]);
            }
            if (mapInfo[vector2.x + 1, vector2.y - 1] != null)//右下
            {
                openList.Add(mapInfo[vector2.x + 1, vector2.y - 1]);
            }
            if (mapInfo[vector2.x - 1, vector2.y - 1] != null)//左下
            {
                openList.Add(mapInfo[vector2.x - 1, vector2.y - 1]);
            }
        }
    }

}
