using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleLight : MonoBehaviour
{
    [SerializeField]
    Transform trackingTarget;
    public float speed = 2;
    private void LateUpdate()
    {
        if (trackingTarget != null)
        {
            Vector3 trPos = transform.position;
            trPos = Vector2.Lerp(trPos, trackingTarget.position, speed * Time.deltaTime);
            trPos.z = transform.position.z;
            transform.position = trPos;
        }
    }


}
