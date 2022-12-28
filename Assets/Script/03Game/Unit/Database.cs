using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WUBT
{
    public class Database : MonoBehaviour
    {
        public RoleController roleController;
        #region 地面配置
        [Header("地面检测配置"), SerializeField]
        protected Vector2 GroundOffset;
        public Vector2 GroundSize;
        [Header("地面图层")]
        public LayerMask GroundMask;
        #endregion
        [Header("输入方向")]
        public Vector2 InputDir;
        [HideInInspector]
        public RoleAttribute roleAttribute;
        [HideInInspector]
        public Vector2 GroundCheckPos;
        public InjuredData injuredData;
        public Action updateInjuredCB;
        public virtual void Init()
        {
            roleController = GetComponent<RoleController>();//获取角色控制器
            roleAttribute = GetComponent<RoleAttribute>();
        }
        public virtual  void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Vector2 GroundCheckPos = transform.position;
            GroundCheckPos += GroundOffset;
            Gizmos.DrawWireCube(GroundCheckPos, GroundSize);
        }
        protected virtual void FixedUpdate()
        {
            //更新检测..
            CheckGround();
        }
        protected void CheckGround()
        {
            GroundCheckPos = transform.position;//检测地面
            GroundCheckPos += GroundOffset;

    
        }
    }
}

