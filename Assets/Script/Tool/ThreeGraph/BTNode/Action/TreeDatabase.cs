using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WMTree
{
    public class TreeDatabase : MonoBehaviour
    {
        [Header("输入方向")]
        public Vector2 InputDir;
        [HideInInspector]
        public Rigidbody2D rig;
        [HideInInspector]
        public Animator animator;
        [HideInInspector]
        public TreeUnitController unitController;
        #region 地面配置
        [Header("地面检测配置"), SerializeField]
        public Vector2 GroundSize;
        public Transform GroundCheckPos;
        [Header("地面图层")]
        public LayerMask GroundMask;
        [SerializeField]
        private bool isGround;
        public bool ISGround { get { return isGround; } }
        #endregion
        private void Awake()
        {
            GetComponent();
            unitController = GetComponent<TreeUnitController>();
            if (unitController == null)
            {
                unitController = gameObject.AddComponent<TreeUnitController>();
            }
            unitController.Init();
        }
        public void GetComponent()
        {
            animator = GetComponentInChildren<Animator>();
            rig = GetComponentInChildren<Rigidbody2D>();
            InputController.Single.inputCB = (dir) => { this.InputDir = dir; };
        }
        protected virtual void FixedUpdate()
        {
            //更新检测..
            CheckGround();
        }
        protected void CheckGround()
        {
            isGround = BoxCheck.Check(GroundCheckPos.transform.position, GroundSize, GroundMask);
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(GroundCheckPos.transform.position, GroundSize);
        }
       
    }

}
