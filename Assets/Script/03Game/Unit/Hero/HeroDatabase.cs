using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WUBT
{
    public class HeroDatabase : Database
    {
        #region ª¨«Ω≈‰÷√
        [Header("ª¨«ΩºÏ≤‚≈‰÷√"), SerializeField]
        public float wallSlierSize;
        [SerializeField]
        private Vector2 wallOffset;
        public Color wallRay;
        [Header("ºÏ≤‚∏ﬂ∂»")]
        public float detectionHighly;
        [HideInInspector]
        public Vector2 wallCheckDir = Vector2.right;
        [HideInInspector]
        public Vector2 wallSliderCheckPos;
        public LayerMask wallMask;
        #endregion
        public RoleState roleState;
        public override void Init()
        {
            base.Init();
            InputController.Single.inputCB += (dir) =>//∂©‘ƒ ‰»Î
            {
                InputDir = dir;
            };
        }

        public override void OnDrawGizmosSelected()
        {
            Gizmos.DrawRay(transform.position, Vector2.down * detectionHighly);
            Gizmos.color = wallRay;
            Gizmos.DrawRay(wallSliderCheckPos + wallOffset, Vector2.right * wallSlierSize);
            base.OnDrawGizmosSelected();
        }
        protected override void FixedUpdate()
        {
            if (InputDir.x != 0) wallCheckDir = transform.right * InputDir.x;
            wallSliderCheckPos = transform.position;
            wallSliderCheckPos += wallOffset;
            base.FixedUpdate();
        }
        public void Injured(InjuredData injuredData)
        {
            roleState = RoleState.Injured;
            this.injuredData = injuredData;
            updateInjuredCB?.Invoke();
        }
    }
}