using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WMState;

namespace WMBT
{
    public class MonsterDatabase : Database
    {
        #region Tacking Config
        //׷�ٷ�Χ ����׷�ٷ�Χ ���뵽Ѳ��
        public Vector2 tackingRangeSize;
        public Vector2 tackingRangeOffset;
        public LayerMask tackingMask;
        public Color tackingDrawColor;
        #endregion
        public Transform[] patrolPoint;
        #region Attack Config
        [Header("������Χ")]
        public Vector2 attackRangeSize;
        public Vector2 attackRangeOffset;
        public LayerMask attackMask;
        public Color attackDrawColor;
        #endregion
        [HideInInspector]
        public Vector2 veTr;
        public MonsterStateEnum monsterStateEnum;
        [Header("Ŀ��")]
        public Transform tackingRangeTarget;
        [Header("����")]
        public int currentShieldValue;
        [Header("��ʼ����ֵ")]
        public int shieldValue;
        [Header("���ָܻ�ʱ��")]
        public int shieldRecover;
        [SerializeField]
        private float currentTime;
        public override void Init()
        {
            base.Init();
            currentShieldValue = shieldValue;
        }
        protected override void FixedUpdate()
        {
            veTr = transform.position;
            if (currentShieldValue <= 0)
            {
                currentTime += Time.deltaTime;
                if (currentTime >= shieldRecover)
                {
                    currentTime = 0;
                    currentShieldValue = shieldValue;
                }
            }
            base.FixedUpdate();
        }

        public void SetInjuredData(InjuredData injuredData)
        {
            currentShieldValue--;
            this.monsterStateEnum = MonsterStateEnum.Injured;
            this.injuredData = injuredData;
            updateInjuredCB?.Invoke();
        }
        public override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();
            Gizmos.color = tackingDrawColor;
            Vector2 tr = transform.position;
            Gizmos.DrawWireCube(tr + tackingRangeOffset, tackingRangeSize);

            Gizmos.color = attackDrawColor;
            Gizmos.DrawWireCube(tr + attackRangeOffset, attackRangeSize);
        }
    }


}
        