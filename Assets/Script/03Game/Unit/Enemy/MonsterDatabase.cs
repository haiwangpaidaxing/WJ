using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WUBT
{
    public class MonsterDatabase : Database
    {
        #region Tacking Config
        //×·×Ù·¶Î§ ³¬³ö×·×Ù·¶Î§ ½øÈëµ½Ñ²Âß
        public Vector2 tackingRangeSize;
        public Vector2 tackingRangeOffset;
        public LayerMask tackingMask;
        public Color tackingDrawColor;
        #endregion

        #region Attack Config
        public Vector2 attackRangeSize;
        public Vector2 attackRangeOffset;
        public LayerMask attackMask;
        public Color attackDrawColor;
        #endregion
        public Vector2 veTr;

        public Transform tackingRangeTarget;
        protected override void FixedUpdate()
        {
            veTr = transform.position;
            base.FixedUpdate();
        }
        public override void OnDrawGizmosSelected()
        {
            Gizmos.color = tackingDrawColor;
            Vector2 tr = transform.position;
            Gizmos.DrawWireCube(tr + tackingRangeOffset, tackingRangeSize);

            Gizmos.color = attackDrawColor;
            Gizmos.DrawWireCube(tr + attackRangeOffset, attackRangeSize);
            base.OnDrawGizmosSelected();
        }
    }
}
