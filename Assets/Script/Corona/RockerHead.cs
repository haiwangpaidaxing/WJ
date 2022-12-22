using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RockerHead : MonoBehaviour, IBaseSole
{
    //圆心的位置
    public Vector2 orginPos;
    public float radius = 100;
    public Vector2 dir;
    private void Start()
    {
        orginPos = transform.position;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        //圆心-拖拽位置=方向向量

        Touch[] touchs = Input.touches;
        int tCount = Input.touchCount;
        Vector2 deltaPos = eventData.position - orginPos;
        if (tCount > 0)
        {
            if (tCount == 1)
            {
                deltaPos = touchs[0].position - orginPos;
            }
            else
            {
                List<float> tm = new List<float>();
                for (int i = 0; i < tCount; i++)
                {
                    Vector2 tPos=touchs[i].position; ;
                    tPos = new Vector2(Mathf.Abs(tPos.x),Mathf.Abs(tPos.y));
                    float _magnitude = Vector2.Distance(orginPos, tPos);
                    tm.Add(_magnitude);
                }
                tm.Sort();
                deltaPos = touchs[0].position - orginPos;
            }

        }
        if (deltaPos.magnitude < radius)
        {
            transform.position = eventData.position;
        }
        else
        {
            transform.position = orginPos + deltaPos.normalized * radius;
        }

        if (orginPos.x - transform.position.x > 0)
        {
            dir.x = -1;
        }
        else
        {
            dir.x = 1;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerDrag.tag== "RouletteArea")
        {

        }
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
        dir.x = 0;
    }
}
