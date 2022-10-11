using FixedPointy;
using System.Collections;
using System.Collections.Generic;
using TF.Core;
using UnityEngine;

/// <summary>
/// 创建人：
/// 创建时间：
/// 功能介绍：
/// 
/// 修改时间:   修改人：  修改内容：  备注：
/// 修改时间:   修改人：  修改内容：  备注：
/// 修改时间:   修改人：  修改内容：  备注：
/// 修改时间:   修改人：  修改内容：  备注：
/// 修改时间:   修改人：  修改内容：  备注：
/// </summary>
public class TestRigid : MonoBehaviour
{
    TFRigidbody fRigidbody;

    private void Awake()
    {
        fRigidbody = GetComponent<TFRigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            fRigidbody.AddForce(FixVec2.up*30*(Fix)Time.fixedDeltaTime,ForceMode2D.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            fRigidbody.AddForce(FixVec2.right* 5);
        }
    }
}
