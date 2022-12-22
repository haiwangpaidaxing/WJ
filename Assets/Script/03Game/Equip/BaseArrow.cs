using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseArrow : MonoBehaviour, IInjured
{
    //目标
    //看向目标点
    //移动目标点
    public float speed;
    public Transform targetPos { get; set; }
    public float TargetPosDistance { get; set; }
    private void Update()
    {
        if (targetPos != null)
        {
            TargetPosDistance = Vector2.Distance(transform.position, targetPos.position);
        }
    }
    public void Move()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }
    public void LookTarget()
    {
        if (targetPos == null)
        {
            return;
        }
        Vector2 v = targetPos.position - transform.position;
        float r;
        r = Vector2.Angle(Vector2.up, v);   
        Vector3 vr = new Vector3();
        vr.z = r;
        if (targetPos.position.x > transform.position.x)
        {
            vr.y = 180;
        }
        else
        {
            vr.y = 0;
        }
        transform.eulerAngles = vr;
        transform.position = Vector2.Lerp(transform.position, targetPos.position, speed * Time.deltaTime);

    }

    public virtual void Injured(InjuredData injuredData)
    {
    }
}
