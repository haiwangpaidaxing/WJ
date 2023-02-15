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
    [SerializeField, Header("开启抖动")]
    bool isStartShake;
    private Transform ThisTrasform = null;
    //总共抖动时间
    public float ShakeTime = 2.0f;
    //在任何方向上偏移的距离
    public float ShakeAmount = 4.0f;
    //相机移动到震动点的速度
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
            //重复此步骤以获得总震动时间

            //在单位球上随机取点
            Vector3 RandomPoint = origPosition + Random.insideUnitSphere * ShakeAmount;
            //更新相机位置
            ThisTrasform.localPosition = Vector3.Lerp(ThisTrasform.localPosition, RandomPoint, Time.deltaTime * ShakeSpeed);
            //更新时间
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= ShakeTime)
            {
                //恢复相机位置
                ThisTrasform.localPosition = origPosition;
                isStartShake = false;
                elapsedTime = 0.0f;
            }
        }
    }

    public void StartShake()
    {
        //开始震动
        isStartShake = true;
        //存下当前相机位置
        origPosition = ThisTrasform.localPosition;
        //记下运行时间
        elapsedTime = 0.0f;
        //StartCoroutine(Shake());
    }


}


