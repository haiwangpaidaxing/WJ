using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WMAStar
{
    public class ASTestGround : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        public Text text;
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        [SerializeField]
        ASTestCreateMap aSTestCreateMap;
        private void OnMouseDown()
        {
            Vector2Int vector2Int = new Vector2Int((int)transform.position.x, (int)transform.position.y);
            aSTestCreateMap.asManager.mapInfo[vector2Int.x, vector2Int.y] = null;
            Destroy(gameObject);
        }
    }

}

