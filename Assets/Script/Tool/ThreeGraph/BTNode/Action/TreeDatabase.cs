using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WMTree
{
    public class TreeDatabase : MonoBehaviour
    {
        [Header("���뷽��")]
        public Vector2 InputDir;
        [HideInInspector]
        public Rigidbody2D rig;
        [HideInInspector]
        public Animator animator;
        [HideInInspector]
        public TreeUnitController unitController;
        #region ��������
        [Header("����������"), SerializeField]
        public Vector2 GroundSize;
        public Transform GroundCheckPos;
        [Header("����ͼ��")]
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
            //���¼��..
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
