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
        Vector2 GroundOffset;
        public Vector2 GroundSize;
        public LayerMask GroundMask;
        #endregion
        #region 滑墙配置
        [Header("滑墙检测配置"), SerializeField]
        public float wallSlierSize;
        [SerializeField]
        private Vector2 wallOffset;
        [Header("检测高度")]
        public float detectionHighly;
        [HideInInspector]
        public Vector2 wallCheckDir = Vector2.right;
        [HideInInspector]
        public Vector2 wallSliderCheckPos;
        public LayerMask wallMask;
        #endregion
        [Header("输入方向")]
        public Vector2 InputDir;
        [HideInInspector]
        public RoleAttribute roleAttribute;
        [HideInInspector]
        public Vector2 GroundCheckPos;

        public void Init()
        {
            roleController = GetComponent<RoleController>();//获取角色控制器
            roleAttribute = GetComponent<RoleAttribute>();
            InputController.Single.inputCB += (dir) =>//订阅输入
            {
                InputDir = dir;
            };
        }
        public void OnDrawGizmosSelected()
        {
            Vector2 GroundCheckPos = transform.position;
            GroundCheckPos += GroundOffset;
            Gizmos.DrawRay(transform.position, Vector2.down * detectionHighly);
            Gizmos.DrawWireCube(GroundCheckPos, GroundSize);
            Gizmos.DrawRay(wallSliderCheckPos, Vector2.right * wallSlierSize);
        }
        private void FixedUpdate()
        {

            //更新检测..
            GroundCheckPos = transform.position;
            GroundCheckPos += GroundOffset;
            if (InputDir.x != 0) wallCheckDir = transform.right * InputDir.x;
            wallSliderCheckPos = transform.position;
            wallSliderCheckPos += wallOffset;
        }
    }
}

