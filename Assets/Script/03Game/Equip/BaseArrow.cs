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
        //transform.position = Vector2.Lerp(transform.position, targetPos.position, speed * Time.deltaTime);

    }
    public void LookTarget(Vector2 targetPos)
    {
        Vector2 v = targetPos - ((Vector2)transform.position);
        float r;
        r = Vector2.Angle(Vector2.up, v);
        Vector3 vr = new Vector3();
        vr.z = r;
        if (targetPos.x > transform.position.x)
        {
            vr.y = 180;
        }
        else
        {
            vr.y = 0;
        }
        transform.eulerAngles = vr;
        transform.position = Vector2.Lerp(transform.position, targetPos, speed * Time.deltaTime);
    }


    /// <summary>
    /// 根据T值，计算贝塞尔曲线上面相对应的点
    /// </summary>
    /// <param name="t"></param>T值
    /// <param name="p0"></param>起始点
    /// <param name="p1"></param>控制点
    /// <param name="p2"></param>目标点
    /// <returns></returns>根据T值计算出来的贝赛尔曲线点
    protected static Vector3 CalculateCubicBezierPoint(float t, Vector2 p0, Vector2 p1, Vector2 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector2 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;
        return p;
    }
    public virtual void Injured(InjuredData injuredData)
    {
    }
}
