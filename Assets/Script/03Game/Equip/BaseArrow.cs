using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseArrow : MonoBehaviour, IInjured
{
    //Ŀ��
    //����Ŀ���
    //�ƶ�Ŀ���
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
    /// ����Tֵ�����㱴���������������Ӧ�ĵ�
    /// </summary>
    /// <param name="t"></param>Tֵ
    /// <param name="p0"></param>��ʼ��
    /// <param name="p1"></param>���Ƶ�
    /// <param name="p2"></param>Ŀ���
    /// <returns></returns>����Tֵ��������ı��������ߵ�
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
