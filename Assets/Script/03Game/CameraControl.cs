using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoSingle<CameraControl>
{
    Transform trackingTarget;
    public Transform TrackingTarget
    {
        get
        {
            return trackingTarget;
        }
    }
    public float speed = 2;
    [SerializeField, Header("��������")]
    bool isStartShake;
    private Transform ThisTrasform = null;
    //�ܹ�����ʱ��
    public float ShakeTime = 2.0f;
    //���κη�����ƫ�Ƶľ���
    public float ShakeAmount = 4.0f;
    //����ƶ����𶯵���ٶ�
    public float ShakeSpeed = 3.0f;
    float elapsedTime;
    Vector3 origPosition;

    private void Awake()
    {
        ThisTrasform = GetComponent<Transform>();
    }
    public void SetTarget(Transform target)
    {
        this.trackingTarget = target;
    }

    private void LateUpdate()
    {
        if (trackingTarget != null)
        {
            Vector3 trPos = transform.position;
            trPos = Vector2.Lerp(trPos, trackingTarget.position, speed * Time.deltaTime);
            trPos.z = transform.position.z;
            transform.position = trPos;
        }
        if (isStartShake)
        {
            //�ظ��˲����Ի������ʱ��

            //�ڵ�λ�������ȡ��
            Vector3 RandomPoint = origPosition + Random.insideUnitSphere * ShakeAmount;
            //�������λ��
            ThisTrasform.localPosition = Vector3.Lerp(ThisTrasform.localPosition, RandomPoint, Time.deltaTime * ShakeSpeed);
            //����ʱ��
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= ShakeTime)
            {
                //�ָ����λ��
                ThisTrasform.localPosition = origPosition;
                isStartShake = false;
                elapsedTime = 0.0f;
            }
        }
    }

    public void StartShake()
    {
        //��ʼ��
        isStartShake = true;
        //���µ�ǰ���λ��
        origPosition = ThisTrasform.localPosition;
        //��������ʱ��
        elapsedTime = 0.0f;
        //StartCoroutine(Shake());
    }


}


