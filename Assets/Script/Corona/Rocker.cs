using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Rocker : MonoSingle<Rocker>, IBaseSole
{
    //圆心的位置
    public Transform head;

    public Vector2 orginPos;
    public float radius = 100;
    [SerializeField]
    public Vector2 dir;

    public Action<Vector2> dirEvent;
    private void Start()
    {
        orginPos = head.position;
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
        //if (tCount > 0)
        //{
        //    if (tCount == 1)
        //    {
        //        deltaPos = touchs[0].position - orginPos;
        //    }
        //    else
        //    {
        //        List<float> tm = new List<float>();
        //        for (int i = 0; i < tCount; i++)
        //        {
        //            Vector2 tPos = touchs[i].position; ;
        //            tPos = new Vector2(Mathf.Abs(tPos.x), Mathf.Abs(tPos.y));
        //            float _magnitude = Vector2.Distance(orginPos, tPos);
        //            tm.Add(_magnitude);
        //        }
        //        tm.Sort();
        //        deltaPos = touchs[0].position - orginPos;
        //    }
        //}
        if (deltaPos.magnitude < radius)
        {
            head.position = eventData.position;
        }
        else
        {
            head.position = orginPos + deltaPos.normalized * radius;
        }
        if (orginPos.x - head.position.x > 0)
        {
            dir.x = -1;
        }
        else
        {
            dir.x = 1;
        }
        if (orginPos.y - head.position.y < -45f)
        {
            dir.y = 1;
        }
        else
        {
            dir.y = 0;
        }
        Debug.Log(orginPos.y - head.position.y);
        dirEvent?.Invoke(dir);
    }

    public void OnDrop(PointerEventData eventData)
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        orginPos = head.position;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        head.localPosition = Vector3.zero;
        dir = Vector2.zero;
        dirEvent?.Invoke(Vector2.zero);
    }
}
interface IBaseSole : IDragHandler, IDropHandler, IPointerUpHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler
{
    /// <summary>
    /// 拖拽凹槽时
    /// </summary>
    void OnDrag(PointerEventData eventData);

    /// <summary>
    /// 凹槽上有物体并且向上抬起时
    /// </summary>
    /// <param name="eventData"></param>
    void OnDrop(PointerEventData eventData);

    /// <summary>
    /// 点击凹槽抬起时
    /// </summary>
    void OnPointerUp(PointerEventData eventData);

    /// <summary>
    /// 点击凹槽时
    /// </summary>
    void OnPointerDown(PointerEventData eventData);

    /// <summary>
    /// 鼠标进入凹槽时
    /// </summary>
    /// <param name="eventData"></param>
    void OnPointerEnter(PointerEventData eventData);

    /// <summary>
    /// 鼠标退出凹槽时
    /// </summary>
    /// <param name="eventData"></param>
    void OnPointerExit(PointerEventData eventData);
    /// <summary>
    /// 当鼠标在A对象按下并开始拖拽时
    /// </summary>
    /// <param name="eventData"></param>
    void OnBeginDrag(PointerEventData eventData);


}