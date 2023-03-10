using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WMTree
{
    public class TreeUnitController : MonoBehaviour
    {
        [Header("下落倍率")]
        public float fallMultiplier = 2.5f;
        [Header("同步图片方向")]
        public bool syncImge = true;
        [Header("当前角色方向")]
        public float roleDir = 1;
        [HideInInspector]
        public SpriteRenderer spriteRenderer;
        [HideInInspector]
        protected Rigidbody2D rig;
        public Vector2 RigVelocity { get { return rig.velocity; } }

        public virtual void Init()
        {
            rig = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        public virtual void Fall()
        {
            rig.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        public virtual void MoveX(float dir, float speed)
        {
            SyncImage(dir);
            if (dir != 0)
            {
                roleDir = dir;
            }
            rig.velocity = new Vector2(dir * speed, rig.velocity.y);
        }
        public virtual void MoveX(float dir, float speed, ForceMode2D forceMode2D)
        {
            Vector2 f = Vector2.right * dir * speed;
            rig.AddForce(f, forceMode2D);
        }
        public virtual void MoveY(float dir, float speed)
        {
            rig.velocity = new Vector2(rig.velocity.x, dir * speed);
        }
        public virtual void Move(Vector2 dir, float speed)
        {
            rig.velocity = dir * speed;
        }
        /// <summary>
        /// 同步方向
        /// </summary>
        /// <param name="dir"></param>
        public virtual void SyncImage(float dir)
        {
            if (syncImge)
            {
                Vector3 trLocalScale;
                trLocalScale = transform.localScale;
                if (dir == 1) trLocalScale.x = Mathf.Abs(trLocalScale.x);
                if (dir == -1 && trLocalScale.x > 0) trLocalScale.x = -trLocalScale.x;
                transform.localScale = trLocalScale;
            }
        }
    }
}
